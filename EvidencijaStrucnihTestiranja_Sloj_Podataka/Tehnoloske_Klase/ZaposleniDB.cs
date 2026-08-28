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
    /// Odgovornost: Izvršava operacije čitanja, dodavanja, izmene i brisanja zaposlenih u bazi.
    /// Saradnici: KonekcijaSaBazom, Zaposleni.
    /// </summary>
    public class ZaposleniDB
    {
        private readonly KonekcijaSaBazom konekcijaSaBazom;

        public ZaposleniDB(KonekcijaSaBazom konekcijaSaBazom)
        {
            this.konekcijaSaBazom = konekcijaSaBazom;
        }

        public List<Zaposleni> VratiSveZaposlene()
        {
            List<Zaposleni> zaposleni = new List<Zaposleni>();

            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = "SELECT JMBG, Ime, Prezime, RadnoMesto, Email FROM ZAPOSLENI";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                konekcija.Open();

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        Zaposleni zaposleniObjekat = new Zaposleni
                        {
                            JMBG = citac["JMBG"].ToString(),
                            Ime = citac["Ime"].ToString(),
                            Prezime = citac["Prezime"].ToString(),
                            RadnoMesto = citac["RadnoMesto"].ToString(),
                            Email = citac["Email"].ToString()
                        };

                        zaposleni.Add(zaposleniObjekat);
                    }
                }
            }

            return zaposleni;
        }
        public Zaposleni? PronadjiZaposlenog(string jmbg)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"SELECT JMBG, Ime, Prezime, RadnoMesto, Email
                        FROM ZAPOSLENI
                        WHERE JMBG = @JMBG";

                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@JMBG", jmbg);

                konekcija.Open();

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    if (citac.Read())
                    {
                        return new Zaposleni
                        {
                            JMBG = citac["JMBG"].ToString(),
                            Ime = citac["Ime"].ToString(),
                            Prezime = citac["Prezime"].ToString(),
                            RadnoMesto = citac["RadnoMesto"].ToString(),
                            Email = citac["Email"].ToString()
                        };
                    }
                }
            }

            return null;
        }
        public bool DodajZaposlenog(Zaposleni zaposleni)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"INSERT INTO ZAPOSLENI
                        (JMBG, Ime, Prezime, RadnoMesto, Email)
                        VALUES
                        (@JMBG, @Ime, @Prezime, @RadnoMesto, @Email)";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue("@JMBG", zaposleni.JMBG);
                komanda.Parameters.AddWithValue("@Ime", zaposleni.Ime);
                komanda.Parameters.AddWithValue("@Prezime", zaposleni.Prezime);
                komanda.Parameters.AddWithValue("@RadnoMesto", zaposleni.RadnoMesto);
                komanda.Parameters.AddWithValue("@Email", zaposleni.Email);

                konekcija.Open();

                int brojRedova = komanda.ExecuteNonQuery();

                return brojRedova > 0;
            }
        }
        public bool IzmeniZaposlenog(string jmbg, Zaposleni zaposleni)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"UPDATE ZAPOSLENI
                        SET Ime = @Ime,
                            Prezime = @Prezime,
                            RadnoMesto = @RadnoMesto,
                            Email = @Email
                        WHERE JMBG = @JMBG";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue("@Ime", zaposleni.Ime);
                komanda.Parameters.AddWithValue("@Prezime", zaposleni.Prezime);
                komanda.Parameters.AddWithValue("@RadnoMesto", zaposleni.RadnoMesto);
                komanda.Parameters.AddWithValue("@Email", zaposleni.Email);
                komanda.Parameters.AddWithValue("@JMBG", jmbg);

                konekcija.Open();

                int brojRedova = komanda.ExecuteNonQuery();

                return brojRedova > 0;
            }
        }
        public bool ObrisiZaposlenog(string jmbg)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = "DELETE FROM ZAPOSLENI WHERE JMBG = @JMBG";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue("@JMBG", jmbg);

                try
                {
                    konekcija.Open();

                    int brojRedova = komanda.ExecuteNonQuery();

                    return brojRedova > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        // Zaposleni ima povezana testiranja
                        return false;
                    }

                    throw;
                }
            }
        }
    }
}