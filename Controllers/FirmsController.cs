using AkwadratDesign.Data;
using AkwadratDesign.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AkwadratDesign.Controllers
{
    public class FirmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FirmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Firms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Firms.Include(f => f.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Firms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Firms == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.FirmId == id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // GET: Firms/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email");
            return View();
        }

        // POST: Firms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirmId,FirmName,ClientId")] Firm firm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(firm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email", firm.ClientId);
            return View(firm);
        }

        // GET: Firms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Firms == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms.FindAsync(id);
            if (firm == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email", firm.ClientId);
            return View(firm);
        }

        // POST: Firms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirmId,FirmName,ClientId")] Firm firm)
        {
            if (id != firm.FirmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirmExists(firm.FirmId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email", firm.ClientId);
            return View(firm);
        }

        // GET: Firms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Firms == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.FirmId == id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // POST: Firms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Firms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Firms'  is null.");
            }
            var firm = await _context.Firms.FindAsync(id);
            if (firm != null)
            {
                _context.Firms.Remove(firm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FirmExists(int id)
        {
            return (_context.Firms?.Any(e => e.FirmId == id)).GetValueOrDefault();
        }
    }
}
