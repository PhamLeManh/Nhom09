using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MdpProject.Controllers
{
    public class MdpDonHangsController : Controller
    {
        private readonly Project2Context _context;

        public MdpDonHangsController(Project2Context context)
        {
            _context = context;
        }

        // ================= Helper =================
        private bool IsAdminOrManager(MdpUser user)
        {
            return user.MdpRole == "admin" || user.MdpRole == "manager";
        }

        private async Task<MdpUser?> GetCurrentUserAsync()
        {
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username)) return null;
            return await _context.MdpUsers.FirstOrDefaultAsync(u => u.MdpTenDangNhap == username);
        }
        // ================= User Index =================
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return RedirectToAction("MdpLogin", "MdpUsers");

            var orders = await _context.MdpDonHangs
                .Where(d => d.MdpKhachHangId == user.MdpUserId)
                .Include(d => d.MdpKm)
                .Include(d => d.MdpChiTietDonHangs)
                    .ThenInclude(c => c.MdpVariant)
                        .ThenInclude(v => v.MdpSanPham)
                .OrderByDescending(d => d.MdpNgayDatDonHang)
                .ToListAsync();

            return View("Index", orders);
        }
        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return RedirectToAction("MdpLogin", "MdpUsers");
            if (!IsAdminOrManager(user)) return Forbid();

            var orders = await _context.MdpDonHangs
                .Include(d => d.MdpKhachHang)
                .Include(d => d.MdpKm)
                .Include(d => d.MdpChiTietDonHangs)
                    .ThenInclude(c => c.MdpVariant)
                        .ThenInclude(v => v.MdpSanPham)
                .OrderByDescending(d => d.MdpNgayDatDonHang)
                .ToListAsync();

            return View("MdpAdminIndex", orders);
        }

       

        // ================= Details =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.MdpDonHangs
                .Include(d => d.MdpKm)
                .Include(d => d.MdpKhachHang)
                .Include(d => d.MdpChiTietDonHangs)
                    .ThenInclude(c => c.MdpVariant)
                        .ThenInclude(v => v.MdpSanPham)
                .FirstOrDefaultAsync(d => d.MdpDonHangId == id);

            if (order == null) return NotFound();
            return View(order);
        }

        // ================= Edit GET =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mdpDonHang = await _context.MdpDonHangs
                .Include(d => d.MdpKhachHang)
                .Include(d => d.MdpKm)
                .FirstOrDefaultAsync(d => d.MdpDonHangId == id);

            if (mdpDonHang == null) return NotFound();

            ViewData["MdpKhachHangId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpHoTen", mdpDonHang.MdpKhachHangId);
            ViewData["MdpKmid"] = new SelectList(_context.MdpKhuyenMais, "MdpKmid", "MdpMaCode", mdpDonHang.MdpKmid);

            return View(mdpDonHang);
        }

        // ================= Edit POST =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int MdpKhachHangId, int? MdpKmid, string MdpDiaChiGiaoHang,
            string MdpPhuongThucThanhToan, string MdpTrangThaiThanhToan, string MdpStatus, decimal MdpTongTien, string? MdpGhiChu)
        {
            var mdpDonHang = await _context.MdpDonHangs.FindAsync(id);
            if (mdpDonHang == null) return NotFound();

            mdpDonHang.MdpKhachHangId = MdpKhachHangId;
            mdpDonHang.MdpKmid = MdpKmid;
            mdpDonHang.MdpDiaChiGiaoHang = MdpDiaChiGiaoHang;
            mdpDonHang.MdpPhuongThucThanhToan = MdpPhuongThucThanhToan;
            mdpDonHang.MdpTrangThaiThanhToan = MdpTrangThaiThanhToan;
            mdpDonHang.MdpStatus = MdpStatus;
            mdpDonHang.MdpTongTien = MdpTongTien;
            mdpDonHang.MdpGhiChu = MdpGhiChu;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cập nhật đơn hàng thành công!";
                return RedirectToAction("MdpAdminIndex");
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = "Lỗi khi cập nhật: " + ex.Message;
                return RedirectToAction("Edit", new { id });
            }
        }

        // ================= Delete GET =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var donHang = await _context.MdpDonHangs
                .Include(d => d.MdpKhachHang)
                .Include(d => d.MdpKm)
                .Include(d => d.MdpChiTietDonHangs)
                .FirstOrDefaultAsync(d => d.MdpDonHangId == id);

            if (donHang == null) return NotFound();

            return View(donHang);
        }

        // ================= Delete POST =================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.MdpDonHangs
                .Include(d => d.MdpChiTietDonHangs)
                .FirstOrDefaultAsync(d => d.MdpDonHangId == id);

            if (donHang != null)
            {
                // Xóa chi tiết đơn trước để tránh FK
                _context.MdpChiTietDonHangs.RemoveRange(donHang.MdpChiTietDonHangs);
                _context.MdpDonHangs.Remove(donHang);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = $"Đơn hàng #{id} đã được xóa thành công.";
            return RedirectToAction(nameof(MdpAdminIndex));
        }
    }
}
