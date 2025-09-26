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
    public class MdpReviewsController : Controller
    {
        private readonly Project2Context _context;

        public MdpReviewsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpReviews
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpReviews.Include(m => m.MdpSanPham).Include(m => m.MdpUser);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpReview = await _context.MdpReviews
                .Include(m => m.MdpSanPham)
                .Include(m => m.MdpUser)
                .FirstOrDefaultAsync(m => m.MdpReviewId == id);
            if (mdpReview == null)
            {
                return NotFound();
            }

            return View(mdpReview);
        }

        // GET: MdpReviews/Create
        public IActionResult Create()
        {
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId");
            ViewData["MdpUserId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId");
            return View();
        }

        // POST: MdpReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpReviewId,MdpSanPhamId,MdpUserId,MdpRating,MdpComment,MdpCreatedAt")] MdpReview mdpReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpReview.MdpSanPhamId);
            ViewData["MdpUserId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpReview.MdpUserId);
            return View(mdpReview);
        }

        // GET: MdpReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpReview = await _context.MdpReviews.FindAsync(id);
            if (mdpReview == null)
            {
                return NotFound();
            }
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpReview.MdpSanPhamId);
            ViewData["MdpUserId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpReview.MdpUserId);
            return View(mdpReview);
        }

        // POST: MdpReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpReviewId,MdpSanPhamId,MdpUserId,MdpRating,MdpComment,MdpCreatedAt")] MdpReview mdpReview)
        {
            if (id != mdpReview.MdpReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpReviewExists(mdpReview.MdpReviewId))
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
            ViewData["MdpSanPhamId"] = new SelectList(_context.MdpSanPhams, "MdpSanPhamId", "MdpSanPhamId", mdpReview.MdpSanPhamId);
            ViewData["MdpUserId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpReview.MdpUserId);
            return View(mdpReview);
        }

        // GET: MdpReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpReview = await _context.MdpReviews
                .Include(m => m.MdpSanPham)
                .Include(m => m.MdpUser)
                .FirstOrDefaultAsync(m => m.MdpReviewId == id);
            if (mdpReview == null)
            {
                return NotFound();
            }

            return View(mdpReview);
        }

        // POST: MdpReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpReview = await _context.MdpReviews.FindAsync(id);
            if (mdpReview != null)
            {
                _context.MdpReviews.Remove(mdpReview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpReviewExists(int id)
        {
            return _context.MdpReviews.Any(e => e.MdpReviewId == id);
        }
    }
}
