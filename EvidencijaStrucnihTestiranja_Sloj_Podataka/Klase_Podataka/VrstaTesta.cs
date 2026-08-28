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
    /// Odgovornost: Predstavlja vrstu stručnog testa i pripadajuće podatke.
    /// Saradnici: VrstaTestaDB, Testiranje.
    /// </summary>
    public class VrstaTesta
        {
        public int IDVrsteTesta { get; set; }

        [Required(ErrorMessage = "Naziv vrste testa je obavezan.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Minimalan broj poena je obavezan.")]
        [Range(0, 100, ErrorMessage = "Minimalan broj poena mora biti između 0 i 100.")]
        public int MinimalanBrojPoena { get; set; }

    }

}
