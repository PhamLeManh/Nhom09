using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpMonitorsController : Controller
    {
        private readonly Project2Context _context;

        public MdpMonitorsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpMonitors
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpMonitors.Include(m => m.MdpSanPham);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpMonitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpMonitor = await _context.MdpMonitors
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpMonitorId == id);
            if (mdpMonitor == null)
            {
                return NotFound();
            }

            return View(mdpMonitor);
        }

        // GET: MdpMonitors/Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId");
            return View();
        }

        // POST: MdpMonitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpMonitorId,MdpSanPhamId,MdpTenMonitor,MdpKichThuoc,MdpDoPhanGiai,MdpTanSoQuet,MdpCongKetNoi,MdpTamNen,MdpCreatedAt,MdpUpdatedAt")] MdpMonitor mdpMonitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpMonitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpMonitor.MdpSanPhamId);
            return View(mdpMonitor);
        }

        // GET: MdpMonitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpMonitor = await _context.MdpMonitors.FindAsync(id);
            if (mdpMonitor == null)
            {
                return NotFound();
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpMonitor.MdpSanPhamId);
            return View(mdpMonitor);
        }

        // POST: MdpMonitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpMonitorId,MdpSanPhamId,MdpTenMonitor,MdpKichThuoc,MdpDoPhanGiai,MdpTanSoQuet,MdpCongKetNoi,MdpTamNen,MdpCreatedAt,MdpUpdatedAt")] MdpMonitor mdpMonitor)
        {
            if (id != mdpMonitor.MdpMonitorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpMonitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpMonitorExists(mdpMonitor.MdpMonitorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpMonitor.MdpSanPhamId);
            return View(mdpMonitor);
        }

        // GET: MdpMonitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpMonitor = await _context.MdpMonitors
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpMonitorId == id);
            if (mdpMonitor == null)
            {
                return NotFound();
            }

            return View(mdpMonitor);
        }

        // POST: MdpMonitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpMonitor = await _context.MdpMonitors.FindAsync(id);
            if (mdpMonitor != null)
            {
                _context.MdpMonitors.Remove(mdpMonitor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpMonitorExists(int id)
        {
            return _context.MdpMonitors.Any(e => e.MdpMonitorId == id);
        }
    }
}
