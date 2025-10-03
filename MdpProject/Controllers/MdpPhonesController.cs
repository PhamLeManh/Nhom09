using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpPhonesController : Controller
    {
        private readonly Project2Context _context;

        public MdpPhonesController(Project2Context context)
        {
            _context = context;
        }

        // ================= CLIENT =================
        // Hiển thị danh sách điện thoại cho khách hàng + filter
        public async Task<IActionResult> Index(string? ram, string? storage, string? chipset)
        {
            var query = _context.MdpPhones
                .Include(p => p.MdpSanPham)
                .AsQueryable();

            if (!string.IsNullOrEmpty(ram))
                query = query.Where(p => p.MdpRam == ram);

            if (!string.IsNullOrEmpty(storage))
                query = query.Where(p => p.MdpStorage == storage);

            if (!string.IsNullOrEmpty(chipset))
                query = query.Where(p => p.MdpChipset == chipset);

            return View(await query.ToListAsync());
        }

        // ================= ADMIN =================
        // Hiển thị danh sách điện thoại trong Admin + filter
        public async Task<IActionResult> MdpAdminIndex(string? ram, string? storage, string? chipset)
        {
            var query = _context.MdpPhones
                .Include(p => p.MdpSanPham)
                .AsQueryable();

            if (!string.IsNullOrEmpty(ram))
                query = query.Where(p => p.MdpRam == ram);

            if (!string.IsNullOrEmpty(storage))
                query = query.Where(p => p.MdpStorage == storage);

            if (!string.IsNullOrEmpty(chipset))
                query = query.Where(p => p.MdpChipset == chipset);

            return View(await query.ToListAsync());
        }

        // ================= DETAILS =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var phone = await _context.MdpPhones
                .Include(p => p.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpPhoneId == id);

            if (phone == null) return NotFound();

            return View(phone);
        }

        // ================= CREATE =================
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpPhone phone, string? imageUrl)
        {
            ModelState.Remove("MdpSanPham");

            if (ModelState.IsValid)
            {
                phone.MdpCreatedAt = DateTime.Now;
                if (!string.IsNullOrEmpty(imageUrl))
                    phone.MdpAnh = imageUrl;

                _context.Add(phone);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Thêm điện thoại thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", phone.MdpSanPhamId);
            return View(phone);
        }

        // ================= EDIT =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var phone = await _context.MdpPhones.FindAsync(id);
            if (phone == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", phone.MdpSanPhamId);
            return View(phone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpPhone phone, string? imageUrl)
        {
            ModelState.Remove("MdpSanPham");

            if (id != phone.MdpPhoneId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    phone.MdpUpdatedAt = DateTime.Now;
                    if (!string.IsNullOrEmpty(imageUrl))
                        phone.MdpAnh = imageUrl;

                    _context.Update(phone);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "✅ Cập nhật điện thoại thành công!";
                    return RedirectToAction(nameof(MdpAdminIndex));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpPhones.Any(e => e.MdpPhoneId == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", phone.MdpSanPhamId);
            return View(phone);
        }

        // ================= DELETE =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var phone = await _context.MdpPhones
                .Include(p => p.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpPhoneId == id);

            if (phone == null) return NotFound();

            return View(phone);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _context.MdpPhones.FindAsync(id);
            if (phone != null)
            {
                _context.MdpPhones.Remove(phone);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Xóa điện thoại thành công!";
            }
            else
            {
                TempData["Error"] = "❌ Điện thoại không tồn tại!";
            }
            return RedirectToAction(nameof(MdpAdminIndex));
        }
    }
}
