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
    public class MdpHoaDonsController : Controller
    {
        private readonly Project2Context _context;

        public MdpHoaDonsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpHoaDons
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpHoaDons.Include(m => m.MdpDonHang);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpHoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpHoaDon = await _context.MdpHoaDons
                .Include(m => m.MdpDonHang)
                .FirstOrDefaultAsync(m => m.MdpHoaDonId == id);
            if (mdpHoaDon == null)
            {
                return NotFound();
            }

            return View(mdpHoaDon);
        }

      

        // GET: MdpHoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpHoaDon = await _context.MdpHoaDons
                .Include(m => m.MdpDonHang)
                .FirstOrDefaultAsync(m => m.MdpHoaDonId == id);
            if (mdpHoaDon == null)
            {
                return NotFound();
            }

            return View(mdpHoaDon);
        }

        // POST: MdpHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpHoaDon = await _context.MdpHoaDons.FindAsync(id);
            if (mdpHoaDon != null)
            {
                _context.MdpHoaDons.Remove(mdpHoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpHoaDonExists(int id)
        {
            return _context.MdpHoaDons.Any(e => e.MdpHoaDonId == id);
        }
    }
}
