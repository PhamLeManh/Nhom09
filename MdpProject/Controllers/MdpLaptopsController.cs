using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;
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

        // GET: MdpLaptops/MdpAdminIndex
        public async Task<IActionResult> MdpAdminIndex()
        {
            var laptops = await _context.MdpLaptops.Include(l => l.MdpSanPham).ToListAsync();
            return View(laptops);
        }
        // ================= Index =================
        public async Task<IActionResult> Index()
        {
            var laptops = _context.MdpLaptops
                .Include(l => l.MdpSanPham);
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
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpLaptop mdpLaptop)
        {
            if (ModelState.IsValid)
            {
                mdpLaptop.MdpCreatedAt = DateTime.Now;
                mdpLaptop.MdpUpdatedAt = DateTime.Now;

                _context.Add(mdpLaptop);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "✅ Thêm Laptop thành công!";
                return RedirectToAction(nameof(Index));
            }

            // Debug lỗi
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["ErrorMessage"] = "❌ Thêm Laptop thất bại: " + string.Join(", ", errors);

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", mdpLaptop.MdpSanPhamId);
            return View(mdpLaptop);
        }

        // ================= Edit =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var laptop = await _context.MdpLaptops.FindAsync(id);
            if (laptop == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", laptop.MdpSanPhamId);
            return View(laptop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpLaptop mdpLaptop)
        {
            if (id != mdpLaptop.MdpLaptopId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    mdpLaptop.MdpUpdatedAt = DateTime.Now;
                    _context.Update(mdpLaptop);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "✅ Cập nhật Laptop thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpLaptops.Any(e => e.MdpLaptopId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", mdpLaptop.MdpSanPhamId);
            return View(mdpLaptop);
        }

        // ================= Delete =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var laptop = await _context.MdpLaptops
                .Include(l => l.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpLaptopId == id);

            if (laptop == null) return NotFound();

            return View(laptop);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptop = await _context.MdpLaptops.FindAsync(id);
            if (laptop != null)
            {
                _context.MdpLaptops.Remove(laptop);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Xóa Laptop thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}