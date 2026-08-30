using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvidencijaStrucnihTestiranja.Prezentacija.Filteri
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Proverava da li postoji aktivna korisnička sesija pre pristupa zaštićenim stranicama.
    /// Saradnici: HttpContext.Session, kontroleri prezentacionog sloja.
    /// </summary>
    public class ProveraSesijeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? korisnickoIme =
                context.HttpContext.Session.GetString("KorisnickoIme");

            if (string.IsNullOrEmpty(korisnickoIme))
            {
                context.Result = new RedirectToActionResult(
                    "Prijava",
                    "Korisnik",
                    null
                );
            }
        }
    }
    public class ProveraAdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? uloga = context.HttpContext.Session.GetString("Uloga");

            if (uloga != "Admin")
            {
                context.Result = new RedirectToActionResult(
                    "Index",
                    "Pocetna",
                    null
                );
            }
        }
    }
}