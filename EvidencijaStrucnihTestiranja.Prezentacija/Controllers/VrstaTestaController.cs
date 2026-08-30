using EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase;
using Microsoft.AspNetCore.Mvc;
using EvidencijaStrucnihTestiranja.Prezentacija.Filteri;

namespace EvidencijaStrucnihTestiranja.Prezentacija.Controllers
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Upravlja prikazom i CRUD operacijama nad vrstama testova.
    /// Saradnici: VrstaTestaServis, VrstaTesta.
    /// </summary>
    [ProveraSesijeFilter]
    [ProveraAdminFilter]
    public class VrstaTestaController : Controller
    {
        private readonly VrstaTestaServis vrstaTestaServis;

        public VrstaTestaController(VrstaTestaServis vrstaTestaServis)
        {
            this.vrstaTestaServis = vrstaTestaServis;
        }

        public IActionResult Index()
        {
            var vrsteTestova = vrstaTestaServis.VratiSveVrsteTestova();

            return View(vrsteTestova);
        }
        [HttpGet]
        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(
            EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka.VrstaTesta vrstaTesta)
        {
            if (!ModelState.IsValid)
            {
                return View(vrstaTesta);
            }

            if (vrstaTestaServis.DodajVrstuTesta(vrstaTesta))
            {
                return RedirectToAction("Index");
            }

            return View(vrstaTesta);
        }
        [HttpGet]
        public IActionResult Izmeni(int id)
        {
            var vrstaTesta = vrstaTestaServis.PronadjiVrstuTesta(id);

            if (vrstaTesta == null)
            {
                return NotFound();
            }

            return View(vrstaTesta);
        }

        [HttpPost]
        public IActionResult Izmeni(int id,
            EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka.VrstaTesta vrstaTesta)
        {
            if (!ModelState.IsValid)
            {
                return View(vrstaTesta);
            }

            if (vrstaTestaServis.IzmeniVrstuTesta(id, vrstaTesta))
            {
                return RedirectToAction("Index");
            }

            return View(vrstaTesta);
        }

        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            var vrstaTesta = vrstaTestaServis.PronadjiVrstuTesta(id);

            if (vrstaTesta == null)
            {
                return NotFound();
            }

            return View(vrstaTesta);
        }

        [HttpPost]

        public IActionResult ObrisiPotvrda(int id)
        {
            bool obrisana = vrstaTestaServis.ObrisiVrstuTesta(id);

            if (!obrisana)
            {
                TempData["Greska"] =
                    "Vrstu testa nije moguće obrisati jer postoje evidentirana testiranja koja koriste ovu vrstu testa.";

                return RedirectToAction("Index");
            }

            TempData["Uspeh"] = "Vrsta testa je uspešno obrisana.";

            return RedirectToAction("Index");
        }
    }
}