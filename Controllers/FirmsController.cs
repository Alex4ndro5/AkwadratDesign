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
        /// <summary>
        ///  Pobiera listę wszystkich firm z bazy danych wraz z powiązanymi klientami.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Firms.Include(f => f.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Firms/Details/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Firms/Details/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Jeśli identyfikator nie został podany lub zbiór danych 'ApplicationDbContext.Firms' jest pusty, zostanie zwrócony błąd. W przeciwnym razie pobiera firmę o podanym identyfikatorze wraz z powiązanym klientem.Jeśli firma nie istnieje, zostanie zwrócony błąd. W przeciwnym razie zwraca widok z danymi firmy.</returns>
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
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Firms/Create
        /// </summary>
        /// <returns> Zwraca widok z formularzem tworzenia nowej firmy. Dodatkowo pobiera listę klientów z bazy danych i przekazuje ją do widoku jako SelectList.</returns>
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Email");
            return View();
        }

        // POST: Firms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Firms/Create
        /// </summary>
        /// <param name="firm"></param>
        /// <returns> Jeśli dane firmy są poprawne, firma zostaje dodana do bazy danych. Następnie użytkownik zostaje przekierowany do strony z listą firm. Jeśli dane firmy są niepoprawne, zwracany jest widok z formularzem wraz z błędami.</returns>
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
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Firms/Edit/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Jeśli identyfikator nie został podany lub zbiór danych 'ApplicationDbContext.Firms' jest pusty, zostanie zwrócony błąd. W przeciwnym razie pobiera firmę o podanym identyfikatorze.Jeśli firma nie istnieje, zostanie zwrócony błąd. W przeciwnym razie zwraca widok z formularzem edycji firmy. Dodatkowo pobiera listę klientów z bazy danych i przekazuje ją do widoku jako SelectList.</returns>
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
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Firms/Edit/{id}
        /// </summary>
        /// <param name="id"> Parametr 'id' to identyfikator firmy, którą użytkownik chce edytować. Parametr 'firm' to obiekt reprezentujący zmienione dane firmy.</param>
        /// <param name="firm"></param>
        /// <returns>Jeśli identyfikator nie jest zgodny z identyfikatorem firmy w obiekcie 'firm', zostanie zwrócony błąd. Jeśli dane firmy są poprawne, firma zostaje zaktualizowana w bazie danych. Następnie użytkownik zostaje przekierowany do strony z listą firm. Jeśli dane firmy są niepoprawne, zwracany jest widok z formularzem edycji wraz z błędami.</returns>
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
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Firms/Delete/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Jeśli identyfikator nie został podany lub zbiór danych 'ApplicationDbContext.Firms' jest pusty, zostanie zwrócony błąd. W przeciwnym razie pobiera firmę o podanym identyfikatorze wraz z powiązanym klientem. Jeśli firma nie istnieje, zostanie zwrócony błąd. W przeciwnym razie zwraca widok z danymi firmy, który potwierdza chęć usunięcia firmy.</returns>
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
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Firms/DeleteConfirmed/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Jeśli zbiór danych 'ApplicationDbContext.Firms' jest pusty, zostanie zwrócony błąd. Jeśli firma o podanym identyfikatorze istnieje, zostaje usunięta z bazy danych. Następnie użytkownik zostaje przekierowany do strony z listą firm.</returns>
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
        /// <summary>
        /// Metoda sprawdza, czy firma o podanym identyfikatorze istnieje w bazie danych.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool FirmExists(int id)
        {
            return (_context.Firms?.Any(e => e.FirmId == id)).GetValueOrDefault();
        }
    }
}
