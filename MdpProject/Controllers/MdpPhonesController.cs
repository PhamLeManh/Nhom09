using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpPhonesController : Controller
    {
        private readonly Project2Context _context;

        public MdpPhonesController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpPhones (có bộ lọc)
        public async Task<IActionResult> Index(string ram, string storage, string chipset)
        {
            var query = _context.MdpPhones
                .Include(p => p.MdpSanPham)
                .AsQueryable();

            if (!string.IsNullOrEmpty(ram))
                query = query.Where(p => p.MdpRam == ram);

            if (!string.IsNullOrEmpty(storage))
                query = query.Where(p => p.MdpStorage == storage);

            if (!string.IsNullOrEmpty(chipset))
                query = query.Where(p => p.MdpChipset == chipset);

            return View(await query.ToListAsync());
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var phone = await _context.MdpPhones
                .Include(p => p.MdpSanPham)
                .FirstOrDefaultAsync(p => p.MdpPhoneId == id);

            if (phone == null) return NotFound();

            return View(phone);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpPhone phone)
        {
            if (ModelState.IsValid)
            {
                phone.MdpCreatedAt = DateTime.Now;
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var phone = await _context.MdpPhones.FindAsync(id);
            if (phone == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", phone.MdpSanPhamId);
            return View(phone);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpPhone phone)
        {
            if (id != phone.MdpPhoneId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    phone.MdpUpdatedAt = DateTime.Now;
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpPhones.Any(e => e.MdpPhoneId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var phone = await _context.MdpPhones
                .Include(p => p.MdpSanPham)
                .FirstOrDefaultAsync(m => m.MdpPhoneId == id);

            if (phone == null) return NotFound();

            return View(phone);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _context.MdpPhones.FindAsync(id);
            if (phone != null)
            {
                _context.MdpPhones.Remove(phone);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
