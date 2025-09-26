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
    public class MdpKhoHangsController : Controller
    {
        private readonly Project2Context _context;

        public MdpKhoHangsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpKhoHangs
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpKhoHangs.Include(m => m.MdpSanPham);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpKhoHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpKhoHang = await _context.MdpKhoHangs
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpKhoId == id);
            if (mdpKhoHang == null)
            {
                return NotFound();
            }

            return View(mdpKhoHang);
        }

        // GET: MdpKhoHangs/Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId");
            return View();
        }

        // POST: MdpKhoHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpKhoId,MdpSanPhamId,MdpSoLuongNhap,MdpSoLuongTon,MdpNgayNhap")] MdpKhoHang mdpKhoHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpKhoHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpKhoHang.MdpSanPhamId);
            return View(mdpKhoHang);
        }

        // GET: MdpKhoHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpKhoHang = await _context.MdpKhoHangs.FindAsync(id);
            if (mdpKhoHang == null)
            {
                return NotFound();
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpKhoHang.MdpSanPhamId);
            return View(mdpKhoHang);
        }

        // POST: MdpKhoHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpKhoId,MdpSanPhamId,MdpSoLuongNhap,MdpSoLuongTon,MdpNgayNhap")] MdpKhoHang mdpKhoHang)
        {
            if (id != mdpKhoHang.MdpKhoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpKhoHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpKhoHangExists(mdpKhoHang.MdpKhoId))
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
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpKhoHang.MdpSanPhamId);
            return View(mdpKhoHang);
        }

        // GET: MdpKhoHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpKhoHang = await _context.MdpKhoHangs
                .Include(m => m.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpKhoId == id);
            if (mdpKhoHang == null)
            {
                return NotFound();
            }

            return View(mdpKhoHang);
        }

        // POST: MdpKhoHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpKhoHang = await _context.MdpKhoHangs.FindAsync(id);
            if (mdpKhoHang != null)
            {
                _context.MdpKhoHangs.Remove(mdpKhoHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpKhoHangExists(int id)
        {
            return _context.MdpKhoHangs.Any(e => e.MdpKhoId == id);
        }
    }
}
