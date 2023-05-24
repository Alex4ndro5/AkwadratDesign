using AkwadratDesign.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AkwadratDesign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Kontroler obsługujący stronę główną i inne podstrony.
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Home/Index.
        /// </summary>
        /// <returns> Zwraca widok domyślny dla strony głównej.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /agnieszka-osypanka/.
        /// </summary>
        /// <returns>Zwraca widok z informacjami o Agnieszce Osypance-architektce.</returns>
        [Route("/agnieszka-osypanka/")]
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /kontakt/.
        /// </summary>
        /// <returns> Zwraca widok z formularzem kontaktowym.</returns>
        [Route("/kontakt/")]
        public IActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /wspolpraca/.
        /// </summary>
        /// <returns> Zwraca widok z informacjami o współpracy.</returns>
        [Route("/wspolpraca/")]
        public IActionResult Collaborations()
        {
            return View();
        }

        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /realizacje/.
        /// </summary>
        /// <returns> Zwraca widok z portfolio realizacji.</returns>
        [Route("/realizacje/")]
        public IActionResult Portfolio()
        {
            return View();
        }

        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /admin.
        /// </summary>
        /// <returns> Zwraca widok panelu administracyjnego.</returns>
        [Route("/admin")]
        public IActionResult Admin()
        {
            return View();
        }
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /admin-panel.
        /// </summary>
        /// <returns> Wymaga wcześniejszego uwierzytelnienia użytkownika. Zwraca widok panelu administracyjnego.</returns>
        [Authorize]
        [Route("/admin-panel")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /polityka-prywatnosci/.
        /// </summary>
        /// <returns>  Zwraca widok z polityką prywatności.</returns>
        [Route("/polityka-prywatnosci/")]
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Metoda obsługuje żądanie GET na adres /Home/Error.
        /// </summary>
        /// <returns>  Zwraca widok z informacją o błędzie. Przekazuje identyfikator błędu do widoku.</returns>

        [Route("/dokumentacja/")]
        public IActionResult Doc()
        {
            return View();
        }
        [Route("/instrukcja/")]
        public IActionResult UserGuide()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}