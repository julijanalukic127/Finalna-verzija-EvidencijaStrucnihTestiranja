using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka
{

    /// <summary>
    /// CRC:
    /// Odgovornost: Čuva podatke o zaposlenom i definiše osnovne validacije podataka.
    /// Saradnici: Osoba, ZaposleniDB, ZaposleniLogika.
    /// </summary>
    public class Zaposleni : Osoba
    {
        [Required(ErrorMessage = "JMBG je obavezan.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG mora sadržati tačno 13 cifara.")]
        public string JMBG { get; set; }

        [Required(ErrorMessage = "Radno mesto je obavezno.")]
        public string RadnoMesto { get; set; }
        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Unesite ispravnu email adresu.")]
        public string Email { get; set; }
    }
}
