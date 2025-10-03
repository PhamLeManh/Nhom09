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
            var kho = _context.MdpKhoHangs.Include(k => k.MdpSanPham);
            return View(await kho.ToListAsync());
        }

        // GET: MdpKhoHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var kho = await _context.MdpKhoHangs
                .Include(k => k.MdpSanPham)
                .FirstOrDefaultAsync(k => k.MdpKhoId == id);

            if (kho == null) return NotFound();
            return View(kho);
        }

        // GET: KhoHang/Create
        public IActionResult Create()
        {
            ViewBag.MdpSanPhamId = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpKhoHang khoHang)
        {
            // Bỏ validation cho navigation property
            ModelState.Remove("MdpSanPham");

            Console.WriteLine($"SanPhamId = {khoHang.MdpSanPhamId}, Nhap = {khoHang.MdpSoLuongNhap}, Ton = {khoHang.MdpSoLuongTon}");

            if (ModelState.IsValid)
            {
                khoHang.MdpNgayNhap = DateTime.Now;
                _context.Add(khoHang);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Thêm kho hàng thành công!";
                return RedirectToAction(nameof(Index));
            }

            // Debug lỗi
            foreach (var item in ModelState)
            {
                foreach (var err in item.Value.Errors)
                {
                    Console.WriteLine($"Lỗi: {item.Key} - {err.ErrorMessage}");
                }
            }

            ViewBag.MdpSanPhamId = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", khoHang.MdpSanPhamId);
            return View(khoHang);
        }





        // GET: MdpKhoHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var kho = await _context.MdpKhoHangs.FindAsync(id);
            if (kho == null) return NotFound();

            ViewBag.MdpSanPhamId = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", kho.MdpSanPhamId);
            return View(kho);
        }

        // POST: MdpKhoHangs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpKhoHang khoHang)
        {
            // Xóa validation cho navigation property
            ModelState.Remove("MdpSanPham");

            if (id != khoHang.MdpKhoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Nếu nhập số âm thì ép về 0
                    if (khoHang.MdpSoLuongNhap < 0) khoHang.MdpSoLuongNhap = 0;
                    if (khoHang.MdpSoLuongTon < 0) khoHang.MdpSoLuongTon = 0;

                    // Gán lại ngày cập nhật
                    khoHang.MdpNgayNhap = khoHang.MdpNgayNhap == default ? DateTime.Now : khoHang.MdpNgayNhap;

                    _context.Update(khoHang);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "✅ Cập nhật kho hàng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpKhoHangExists(khoHang.MdpKhoId)) return NotFound();
                    else throw;
                }
            }

            // Debug lỗi validation nếu có
            foreach (var item in ModelState)
            {
                foreach (var err in item.Value.Errors)
                {
                    Console.WriteLine($"Lỗi: {item.Key} - {err.ErrorMessage}");
                }
            }

            ViewBag.MdpSanPhamId = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpTenSanPham", khoHang.MdpSanPhamId);
            return View(khoHang);
        }

        // GET: MdpKhoHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var kho = await _context.MdpKhoHangs
                .Include(k => k.MdpSanPham)
                .FirstOrDefaultAsync(k => k.MdpKhoId == id);

            if (kho == null) return NotFound();

            return View(kho);
        }

        // POST: MdpKhoHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var kho = await _context.MdpKhoHangs.FindAsync(id);
                if (kho == null)
                {
                    TempData["ErrorMessage"] = "❌ Không tìm thấy kho để xóa!";
                    return RedirectToAction(nameof(Index));
                }

                _context.MdpKhoHangs.Remove(kho);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "✅ Xóa kho hàng thành công!";
            }
            catch (DbUpdateException ex)
            {
                // Bắt lỗi khóa ngoại (nếu kho đang được tham chiếu bởi bảng khác)
                TempData["ErrorMessage"] = "❌ Không thể xóa kho vì đang được tham chiếu ở bảng khác!";
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MdpKhoHangExists(int id)
        {
            return _context.MdpKhoHangs.Any(e => e.MdpKhoId == id);
        }

    }
}
