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
    public class MdpPcsController : Controller
    {
        private readonly Project2Context _context;

        public MdpPcsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpPcs
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpPcs.Include(m => m.MdpSanPham);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpPcs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpPc = await _context.MdpPcs
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpPcid == id);
            if (mdpPc == null)
            {
                return NotFound();
            }

            return View(mdpPc);
        }

        // GET: MdpPcs/Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId");
            return View();
        }

        // POST: MdpPcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpPcid,MdpSanPhamId,MdpTenPc,MdpCpu,MdpGpu,MdpRam,MdpStorage,MdpMainboard,MdpPsu,MdpCaseType,MdpCreatedAt,MdpUpdatedAt")] MdpPc mdpPc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpPc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpPc.MdpSanPhamId);
            return View(mdpPc);
        }

        // GET: MdpPcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpPc = await _context.MdpPcs.FindAsync(id);
            if (mdpPc == null)
            {
                return NotFound();
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpPc.MdpSanPhamId);
            return View(mdpPc);
        }

        // POST: MdpPcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpPcid,MdpSanPhamId,MdpTenPc,MdpCpu,MdpGpu,MdpRam,MdpStorage,MdpMainboard,MdpPsu,MdpCaseType,MdpCreatedAt,MdpUpdatedAt")] MdpPc mdpPc)
        {
            if (id != mdpPc.MdpPcid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpPc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpPcExists(mdpPc.MdpPcid))
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
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpPc.MdpSanPhamId);
            return View(mdpPc);
        }

        // GET: MdpPcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpPc = await _context.MdpPcs
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpPcid == id);
            if (mdpPc == null)
            {
                return NotFound();
            }

            return View(mdpPc);
        }

        // POST: MdpPcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpPc = await _context.MdpPcs.FindAsync(id);
            if (mdpPc != null)
            {
                _context.MdpPcs.Remove(mdpPc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpPcExists(int id)
        {
            return _context.MdpPcs.Any(e => e.MdpPcid == id);
        }
    }
}
