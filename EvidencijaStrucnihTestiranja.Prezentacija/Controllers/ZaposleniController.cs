using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase;
using Microsoft.AspNetCore.Mvc;
using EvidencijaStrucnihTestiranja.Prezentacija.Filteri;



namespace EvidencijaStrucnihTestiranja.Prezentacija.Controllers
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Upravlja prikazom, unosom, izmenom, brisanjem i pretragom zaposlenih.
    /// Saradnici: ZaposleniServis, Zaposleni.
    /// </summary>
    [ProveraSesijeFilter]
    [ProveraAdminFilter]
    public class ZaposleniController : Controller
    {
        private readonly ZaposleniServis zaposleniServis;

        public ZaposleniController(ZaposleniServis zaposleniServis)
        {
            this.zaposleniServis = zaposleniServis;
        }
        public IActionResult Index(string pretraga)
        {
            var zaposleni = zaposleniServis.VratiSveZaposlene();

            if (!string.IsNullOrWhiteSpace(pretraga))
            {
                zaposleni = zaposleni
                    .Where(z =>
                        z.Ime.Contains(pretraga, StringComparison.OrdinalIgnoreCase) ||
                        z.Prezime.Contains(pretraga, StringComparison.OrdinalIgnoreCase) ||
                        z.JMBG.Contains(pretraga))
                    .ToList();
            }

            ViewBag.Pretraga = pretraga;

            return View(zaposleni);
        }
        [HttpGet]
        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return View(zaposleni);
            }

            var postojeciZaposleni =
                zaposleniServis.PronadjiZaposlenog(zaposleni.JMBG);

            if (postojeciZaposleni != null)
            {
                ModelState.AddModelError(
                    "JMBG",
                    "Zaposleni sa unetim JMBG-om već postoji.");

                return View(zaposleni);
            }

            if (zaposleniServis.DodajZaposlenog(zaposleni))
            {
                return RedirectToAction("Index");
            }

            return View(zaposleni);
        }
        [HttpGet]
        public IActionResult Izmeni(string jmbg)
        {
            var zaposleni = zaposleniServis.PronadjiZaposlenog(jmbg);

            if (zaposleni == null)
            {
                return NotFound();
            }

            return View(zaposleni);
        }

        [HttpPost]
        public IActionResult Izmeni(EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka.Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return View(zaposleni);
            }

            if (zaposleniServis.IzmeniZaposlenog(zaposleni.JMBG, zaposleni))
            {
                return RedirectToAction("Index");
            }

            return View(zaposleni);
        }

        [HttpGet]
        public IActionResult Obrisi(string jmbg)
        {
            var zaposleni = zaposleniServis.VratiSveZaposlene()
                .FirstOrDefault(z => z.JMBG == jmbg);

            if (zaposleni == null)
            {
                return NotFound();
            }

            return View(zaposleni);
        }
        [HttpPost]
        public IActionResult ObrisiPotvrda(string jmbg)
        {
            bool obrisan = zaposleniServis.ObrisiZaposlenog(jmbg);

            if (!obrisan)
            {
                TempData["Greska"] =
                    "Zaposlenog nije moguće obrisati jer postoje evidentirana testiranja za tog zaposlenog.";

                return RedirectToAction("Index");
            }

            TempData["Uspeh"] = "Zaposleni je uspešno obrisan.";

            return RedirectToAction("Index");
        }
    }

}
