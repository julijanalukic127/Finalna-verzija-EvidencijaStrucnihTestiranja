using EvidencijaStrucnihTestiranja.Prezentacija.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EvidencijaStrucnihTestiranja.Prezentacija.Filteri;

namespace EvidencijaStrucnihTestiranja.Prezentacija.Controllers
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Upravlja prikazom početne stranice aplikacije.
    /// Saradnici: Home pogledi.
    /// </summary>
    [ProveraSesijeFilter]
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
