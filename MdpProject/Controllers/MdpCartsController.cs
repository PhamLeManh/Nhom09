using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;
using System;
using System.Linq;

namespace MdpProject.Controllers
{
    public class MdpCartsController : Controller
    {
        private readonly Project2Context _context;

        public MdpCartsController(Project2Context context)
        {
            _context = context;
        }
        // ============ INDEX (Quản lý giỏ hàng) ============
        public async Task<IActionResult> MdpAdminIndex()
        {
            var carts = await _context.MdpCarts
                .Include(c => c.MdpUser)
                .Include(c => c.MdpVariant).ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpVariant).ThenInclude(v => v.MdpMauSac)
                .ToListAsync();

            return View(carts);
        }
        // ================= Thêm sản phẩm vào giỏ =================
        [HttpPost]
        public IActionResult AddToCart(int variantId)
        {
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId == null)
            {
                TempData["LoginRequiredMessage"] = "Vui lòng đăng nhập để mua sản phẩm!";
                return RedirectToAction("MdpLogin", "MdpUsers");
            }

            AddOrUpdateCart(userId.Value, variantId);
            TempData["SuccessMessage"] = "Sản phẩm đã được thêm vào giỏ hàng!";
            return RedirectToAction("Index");
        }

        // ================= Xem giỏ hàng =================
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId == null)
            {
                TempData["LoginRequiredMessage"] = "Vui lòng đăng nhập để xem giỏ hàng!";
                return RedirectToAction("MdpLogin", "MdpUsers");
            }

            var cartItems = _context.MdpCarts
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpMauSac)
                .Where(c => c.MdpUserId == userId)
                .ToList();

            // Hiển thị đơn hàng vừa đặt (nếu có)
            if (TempData["LastOrderId"] != null)
            {
                int orderId = (int)TempData["LastOrderId"];
                ViewBag.LastOrder = _context.MdpChiTietDonHangs
                    .Include(d => d.MdpVariant)
                        .ThenInclude(v => v.MdpSanPham)
                    .Include(d => d.MdpVariant)
                        .ThenInclude(v => v.MdpMauSac)
                    .Where(d => d.MdpDonHangId == orderId)
                    .Select(d => new
                    {
                        ProductName = d.MdpVariant.MdpSanPham.MdpTenSanPham,
                        Variant = (d.MdpVariant.MdpMauSac != null ? d.MdpVariant.MdpMauSac.MdpTenMau : "")
                                  + " - " + d.MdpVariant.MdpDungLuong,
                        Quantity = d.MdpSoLuong,
                        Price = d.MdpGia
                    })
                    .ToList();
            }

            return View(cartItems);
        }

        // ================= Trang Checkout =================
        public IActionResult MdpCheckout()
        {
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId == null) return RedirectToAction("MdpLogin", "MdpUsers");

            var cartItems = _context.MdpCarts
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpMauSac)
                .Where(c => c.MdpUserId == userId)
                .ToList();

            var user = _context.MdpUsers.FirstOrDefault(u => u.MdpUserId == userId);
            ViewBag.User = user;

            return View(cartItems);
        }

        // ================= Xử lý Checkout =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MdpCheckout(IFormCollection form)
        {
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId == null) return RedirectToAction("MdpLogin", "MdpUsers");

            var cartItems = _context.MdpCarts
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpSanPham)
                .Include(c => c.MdpVariant)
                    .ThenInclude(v => v.MdpMauSac)
                .Where(c => c.MdpUserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng trống!";
                return RedirectToAction("Index");
            }

            // Cập nhật thông tin user
            var user = _context.MdpUsers.FirstOrDefault(u => u.MdpUserId == userId);
            if (user != null)
            {
                user.MdpHoTen = form["fullname"];
                user.MdpEmail = form["email"];
                user.MdpDiaChi = form["address"];
                user.MdpSoDienThoai = form["phone"];
            }

            // Cập nhật số lượng trong giỏ
            foreach (var item in cartItems)
            {
                if (form[$"qty_{item.MdpCartId}"].FirstOrDefault() is string qtyStr &&
                    int.TryParse(qtyStr, out int qty))
                {
                    item.MdpSoLuong = qty;
                }
                item.MdpNgayThem = DateTime.Now;
            }

            _context.SaveChanges();

            // ================= Tạo đơn hàng mới =================
            var order = new MdpDonHang
            {
                MdpKhachHangId = userId.Value,
                MdpDiaChiGiaoHang = user?.MdpDiaChi,
                MdpNgayDatDonHang = DateTime.Now,
                MdpPhuongThucThanhToan = form["paymentMethod"].FirstOrDefault() ?? "COD",
                MdpTrangThaiThanhToan = "unpaid",
                MdpStatus = "pending",
                MdpTongTien = cartItems.Sum(c => (c.MdpSoLuong ?? 1) * (c.MdpVariant.MdpGia ?? 0m)),
                MdpGhiChu = form["note"].FirstOrDefault()
            };
            _context.MdpDonHangs.Add(order);
            _context.SaveChanges();

            // ================= Thêm chi tiết đơn hàng =================
            foreach (var item in cartItems)
            {
                _context.MdpChiTietDonHangs.Add(new MdpChiTietDonHang
                {
                    MdpDonHangId = order.MdpDonHangId,
                    MdpVariantId = item.MdpVariantId,
                    MdpSoLuong = item.MdpSoLuong ?? 1,
                    MdpGia = item.MdpVariant.MdpGia ?? 0m
                });
            }

            // 👉 Không xóa giỏ hàng, giữ nguyên
            _context.SaveChanges();

            TempData["LastOrderId"] = order.MdpDonHangId;
            TempData["SuccessMessage"] = "Đơn hàng đã được đặt thành công!";

            return RedirectToAction("Index");
        }

        // ================= Xóa sản phẩm trong giỏ =================
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId == null) return RedirectToAction("MdpLogin", "MdpUsers");

            var item = _context.MdpCarts.FirstOrDefault(c => c.MdpCartId == id && c.MdpUserId == userId);
            if (item != null)
            {
                _context.MdpCarts.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ================= Helper =================
        private void AddOrUpdateCart(int userId, int variantId)
        {
            var cartItem = _context.MdpCarts
                .FirstOrDefault(c => c.MdpUserId == userId && c.MdpVariantId == variantId);

            if (cartItem != null)
            {
                cartItem.MdpSoLuong = (cartItem.MdpSoLuong ?? 0) + 1;
                cartItem.MdpNgayThem = DateTime.Now;
            }
            else
            {
                _context.MdpCarts.Add(new MdpCart
                {
                    MdpUserId = userId,
                    MdpVariantId = variantId,
                    MdpSoLuong = 1,
                    MdpNgayThem = DateTime.Now
                });
            }

            _context.SaveChanges();
        }
    }
}
