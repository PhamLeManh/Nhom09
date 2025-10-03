using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpChiTietDonHangsController : Controller
    {
        private readonly Project2Context _context;

        public MdpChiTietDonHangsController(Project2Context context)
        {
            _context = context;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            var model = await _context.MdpChiTietDonHangs
                .Include(c => c.MdpDonHang)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpMauSac)
                .ToListAsync();
            return View(model);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var chiTiet = await _context.MdpChiTietDonHangs
                .Include(c => c.MdpDonHang)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpMauSac)
                .FirstOrDefaultAsync(c => c.MdpChiTietDonHangId == id);

            if (chiTiet == null) return NotFound();

            return View(chiTiet);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var chiTiet = await _context.MdpChiTietDonHangs
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpMauSac)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpDonHang)
                .FirstOrDefaultAsync(c => c.MdpChiTietDonHangId == id);

            if (chiTiet == null) return NotFound();

            ViewData["MdpDonHangId"] = new SelectList(_context.MdpDonHangs, "MdpDonHangId", "MdpDonHangId", chiTiet.MdpDonHangId);
            ViewData["MdpVariantId"] = new SelectList(_context.MdpProductVariants, "MdpVariantId", "MdpVariantId", chiTiet.MdpVariantId);

            return View(chiTiet);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpChiTietDonHangId,MdpDonHangId,MdpVariantId,MdpSoLuong,MdpGia,MdpGiamGia")] MdpChiTietDonHang model)
        {
            if (id != model.MdpChiTietDonHangId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model); // EF sẽ tự ignore MdpThanhTien
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thành công!";
                    return RedirectToAction(nameof(Edit), new { id = model.MdpChiTietDonHangId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpChiTietDonHangs.Any(e => e.MdpChiTietDonHangId == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["MdpDonHangId"] = new SelectList(_context.MdpDonHangs, "MdpDonHangId", "MdpDonHangId", model.MdpDonHangId);
            ViewData["MdpVariantId"] = new SelectList(_context.MdpProductVariants, "MdpVariantId", "MdpVariantId", model.MdpVariantId);
            return View(model);
        }



        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var chiTiet = await _context.MdpChiTietDonHangs
                .Include(c => c.MdpDonHang)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpMauSac)
                .FirstOrDefaultAsync(c => c.MdpChiTietDonHangId == id);

            if (chiTiet == null) return NotFound();

            return View(chiTiet);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTiet = await _context.MdpChiTietDonHangs.FindAsync(id);
            if (chiTiet != null)
            {
                _context.MdpChiTietDonHangs.Remove(chiTiet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
