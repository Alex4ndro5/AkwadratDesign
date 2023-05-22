using AkwadratDesign.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AkwadratDesign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("/agnieszka-osypanka/")]
        public IActionResult About()
        {
            return View();
        }
        [Route("/kontakt/")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("/wspolpraca/")]
        public IActionResult Collaborations()
        {
            return View();
        }
        [Route("/realizacje/")]
        public IActionResult Portfolio()
        {
            return View();
        }
        [Route("/admin")]
        public IActionResult Admin()
        {
            return View();
        }
        [Route("/polityka-prywatnosci/")]
        public IActionResult Privacy()
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