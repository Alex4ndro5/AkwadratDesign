using AkwadratDesign.Data;
using AkwadratDesign.Models.DbModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace AkwadratDesign.Controllers
{
    public class ProjectFirmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectFirmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectFirms
        /// <summary>
        /// Akcja HTTP GET dla wyświetlenia listy ProjectFirms.
        /// </summary>
        /// <returns>Widok zawierający listę ProjectFirms.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectFirms.Include(p => p.Firm).Include(p => p.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectFirms/Details/5
        /// <summary>
        /// Akcja HTTP GET dla wyświetlenia szczegółowych informacji o ProjectFirm.
        /// </summary>
        /// <param name="id">Identyfikator ProjectFirm.</param>
        /// <returns>Widok zawierający szczegółowe informacje o ProjectFirm lub NotFound, jeśli ProjectFirm nie istnieje.</returns>
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
        /// Akcja HTTP GET dla wyświetlenia formularza do tworzenia ProjectFirm.
        /// </summary>
        /// <returns>Widok zawierający formularz do tworzenia ProjectFirm.</returns>
        public IActionResult Create()
        {
            ViewData["FirmsId"] = new SelectList(_context.Firms, "FirmId", "FirmName");
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            return View();
        }

        // POST: ProjectFirms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Akcja HTTP POST dla utworzenia ProjectFirm.
        /// </summary>
        /// <param name="projectFirm">Dane ProjectFirm przesłane z formularza.</param>
        /// <returns>Przekierowanie do akcji Index po utworzeniu ProjectFirm lub ponowne wyświetlenie formularza w przypadku błędów walidacji.</returns>
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
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", projectFirm.ProjectsId);
            return View(projectFirm);
        }

        // GET: ProjectFirms/Edit/5
        /// <summary>
        /// Akcja HTTP GET dla wyświetlenia formularza do edycji ProjectFirm.
        /// </summary>
        /// <param name="id">Identyfikator ProjectFirm.</param>
        /// <returns>Widok zawierający formularz do edycji ProjectFirm lub NotFound, jeśli ProjectFirm nie istnieje
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
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", projectFirm.ProjectsId);
            return View(projectFirm);
        }

        // POST: ProjectFirms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Akcja HTTP POST dla edycji ProjectFirm.
        /// </summary>
        /// <param name="id">Identyfikator ProjectFirm.</param>
        /// <param name="projectFirm">Zaktualizowane dane ProjectFirm przesłane z formularza.</param>
        /// <returns>Przekierowanie do akcji Index po zapisaniu zmian w ProjectFirm lub ponowne wyświetlenie formularza w przypadku błędów walidacji.</returns>
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
            ViewData["ProjectsId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", projectFirm.ProjectsId);
            return View(projectFirm);
        }

        // GET: ProjectFirms/Delete/5
        /// <summary>
        /// Akcja HTTP GET dla wyświetlenia potwierdzenia usunięcia ProjectFirm.
        /// </summary>
        /// <param name="id">Identyfikator ProjectFirm.</param>
        /// <returns>Widok potwierdzenia usunięcia ProjectFirm lub NotFound, jeśli ProjectFirm nie istnieje.</returns>
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
        /// Akcja HTTP POST dla potwierdzenia usunięcia ProjectFirm.
        /// </summary>
        /// <param name="id">Identyfikator ProjectFirm do usunięcia.</param>
        /// <returns>Przekierowanie do akcji Index po usunięciu ProjectFirm.</returns>
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
        /// <summary>
        /// Sprawdza, czy ProjectFirm o podanym identyfikatorze istnieje w bazie danych.
        /// </summary>
        /// <param name="id">Identyfikator ProjectFirm.</param>
        /// <returns>True, jeśli ProjectFirm istnieje, w przeciwnym razie False.</returns>
        private bool ProjectFirmExists(int id)
        {
            return (_context.ProjectFirms?.Any(e => e.FirmsId == id)).GetValueOrDefault();
        }
    }
}
