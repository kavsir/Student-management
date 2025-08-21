using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Student_Management.Models;

namespace Student_Management.Controllers
{
    public class LoginController : Controller
    {
        private readonly QuanLyHocSinhContext _context;
        public LoginController(QuanLyHocSinhContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return View();
            }

            var user = _context.Taikhoans.FirstOrDefault(
                t => t.TenDangNhap == tenDangNhap && t.MatKhau == matKhau);

            if (user == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }

            // Lưu session chung
            HttpContext.Session.SetInt32("MaTaiKhoan", user.MaTk);
            HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
            HttpContext.Session.SetString("VaiTro", user.VaiTro ?? "");

            if (user.VaiTro == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.VaiTro == "GiaoVien")
            {
                // THIẾT LẬP GiaoVien CHO SESSION
                var kh = _context.Giaoviens.FirstOrDefault(k => k.MaGv == user.MaTk);
                if (kh != null)
                {
                    HttpContext.Session.SetInt32("MaGV", kh.MaGv);
                }

                return RedirectToAction("Index", "Giaovien");
            }
            else if (user.VaiTro == "HocSinh")
            {
                // THIẾT LẬP HocSinh CHO SESSION
                var kh = _context.Hocsinhs.FirstOrDefault(k => k.MaHs == user.MaTk);
                if (kh != null)
                {
                    HttpContext.Session.SetInt32("MaHS", kh.MaHs);
                }

                return RedirectToAction("Index", "Hocsinh");
            }

            ViewBag.Error = "Vai trò không hợp lệ.";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult CreateAccount()
        {
            var isAdmin = HttpContext.Session.GetString("VaiTro") == "Admin";
            ViewBag.IsAdmin = isAdmin;
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string tenDangNhap, string email)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ Tên đăng nhập và Email.";
                return View();
            }

            // Tìm tài khoản
            var user = _context.Taikhoans.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
            if (user == null)
            {
                ViewBag.Error = "Không tìm thấy tài khoản!";
                return View();
            }

            // Xác thực email khớp với Học sinh/Giáo viên
            bool emailHopLe = false;

            if (user.VaiTro == "HocSinh")
            {
                var hs = _context.Hocsinhs.FirstOrDefault(h => h.MaHs == user.MaTk && h.Email == email);
                if (hs != null) emailHopLe = true;
            }
            else if (user.VaiTro == "GiaoVien")
            {
                var gv = _context.Giaoviens.FirstOrDefault(g => g.MaGv == user.MaTk && g.Email == email);
                if (gv != null) emailHopLe = true;
            }

            if (!emailHopLe)
            {
                ViewBag.Error = "Email không khớp với tài khoản.";
                return View();
            }

            // Sinh mật khẩu mới (random)
            string newPassword = GenerateRandomPassword(8);
            user.MatKhau = newPassword;
            _context.Update(user);
            _context.SaveChanges();

            ViewBag.Success = $"Mật khẩu mới của bạn là: {newPassword}. Vui lòng đăng nhập và đổi mật khẩu ngay!";
            return View();
        }

        // Hàm sinh mật khẩu random mạnh hơn
        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        // 

        ////tạo tài khoản
        //[HttpPost]
        //[HttpPost]
        //public IActionResult CreateAccount(Taikhoan model)
        //{
        //    var isAdmin = HttpContext.Session.GetString("VaiTro") == "Admin";
        //    ViewBag.IsAdmin = isAdmin;

        //    // Gán vai trò mặc định là User nếu không phải admin
        //    if (!isAdmin)
        //        model.VaiTro = "User";

        //    var regex = new Regex("^[A-Za-z0-9_]+$");
        //    if (string.IsNullOrWhiteSpace(model.TenDangNhap) || string.IsNullOrWhiteSpace(model.MatKhau))
        //    {
        //        ViewBag.Error = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
        //        return View(model);
        //    }

        //    if (!regex.IsMatch(model.TenDangNhap) || !regex.IsMatch(model.MatKhau))
        //    {
        //        ViewBag.Error = "Tên đăng nhập và mật khẩu chỉ được dùng chữ cái không dấu, số và dấu gạch dưới.";
        //        return View(model);
        //    }

        //    // Kiểm tra tồn tại
        //    var existingUser = _context.Taikhoans.FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap);
        //    if (existingUser != null)
        //    {
        //        ViewBag.Error = "Tên đăng nhập đã tồn tại.";
        //        return View(model);
        //    }

        //    // Lưu tài khoản
        //    var newAccount = new Taikhoan
        //    {
        //        TenDangNhap = model.TenDangNhap,
        //        MatKhau = model.MatKhau,
        //        VaiTro = model.VaiTro
        //    };
        //    _context.Taikhoans.Add(newAccount);
        //    _context.SaveChanges();

        //    // Lấy thông tin nhập từ form
        //    var hoTen = Request.Form["HoTen"];
        //    var email = Request.Form["Email"];
        //    var diaChi = Request.Form["DiaChi"];
        //    var soDienThoai = Request.Form["SoDienThoai"];
        //    var ngayDangKy = DateOnly.FromDateTime(DateTime.Now);

        //    // Tạo bảng tương ứng theo VaiTro
        //    if (model.VaiTro == "HocSinh")
        //    {
        //        var kh = new Hocsinh
        //        {
        //            MaHs = newAccount.MaTk,
        //            HoTen = hoTen,
        //            Email = email,
        //            DiaChi = diaChi,
        //            Sdt = soDienThoai,
        //        };
        //        _context.Hocsinhs.Add(kh);
        //    }
        //    else if (model.VaiTro == "GiaoVien")
        //    {
        //        var gv = new Giaovien
        //        {
        //            MaGv = newAccount.MaTk,
        //            HoTen = hoTen,
        //            Email = email,
        //            DiaChi = diaChi,
        //            Sdt = soDienThoai,
        //        };
        //        _context.Giaoviens.Add(gv);
        //    }

        //    _context.SaveChanges();

        //    ViewBag.Success = "Tạo tài khoản thành công!";
        //    return View();
        //}
        //public IActionResult DangXuat()
        //{
        //    HttpContext.Session.Clear(); // Xóa toàn bộ session
        //    return RedirectToAction("Index", "Login"); // Quay về trang đăng nhập
        //}

        //public IActionResult ThongTinCaNhan()
        //{
        //    var maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
        //    if (maTaiKhoan == null)
        //    {
        //        return RedirectToAction("Index"); // Chưa đăng nhập
        //    }

        //    var taiKhoan = _context.Taikhoans.FirstOrDefault(t => t.MaTk == maTaiKhoan);
        //    if (taiKhoan == null)
        //    {
        //        ViewBag.Error = "Không tìm thấy tài khoản.";
        //        return View();
        //    }

        //    // Tìm thêm thông tin chi tiết
        //    if (taiKhoan.VaiTro == "HocSinh")
        //    {
        //        var khachHang = _context.Hocsinhs.FirstOrDefault(k => k.MaHs == taiKhoan.MaTk);
        //        ViewBag.KhachHang = khachHang;
        //    }
        //    else if (taiKhoan.VaiTro == "Giao Viên")
        //    {
        //        var quanLy = _context.Giaoviens.FirstOrDefault(q => q.MaGv == taiKhoan.MaTk);
        //        ViewBag.QuanLy = quanLy;
        //    }

        //    return View(taiKhoan);
        //}
        //// GET: Hiển thị form đổi thông tin
        //public IActionResult EditProfile()
        //{
        //    var vaiTro = HttpContext.Session.GetString("VaiTro");
        //    var maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");

        //    if (vaiTro == "HocSinh")
        //    {
        //        var hocsinh = _context.Hocsinhs.FirstOrDefault(k => k.MaHs == maTaiKhoan);
        //        return View("EditUserProfile", hocsinh);
        //    }
        //    else if (vaiTro == "Giao Vien")
        //    {
        //        var giaovien = _context.Giaoviens.FirstOrDefault(q => q.MaGv == maTaiKhoan);
        //        return View("EditAdminProfile", giaovien);
        //    }

        //    return RedirectToAction("Index", "Login");
        //}

        //// POST: Cập nhật thông tin
        //[HttpPost]
        //public IActionResult EditTeacherProfile(Giaovien giaovien)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(giaovien);
        //        _context.SaveChanges();
        //        TempData["Message"] = "Cập nhật thông tin thành công!";
        //        return RedirectToAction("ThongTinCaNhan");
        //    }
        //    return View(giaovien);
        //}

        //[HttpPost]
        //public IActionResult EditStudentProfile(Hocsinh hocsinh)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(hocsinh);
        //        _context.SaveChanges();
        //        TempData["Message"] = "Cập nhật thông tin thành công!";
        //        return RedirectToAction("ThongTinCaNhan");
        //    }
        //    return View(hocsinh);
        //}
    }
}
