using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;

namespace Student_Management.Controllers
{
    public class GiaoVienController : Controller
    {
        private readonly QuanLyHocSinhContext _context;
        public GiaoVienController(QuanLyHocSinhContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách giáo viên
        public async Task<IActionResult> Index()
        {
            var giaoviens = await _context.Giaoviens
                .Include(g => g.MaMonHocNavigation)
                .ToListAsync();
            return View(giaoviens);
        }

        // Hiển thị chi tiết giáo viên với id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var giaovien = await _context.Giaoviens
                .Include(g => g.MaMonHocNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);

            if (giaovien == null) return NotFound();

            return View(giaovien);
        }

        // Hiển thị form tạo giáo viên mới
        public IActionResult Create()
        {
            ViewData["MaMonHoc"] = new SelectList(_context.Monhocs, "MaMonHoc", "TenMonHoc");
            return View();
        }

        // Xử lý tạo mới giáo viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Giaovien giaovien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaovien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaMonHoc"] = new SelectList(_context.Monhocs, "MaMonHoc", "TenMonHoc", giaovien.MaMonHoc);
            return View(giaovien);
        }

        // Hiển thị form sửa giáo viên
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var giaovien = await _context.Giaoviens.FindAsync(id);
            if (giaovien == null) return NotFound();

            ViewData["MaMonHoc"] = new SelectList(_context.Monhocs, "MaMonHoc", "TenMonHoc", giaovien.MaMonHoc);
            return View(giaovien);
        }

        // Xử lý cập nhật giáo viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Giaovien giaovien)
        {
            if (id != giaovien.MaGv) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaovien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Giaoviens.Any(e => e.MaGv == giaovien.MaGv))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaMonHoc"] = new SelectList(_context.Monhocs, "MaMonHoc", "TenMonHoc", giaovien.MaMonHoc);
            return View(giaovien);
        }

        // Xác nhận xóa giáo viên
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var giaovien = await _context.Giaoviens
                .Include(g => g.MaMonHocNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);

            if (giaovien == null) return NotFound();

            return View(giaovien);
        }

        // Xử lý xóa giáo viên
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giaovien = await _context.Giaoviens.FindAsync(id);
            if (giaovien != null)
            {
                _context.Giaoviens.Remove(giaovien);
                await _context.SaveChangesAsync();  
            }
            return RedirectToAction(nameof(Index));
        }
    }
}