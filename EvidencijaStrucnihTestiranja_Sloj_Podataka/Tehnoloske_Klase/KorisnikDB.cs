using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using Microsoft.Data.SqlClient;

namespace EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Pristupa podacima korisnika i proverava podatke potrebne za prijavu.
    /// Saradnici: KonekcijaSaBazom, Korisnik.
    /// </summary>
    public class KorisnikDB
    {
        private readonly KonekcijaSaBazom konekcijaSaBazom;

        public KorisnikDB(KonekcijaSaBazom konekcijaSaBazom)
        {
            this.konekcijaSaBazom = konekcijaSaBazom;
        }

        public Korisnik? PronadjiKorisnika(string korisnickoIme, string sifra)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"SELECT IDKorisnika, Ime, Prezime,
                                       KorisnickoIme, Sifra, Status, Uloga
                                FROM KORISNIK
                                WHERE KorisnickoIme = @KorisnickoIme
                                AND Sifra = @Sifra
                                AND Status = 'Aktivan'";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue(
                    "@KorisnickoIme",
                    korisnickoIme
                );

                komanda.Parameters.AddWithValue(
                    "@Sifra",
                    sifra
                );

                konekcija.Open();

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    if (citac.Read())
                    {
                        return new Korisnik
                        {
                            IDKorisnika = Convert.ToInt32(citac["IDKorisnika"]),
                            Ime = citac["Ime"].ToString(),
                            Prezime = citac["Prezime"].ToString(),
                            KorisnickoIme = citac["KorisnickoIme"].ToString(),
                            Sifra = citac["Sifra"].ToString(),
                            Status = citac["Status"].ToString(),
                            Uloga = citac["Uloga"].ToString()
                        };
                    }
                }
            }

            return null;
        }
    }
}