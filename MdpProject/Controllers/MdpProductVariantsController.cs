using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpProductVariantsController : Controller
    {
        private readonly Project2Context _context;

        public MdpProductVariantsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpProductVariants?sanPhamId=1
        public async Task<IActionResult> Index(int sanPhamId)
        {
            if (sanPhamId == 0) return NotFound();

            var sanPham = await _context.MdpSanPhams
                .FirstOrDefaultAsync(sp => sp.MdpSanPhamId == sanPhamId);

            if (sanPham == null) return NotFound();

            var variants = await _context.MdpProductVariants
                .Include(v => v.MdpMauSac)
                .Where(v => v.MdpSanPhamId == sanPhamId)
                .ToListAsync();

            ViewBag.SanPhamId = sanPham.MdpSanPhamId;
            ViewBag.TenSanPham = sanPham.MdpTenSanPham;

            return View(variants);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewData["MdpMauSacId"] = new SelectList(_context.MdpMauSacs, "MdpMauSacId", "TenMau");
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpVariantId,MdpSanPhamId,MdpMauSacId,MdpDungLuong,MdpGia,MdpSoLuong")] MdpProductVariant mdpProductVariant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpProductVariant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { sanPhamId = mdpProductVariant.MdpSanPhamId });
            }
            ViewData["MdpMauSacId"] = new SelectList(_context.MdpMauSacs, "MdpMauSacId", "TenMau", mdpProductVariant.MdpMauSacId);
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", mdpProductVariant.MdpSanPhamId);
            return View(mdpProductVariant);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var variant = await _context.MdpProductVariants.FindAsync(id);
            if (variant == null) return NotFound();

            ViewData["MdpMauSacId"] = new SelectList(_context.MdpMauSacs, "MdpMauSacId", "TenMau", variant.MdpMauSacId);
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", variant.MdpSanPhamId);

            return View(variant);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpVariantId,MdpSanPhamId,MdpMauSacId,MdpDungLuong,MdpGia,MdpSoLuong")] MdpProductVariant mdpProductVariant)
        {
            if (id != mdpProductVariant.MdpVariantId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpProductVariant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpProductVariantExists(mdpProductVariant.MdpVariantId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index), new { sanPhamId = mdpProductVariant.MdpSanPhamId });
            }
            return View(mdpProductVariant);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var variant = await _context.MdpProductVariants
                .Include(v => v.MdpMauSac)
                .Include(v => v.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpVariantId == id);

            if (variant == null) return NotFound();

            return View(variant);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variant = await _context.MdpProductVariants
                .Include(v => v.MdpCarts)
                .Include(v => v.MdpChiTietDonHangs)
                .Include(v => v.MdpChiTietHoaDons)
                .FirstOrDefaultAsync(v => v.MdpVariantId == id);

            if (variant != null)
            {
                _context.MdpCarts.RemoveRange(variant.MdpCarts);
                _context.MdpChiTietDonHangs.RemoveRange(variant.MdpChiTietDonHangs);
                _context.MdpChiTietHoaDons.RemoveRange(variant.MdpChiTietHoaDons);

                _context.MdpProductVariants.Remove(variant);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { sanPhamId = variant?.MdpSanPhamId });
        }

        private bool MdpProductVariantExists(int id)
        {
            return _context.MdpProductVariants.Any(e => e.MdpVariantId == id);
        }

    }

}
