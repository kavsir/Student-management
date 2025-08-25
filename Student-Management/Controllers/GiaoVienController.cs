using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Controllers
{
    [Authorize(Roles = "GiaoVien")]
    public class GiaoVienController : Controller
    {
        private readonly QuanLyHocSinhContext _context;

        public GiaoVienController(QuanLyHocSinhContext context)
        {
            _context = context;
        }

        // GET: GiaoVien
        public async Task<IActionResult> Index()
        {
            var giaoViens = await _context.Giaoviens
                .Include(g => g.MaMonHocNavigation)
                .Include(g => g.Lops)
                .ToListAsync();

            return View(giaoViens);
        }

        // GET: GiaoVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var giaovien = await _context.Giaoviens
                .Include(g => g.MaMonHocNavigation)
                .Include(g => g.Lops)
                .Include(g => g.Lichhocs).ThenInclude(l => l.MaMonHocNavigation)
                .Include(g => g.Lichhocs).ThenInclude(l => l.MaLopNavigation)
                .Include(g => g.PhancongGiangdays).ThenInclude(p => p.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);

            if (giaovien == null) return NotFound();

            return View(giaovien);
        }

        // GET: GiaoVien/Create
        public IActionResult Create()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Xem trong Output của VS
                }
            }

            LoadDropdowns();   // nạp danh sách Môn học và Lớp
            return View();
        }

        // POST: GiaoVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Giaovien giaovien, int? LopChuNhiemId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaovien);
                await _context.SaveChangesAsync();

                if (LopChuNhiemId.HasValue)
                {
                    var lop = await _context.Lops.FindAsync(LopChuNhiemId.Value);
                    if (lop != null)
                    {
                        lop.MaGvcn = giaovien.MaGv;
                        _context.Update(lop);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            LoadDropdowns();
            return View(giaovien);
        }

        // GET: GiaoVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var giaovien = await _context.Giaoviens
                .Include(g => g.Lops)
                .FirstOrDefaultAsync(g => g.MaGv == id);

            if (giaovien == null) return NotFound();

            var lopChuNhiem = await _context.Lops.FirstOrDefaultAsync(l => l.MaGvcn == giaovien.MaGv);
            ViewBag.LopChuNhiemId = lopChuNhiem?.MaLop;

            LoadDropdowns();
            return View(giaovien);
        }

        // POST: GiaoVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Giaovien giaovien, int? LopChuNhiemId)
        {
            if (id != giaovien.MaGv) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaovien);
                    await _context.SaveChangesAsync();

                    // Clear lớp cũ
                    var oldLops = _context.Lops.Where(l => l.MaGvcn == giaovien.MaGv);
                    foreach (var lop in oldLops)
                    {
                        lop.MaGvcn = null;
                    }

                    if (LopChuNhiemId.HasValue)
                    {
                        var lop = await _context.Lops.FindAsync(LopChuNhiemId.Value);
                        if (lop != null)
                        {
                            lop.MaGvcn = giaovien.MaGv;
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Giaoviens.Any(e => e.MaGv == giaovien.MaGv))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            LoadDropdowns();
            return View(giaovien);
        }

        // GET: GiaoVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var giaovien = await _context.Giaoviens
                .Include(g => g.MaMonHocNavigation)
                .Include(g => g.Lops)
                .Include(g => g.Lichhocs).ThenInclude(l => l.MaMonHocNavigation)
                .Include(g => g.Lichhocs).ThenInclude(l => l.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);

            if (giaovien == null) return NotFound();

            return View(giaovien);
        }

        // POST: GiaoVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giaovien = await _context.Giaoviens
                .Include(g => g.Lops)
                .Include(g => g.Lichhocs)
                .Include(g => g.PhancongGiangdays)
                .FirstOrDefaultAsync(g => g.MaGv == id);

            if (giaovien != null)
            {
                // 1. Bỏ liên kết lớp chủ nhiệm
                foreach (var lop in giaovien.Lops)
                {
                    lop.MaGvcn = null;
                }

                // 2. Xóa lịch học liên quan
                foreach (var lich in giaovien.Lichhocs.ToList())
                {
                    _context.Lichhocs.Remove(lich);
                }

                // 3. Xóa phân công giảng dạy liên quan
                foreach (var pc in giaovien.PhancongGiangdays.ToList())
                {
                    _context.PhancongGiangdays.Remove(pc);
                }

                // 4. Xóa giáo viên
                _context.Giaoviens.Remove(giaovien);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private void LoadDropdowns()
        {
            ViewBag.MonHocList = _context.Monhocs.ToList();
            ViewBag.LopList = _context.Lops.ToList();
        }
    }
}
