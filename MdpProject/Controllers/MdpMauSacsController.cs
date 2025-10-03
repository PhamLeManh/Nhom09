using MdpProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MdpMauSacsController : Controller
{
    private readonly Project2Context _context;

    public MdpMauSacsController(Project2Context context)
    {
        _context = context;
    }
    private bool MdpMauSacExists(int id)
    {
        return _context.MdpMauSacs.Any(e => e.MdpMauSacId == id);
    }

    // GET: Index
    public async Task<IActionResult> Index()
    {
        return View(await _context.MdpMauSacs.ToListAsync());
    }

    // GET: Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MdpMauSac mdpMauSac)
    {
        if (ModelState.IsValid)
        {
            _context.Add(mdpMauSac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(mdpMauSac);
    }

    // GET: Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var mdpMauSac = await _context.MdpMauSacs.FindAsync(id);
        if (mdpMauSac == null) return NotFound();

        return View(mdpMauSac);
    }

    // POST: Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MdpMauSac mdpMauSac)
    {
        if (id != mdpMauSac.MdpMauSacId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(mdpMauSac);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MdpMauSacExists(mdpMauSac.MdpMauSacId))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(mdpMauSac);
    }

    // GET: MdpMauSacs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var mdpMauSac = await _context.MdpMauSacs.FirstOrDefaultAsync(m => m.MdpMauSacId == id);
        if (mdpMauSac == null) return NotFound();

        return View(mdpMauSac);
    }

    // POST: MdpMauSacs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var mdpMauSac = await _context.MdpMauSacs.FindAsync(id);
        if (mdpMauSac != null)
        {
            _context.MdpMauSacs.Remove(mdpMauSac);
            await _context.SaveChangesAsync();
            TempData["Success"] = "✅ Xóa màu sắc thành công!";
        }
        else
        {
            TempData["Error"] = "⚠ Màu sắc không tồn tại!";
        }
        return RedirectToAction(nameof(Index));
    }

}

