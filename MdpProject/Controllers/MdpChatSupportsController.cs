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
    public class MdpChatSupportsController : Controller
    {
        private readonly Project2Context _context;

        public MdpChatSupportsController(Project2Context context)
        {
            _context = context;
        }

        // GET: MdpChatSupports
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.MdpChatSupports.Include(m => m.MdpDonHang).Include(m => m.MdpSender);
            return View(await project2Context.ToListAsync());
        }

        // GET: MdpChatSupports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpChatSupport = await _context.MdpChatSupports
                .Include(m => m.MdpDonHang)
                .Include(m => m.MdpSender)
                .FirstOrDefaultAsync(m => m.MdpChatId == id);
            if (mdpChatSupport == null)
            {
                return NotFound();
            }

            return View(mdpChatSupport);
        }

        // GET: MdpChatSupports/Create
        public IActionResult Create()
        {
            ViewData["MdpDonHangId"] = new SelectList(_context.MdpDonHangs, "MdpDonHangId", "MdpDonHangId");
            ViewData["MdpSenderId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId");
            return View();
        }

        // POST: MdpChatSupports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdpChatId,MdpDonHangId,MdpSenderId,MdpTinNhan,MdpSentAt,MdpFileDinhKem,MdpDaXem")] MdpChatSupport mdpChatSupport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mdpChatSupport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MdpDonHangId"] = new SelectList(_context.MdpDonHangs, "MdpDonHangId", "MdpDonHangId", mdpChatSupport.MdpDonHangId);
            ViewData["MdpSenderId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpChatSupport.MdpSenderId);
            return View(mdpChatSupport);
        }

        // GET: MdpChatSupports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpChatSupport = await _context.MdpChatSupports.FindAsync(id);
            if (mdpChatSupport == null)
            {
                return NotFound();
            }
            ViewData["MdpDonHangId"] = new SelectList(_context.MdpDonHangs, "MdpDonHangId", "MdpDonHangId", mdpChatSupport.MdpDonHangId);
            ViewData["MdpSenderId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpChatSupport.MdpSenderId);
            return View(mdpChatSupport);
        }

        // POST: MdpChatSupports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MdpChatId,MdpDonHangId,MdpSenderId,MdpTinNhan,MdpSentAt,MdpFileDinhKem,MdpDaXem")] MdpChatSupport mdpChatSupport)
        {
            if (id != mdpChatSupport.MdpChatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mdpChatSupport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MdpChatSupportExists(mdpChatSupport.MdpChatId))
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
            ViewData["MdpDonHangId"] = new SelectList(_context.MdpDonHangs, "MdpDonHangId", "MdpDonHangId", mdpChatSupport.MdpDonHangId);
            ViewData["MdpSenderId"] = new SelectList(_context.MdpUsers, "MdpUserId", "MdpUserId", mdpChatSupport.MdpSenderId);
            return View(mdpChatSupport);
        }

        // GET: MdpChatSupports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mdpChatSupport = await _context.MdpChatSupports
                .Include(m => m.MdpDonHang)
                .Include(m => m.MdpSender)
                .FirstOrDefaultAsync(m => m.MdpChatId == id);
            if (mdpChatSupport == null)
            {
                return NotFound();
            }

            return View(mdpChatSupport);
        }

        // POST: MdpChatSupports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mdpChatSupport = await _context.MdpChatSupports.FindAsync(id);
            if (mdpChatSupport != null)
            {
                _context.MdpChatSupports.Remove(mdpChatSupport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MdpChatSupportExists(int id)
        {
            return _context.MdpChatSupports.Any(e => e.MdpChatId == id);
        }
    }
}
