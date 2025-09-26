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
    public class MdpCamerasController : Controller
    {
        private readonly Project2Context _context;

        public MdpCamerasController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpCameras
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpCameras.Include(m => m.MdpSanPham);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpCameras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpCamera = await _context.MdpCameras
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpCameraId == id);
            if (mdpCamera == null)
            {
                return NotFound();
            }

            return View(mdpCamera);
        }

        // GET: MdpCameras/Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId");
            return View();
        }

        // POST: MdpCameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpCameraId,MdpSanPhamId,MdpTenCamera,MdpDoPhanGiai,MdpCamBien,MdpOngKinh,MdpBoNho,MdpCreatedAt,MdpUpdatedAt")] MdpCamera mdpCamera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpCamera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpCamera.MdpSanPhamId);
            return View(mdpCamera);
        }

        // GET: MdpCameras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpCamera = await _context.MdpCameras.FindAsync(id);
            if (mdpCamera == null)
            {
                return NotFound();
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpCamera.MdpSanPhamId);
            return View(mdpCamera);
        }

        // POST: MdpCameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpCameraId,MdpSanPhamId,MdpTenCamera,MdpDoPhanGiai,MdpCamBien,MdpOngKinh,MdpBoNho,MdpCreatedAt,MdpUpdatedAt")] MdpCamera mdpCamera)
        {
            if (id != mdpCamera.MdpCameraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpCamera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpCameraExists(mdpCamera.MdpCameraId))
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
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpCamera.MdpSanPhamId);
            return View(mdpCamera);
        }

        // GET: MdpCameras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpCamera = await _context.MdpCameras
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpCameraId == id);
            if (mdpCamera == null)
            {
                return NotFound();
            }

            return View(mdpCamera);
        }

        // POST: MdpCameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpCamera = await _context.MdpCameras.FindAsync(id);
            if (mdpCamera != null)
            {
                _context.MdpCameras.Remove(mdpCamera);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpCameraExists(int id)
        {
            return _context.MdpCameras.Any(e => e.MdpCameraId == id);
        }
    }
}
