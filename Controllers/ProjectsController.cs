using AkwadratDesign.Data;
using AkwadratDesign.Models.DbModels;
using AkwadratDesign.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Interfaces;

namespace AkwadratDesign.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;
        /// <summary>
        /// Konstruktor klasy ProjectsController.
        /// </summary>
        /// <param name="context">Kontekst bazy danych.</param>
        /// <param name="photoservice">Usługa obsługująca zdjęcia.</param>
        public ProjectsController(ApplicationDbContext context, IPhotoService photoservice)
        {
            _context = context;
            _photoService = photoservice;
        }

        // GET: Projects
        /// <summary>
        /// Akcja wyświetlająca listę wszystkich projektów.
        /// </summary>
        /// <returns>Widok zawierający listę projektów.</returns>
        public async Task<IActionResult> Index()
        {
            return _context.Projects != null ?
                        View(await _context.Projects.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
        }

        // GET: Projects/Details/5
        /// <summary>
        /// Akcja wyświetlająca szczegóły projektu o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator projektu.</param>
        /// <returns>Widok zawierający szczegóły projektu.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        /// <summary>
        /// Akcja wyświetlająca formularz tworzenia nowego projektu.
        /// </summary>
        /// <returns>Widok zawierający formularz tworzenia projektu.</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Akcja obsługująca dodawanie nowego projektu.
        /// </summary>
        /// <param name="projectVM">Dane projektu przekazane z formularza.</param>
        /// <returns>Przekierowanie do widoku z listą projektów.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectViewModel projectVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectVM);
                var result = await _photoService.AddPhotoAsync(projectVM.Image);
                var project = new Project
                {
                    Title = projectVM.Title,
                    Description = projectVM.Description,
                    Image = result.Url.ToString(),
                    TypeClient = projectVM.TypeClient,
                    Type = projectVM.Type

                };

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectVM);
        }

        // GET: Projects/Edit/5
        /// <summary>
        /// Akcja wyświetlająca formularz edycji projektu o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator projektu.</param>
        /// <returns>Widok zawierający formularz edycji projektu.</returns>
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
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Akcja obsługująca zapisywanie edytowanego projektu.
        /// </summary>
        /// <param name="id">Identyfikator projektu.</param>
        /// <param name="project">Dane edytowanego projektu.</param>
        /// <returns>Przekierowanie do widoku z listą projektów.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Title,Description,Image,Type,TypeClient")] Project project)
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
            return View(project);
        }

        // GET: Projects/Delete/5
        /// <summary>
        /// Akcja wyświetlająca potwierdzenie usunięcia projektu o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator projektu.</param>
        /// <returns>Widok potwierdzenia usunięcia projektu.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        /// <summary>
        /// Akcja obsługująca usuwanie projektu.
        /// </summary>
        /// <param name="id">Identyfikator projektu.</param>
        /// <returns>Przekierowanie do widoku z listą projektów.</returns>
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
        /// <summary>
        /// Sprawdza, czy projekt o podanym identyfikatorze istnieje w bazie danych.
        /// </summary>
        /// <param name="id">Identyfikator projektu.</param>
        /// <returns>True, jeśli projekt istnieje, w przeciwnym razie False.</returns>
        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }

        public IActionResult Portfolio()
        {
            List<Project> projects = _context.Projects.ToList();
            return View(projects);
        }
    }
}
