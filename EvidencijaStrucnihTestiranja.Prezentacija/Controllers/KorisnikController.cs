using Microsoft.AspNetCore.Mvc;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase;

namespace EvidencijaStrucnihTestiranja.Prezentacija.Controllers
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Upravlja prijavom, sesijom i odjavom korisnika.
    /// Saradnici: KorisnikServis, HttpContext.Session.
    /// </summary>
    public class KorisnikController : Controller
    {
        private readonly KorisnikServis korisnikServis;

        public KorisnikController(KorisnikServis korisnikServis)
        {
            this.korisnikServis = korisnikServis;
        }

        [HttpGet]
        public IActionResult Prijava()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Prijava(string korisnickoIme, string sifra)
        {
            var korisnik = korisnikServis.PrijaviKorisnika(korisnickoIme, sifra);

            if (korisnik == null)
            {
                ViewBag.Greska = "Pogrešno korisničko ime ili šifra.";
                return View();
            }

            HttpContext.Session.SetString("KorisnickoIme", korisnik.KorisnickoIme);
            HttpContext.Session.SetString("ImePrezime", korisnik.Ime + " " + korisnik.Prezime);
            HttpContext.Session.SetString("Uloga", korisnik.Uloga);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Prijava");
        }
    }
}