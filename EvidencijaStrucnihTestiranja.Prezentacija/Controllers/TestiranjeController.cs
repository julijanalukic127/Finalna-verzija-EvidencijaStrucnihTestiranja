using Microsoft.AspNetCore.Mvc;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using EvidencijaStrucnihTestiranja.Prezentacija.Filteri;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Interfejsi;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase;

namespace EvidencijaStrucnihTestiranja.Prezentacija.Controllers
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Upravlja testiranjima, štampom, parametarskom štampom i XML prikazom.
    /// Saradnici: ITestiranjeServis, ZaposleniServis, VrstaTestaServis, TestiranjeXML.
    /// </summary>
    [ProveraSesijeFilter]
    public class TestiranjeController : Controller
    {

        public IActionResult Index()
        {
            var testiranja = testiranjeServis.VratiSvaTestiranja();

            return View(testiranja);
        }
        private readonly ITestiranjeServis testiranjeServis;
        private readonly ZaposleniServis zaposleniServis;
        private readonly VrstaTestaServis vrstaTestaServis;
        private readonly TestiranjeXML testiranjeXML;

        public TestiranjeController(
    ITestiranjeServis testiranjeServis,
    ZaposleniServis zaposleniServis,
    VrstaTestaServis vrstaTestaServis,
    TestiranjeXML testiranjeXML)
        {
            this.testiranjeServis = testiranjeServis;
            this.zaposleniServis = zaposleniServis;
            this.vrstaTestaServis = vrstaTestaServis;
            this.testiranjeXML = testiranjeXML;
        }
        
        [HttpGet]
        public IActionResult Dodaj()
        {
            ViewBag.Zaposleni = zaposleniServis.VratiSveZaposlene();
            ViewBag.VrsteTestova = vrstaTestaServis.VratiSveVrsteTestova();

            return View();
        }
        [HttpPost]
        public IActionResult Dodaj(Testiranje testiranje)
        {
            if (testiranje.DatumTestiranja.Date > DateTime.Today)
            {
                ModelState.AddModelError(
                    "DatumTestiranja",
                    "Datum testiranja ne može biti u budućnosti."
                );
            }
            if (!ModelState.IsValid)
            {

                ViewBag.Zaposleni = zaposleniServis.VratiSveZaposlene();
                ViewBag.VrsteTestova = vrstaTestaServis.VratiSveVrsteTestova();

                return View(testiranje);
            }

            if (testiranjeServis.DodajTestiranje(testiranje))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Zaposleni = zaposleniServis.VratiSveZaposlene();
            ViewBag.VrsteTestova = vrstaTestaServis.VratiSveVrsteTestova();

            return View(testiranje);
        }
        [HttpGet]
        public IActionResult Izmeni(int id)
        {
            var testiranje = testiranjeServis.PronadjiTestiranje(id);

            if (testiranje == null)
            {
                return NotFound();
            }

            ViewBag.Zaposleni = zaposleniServis.VratiSveZaposlene();
            ViewBag.VrsteTestova = vrstaTestaServis.VratiSveVrsteTestova();

            return View(testiranje);
        }
        [HttpPost]
        public IActionResult Izmeni(Testiranje testiranje)
        {
            if (testiranje.DatumTestiranja.Date > DateTime.Today)
            {
                ModelState.AddModelError(
                    "DatumTestiranja",
                    "Datum testiranja ne može biti u budućnosti."
                );
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Zaposleni = zaposleniServis.VratiSveZaposlene();
                ViewBag.VrsteTestova = vrstaTestaServis.VratiSveVrsteTestova();

                return View(testiranje);
            }

            if (testiranjeServis.IzmeniTestiranje(testiranje.IDTestiranja, testiranje))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Zaposleni = zaposleniServis.VratiSveZaposlene();
            ViewBag.VrsteTestova = vrstaTestaServis.VratiSveVrsteTestova();

            return View(testiranje);
        }
        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            var testiranje = testiranjeServis.PronadjiTestiranje(id);

            if (testiranje == null)
            {
                return NotFound();
            }

            return View(testiranje);
        }
        [HttpPost]
        public IActionResult ObrisiPotvrda(int id)
        {
            testiranjeServis.ObrisiTestiranje(id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Stampa()
        {
            var testiranja = testiranjeServis.VratiSvaTestiranja();

            return View(testiranja);
        }
        [HttpGet]
        public IActionResult ParametarskaStampa(string? jmbg)
        {
            var testiranja = testiranjeServis.VratiSvaTestiranja();

            ViewBag.Zaposleni = zaposleniServis.VratiSveZaposlene();
            ViewBag.IzabraniJMBG = jmbg;

            if (!string.IsNullOrWhiteSpace(jmbg))
            {
                testiranja = testiranja
                    .Where(t => t.JMBG == jmbg)
                    .ToList();
            }

            return View(testiranja);
        }
        [HttpGet]
        public IActionResult PrikaziXML()
        {
            var testiranja = testiranjeServis.VratiSvaTestiranja();

            string xml = testiranjeXML.KreirajXML(testiranja);

            return Content(xml, "application/xml");
        }
    }
}
