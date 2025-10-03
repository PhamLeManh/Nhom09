using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpChiTietHoaDonsController : Controller
    {
        private readonly Project2Context _context;

        public MdpChiTietHoaDonsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpChiTietHoaDons
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpChiTietHoaDons
                .Include(m => m.MdpHoaDon)
                .Include(m => m.MdpVariant);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpChiTietHoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var mdpChiTietHoaDon = await _context.MdpChiTietHoaDons
                .Include(m => m.MdpHoaDon)
                .Include(m => m.MdpVariant)
                .FirstOrDefaultAsync(m => m.MdpChiTietHdid == id);

            if (mdpChiTietHoaDon == null) return NotFound();

            return View(mdpChiTietHoaDon);
        }

        // GET: MdpChiTietHoaDons/Create
        public IActionResult Create()
        {
            ViewData["MdpHoaDonId"] = new SelectList(_context.MdpHoaDons, "MdpHoaDonId", "MdpHoaDonId");
            ViewData["MdpVariantId"] = new SelectList(_context.MdpProductVariants, "MdpVariantId", "MdpVariantId");
            return View();
        }

        // POST: MdpChiTietHoaDons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpChiTietHdid,MdpHoaDonId,MdpVariantId,MdpSoLuong,MdpGia,MdpGiamGia,MdpThanhTien")] MdpChiTietHoaDon mdpChiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpChiTietHoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpHoaDonId"] = new SelectList(_context.MdpHoaDons, "MdpHoaDonId", "MdpHoaDonId", mdpChiTietHoaDon.MdpHoaDonId);
            ViewData["MdpVariantId"] = new SelectList(_context.MdpProductVariants, "MdpVariantId", "MdpVariantId", mdpChiTietHoaDon.MdpVariantId);
            return View(mdpChiTietHoaDon);
        }

        // GET: MdpChiTietHoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mdpChiTietHoaDon = await _context.MdpChiTietHoaDons.FindAsync(id);
            if (mdpChiTietHoaDon == null) return NotFound();

            ViewData["MdpHoaDonId"] = new SelectList(_context.MdpHoaDons, "MdpHoaDonId", "MdpHoaDonId", mdpChiTietHoaDon.MdpHoaDonId);
            ViewData["MdpVariantId"] = new SelectList(_context.MdpProductVariants, "MdpVariantId", "MdpVariantId", mdpChiTietHoaDon.MdpVariantId);
            return View(mdpChiTietHoaDon);
        }

        // POST: MdpChiTietHoaDons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpChiTietHdid,MdpHoaDonId,MdpVariantId,MdpSoLuong,MdpGia,MdpGiamGia,MdpThanhTien")] MdpChiTietHoaDon mdpChiTietHoaDon)
        {
            if (id != mdpChiTietHoaDon.MdpChiTietHdid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpChiTietHoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpChiTietHoaDonExists(mdpChiTietHoaDon.MdpChiTietHdid))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MdpHoaDonId"] = new SelectList(_context.MdpHoaDons, "MdpHoaDonId", "MdpHoaDonId", mdpChiTietHoaDon.MdpHoaDonId);
            ViewData["MdpVariantId"] = new SelectList(_context.MdpProductVariants, "MdpVariantId", "MdpVariantId", mdpChiTietHoaDon.MdpVariantId);
            return View(mdpChiTietHoaDon);
        }

        // GET: MdpChiTietHoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mdpChiTietHoaDon = await _context.MdpChiTietHoaDons
                .Include(m => m.MdpHoaDon)
                .Include(m => m.MdpVariant)
                .FirstOrDefaultAsync(m => m.MdpChiTietHdid == id);

            if (mdpChiTietHoaDon == null) return NotFound();

            return View(mdpChiTietHoaDon);
        }

        // POST: MdpChiTietHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpChiTietHoaDon = await _context.MdpChiTietHoaDons.FindAsync(id);
            if (mdpChiTietHoaDon != null)
            {
                _context.MdpChiTietHoaDons.Remove(mdpChiTietHoaDon);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MdpChiTietHoaDonExists(int id)
        {
            return _context.MdpChiTietHoaDons.Any(e => e.MdpChiTietHdid == id);
        }
    }
}
