using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.DTO;

namespace EvidencijaStrucnihTestiranja_Sloj_Servisa.Maperi
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Preslikava objekte Testiranje u TestiranjeDTO objekte.
    /// Saradnici: Testiranje, TestiranjeDTO.
    /// </summary>
    public static class TestiranjeMapper
    {
        public static TestiranjeDTO UDTO(Testiranje testiranje)
        {
            return new TestiranjeDTO
            {
                IDTestiranja = testiranje.IDTestiranja,
                JMBG = testiranje.JMBG,
                IDVrsteTesta = testiranje.IDVrsteTesta,
                DatumTestiranja = testiranje.DatumTestiranja,
                BrojPoena = testiranje.BrojPoena,
                Polozio = testiranje.Polozio
            };
        }

        public static List<TestiranjeDTO> UListuDTO(List<Testiranje> testiranja)
        {
            return testiranja
                .Select(t => UDTO(t))
                .ToList();
        }
    }
}
