using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaStrucnihTestiranja_Sloj_Servisa.DTO
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Prenosi podatke o testiranju između slojeva aplikacije.
    /// Saradnici: Testiranje, TestiranjeMapper.
    /// </summary>
    public class TestiranjeDTO
    {
        public int IDTestiranja { get; set; }
        public string JMBG { get; set; } = string.Empty;
        public int IDVrsteTesta { get; set; }
        public DateTime DatumTestiranja { get; set; }
        public int BrojPoena { get; set; }
        public bool Polozio { get; set; }

        public string Rezultat
        {
            get
            {
                return Polozio ? "Položio" : "Nije položio";
            }
        }
    }
}
