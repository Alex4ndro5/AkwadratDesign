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
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Projects.
        /// </summary>
        /// <returns>Zwraca widok z listą projektów.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(p => p.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Projects/Details/5.
        /// </summary>
        /// <param name="id">Identyfikator projektu do wyświetlenia.</param>
        /// <returns>Zwraca widok szczegółów projektu o podanym identyfikatorze.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Projects/Create.
        /// </summary>
        /// <returns>Zwraca widok do tworzenia nowego projektu.</returns>
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Projects/Create.
        /// </summary>
        /// <param name="project">Nowy projekt do utworzenia.</param>
        /// <returns>Jeśli utworzenie projektu powiedzie się, przekierowuje na stronę z listą projektów. W przeciwnym razie zwraca widok z formularzem do poprawy danych.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,Title,Description,Image,TypeProject,TypeClient,ClientId")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClientId = new SelectList(_context.Clients, "ClientId", "Email", project.ClientId);
            return View(project);
        }

        // GET: Projects/Edit/5

        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Projects/Edit/5.
        /// </summary>
        /// <param name="id">Identyfikator projektu do edycji.</param>
        /// <returns>Zwraca widok edycji projektu o podanym identyfikatorze.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email", project.ClientId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Projects/Edit/5.
        /// </summary>
        /// <param name="id">Identyfikator projektu do edycji.</param>
        /// <param name="project">Edytowany projekt.</param>
        /// <returns>Jeśli edycja projektu powiedzie się, przekierowuje na stronę z listą projektów. W przeciwnym razie zwraca widok z formularzem do poprawy danych.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Title,Description,Image,TypeProject,TypeClient,ClientId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email", project.ClientId);
            return View(project);
        }

        // GET: Projects/Delete/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Projects/Delete/5.
        /// </summary>
        /// <param name="id">Identyfikator projektu do usunięcia.</param>
        /// <returns>Zwraca widok potwierdzenia usunięcia projektu o podanym identyfikatorze.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Projects/Delete/5.
        /// </summary>
        /// <param name="id">Identyfikator projektu do usunięcia.</param>
        /// <returns>Jeśli usunięcie projektu powiedzie się, przekierowuje na stronę z listą projektów. W przeciwnym razie zwraca błąd.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Projects/Portfolio.
        /// </summary>
        /// <returns>Zwraca widok z listą projektów.</returns>
        public IActionResult Portfolio()
        {
            List<Project> projects = _context.Projects.ToList();
            return View(projects);
        }
    }
}
