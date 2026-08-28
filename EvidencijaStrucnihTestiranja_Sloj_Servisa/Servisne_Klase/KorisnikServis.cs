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
    /// Odgovornost: Posreduje između prezentacionog sloja i sloja podataka pri radu sa korisnicima.
    /// Saradnici: KorisnikDB, Korisnik.
    /// </summary>
    public class KorisnikServis
    {
        private readonly KorisnikDB korisnikDB;

        public KorisnikServis(KorisnikDB korisnikDB)
        {
            this.korisnikDB = korisnikDB;
        }

        public Korisnik? PrijaviKorisnika(string korisnickoIme, string sifra)
        {
            return korisnikDB.PronadjiKorisnika(korisnickoIme, sifra);
        }
    }
}