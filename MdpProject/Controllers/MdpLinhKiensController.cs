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
    public class MdpLinhKiensController : Controller
    {
        private readonly Project2Context _context;

        public MdpLinhKiensController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpLinhKiens
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpLinhKiens.Include(m => m.MdpSanPham);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpLinhKiens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpLinhKien = await _context.MdpLinhKiens
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpLinhKienId == id);
            if (mdpLinhKien == null)
            {
                return NotFound();
            }

            return View(mdpLinhKien);
        }

        // GET: MdpLinhKiens/Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId");
            return View();
        }

        // POST: MdpLinhKiens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpLinhKienId,MdpSanPhamId,MdpTenLinhKien,MdpLoaiLinhKien,MdpThongSoKyThuat,MdpCreatedAt,MdpUpdatedAt")] MdpLinhKien mdpLinhKien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpLinhKien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpLinhKien.MdpSanPhamId);
            return View(mdpLinhKien);
        }

        // GET: MdpLinhKiens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpLinhKien = await _context.MdpLinhKiens.FindAsync(id);
            if (mdpLinhKien == null)
            {
                return NotFound();
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpLinhKien.MdpSanPhamId);
            return View(mdpLinhKien);
        }

        // POST: MdpLinhKiens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpLinhKienId,MdpSanPhamId,MdpTenLinhKien,MdpLoaiLinhKien,MdpThongSoKyThuat,MdpCreatedAt,MdpUpdatedAt")] MdpLinhKien mdpLinhKien)
        {
            if (id != mdpLinhKien.MdpLinhKienId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpLinhKien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpLinhKienExists(mdpLinhKien.MdpLinhKienId))
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
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpLinhKien.MdpSanPhamId);
            return View(mdpLinhKien);
        }

        // GET: MdpLinhKiens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpLinhKien = await _context.MdpLinhKiens
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpLinhKienId == id);
            if (mdpLinhKien == null)
            {
                return NotFound();
            }

            return View(mdpLinhKien);
        }

        // POST: MdpLinhKiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpLinhKien = await _context.MdpLinhKiens.FindAsync(id);
            if (mdpLinhKien != null)
            {
                _context.MdpLinhKiens.Remove(mdpLinhKien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpLinhKienExists(int id)
        {
            return _context.MdpLinhKiens.Any(e => e.MdpLinhKienId == id);
        }
    }
}
