using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase;

namespace EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Posreduje između prezentacionog sloja i sloja podataka pri radu sa zaposlenima.
    /// Saradnici: ZaposleniDB, Zaposleni.
    /// </summary>
    public class ZaposleniServis
    {
        
        private readonly ZaposleniDB zaposleniDB;

        public ZaposleniServis(ZaposleniDB zaposleniDB)
        {
            this.zaposleniDB = zaposleniDB;
            
        }

        public bool DodajZaposlenog(Zaposleni noviZaposleni)
        {
            return zaposleniDB.DodajZaposlenog(noviZaposleni);
        }
        public List<Zaposleni> VratiSveZaposlene()
        {
            return zaposleniDB.VratiSveZaposlene();
        }
        public Zaposleni? PronadjiZaposlenog(string jmbg)
        {
            return zaposleniDB.PronadjiZaposlenog(jmbg);
        }
        public bool IzmeniZaposlenog(string jmbg, Zaposleni izmenjeniZaposleni)
        {
            return zaposleniDB.IzmeniZaposlenog(jmbg, izmenjeniZaposleni);
        }
        public bool ObrisiZaposlenog(string jmbg)
        {
            return zaposleniDB.ObrisiZaposlenog(jmbg);
        }
    }
}
