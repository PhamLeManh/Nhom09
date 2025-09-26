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
    public class MdpSanPhamsController : Controller
    {
        private readonly Project2Context _context;

        public MdpSanPhamsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpSanPhams
        public async Task<IActionResult> Index()
        {
            return View(await _context.MdpSanPhams.ToListAsync());
        }

        // GET: MdpSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpSanPham = await _context.MdpSanPhams
                .FirstOrDefaultAsync(m => m.MdpSanPhamId == id);
            if (mdpSanPham == null)
            {
                return NotFound();
            }

            return View(mdpSanPham);
        }

        // GET: MdpSanPhams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MdpSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpSanPhamId,MdpTenSanPham,MdpMoTa,MdpCreatedAt,MdpUpdatedAt")] MdpSanPham mdpSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mdpSanPham);
        }

        // GET: MdpSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpSanPham = await _context.MdpSanPhams.FindAsync(id);
            if (mdpSanPham == null)
            {
                return NotFound();
            }
            return View(mdpSanPham);
        }

        // POST: MdpSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpSanPhamId,MdpTenSanPham,MdpMoTa,MdpCreatedAt,MdpUpdatedAt")] MdpSanPham mdpSanPham)
        {
            if (id != mdpSanPham.MdpSanPhamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpSanPhamExists(mdpSanPham.MdpSanPhamId))
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
            return View(mdpSanPham);
        }

        // GET: MdpSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpSanPham = await _context.MdpSanPhams
                .FirstOrDefaultAsync(m => m.MdpSanPhamId == id);
            if (mdpSanPham == null)
            {
                return NotFound();
            }

            return View(mdpSanPham);
        }

        // POST: MdpSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpSanPham = await _context.MdpSanPhams.FindAsync(id);
            if (mdpSanPham != null)
            {
                _context.MdpSanPhams.Remove(mdpSanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpSanPhamExists(int id)
        {
            return _context.MdpSanPhams.Any(e => e.MdpSanPhamId == id);
        }
    }
}
