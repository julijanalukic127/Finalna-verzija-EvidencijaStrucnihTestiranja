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
    /// Odgovornost: Predstavlja osnovne zajedničke podatke o osobi.
    /// Saradnici: Zaposleni, Korisnik.
    /// </summary>
    public class Osoba
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        public string Prezime { get; set; }

    }
}
