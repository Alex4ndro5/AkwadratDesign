using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AkwadratDesign.Data;
using AkwadratDesign.Models.DbModels;

namespace AkwadratDesign.Controllers
{
    /// <summary>
    /// Kontroler obsługujący zarządzanie firmami.
    /// </summary>
    public class FirmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FirmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Firms
        /// <summary>
        /// Akcja wyświetlająca listę firm.
        /// </summary>
        /// <returns>Widok zawierający listę firm.</returns>
        public async Task<IActionResult> Index()
        {
              return _context.Firms != null ? 
                          View(await _context.Firms.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Firms'  is null.");
        }

        // GET: Firms/Details/5
        /// <summary>
        /// Akcja wyświetlająca szczegóły firmy o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator firmy.</param>
        /// <returns>Widok zawierający szczegóły firmy.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Firms == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms
                .FirstOrDefaultAsync(m => m.FirmId == id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // GET: Firms/Create
        /// <summary>
        /// Akcja wyświetlająca formularz do tworzenia nowej firmy.
        /// </summary>
        /// <returns>Widok zawierający formularz do tworzenia nowej firmy.</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Firms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Akcja obsługująca tworzenie nowej firmy.
        /// </summary>
        /// <param name="firm">Nowa firma do dodania.</param>
        /// <returns>Przekierowanie do widoku listy firm po pomyślnym dodaniu firmy.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirmId,FirmName")] Firm firm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(firm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(firm);
        }

        // GET: Firms/Edit/5
        /// <summary>
        /// Akcja wyświetlająca formularz do edycji danych firmy o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator firmy.</param>
        /// <returns>Widok zawierający formularz do edycji danych firmy.</returns>
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
            return View(firm);
        }

        // POST: Firms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Akcja obsługująca zapis edytowanych danych firmy.
        /// </summary>
        /// <param name="id">Identyfikator firmy.</param>
        /// <param name="firm">Edytowana firma.</param>
        /// <returns>Przekierowanie do widoku listy firm po pomyślnym zapisie zmian.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirmId,FirmName")] Firm firm)
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
            return View(firm);
        }

        // GET: Firms/Delete/5
        /// <summary>
        /// Akcja wyświetlająca formularz potwierdzenia usunięcia firmy o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator firmy.</param>
        /// <returns>Widok zawierający formularz potwierdzenia usunięcia firmy.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Firms == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms
                .FirstOrDefaultAsync(m => m.FirmId == id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // POST: Firms/Delete/5
        /// <summary>
        /// Akcja obsługująca usunięcie firmy o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator firmy.</param>
        /// <returns>Przekierowanie do widoku listy firm po pomyślnym usunięciu firmy.</returns>
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
