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
    public class MdpCategoriesController : Controller
    {
        private readonly Project2Context _context;

        public MdpCategoriesController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.MdpCategories.ToListAsync());
        }

        // GET: MdpCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpCategory = await _context.MdpCategories
                .FirstOrDefaultAsync(m => m.MdpCategoryId == id);
            if (mdpCategory == null)
            {
                return NotFound();
            }

            return View(mdpCategory);
        }

        // GET: MdpCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MdpCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpCategoryId,MdpTenDanhMuc,MdpMoTa,MdpAnhDanhMuc,MdpCreatedAt")] MdpCategory mdpCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mdpCategory);
        }

        // GET: MdpCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpCategory = await _context.MdpCategories.FindAsync(id);
            if (mdpCategory == null)
            {
                return NotFound();
            }
            return View(mdpCategory);
        }

        // POST: MdpCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpCategoryId,MdpTenDanhMuc,MdpMoTa,MdpAnhDanhMuc,MdpCreatedAt")] MdpCategory mdpCategory)
        {
            if (id != mdpCategory.MdpCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpCategoryExists(mdpCategory.MdpCategoryId))
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
            return View(mdpCategory);
        }

        // GET: MdpCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpCategory = await _context.MdpCategories
                .FirstOrDefaultAsync(m => m.MdpCategoryId == id);
            if (mdpCategory == null)
            {
                return NotFound();
            }

            return View(mdpCategory);
        }

        // POST: MdpCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpCategory = await _context.MdpCategories.FindAsync(id);
            if (mdpCategory != null)
            {
                _context.MdpCategories.Remove(mdpCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpCategoryExists(int id)
        {
            return _context.MdpCategories.Any(e => e.MdpCategoryId == id);
        }
    }
}
