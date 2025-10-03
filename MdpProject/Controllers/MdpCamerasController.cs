using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

namespace MdpProject.Controllers
{
    public class MdpCamerasController : Controller
    {
        private readonly Project2Context _context;

        public MdpCamerasController(Project2Context context)
        {
            _context = context;
        }

        // ================= Admin Index =================
        public async Task<IActionResult> MdpAdminIndex()
        {
            var list = await _context.MdpCameras
                                     .Include(c => c.MdpSanPham)
                                     .ToListAsync();
            return View(list);
        }

        // ================= Index (cho khách hàng) =================
        public async Task<IActionResult> Index()
        {
            var list = await _context.MdpCameras
                                     .Include(c => c.MdpSanPham)
                                     .ToListAsync();
            return View(list);
        }

        // ================= Details =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var camera = await _context.MdpCameras
                                       .Include(c => c.MdpSanPham)
                                       .FirstOrDefaultAsync(m => m.MdpCameraId == id);

            if (camera == null) return NotFound();

            return View(camera);
        }

        // ================= Create =================
        // ================= Create =================
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(
                _context.MdpSanPhams,
                "MdpSanPhamId",
                "MdpTenSanPham"
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdpCamera camera)
        {
            // bỏ validation cho navigation property
            ModelState.Remove("MdpSanPham");

            if (ModelState.IsValid)
            {
                camera.MdpCreatedAt = DateTime.Now;
                camera.MdpUpdatedAt = DateTime.Now;

                _context.Add(camera);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Thêm Camera thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }

            ViewData["MdpSanPhamId"] = new SelectList(
                _context.MdpSanPhams,
                "MdpSanPhamId",
                "MdpTenSanPham",
                camera.MdpSanPhamId
            );
            return View(camera);
        }


        // ================= Edit =================
        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MdpCamera mdpCamera)
        {
            if (id != mdpCamera.MdpCameraId) return NotFound();

            // Loại bỏ navigation property khỏi ModelState để không lỗi
            ModelState.Remove("MdpSanPham");

            if (!ModelState.IsValid)
            {
                ViewData["MdpSanPhamId"] = new SelectList(
                    _context.MdpSanPhams,
                    "MdpSanPhamId",
                    "MdpTenSanPham",
                    mdpCamera.MdpSanPhamId
                );
                return View(mdpCamera);
            }

            // Lấy entity gốc từ DB
            var camera = await _context.MdpCameras.FindAsync(id);
            if (camera == null) return NotFound();

            // Cập nhật từng field
            camera.MdpSanPhamId = mdpCamera.MdpSanPhamId;
            camera.MdpTenCamera = mdpCamera.MdpTenCamera?.Trim();
            camera.MdpDoPhanGiai = mdpCamera.MdpDoPhanGiai?.Trim();
            camera.MdpCamBien = mdpCamera.MdpCamBien?.Trim();
            camera.MdpOngKinh = mdpCamera.MdpOngKinh?.Trim();
            camera.MdpBoNho = mdpCamera.MdpBoNho?.Trim();
            camera.MdpAnh = string.IsNullOrWhiteSpace(mdpCamera.MdpAnh) ? camera.MdpAnh : mdpCamera.MdpAnh.Trim();
            camera.MdpUpdatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Cập nhật Camera thành công!";
                return RedirectToAction(nameof(MdpAdminIndex));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MdpCameras.Any(e => e.MdpCameraId == id))
                    return NotFound();
                else
                    throw;
            }
        }

        // ================= Delete =================
        // GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var camera = await _context.MdpCameras
                                       .Include(c => c.MdpSanPham)
                                       .FirstOrDefaultAsync(m => m.MdpCameraId == id);
            if (camera == null) return NotFound();

            return View(camera);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _context.MdpCameras.FindAsync(id);
            if (camera != null)
            {
                _context.MdpCameras.Remove(camera);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MdpAdminIndex));
        }

        // ================= Helper =================
        private bool MdpCameraExists(int id)
        {
            return _context.MdpCameras.Any(e => e.MdpCameraId == id);
        }
    }
}
