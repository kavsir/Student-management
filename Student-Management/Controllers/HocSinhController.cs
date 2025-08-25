using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Controllers
{
    [Authorize(Roles = "HocSinh")]
    public class HocSinhController : Controller
    {
        private readonly QuanLyHocSinhContext _context;

        public HocSinhController(QuanLyHocSinhContext context)
        {
            _context = context;
        }

        // GET: HocSinh (chọn lớp)
        public async Task<IActionResult> Index()
        {
            var lopList = await _context.Lops
                .Include(l => l.MaGvcnNavigation) // giáo viên chủ nhiệm
                .Include(l => l.MaNamHocNavigation)
                .ToListAsync();

            return View(lopList);
        }

        // GET: HocSinh/ByClass/5
        public async Task<IActionResult> ByClass(int? id)
        {
            if (id == null) return NotFound();

            var lop = await _context.Lops
                .Include(l => l.Hocsinhs)
                .Include(l => l.MaGvcnNavigation)
                .FirstOrDefaultAsync(l => l.MaLop == id);

            if (lop == null) return NotFound();

            return View(lop);
        }

        // GET: HocSinh/Create
        public IActionResult Create(int? lopId)
        {
            ViewBag.LopList = new SelectList(_context.Lops, "MaLop", "TenLop", lopId);
            return View();
        }

        // POST: HocSinh/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hocsinh hocsinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocsinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ByClass), new { id = hocsinh.MaLop });
            }

            ViewBag.LopList = new SelectList(_context.Lops, "MaLop", "TenLop", hocsinh.MaLop);
            return View(hocsinh);
        }

        // GET: HocSinh/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hocsinh = await _context.Hocsinhs.FindAsync(id);
            if (hocsinh == null) return NotFound();

            ViewBag.LopList = new SelectList(_context.Lops, "MaLop", "TenLop", hocsinh.MaLop);
            return View(hocsinh);
        }

        // POST: HocSinh/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hocsinh hocsinh)
        {
            if (id != hocsinh.MaHs) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocsinh);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ByClass), new { id = hocsinh.MaLop });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Hocsinhs.Any(e => e.MaHs == hocsinh.MaHs)) return NotFound();
                    else throw;
                }
            }
            ViewBag.LopList = new SelectList(_context.Lops, "MaLop", "TenLop", hocsinh.MaLop);
            return View(hocsinh);
        }

        // GET: HocSinh/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var hocsinh = await _context.Hocsinhs
                .Include(h => h.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaHs == id);

            if (hocsinh == null) return NotFound();
            return View(hocsinh);
        }

        // GET: HocSinh/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hocsinh = await _context.Hocsinhs
                .Include(h => h.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaHs == id);

            if (hocsinh == null) return NotFound();
            return View(hocsinh);
        }

        // POST: HocSinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hocsinh = await _context.Hocsinhs.FindAsync(id);
            if (hocsinh != null)
            {
                int lopId = hocsinh.MaLop;
                _context.Hocsinhs.Remove(hocsinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ByClass), new { id = lopId });
            }
            return NotFound();
        }
    }
}
