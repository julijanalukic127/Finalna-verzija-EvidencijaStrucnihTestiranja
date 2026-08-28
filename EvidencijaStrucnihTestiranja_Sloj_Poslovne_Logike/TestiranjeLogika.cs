using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;

namespace EvidencijaStrucnihTestiranja_Sloj_Poslovne_Logike
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Primena poslovnih pravila vezanih za testiranje,
    /// proveru broja poena, datuma i određivanje rezultata testiranja.
    /// Saradnici: Testiranje, VrstaTestaServis.
    /// </summary>
    public class TestiranjeLogika
    {
        private readonly int minimalanBrojPoena;

        public TestiranjeLogika(int minimalanBrojPoena)
        {
            this.minimalanBrojPoena = minimalanBrojPoena;
        }
        public bool OdrediDaLiJePolozio(int brojPoena)
        {
            return brojPoena >= minimalanBrojPoena;
        }

        public bool OdrediDaLiJePolozio(Testiranje testiranje)
        {
            if (testiranje == null)
            {
                return false;
            }

            return OdrediDaLiJePolozio(testiranje.BrojPoena);
        }
        public bool ProveriBrojPoena(int brojPoena)
        {
            return brojPoena >= 0 && brojPoena <= 100;
        }
        public bool ProveriDatumTestiranja(DateTime datumTestiranja)
        {
            return datumTestiranja.Date <= DateTime.Today;
        }
        public bool ProveriPodatkeTestiranja(Testiranje testiranje)
        {
            if (testiranje == null)
            {
                return false;
            }

            if (!ProveriBrojPoena(testiranje.BrojPoena))
            {
                return false;
            }
            if (!ProveriDatumTestiranja(testiranje.DatumTestiranja))
            {
                return false;
            }
            return true;
        }

        public void OdrediRezultatTestiranja(Testiranje testiranje)
        {
            if (testiranje == null)
            {
                return;
            }

            testiranje.Polozio = OdrediDaLiJePolozio(testiranje);
        }
    }
}
