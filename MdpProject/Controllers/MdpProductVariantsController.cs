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

        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            // Lấy danh sách ProductVariant kèm tên sản phẩm và màu sắc
            var variants = await _context.MdpProductVariants
                                         .Include(p => p.MdpSanPham)
                                         .Include(p => p.MdpMauSac)
                                         .ToListAsync();

            // Nếu muốn dropdown filter sản phẩm (có thể dùng sau)
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");

            return View(variants);
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
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            ViewData["MdpMauSacId"] = new SelectList(_context.MdpMauSacs, "MdpMauSacId", "MdpTenMau");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpProductVariant variant)
        {
            ModelState.Remove("MdpSanPham");
            ModelState.Remove("MdpMauSac");

            // Kiểm tra foreign key trước khi thêm
            var spExists = await _context.MdpSanPhams.AnyAsync(s => s.MdpSanPhamId == variant.MdpSanPhamId);
            if (!spExists)
            {
                ModelState.AddModelError("MdpSanPhamId", "Sản phẩm không tồn tại!");
            }

            if (variant.MdpMauSacId.HasValue)
            {
                var colorExists = await _context.MdpMauSacs.AnyAsync(c => c.MdpMauSacId == variant.MdpMauSacId.Value);
                if (!colorExists)
                {
                    ModelState.AddModelError("MdpMauSacId", "Màu sắc không tồn tại!");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(variant);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "✅ Thêm Variant thành công!";
                    return RedirectToAction("MdpAdminIndex");
                }
                catch (DbUpdateException dbEx)
                {
                    var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                    TempData["Error"] = "❌ Lỗi khi thêm: " + inner;
                }
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", variant.MdpSanPhamId);
            ViewData["MdpMauSacId"] = new SelectList(_context.MdpMauSacs, "MdpMauSacId", "MdpTenMau", variant.MdpMauSacId);
            return View(variant);
        }



        // ================= Edit =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var variant = await _context.MdpProductVariants.FindAsync(id);
            if (variant == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", variant.MdpSanPhamId);
            ViewData["MdpMauSacId"] = new SelectList(_context.MdpMauSacs, "MdpMauSacId", "MdpTenMau", variant.MdpMauSacId);

            return View(variant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpProductVariant variant)
        {
            if (id != variant.MdpVariantId) return NotFound();

            // Remove validation cho navigation properties
            ModelState.Remove("MdpSanPham");
            ModelState.Remove("MdpMauSac");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variant);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "✅ Cập nhật Variant thành công!";
                    return RedirectToAction("MdpAdminIndex");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpProductVariants.Any(e => e.MdpVariantId == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", variant.MdpSanPhamId);
            ViewData["MdpMauSacId"] = new SelectList(_context.MdpMauSacs, "MdpMauSacId", "MdpTenMau", variant.MdpMauSacId);

            return View(variant);
        }

        // GET: Delete page
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var variant = await _context.MdpProductVariants
                .Include(v => v.MdpSanPham)
                .Include(v => v.MdpMauSac)
                .FirstOrDefaultAsync(v => v.MdpVariantId == id);

            if (variant == null) return NotFound();

            return View(variant);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variant = await _context.MdpProductVariants
                .Include(v => v.MdpCarts)
                .Include(v => v.MdpChiTietDonHangs)
                .Include(v => v.MdpChiTietHoaDons)
                .Include(v => v.MdpDoanhThuSanPhams)
                .FirstOrDefaultAsync(v => v.MdpVariantId == id);

            if (variant != null)
            {
                // Xóa các bản ghi con
                _context.MdpCarts.RemoveRange(variant.MdpCarts);
                _context.MdpChiTietDonHangs.RemoveRange(variant.MdpChiTietDonHangs);
                _context.MdpChiTietHoaDons.RemoveRange(variant.MdpChiTietHoaDons);
                _context.MdpDoanhThuSanPhams.RemoveRange(variant.MdpDoanhThuSanPhams);

                // Xóa bản ghi chính
                _context.MdpProductVariants.Remove(variant);

                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Variant đã được xóa thành công!";
            }
            else
            {
                TempData["Error"] = "❌ Variant không tồn tại!";
            }

            return RedirectToAction("MdpAdminIndex"); // Hoặc Index
        }




    }
}
