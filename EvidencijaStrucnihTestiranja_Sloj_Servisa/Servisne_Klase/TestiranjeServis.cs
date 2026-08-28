using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using EvidencijaStrucnihTestiranja_Sloj_Poslovne_Logike;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Interfejsi;


namespace EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Koordinira rad sa testiranjima između prezentacije, poslovne logike i sloja podataka.
    /// Saradnici: ITestiranjeServis, TestiranjeDB, TestiranjeLogika, VrstaTestaServis.
    /// </summary>
    public class TestiranjeServis : ITestiranjeServis
    {
       
        private readonly TestiranjeLogika testiranjeLogika;
        private readonly VrstaTestaServis vrstaTestaServis;
        private readonly TestiranjeDB testiranjeDB;

        public TestiranjeServis(
    TestiranjeLogika testiranjeLogika,
    VrstaTestaServis vrstaTestaServis,
    TestiranjeDB testiranjeDB)
        {
            this.testiranjeLogika = testiranjeLogika;
            this.vrstaTestaServis = vrstaTestaServis;
            this.testiranjeDB = testiranjeDB;
        }
        public bool DodajTestiranje(Testiranje novoTestiranje)
        {
            if (!testiranjeLogika.ProveriPodatkeTestiranja(novoTestiranje))
            {
                return false;
            }

            VrstaTesta? vrstaTesta =
     vrstaTestaServis.PronadjiVrstuTesta(novoTestiranje.IDVrsteTesta);

            if (vrstaTesta == null)
            {
                return false;
            }

            testiranjeLogika.OdrediRezultatTestiranja(novoTestiranje);

            return testiranjeDB.DodajTestiranje(novoTestiranje);
        }
        public List<Testiranje> VratiSvaTestiranja()
        {
            return testiranjeDB.VratiSvaTestiranja();
        }
        public Testiranje? PronadjiTestiranje(int id)
        {
            return testiranjeDB.VratiSvaTestiranja()
                .FirstOrDefault(t => t.IDTestiranja == id);
        }
        public bool IzmeniTestiranje(int id, Testiranje izmenjenoTestiranje)
        {
            Testiranje? testiranjeZaIzmenu = PronadjiTestiranje(id);

            if (testiranjeZaIzmenu != null)
            {
                testiranjeZaIzmenu.JMBG = izmenjenoTestiranje.JMBG;
                testiranjeZaIzmenu.IDVrsteTesta = izmenjenoTestiranje.IDVrsteTesta;
                testiranjeZaIzmenu.DatumTestiranja = izmenjenoTestiranje.DatumTestiranja;
                testiranjeZaIzmenu.BrojPoena = izmenjenoTestiranje.BrojPoena;

                testiranjeLogika.OdrediRezultatTestiranja(testiranjeZaIzmenu);

                return testiranjeDB.IzmeniTestiranje(id, testiranjeZaIzmenu);
            }
            else
            {
                return false;
            }
        }
        public bool ObrisiTestiranje(int id)
        {
            Testiranje? testiranjeZaBrisanje = PronadjiTestiranje(id);

            if (testiranjeZaBrisanje != null)
            {
                return testiranjeDB.ObrisiTestiranje(id);
            }
            else
            {
                return false;
            }
        }

    }
}
