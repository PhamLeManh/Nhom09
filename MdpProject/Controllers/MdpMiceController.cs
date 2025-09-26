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
    public class MdpMiceController : Controller
    {
        private readonly Project2Context _context;

        public MdpMiceController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpMice
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpMouses.Include(m => m.MdpSanPham);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpMice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpMouse = await _context.MdpMouses
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpMouseId == id);
            if (mdpMouse == null)
            {
                return NotFound();
            }

            return View(mdpMouse);
        }

        // GET: MdpMice/Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId");
            return View();
        }

        // POST: MdpMice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpMouseId,MdpSanPhamId,MdpTenMouse,MdpKieuKetNoi,MdpDoPhanGiai,MdpSoNut,MdpDenLed,MdpCreatedAt,MdpUpdatedAt")] MdpMouse mdpMouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpMouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpMouse.MdpSanPhamId);
            return View(mdpMouse);
        }

        // GET: MdpMice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpMouse = await _context.MdpMouses.FindAsync(id);
            if (mdpMouse == null)
            {
                return NotFound();
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpMouse.MdpSanPhamId);
            return View(mdpMouse);
        }

        // POST: MdpMice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpMouseId,MdpSanPhamId,MdpTenMouse,MdpKieuKetNoi,MdpDoPhanGiai,MdpSoNut,MdpDenLed,MdpCreatedAt,MdpUpdatedAt")] MdpMouse mdpMouse)
        {
            if (id != mdpMouse.MdpMouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpMouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpMouseExists(mdpMouse.MdpMouseId))
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
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpMouse.MdpSanPhamId);
            return View(mdpMouse);
        }

        // GET: MdpMice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpMouse = await _context.MdpMouses
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpMouseId == id);
            if (mdpMouse == null)
            {
                return NotFound();
            }

            return View(mdpMouse);
        }

        // POST: MdpMice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpMouse = await _context.MdpMouses.FindAsync(id);
            if (mdpMouse != null)
            {
                _context.MdpMouses.Remove(mdpMouse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpMouseExists(int id)
        {
            return _context.MdpMouses.Any(e => e.MdpMouseId == id);
        }
    }
}
