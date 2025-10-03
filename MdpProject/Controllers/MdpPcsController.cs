using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MdpProject.Controllers
{
    public class MdpPcsController : Controller
    {
        private readonly Project2Context _context;

        public MdpPcsController(Project2Context context)
        {
            _context = context;
        }

        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var pcs = await _context.MdpPcs
                                    .Include(p => p.MdpSanPham)
                                    .ToListAsync();

            return View(pcs);
        }
        // ================= Index (Client) =================
        public async Task<IActionResult> Index()
        {
            var laptops = _context.MdpPcs.Include(l => l.MdpSanPham);
            return View(await laptops.ToListAsync());
        }


        // ================= Details =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pc = await _context.MdpPcs
                                   .Include(p => p.MdpSanPham) // lấy luôn thông tin danh mục
                                   .FirstOrDefaultAsync(p => p.MdpPcid == id);

            if (pc == null) return NotFound();

            return View(pc); // trả về View Details.cshtml
        }


        // ================= Create =================
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpSanPhamId,MdpTenPc,MdpCpu,MdpGpu,MdpRam,MdpStorage,MdpMainboard,MdpPsu,MdpCaseType")] MdpPc pc, string? imageUrl)
        {
            ModelState.Remove("MdpSanPham"); // Bỏ validate navigation property

            if (ModelState.IsValid)
            {
                pc.MdpCreatedAt = DateTime.Now;
                if (!string.IsNullOrEmpty(imageUrl))
                    pc.MdpAnh = imageUrl;

                _context.Add(pc);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Thêm PC thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", pc.MdpSanPhamId);
            return View(pc);
        }






        // ================= Edit =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pc = await _context.MdpPcs.FindAsync(id);
            if (pc == null) return NotFound();

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", pc.MdpSanPhamId);
            return View(pc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpPc pc, string? imageUrl)
        {
            ModelState.Remove("MdpSanPham"); // tránh lỗi dynamic

            if (id != pc.MdpPcid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    pc.MdpUpdatedAt = DateTime.Now;
                    if (!string.IsNullOrEmpty(imageUrl))
                        pc.MdpAnh = imageUrl;

                    _context.Entry(pc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "✅ Cập nhật PC thành công!";
                    return RedirectToAction(nameof(MdpAdminIndex));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MdpPcs.Any(e => e.MdpPcid == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", pc.MdpSanPhamId);
            return View(pc);
        }

        // ================= Delete =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pc = await _context.MdpPcs
                                   .Include(p => p.MdpSanPham)
                                   .FirstOrDefaultAsync(m => m.MdpPcid == id);

            if (pc == null) return NotFound();

            return View(pc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pc = await _context.MdpPcs.FindAsync(id);
            if (pc != null)
            {
                _context.MdpPcs.Remove(pc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Xóa PC thành công!";
            }
            else
            {
                TempData["Error"] = "❌ PC không tồn tại!";
            }
            return RedirectToAction(nameof(MdpAdminIndex));
        }
    }
}
