using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MdpProject.Controllers
{
    public class MdpLaptopsController : Controller
    {
        private readonly Project2Context _context;

        public MdpLaptopsController(Project2Context context)
        {
            _context = context;
        }

        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var laptops = await _context.MdpLaptops
                                        .Include(l => l.MdpSanPham)
                                        .ToListAsync();
            return View(laptops);
        }

        // ================= Index (Client) =================
        public async Task<IActionResult> Index()
        {
            var laptops = _context.MdpLaptops.Include(l => l.MdpSanPham);
            return View(await laptops.ToListAsync());
        }

        // ================= Details =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var laptop = await _context.MdpLaptops
                .Include(l => l.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpLaptopId == id);

            if (laptop == null) return NotFound();

            return View(laptop);
        }

        // ================= Create =================
        // GET
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpLaptop mdpLaptop)
        {
            // Bỏ validation cho navigation property
            ModelState.Remove("MdpSanPham");

            if (ModelState.IsValid)
            {
                mdpLaptop.MdpCreatedAt = DateTime.Now;
                mdpLaptop.MdpUpdatedAt = DateTime.Now;

                _context.Add(mdpLaptop);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Thêm laptop thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }

            // Nếu ModelState invalid
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            TempData["Error"] = "Lỗi nhập liệu:<br/>" + string.Join("<br/>", errors);

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", mdpLaptop.MdpSanPhamId);
            return View(mdpLaptop);
        }

        // ================= Edit =================
        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var laptop = await _context.MdpLaptops.FindAsync(id);
            if (laptop == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", laptop.MdpSanPhamId);
            return View(laptop);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpLaptop mdpLaptop)
        {
            ModelState.Remove("MdpSanPham"); // bỏ validation navigation property

            if (id != mdpLaptop.MdpLaptopId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    mdpLaptop.MdpUpdatedAt = DateTime.Now;
                    _context.Update(mdpLaptop);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "✅ Cập nhật Laptop thành công!";
                    return RedirectToAction(nameof(MdpAdminIndex));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpLaptops.Any(e => e.MdpLaptopId == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", mdpLaptop.MdpSanPhamId);
            return View(mdpLaptop);
        }

        // ================= Delete =================
        // GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var laptop = await _context.MdpLaptops
                .Include(l => l.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpLaptopId == id);

            if (laptop == null) return NotFound();

            return View(laptop);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptop = await _context.MdpLaptops.FindAsync(id);
            if (laptop != null)
            {
                _context.MdpLaptops.Remove(laptop);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Xóa Laptop thành công!";
            }
            else
            {
                TempData["Error"] = "❌ Laptop không tồn tại!";
            }

            return RedirectToAction(nameof(MdpAdminIndex));
        }
    }
}
