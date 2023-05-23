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
{/// <summary>
 /// Kontroler obsługujący zarządzanie klientami.
 /// </summary>
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        /// <summary>
        /// Akcja wyświetlająca listę klientów.
        /// </summary>
        /// <returns>Widok zawierający listę klientów.</returns>
        public async Task<IActionResult> Index()
        {
              return _context.Clients != null ? 
                          View(await _context.Clients.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
        }

        // GET: Clients/Details/5
        /// <summary>
        /// Akcja wyświetlająca szczegóły klienta o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <returns>Widok zawierający szczegóły klienta.</returns>
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
        /// Akcja wyświetlająca formularz do tworzenia nowego klienta.
        /// </summary>
        /// <returns>Widok zawierający formularz do tworzenia nowego klienta.</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Akcja obsługująca tworzenie nowego klienta.
        /// </summary>
        /// <param name="client">Nowy klient do dodania.</param>
        /// <returns>Przekierowanie do widoku listy klientów po pomyślnym dodaniu klienta.</returns>
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
        /// Akcja wyświetlająca formularz do edycji danych klienta o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <returns>Widok zawierający formularz do edycji danych klienta.</returns>
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
        /// Akcja obsługująca zapis edytowanych danych klienta.
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <param name="client">Edytowany klient.</param>
        /// <returns>Przekierowanie do widoku listy klientów po pomyślnym zapisie zmian.</returns>
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
        /// Akcja wyświetlająca formularz potwierdzenia usunięcia klienta o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <returns>Widok zawierający formularz potwierdzenia usunięcia klienta.</returns>
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
        /// Akcja obsługująca usunięcie klienta o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <returns>Przekierowanie do widoku listy klientów po pomyślnym usunięciu klienta.</returns>
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

        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
