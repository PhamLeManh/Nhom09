using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MdpProject.Controllers
{
    public class MdpMonitorsController : Controller
    {
        private readonly Project2Context _context;

        public MdpMonitorsController(Project2Context context)
        {
            _context = context;
        }

        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var monitors = await _context.MdpMonitors
                                         .Include(m => m.MdpSanPham)
                                         .ToListAsync();
            return View(monitors);
        }
        // ================= Index (Client) =================
        public async Task<IActionResult> Index()
        {
            var laptops = _context.MdpMonitors.Include(l => l.MdpSanPham);
            return View(await laptops.ToListAsync());
        }



        // ================= Create =================
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpMonitor monitor)
        {
            ModelState.Remove("MdpSanPham"); // bỏ validation navigation property

            if (ModelState.IsValid)
            {
                monitor.MdpCreatedAt = DateTime.Now;
                _context.Add(monitor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Thêm Monitor thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", monitor.MdpSanPhamId);
            return View(monitor);
        }

        // ================= Edit =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var monitor = await _context.MdpMonitors.FindAsync(id);
            if (monitor == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", monitor.MdpSanPhamId);
            return View(monitor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpMonitor monitor)
        {
            ModelState.Remove("MdpSanPham");

            if (id != monitor.MdpMonitorId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    monitor.MdpUpdatedAt = DateTime.Now;
                    _context.Entry(monitor).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "✅ Cập nhật Monitor thành công!";
                    return RedirectToAction(nameof(MdpAdminIndex));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpMonitors.Any(e => e.MdpMonitorId == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", monitor.MdpSanPhamId);
            return View(monitor);
        }

        // ================= Delete =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var monitor = await _context.MdpMonitors
                                        .Include(m => m.MdpSanPham)
                                        .FirstOrDefaultAsync(m => m.MdpMonitorId == id);
            if (monitor == null) return NotFound();

            return View(monitor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monitor = await _context.MdpMonitors.FindAsync(id);
            if (monitor != null)
            {
                _context.MdpMonitors.Remove(monitor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Xóa Monitor thành công!";
            }
            else
            {
                TempData["Error"] = "❌ Monitor không tồn tại!";
            }
            return RedirectToAction(nameof(MdpAdminIndex));
        }

        // ================= Details =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var monitor = await _context.MdpMonitors
                                        .Include(m => m.MdpSanPham)
                                        .FirstOrDefaultAsync(m => m.MdpMonitorId == id);
            if (monitor == null) return NotFound();

            return View(monitor);
        }
    }
}
