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
    /// Kontroler obsługujący operacje na danych dotyczących firm projektowych.
    /// </summary>
    public class ProjectFirmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectFirmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectFirms
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /ProjectFirms/Index.
        /// </summary>
        /// <returns>Zwraca widok z listą firm projektowych.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectFirms.Include(p => p.Firm).Include(p => p.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectFirms/Details/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /ProjectFirms/Details/5.
        /// </summary>
        /// <param name="id">Identyfikator firmy projektowej.</param>
        /// <returns>Zwraca widok z informacjami szczegółowymi o firmie projektowej o podanym identyfikatorze.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectFirms == null)
            {
                return NotFound();
            }

            var projectFirm = await _context.ProjectFirms
                .Include(p => p.Firm)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.FirmsId == id);
            if (projectFirm == null)
            {
                return NotFound();
            }

            return View(projectFirm);
        }

        // GET: ProjectFirms/Create
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /ProjectFirms/Create.
        /// </summary>
        /// <returns>Zwraca widok do tworzenia nowej firmy projektowej.</returns>
        public IActionResult Create()
        {
            ViewData["FirmsId"] = new SelectList(_context.Firms, "FirmId", "FirmName");
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "Description");
            return View();
        }

        // POST: ProjectFirms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /ProjectFirms/Create.
        /// </summary>
        /// <param name="projectFirm">Nowa firma projektowa do utworzenia.</param>
        /// <returns>Jeśli zapisanie firmy projektowej powiedzie się, przekierowuje na stronę z listą firm projektowych. W przeciwnym razie zwraca widok z formularzem do poprawy danych.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirmsId,ProjectsId")] ProjectFirm projectFirm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectFirm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirmsId"] = new SelectList(_context.Firms, "FirmId", "FirmName", projectFirm.FirmsId);
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "Description", projectFirm.ProjectsId);
            return View(projectFirm);
        }

        // GET: ProjectFirms/Edit/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /ProjectFirms/Edit/5.
        /// </summary>
        /// <param name="id">Identyfikator firmy projektowej do edycji.</param>
        /// <returns>Zwraca widok edycji firmy projektowej o podanym identyfikatorze.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectFirms == null)
            {
                return NotFound();
            }

            var projectFirm = await _context.ProjectFirms.FindAsync(id);
            if (projectFirm == null)
            {
                return NotFound();
            }
            ViewData["FirmsId"] = new SelectList(_context.Firms, "FirmId", "FirmName", projectFirm.FirmsId);
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "Description", projectFirm.ProjectsId);
            return View(projectFirm);
        }

        // POST: ProjectFirms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /ProjectFirms/Edit/5.
        /// </summary>
        /// <param name="id">Identyfikator firmy projektowej do edycji.</param>
        /// <param name="projectFirm">Edytowana firma projektowa.</param>
        /// <returns>Jeśli edycja firmy projektowej powiedzie się, przekierowuje na stronę z listą firm projektowych. W przeciwnym razie zwraca widok z formularzem do poprawy danych.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirmsId,ProjectsId")] ProjectFirm projectFirm)
        {
            if (id != projectFirm.FirmsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectFirm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectFirmExists(projectFirm.FirmsId))
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
            ViewData["FirmsId"] = new SelectList(_context.Firms, "FirmId", "FirmName", projectFirm.FirmsId);
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "Description", projectFirm.ProjectsId);
            return View(projectFirm);
        }

        // GET: ProjectFirms/Delete/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /ProjectFirms/Delete/5.
        /// </summary>
        /// <param name="id">Identyfikator firmy projektowej do usunięcia.</param>
        /// <returns>Zwraca widok potwierdzenia usunięcia firmy projektowej o podanym identyfikatorze.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectFirms == null)
            {
                return NotFound();
            }

            var projectFirm = await _context.ProjectFirms
                .Include(p => p.Firm)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.FirmsId == id);
            if (projectFirm == null)
            {
                return NotFound();
            }

            return View(projectFirm);
        }

        // POST: ProjectFirms/Delete/5

        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /ProjectFirms/Delete/5.
        /// </summary>
        /// <param name="id">Identyfikator firmy projektowej do usunięcia.</param>
        /// <returns>Jeśli usunięcie firmy projektowej powiedzie się, przekierowuje na stronę z listą firm projektowych. W przeciwnym razie zwraca błąd.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectFirms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProjectFirms'  is null.");
            }
            var projectFirm = await _context.ProjectFirms.FindAsync(id);
            if (projectFirm != null)
            {
                _context.ProjectFirms.Remove(projectFirm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectFirmExists(int id)
        {
          return (_context.ProjectFirms?.Any(e => e.FirmsId == id)).GetValueOrDefault();
        }
    }
}
