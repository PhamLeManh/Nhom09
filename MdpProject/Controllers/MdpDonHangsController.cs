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
    public class MdpDonHangsController : Controller
    {
        private readonly Project2Context _context;

        public MdpDonHangsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpDonHangs
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpDonHangs.Include(m => m.MdpKhachHang).Include(m => m.MdpKm);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpDonHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpDonHang = await _context.MdpDonHangs
                .Include(m => m.MdpKhachHang)
                .Include(m => m.MdpKm)
                .FirstOrDefaultAsync(m => m.MdpDonHangId == id);
            if (mdpDonHang == null)
            {
                return NotFound();
            }

            return View(mdpDonHang);
        }

        // GET: MdpDonHangs/Create
        public IActionResult Create()
        {
            ViewData["MdpKhachHangId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId");
            ViewData["MdpKmid"] = new SelectList(_context.MdpKhuyenMais, "MdpKmid", "MdpKmid");
            return View();
        }

        // POST: MdpDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpDonHangId,MdpKhachHangId,MdpNgayDatDonHang,MdpNgayGiaoHang,MdpDiaChiGiaoHang,MdpPhuongThucThanhToan,MdpTrangThaiThanhToan,MdpStatus,MdpTongTien,MdpKmid,MdpGhiChu")] MdpDonHang mdpDonHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpKhachHangId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpDonHang.MdpKhachHangId);
            ViewData["MdpKmid"] = new SelectList(_context.MdpKhuyenMais, "MdpKmid", "MdpKmid", mdpDonHang.MdpKmid);
            return View(mdpDonHang);
        }

        // GET: MdpDonHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpDonHang = await _context.MdpDonHangs.FindAsync(id);
            if (mdpDonHang == null)
            {
                return NotFound();
            }
            ViewData["MdpKhachHangId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpDonHang.MdpKhachHangId);
            ViewData["MdpKmid"] = new SelectList(_context.MdpKhuyenMais, "MdpKmid", "MdpKmid", mdpDonHang.MdpKmid);
            return View(mdpDonHang);
        }

        // POST: MdpDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpDonHangId,MdpKhachHangId,MdpNgayDatDonHang,MdpNgayGiaoHang,MdpDiaChiGiaoHang,MdpPhuongThucThanhToan,MdpTrangThaiThanhToan,MdpStatus,MdpTongTien,MdpKmid,MdpGhiChu")] MdpDonHang mdpDonHang)
        {
            if (id != mdpDonHang.MdpDonHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpDonHangExists(mdpDonHang.MdpDonHangId))
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
            ViewData["MdpKhachHangId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpDonHang.MdpKhachHangId);
            ViewData["MdpKmid"] = new SelectList(_context.MdpKhuyenMais, "MdpKmid", "MdpKmid", mdpDonHang.MdpKmid);
            return View(mdpDonHang);
        }

        // GET: MdpDonHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpDonHang = await _context.MdpDonHangs
                .Include(m => m.MdpKhachHang)
                .Include(m => m.MdpKm)
                .FirstOrDefaultAsync(m => m.MdpDonHangId == id);
            if (mdpDonHang == null)
            {
                return NotFound();
            }

            return View(mdpDonHang);
        }

        // POST: MdpDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpDonHang = await _context.MdpDonHangs.FindAsync(id);
            if (mdpDonHang != null)
            {
                _context.MdpDonHangs.Remove(mdpDonHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpDonHangExists(int id)
        {
            return _context.MdpDonHangs.Any(e => e.MdpDonHangId == id);
        }
    }
}
