using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Čuva podatke potrebne za prijavu i identifikaciju korisnika sistema.
    /// Saradnici: Osoba, KorisnikDB.
    /// </summary>
    public class Korisnik : Osoba
    {
        public int IDKorisnika { get; set; }

       
        public string KorisnickoIme { get; set; }

        public string Sifra { get; set; }

        public string Status { get; set; }

        public string Uloga { get; set; }
    }
}
