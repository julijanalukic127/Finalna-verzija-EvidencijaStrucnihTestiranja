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
    /// Odgovornost: Predstavlja podatke o sprovedenom stručnom testiranju zaposlenog.
    /// Saradnici: TestiranjeDB, TestiranjeLogika.
    /// </summary>
    public class Testiranje
    {
        public int IDTestiranja { get; set; }

        public string NazivVrsteTesta { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zaposleni je obavezan.")]
        public string JMBG { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Vrsta testa je obavezna.")]
        public int IDVrsteTesta { get; set; }

        [Required(ErrorMessage = "Datum testiranja je obavezan.")]
        public DateTime DatumTestiranja { get; set; }

        [Required(ErrorMessage = "Broj poena je obavezan.")]
        [Range(0, 100, ErrorMessage = "Broj poena mora biti između 0 i 100.")]
        public int BrojPoena { get; set; }

        public bool Polozio { get; set; }
    }
}
