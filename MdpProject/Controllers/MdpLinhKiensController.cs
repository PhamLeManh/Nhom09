using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpLinhKiensController : Controller
    {
        private readonly Project2Context _context;

        public MdpLinhKiensController(Project2Context context)
        {
            _context = context;
        }

        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var list = await _context.MdpLinhKiens
                                     .Include(m => m.MdpSanPham)
                                     .ToListAsync();
            return View(list);
        }

        // ================= Index (cho khách) =================
        public async Task<IActionResult> Index()
        {
            var list = await _context.MdpLinhKiens
                                     .Include(m => m.MdpSanPham)
                                     .ToListAsync();
            return View(list);
        }

        // ================= Details =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var linhKien = await _context.MdpLinhKiens
                                         .Include(m => m.MdpSanPham)
                                         .FirstOrDefaultAsync(m => m.MdpLinhKienId == id);
            if (linhKien == null) return NotFound();

            return View(linhKien);
        }

        // ================= Create =================
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpLinhKien linhKien)
        {
            ModelState.Remove("MdpSanPham"); // bỏ validation navigation property

            if (ModelState.IsValid)
            {
                linhKien.MdpCreatedAt = DateTime.Now;
                linhKien.MdpUpdatedAt = DateTime.Now;

                _context.Add(linhKien);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Thêm Linh Kiện thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", linhKien.MdpSanPhamId);
            return View(linhKien);
        }

        // ================= Edit =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var linhKien = await _context.MdpLinhKiens.FindAsync(id);
            if (linhKien == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", linhKien.MdpSanPhamId);
            return View(linhKien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpLinhKien linhKien)
        {
            if (id != linhKien.MdpLinhKienId) return NotFound();

            ModelState.Remove("MdpSanPham");

            if (!ModelState.IsValid)
            {
                ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", linhKien.MdpSanPhamId);
                return View(linhKien);
            }

            var entity = await _context.MdpLinhKiens.FindAsync(id);
            if (entity == null) return NotFound();

            // Cập nhật các field
            entity.MdpSanPhamId = linhKien.MdpSanPhamId;
            entity.MdpTenLinhKien = linhKien.MdpTenLinhKien?.Trim();
            entity.MdpLoaiLinhKien = linhKien.MdpLoaiLinhKien?.Trim();
            entity.MdpThongSoKyThuat = linhKien.MdpThongSoKyThuat?.Trim();
            entity.MdpAnh = linhKien.MdpAnh?.Trim();
            entity.MdpUpdatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Cập nhật Linh Kiện thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MdpLinhKiens.Any(e => e.MdpLinhKienId == id)) return NotFound();
                else throw;
            }
        }

        // ================= Delete =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var linhKien = await _context.MdpLinhKiens
                                         .Include(m => m.MdpSanPham)
                                         .FirstOrDefaultAsync(m => m.MdpLinhKienId == id);
            if (linhKien == null) return NotFound();

            return View(linhKien);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linhKien = await _context.MdpLinhKiens.FindAsync(id);
            if (linhKien != null)
            {
                _context.MdpLinhKiens.Remove(linhKien);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MdpAdminIndex));
        }

    }
}
