using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly QuanLyHocSinhContext _context;

        public AdminController(QuanLyHocSinhContext context)
        {
            _context = context;
        }

        // DASHBOARD
        public async Task<IActionResult> Index()
        {
            ViewBag.TongHocSinh = await _context.Hocsinhs.CountAsync();
            ViewBag.TongGiaoVien = await _context.Giaoviens.CountAsync();
            ViewBag.TongLop = await _context.Lops.CountAsync();
            ViewBag.TongTaiKhoan = await _context.Taikhoans.CountAsync();
            return View();
        }

        // ============ TÀI KHOẢN ==============

        public async Task<IActionResult> TaiKhoan()
        {
            var danhSachTaiKhoan = await _context.Taikhoans
                .Include(t => t.MaHsNavigation)
                .Include(t => t.MaGvNavigation)
                .ToListAsync();

            return View(danhSachTaiKhoan);
        }

        public IActionResult TaoTaiKhoan()
        {
            ViewBag.HocSinhList = new SelectList(_context.Hocsinhs, "MaHs", "HoTen");
            ViewBag.GiaoVienList = new SelectList(_context.Giaoviens, "MaGv", "HoTen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaoTaiKhoan(Taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TaiKhoan));
            }

            ViewBag.HocSinhList = new SelectList(_context.Hocsinhs, "MaHs", "HoTen", taikhoan.MaHs);
            ViewBag.GiaoVienList = new SelectList(_context.Giaoviens, "MaGv", "HoTen", taikhoan.MaGv);
            return View(taikhoan);
        }

        public async Task<IActionResult> XoaTaiKhoan(int? id)
        {
            if (id == null) return NotFound();

            var tk = await _context.Taikhoans
                .Include(t => t.MaHsNavigation)
                .Include(t => t.MaGvNavigation)
                .FirstOrDefaultAsync(m => m.MaTk == id);

            if (tk == null) return NotFound();

            return View(tk);
        }

        [HttpPost, ActionName("XoaTaiKhoan")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XacNhanXoaTaiKhoan(int id)
        {
            var tk = await _context.Taikhoans.FindAsync(id);
            if (tk != null)
            {
                _context.Taikhoans.Remove(tk);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(TaiKhoan));
        }

        // ============ DANH SÁCH GIÁO VIÊN ==============
        public async Task<IActionResult> DanhSachGiaoVien()
        {
            var gv = await _context.Giaoviens
                .Include(g => g.MaMonHocNavigation)
                .ToListAsync();
            return View(gv);
        }

        // ============ DANH SÁCH HỌC SINH ==============
        public async Task<IActionResult> DanhSachHocSinh()
        {
            var hs = await _context.Hocsinhs
                .Include(h => h.MaLopNavigation)
                .ToListAsync();
            return View(hs);
        }

        // ============ DANH SÁCH LỚP HỌC ==============
        public async Task<IActionResult> DanhSachLop()
        {
            var lop = await _context.Lops
                .Include(l => l.MaGvcnNavigation)
                .Include(l => l.MaNamHocNavigation)
                .ToListAsync();
            return View(lop);
        }
    }
}
