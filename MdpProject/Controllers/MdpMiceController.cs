using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpMiceController : Controller
    {
        private readonly Project2Context _context;

        public MdpMiceController(Project2Context context)
        {
            _context = context;
        }

        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var list = await _context.MdpMouses
                                     .Include(m => m.MdpSanPham)
                                     .ToListAsync();
            return View(list);
        }

        // ================= Index (cho khách hàng) =================
        public async Task<IActionResult> Index()
        {
            var list = await _context.MdpMouses.Include(m => m.MdpSanPham).ToListAsync();
            return View(list);
        }

        // ================= Details =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var mouse = await _context.MdpMouses
                                      .Include(m => m.MdpSanPham)
                                      .FirstOrDefaultAsync(m => m.MdpMouseId == id);
            if (mouse == null) return NotFound();

            return View(mouse);
        }

        // ================= Create =================
        // GET: Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpMouse mouse)
        {
            // bỏ validation navigation property
            ModelState.Remove("MdpSanPham");

            if (ModelState.IsValid)
            {
                mouse.MdpCreatedAt = DateTime.Now;
                mouse.MdpUpdatedAt = DateTime.Now;

                _context.Add(mouse);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Thêm Mouse thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }

            ViewData["MdpSanPhamId"] = new SelectList(
                _context.MdpSanPhams,
                "MdpSanPhamId",
                "MdpTenSanPham",
                mouse.MdpSanPhamId
            );
            return View(mouse);
        }


        // ================= Edit =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mouse = await _context.MdpMouses.FindAsync(id);
            if (mouse == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(
                _context.MdpSanPhams,
                "MdpSanPhamId",
                "MdpTenSanPham",
                mouse.MdpSanPhamId
            );
            return View(mouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpMouse mouse)
        {
            if (id != mouse.MdpMouseId) return NotFound();

            ModelState.Remove("MdpSanPham"); // bỏ validation navigation property

            if (!ModelState.IsValid)
            {
                ViewData["MdpSanPhamId"] = new SelectList(
                    _context.MdpSanPhams,
                    "MdpSanPhamId",
                    "MdpTenSanPham",
                    mouse.MdpSanPhamId
                );
                return View(mouse);
            }

            var entity = await _context.MdpMouses.FindAsync(id);
            if (entity == null) return NotFound();

            // Cập nhật các field
            entity.MdpSanPhamId = mouse.MdpSanPhamId;
            entity.MdpTenMouse = mouse.MdpTenMouse?.Trim();
            entity.MdpKieuKetNoi = mouse.MdpKieuKetNoi?.Trim();
            entity.MdpDoPhanGiai = mouse.MdpDoPhanGiai?.Trim();
            entity.MdpSoNut = mouse.MdpSoNut;
            entity.MdpDenLed = mouse.MdpDenLed;
            entity.MdpUpdatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Cập nhật Mouse thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MdpMouses.Any(e => e.MdpMouseId == id)) return NotFound();
                else throw;
            }
        }

        // ================= Delete =================
        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mouse = await _context.MdpMouses
                                      .Include(m => m.MdpSanPham)
                                      .FirstOrDefaultAsync(m => m.MdpMouseId == id);
            if (mouse == null) return NotFound();

            return View(mouse);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mouse = await _context.MdpMouses.FindAsync(id);
            if (mouse != null)
            {
                _context.MdpMouses.Remove(mouse);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MdpAdminIndex));
        }

    }


}
