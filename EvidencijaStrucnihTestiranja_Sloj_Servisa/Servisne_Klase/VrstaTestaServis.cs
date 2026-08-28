using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase;

namespace EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Posreduje između prezentacionog sloja i sloja podataka pri radu sa vrstama testova.
    /// Saradnici: VrstaTestaDB, VrstaTesta.
    /// </summary>
    public class VrstaTestaServis
    {
        private readonly VrstaTestaDB vrstaTestaDB;

        public VrstaTestaServis(VrstaTestaDB vrstaTestaDB)
        {
            this.vrstaTestaDB = vrstaTestaDB;
        }

        public bool DodajVrstuTesta(VrstaTesta novaVrstaTesta)
        {
            return vrstaTestaDB.DodajVrstuTesta(novaVrstaTesta);
        }

        public List<VrstaTesta> VratiSveVrsteTestova()
        {
            return vrstaTestaDB.VratiSveVrsteTestova();
        }

        public VrstaTesta? PronadjiVrstuTesta(int id)
        {
            return vrstaTestaDB.PronadjiVrstuTesta(id);
        }

        public bool IzmeniVrstuTesta(int id, VrstaTesta izmenjenaVrstaTesta)
        {
            return vrstaTestaDB.IzmeniVrstuTesta(id, izmenjenaVrstaTesta);
        }

        public bool ObrisiVrstuTesta(int id)
        {
            return vrstaTestaDB.ObrisiVrstuTesta(id);
        }
    }
}
