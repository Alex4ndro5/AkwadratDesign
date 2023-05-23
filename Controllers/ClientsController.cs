using AkwadratDesign.Data;
using AkwadratDesign.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkwadratDesign.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Clients
        /// </summary>
        /// <returns>Zwraca widok listy klientów przechowywanych w bazie danych.</returns>
        public async Task<IActionResult> Index()
        {
            return _context.Clients != null ?
                        View(await _context.Clients.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
        }

        // GET: Clients/Details/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Clients/Details/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Zwraca widok szczegółowych informacji o kliencie o podanym identyfikatorze. Jeśli klient o podanym identyfikatorze nie istnieje lub zbiór danych 'ApplicationDbContext.Clients' jest pusty, zostanie zwrócony błąd.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Clients/Create 
        /// </summary>
        /// <returns> Zwraca formularz do tworzenia nowego klienta.</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Clients/Create
        /// </summary>
        /// <param name="client"></param>
        /// <returns> Jeśli dane klienta są poprawne, zostaje on dodany do bazy danych. Następnie użytkownik zostaje przekierowany do strony z listą klientów. Jeśli dane klienta są niepoprawne, zwracany jest widok z formularzem wraz z błędami.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,Name,Surname,Email,Message")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Clients/Edit/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  Zwraca formularz edycji danych klienta o podanym identyfikatorze. Jeśli klient o podanym identyfikatorze nie istnieje lub zbiór danych 'ApplicationDbContext.Clients' jest pusty, zostanie zwrócony błąd.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Clients/Edit/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns> Jeśli identyfikator klienta nie pasuje do identyfikatora w obiekcie klienta, zostanie zwrócony błąd. Jeśli dane klienta są poprawne, zostają zaktualizowane w bazie danych.Jeśli wystąpił błąd równoczesnej aktualizacji danych (DbUpdateConcurrencyException), sprawdzane jest, czy klient istnieje w bazie danych.\ Jeśli klient nie istnieje, zostanie zwrócony błąd. W przeciwnym razie zostanie zgłoszony wyjątek. Następnie użytkownik zostaje przekierowany do strony z listą klientów. Jeśli dane klienta są niepoprawne, zwracany jest widok z formularzem wraz z błędami.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,Surname,Email,Message")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Clients/Delete/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Zwraca formularz potwierdzający usunięcie klienta o podanym identyfikatorze.Jeśli klient o podanym identyfikatorze nie istnieje lub zbiór danych 'ApplicationDbContext.Clients' jest pusty, zostanie zwrócony błąd.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        /// <summary>
        /// Metoda obsługuje żądanie POST na adres /Clients/DeleteConfirmed/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Jeśli zbiór danych 'ApplicationDbContext.Clients' jest pusty, zostanie zwrócony błąd. Klient o podanym identyfikatorze zostaje odnaleziony w bazie danych i usunięty. Następnie użytkownik zostaje przekierowany do strony z listą klientów.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        ///   Sprawdza, czy klient o podanym identyfikatorze istnieje w bazie danych.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Jeśli zbiór danych 'ApplicationDbContext.Clients' jest pusty, zwraca wartość false. W przeciwnym razie sprawdza, czy istnieje klient o podanym identyfikatorze.</returns>
        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
