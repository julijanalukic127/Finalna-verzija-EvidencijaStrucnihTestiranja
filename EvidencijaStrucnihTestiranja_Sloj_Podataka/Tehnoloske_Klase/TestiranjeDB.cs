using System;
using System.Collections.Generic;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;
using Microsoft.Data.SqlClient;

namespace EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Izvršava operacije nad testiranjima, stored procedurom, pogledom i transakcijama.
    /// Saradnici: KonekcijaSaBazom, Testiranje.
    /// </summary>
    public class TestiranjeDB
    {
        private readonly KonekcijaSaBazom konekcijaSaBazom;

        public TestiranjeDB(KonekcijaSaBazom konekcijaSaBazom)
        {
            this.konekcijaSaBazom = konekcijaSaBazom;
        }

        public List<Testiranje> VratiSvaTestiranja()
        {
            List<Testiranje> testiranja = new List<Testiranje>();

            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"SELECT IDTestiranja, JMBG, IDVrsteTesta,
                       NazivVrsteTesta,
                       DatumTestiranja, BrojPoena, Polozio
                FROM PregledTestiranja";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                konekcija.Open();

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        Testiranje testiranje = new Testiranje
                        {
                            IDTestiranja = Convert.ToInt32(citac["IDTestiranja"]),
                            JMBG = citac["JMBG"].ToString(),
                            IDVrsteTesta = Convert.ToInt32(citac["IDVrsteTesta"]),
                            DatumTestiranja = Convert.ToDateTime(citac["DatumTestiranja"]),
                            BrojPoena = Convert.ToInt32(citac["BrojPoena"]),
                            Polozio = Convert.ToBoolean(citac["Polozio"]),
                            NazivVrsteTesta = citac["NazivVrsteTesta"].ToString()
                        };

                        testiranja.Add(testiranje);
                    }
                }
            }

            return testiranja;
        }
        public bool DodajTestiranje(Testiranje testiranje)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                konekcija.Open();

                using (SqlTransaction transakcija = konekcija.BeginTransaction())
                {
                    try
                    {
                        // PRVI UPIT - dodavanje testiranja preko stored procedure
                        SqlCommand komandaTestiranje =
                            new SqlCommand("DodajTestiranje", konekcija, transakcija);

                        komandaTestiranje.CommandType =
                            System.Data.CommandType.StoredProcedure;

                        komandaTestiranje.Parameters.AddWithValue(
                            "@JMBG", testiranje.JMBG);

                        komandaTestiranje.Parameters.AddWithValue(
                            "@IDVrsteTesta", testiranje.IDVrsteTesta);

                        komandaTestiranje.Parameters.AddWithValue(
                            "@DatumTestiranja", testiranje.DatumTestiranja);

                        komandaTestiranje.Parameters.AddWithValue(
                            "@BrojPoena", testiranje.BrojPoena);

                        komandaTestiranje.Parameters.AddWithValue(
                            "@Polozio", testiranje.Polozio);

                        int idTestiranja =
                            Convert.ToInt32(komandaTestiranje.ExecuteScalar());


                        // DRUGI UPIT - upis u dnevnik aktivnosti
                        string upitDnevnik = @"
                    INSERT INTO DNEVNIK_AKTIVNOSTI
                        (IDTestiranja, Akcija)
                    VALUES
                        (@IDTestiranja, @Akcija)";

                        SqlCommand komandaDnevnik =
                            new SqlCommand(upitDnevnik, konekcija, transakcija);

                        komandaDnevnik.Parameters.AddWithValue(
                            "@IDTestiranja", idTestiranja);

                        komandaDnevnik.Parameters.AddWithValue(
                            "@Akcija", "Dodato novo testiranje");

                        komandaDnevnik.ExecuteNonQuery();


                        transakcija.Commit();

                        return true;
                    }
                    catch
                    {
                        transakcija.Rollback();

                        return false;
                    }
                }
            }
        }
        public bool IzmeniTestiranje(int id, Testiranje testiranje)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"UPDATE TESTIRANJE
                        SET JMBG = @JMBG,
                            IDVrsteTesta = @IDVrsteTesta,
                            DatumTestiranja = @DatumTestiranja,
                            BrojPoena = @BrojPoena,
                            Polozio = @Polozio
                        WHERE IDTestiranja = @IDTestiranja";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue("@JMBG", testiranje.JMBG);
                komanda.Parameters.AddWithValue("@IDVrsteTesta", testiranje.IDVrsteTesta);
                komanda.Parameters.AddWithValue("@DatumTestiranja", testiranje.DatumTestiranja);
                komanda.Parameters.AddWithValue("@BrojPoena", testiranje.BrojPoena);
                komanda.Parameters.AddWithValue("@Polozio", testiranje.Polozio);
                komanda.Parameters.AddWithValue("@IDTestiranja", id);

                konekcija.Open();

                int brojRedova = komanda.ExecuteNonQuery();

                return brojRedova > 0;
            }
        }
        public bool ObrisiTestiranje(int id)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                konekcija.Open();

                using (SqlTransaction transakcija = konekcija.BeginTransaction())
                {
                    try
                    {
                        // Sačuvaj dnevnik, ali ukloni vezu ka testiranju
                        string upitDnevnik = @"
                    UPDATE DNEVNIK_AKTIVNOSTI
                    SET IDTestiranja = NULL
                    WHERE IDTestiranja = @IDTestiranja";

                        SqlCommand komandaDnevnik =
                            new SqlCommand(upitDnevnik, konekcija, transakcija);

                        komandaDnevnik.Parameters.AddWithValue(
                            "@IDTestiranja", id);

                        komandaDnevnik.ExecuteNonQuery();


                        // Obriši testiranje
                        string upitTestiranje = @"
                    DELETE FROM TESTIRANJE
                    WHERE IDTestiranja = @IDTestiranja";

                        SqlCommand komandaTestiranje =
                            new SqlCommand(upitTestiranje, konekcija, transakcija);

                        komandaTestiranje.Parameters.AddWithValue(
                            "@IDTestiranja", id);

                        int brojRedova =
                            komandaTestiranje.ExecuteNonQuery();

                        transakcija.Commit();

                        return brojRedova > 0;
                    }
                    catch
                    {
                        transakcija.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}