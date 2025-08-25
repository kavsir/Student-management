using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System.Security.Cryptography;
using System.Text;

namespace Student_Management.Controllers;

public class AuthController(QuanLyHocSinhContext context) : Controller
{
    private readonly QuanLyHocSinhContext _context = context;

    // GET: /Auth/Login
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
            return View();
        }

        string hashedPassword = HashPassword(password);
        var account = await _context.Taikhoans
            .FirstOrDefaultAsync(t => t.TenDangNhap == username && t.MatKhau == hashedPassword);

        if (account == null)
        {
            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return View();
        }

        HttpContext.Session.SetString("Username", account.TenDangNhap);
        HttpContext.Session.SetString("Role", account.VaiTro);
        HttpContext.Session.SetInt32("UserId", account.MaTk);

        return RedirectToAction("Index", account.VaiTro switch
        {
            "Admin" => "Admin",
            "GiaoVien" => "GiaoVien",
            "HocSinh" => "HocSinh",
            _ => "Home"
        });
    }

    // GET: /Auth/Register
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string username, string password, string confirmPassword, string role)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
            return View();
        }

        if (password != confirmPassword)
        {
            ViewBag.Error = "Mật khẩu xác nhận không khớp.";
            return View();
        }

        bool exists = await _context.Taikhoans.AnyAsync(t => t.TenDangNhap == username);
        if (exists)
        {
            ViewBag.Error = "Tên đăng nhập đã tồn tại.";
            return View();
        }

        var newAccount = new Taikhoan
        {
            TenDangNhap = username,
            MatKhau = HashPassword(password),
            VaiTro = role
        };

        _context.Add(newAccount);
        await _context.SaveChangesAsync();

        ViewBag.Success = "Đăng ký thành công. Vui lòng đăng nhập.";
        return View();
    }

    // GET: /Auth/ForgotPassword
    public IActionResult ForgotPassword() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(string username, string newPassword, string confirmNewPassword)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmNewPassword))
        {
            ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
            return View();
        }

        if (newPassword != confirmNewPassword)
        {
            ViewBag.Error = "Mật khẩu xác nhận không khớp.";
            return View();
        }

        var account = await _context.Taikhoans.FirstOrDefaultAsync(t => t.TenDangNhap == username);
        if (account == null)
        {
            ViewBag.Error = "Không tìm thấy tài khoản.";
            return View();
        }

        account.MatKhau = HashPassword(newPassword);
        _context.Update(account);
        await _context.SaveChangesAsync();

        ViewBag.Success = "Đặt lại mật khẩu thành công. Vui lòng đăng nhập.";
        return View();
    }

    // GET: /Auth/ChangePassword
    public IActionResult ChangePassword() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
    {
        string? username = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login");
        }

        var account = await _context.Taikhoans.FirstOrDefaultAsync(t => t.TenDangNhap == username);
        if (account == null)
        {
            return RedirectToAction("Login");
        }

        if (HashPassword(oldPassword) != account.MatKhau)
        {
            ViewBag.Error = "Mật khẩu cũ không đúng.";
            return View();
        }

        if (newPassword != confirmNewPassword)
        {
            ViewBag.Error = "Mật khẩu xác nhận không khớp.";
            return View();
        }

        account.MatKhau = HashPassword(newPassword);
        _context.Update(account);
        await _context.SaveChangesAsync();

        ViewBag.Success = "Đổi mật khẩu thành công.";
        return View();
    }

    // Đăng xuất
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    // Băm mật khẩu SHA256
    private static string HashPassword(string password)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash); // viết hoa A-F
    }
}
