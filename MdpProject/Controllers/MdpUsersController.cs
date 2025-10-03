using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MdpProject.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace project2.Controllers
{
    public class MdpUsersController : Controller
    {
        private readonly Project2Context _context;
        private readonly IWebHostEnvironment _env;

        public MdpUsersController(Project2Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ===================== ĐĂNG KÝ =====================
        [HttpGet]
        public IActionResult MdpRegister() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdpRegister(MdpUser user)
        {
            if (ModelState.IsValid)
            {
                if (_context.MdpUsers.Any(u => u.MdpTenDangNhap == user.MdpTenDangNhap))
                {
                    ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                    return View(user);
                }

                user.MdpRole = "User"; // Mặc định khách hàng
                user.MdpCreatedAt = DateTime.Now;

                _context.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "✅ Đăng ký thành công! Hãy đăng nhập.";
                return RedirectToAction("MdpLogin");
            }
            return View(user);
        }

        // ===================== ĐĂNG NHẬP =====================
        [HttpGet]
        public IActionResult MdpLogin() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdpLogin(string username, string password)
        {
            var user = await _context.MdpUsers
                .FirstOrDefaultAsync(u => u.MdpTenDangNhap == username && u.MdpMatKhau == password);

            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View();
            }

            // Lưu session
            HttpContext.Session.SetInt32("userid", user.MdpUserId);
            HttpContext.Session.SetString("username", user.MdpTenDangNhap);
            HttpContext.Session.SetString("avatar", user.MdpAvatar ?? "");
            HttpContext.Session.SetString("role", user.MdpRole ?? "User");

            // Admin/Manager → về MdpAdmin
            if (user.MdpRole == "admin" || user.MdpRole == "manager")
            {
                return View("~/Views/Shared/MdpAdmin.cshtml");
            }

            // Khách hàng → về trang chủ
            return RedirectToAction("Index", "MDPHome");
        }

        // ===================== ĐĂNG XUẤT =====================
        public IActionResult MdpLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "MDPHome");
        }

        // ===================== XEM HỒ SƠ =====================
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId == null) return RedirectToAction("MdpLogin");

            var user = await _context.MdpUsers.FindAsync(userId);
            if (user == null) return NotFound();

            ViewBag.GioiTinhList = new SelectList(new[]
            {
                new { Value = "", Text = "-- Chọn giới tính --" },
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" },
                new { Value = "Khác", Text = "Khác" }
            }, "Value", "Text", user.MdpGioiTinh);

            return View(user);
        }

        // ===================== CHỈNH SỬA HỒ SƠ =====================
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            var userId = HttpContext.Session.GetInt32("userid");
            if (role == null || userId == null) return RedirectToAction("MdpLogin");

            MdpUser user;
            if (role == "admin" || role == "manager")
            {
                if (id == null) return BadRequest();
                user = await _context.MdpUsers.FindAsync(id.Value);
                if (user == null) return NotFound();
            }
            else
            {
                user = await _context.MdpUsers.FindAsync(userId.Value);
                if (user == null) return NotFound();
            }

            ViewBag.GioiTinhList = new SelectList(new[]
            {
                new { Value = "", Text = "-- Chọn giới tính --" },
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" },
                new { Value = "Khác", Text = "Khác" }
            }, "Value", "Text", user.MdpGioiTinh);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MdpUser model, IFormFile? avatarFile)
        {
            var role = HttpContext.Session.GetString("role");
            var userId = HttpContext.Session.GetInt32("userid");
            if (role == null || userId == null) return RedirectToAction("MdpLogin");

            MdpUser user;
            if (role == "admin" || role == "manager")
            {
                user = await _context.MdpUsers.FindAsync(model.MdpUserId);
                if (user == null) return NotFound();
            }
            else
            {
                if (userId.Value != model.MdpUserId) return Unauthorized();
                user = await _context.MdpUsers.FindAsync(userId.Value);
                if (user == null) return NotFound();
            }

            // Cập nhật thông tin
            user.MdpHoTen = model.MdpHoTen;
            user.MdpEmail = model.MdpEmail;
            user.MdpSoDienThoai = model.MdpSoDienThoai;
            user.MdpDiaChi = model.MdpDiaChi;
            user.MdpGioiTinh = model.MdpGioiTinh;
            user.MdpNgaySinh = model.MdpNgaySinh;

            if (role == "admin" || role == "manager")
            {
                user.MdpRole = model.MdpRole; // admin/manager mới được chỉnh role
            }

            // Avatar là URL hoặc file
            if (avatarFile != null && avatarFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(avatarFile.FileName)}";
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads/avatars");
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await avatarFile.CopyToAsync(stream);

                user.MdpAvatar = "/uploads/avatars/" + fileName;
            }
            else
            {
                // nếu user nhập URL trong model, lưu URL
                user.MdpAvatar = string.IsNullOrEmpty(model.MdpAvatar) ? user.MdpAvatar : model.MdpAvatar;
            }

            user.MdpUpdatedAt = DateTime.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();

            if (role == "User")
                HttpContext.Session.SetString("avatar", user.MdpAvatar ?? "");

            TempData["SuccessMessage"] = "✅ Thông tin đã được cập nhật!";
            return role == "admin" || role == "manager"
                ? RedirectToAction("MdpAdminIndex")
                : RedirectToAction("Index");
        }

        // ===================== QUẢN LÝ TOÀN BỘ USER (ADMIN/MANAGER) =====================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "admin" && role != "manager")
                return Unauthorized();

            var users = await _context.MdpUsers.ToListAsync();
            return View(users);
        }

        // ===================== THÊM USER =====================
        [HttpGet]
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "admin" && role != "manager") return Unauthorized();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpUser model)
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "admin" && role != "manager") return Unauthorized();

            if (_context.MdpUsers.Any(u => u.MdpTenDangNhap == model.MdpTenDangNhap))
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View(model);
            }

            model.MdpCreatedAt = DateTime.Now;
            model.MdpRole = model.MdpRole ?? "User";
            model.MdpAvatar = string.IsNullOrEmpty(model.MdpAvatar) ? null : model.MdpAvatar;

            _context.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ User mới đã được thêm thành công!";
            return RedirectToAction("MdpAdminIndex");
        }

        // ===================== XÓA USER =====================
        public async Task<IActionResult> Delete(int id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "admin" && role != "manager") return Unauthorized();

            var user = await _context.MdpUsers.FindAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "⚠️ User không tồn tại!";
                return RedirectToAction("MdpAdminIndex");
            }

            _context.MdpUsers.Remove(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ User đã bị xóa thành công!";
            return RedirectToAction("MdpAdminIndex");
        }
    }
}
