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
    public class MdpKhuyenMaisController : Controller
    {
        private readonly Project2Context _context;

        public MdpKhuyenMaisController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpKhuyenMais
        public async Task<IActionResult> Index()
        {
            return View(await _context.MdpKhuyenMais.ToListAsync());
        }

        // GET: MdpKhuyenMais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpKhuyenMai = await _context.MdpKhuyenMais
                .FirstOrDefaultAsync(m => m.MdpKmid == id);
            if (mdpKhuyenMai == null)
            {
                return NotFound();
            }

            return View(mdpKhuyenMai);
        }

        // GET: MdpKhuyenMais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MdpKhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpKmid,MdpMaCode,MdpMoTa,MdpGiaTri,MdpPhanTram,MdpNgayBatDau,MdpNgayKetThuc,MdpTrangThai")] MdpKhuyenMai mdpKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpKhuyenMai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mdpKhuyenMai);
        }

        // GET: MdpKhuyenMais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpKhuyenMai = await _context.MdpKhuyenMais.FindAsync(id);
            if (mdpKhuyenMai == null)
            {
                return NotFound();
            }
            return View(mdpKhuyenMai);
        }

        // POST: MdpKhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpKmid,MdpMaCode,MdpMoTa,MdpGiaTri,MdpPhanTram,MdpNgayBatDau,MdpNgayKetThuc,MdpTrangThai")] MdpKhuyenMai mdpKhuyenMai)
        {
            if (id != mdpKhuyenMai.MdpKmid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpKhuyenMai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpKhuyenMaiExists(mdpKhuyenMai.MdpKmid))
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
            return View(mdpKhuyenMai);
        }

        // GET: MdpKhuyenMais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpKhuyenMai = await _context.MdpKhuyenMais
                .FirstOrDefaultAsync(m => m.MdpKmid == id);
            if (mdpKhuyenMai == null)
            {
                return NotFound();
            }

            return View(mdpKhuyenMai);
        }

        // POST: MdpKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpKhuyenMai = await _context.MdpKhuyenMais.FindAsync(id);
            if (mdpKhuyenMai != null)
            {
                _context.MdpKhuyenMais.Remove(mdpKhuyenMai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpKhuyenMaiExists(int id)
        {
            return _context.MdpKhuyenMais.Any(e => e.MdpKmid == id);
        }
    }
}
