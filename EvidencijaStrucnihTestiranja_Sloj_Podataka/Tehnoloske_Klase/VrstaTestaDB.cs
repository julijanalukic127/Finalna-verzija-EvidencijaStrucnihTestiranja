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
    /// Odgovornost: Izvršava operacije čitanja, dodavanja, izmene i brisanja vrsta testova.
    /// Saradnici: KonekcijaSaBazom, VrstaTesta.
    /// </summary>
    public class VrstaTestaDB
    {
        private readonly KonekcijaSaBazom konekcijaSaBazom;

        public VrstaTestaDB(KonekcijaSaBazom konekcijaSaBazom)
        {
            this.konekcijaSaBazom = konekcijaSaBazom;
        }
        public List<VrstaTesta> VratiSveVrsteTestova()
        {
            List<VrstaTesta> vrsteTestova = new List<VrstaTesta>();

            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"SELECT IDVrsteTesta, Naziv, MinimalanBrojPoena
                        FROM VRSTA_TESTA";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                konekcija.Open();

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        VrstaTesta vrstaTesta = new VrstaTesta
                        {
                            IDVrsteTesta = Convert.ToInt32(citac["IDVrsteTesta"]),
                            Naziv = citac["Naziv"].ToString(),
                            MinimalanBrojPoena = Convert.ToInt32(citac["MinimalanBrojPoena"])
                        };

                        vrsteTestova.Add(vrstaTesta);
                    }
                }
            }

            return vrsteTestova;

        }
        public VrstaTesta? PronadjiVrstuTesta(int id)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"SELECT IDVrsteTesta, Naziv, MinimalanBrojPoena
                        FROM VRSTA_TESTA
                        WHERE IDVrsteTesta = @IDVrsteTesta";

                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@IDVrsteTesta", id);

                konekcija.Open();

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    if (citac.Read())
                    {
                        return new VrstaTesta
                        {
                            IDVrsteTesta = Convert.ToInt32(citac["IDVrsteTesta"]),
                            Naziv = citac["Naziv"].ToString(),
                            MinimalanBrojPoena = Convert.ToInt32(citac["MinimalanBrojPoena"])
                        };
                    }
                }
            }

            return null;
        }
        public bool DodajVrstuTesta(VrstaTesta novaVrstaTesta)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"INSERT INTO VRSTA_TESTA
                        (Naziv, MinimalanBrojPoena)
                        VALUES
                        (@Naziv, @MinimalanBrojPoena)";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue("@Naziv", novaVrstaTesta.Naziv);
                komanda.Parameters.AddWithValue("@MinimalanBrojPoena",
                                                novaVrstaTesta.MinimalanBrojPoena);

                konekcija.Open();

                int brojRedova = komanda.ExecuteNonQuery();

                return brojRedova > 0;
            }
        }
        public bool IzmeniVrstuTesta(int id, VrstaTesta izmenjenaVrstaTesta)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = @"UPDATE VRSTA_TESTA
                        SET Naziv = @Naziv,
                            MinimalanBrojPoena = @MinimalanBrojPoena
                        WHERE IDVrsteTesta = @IDVrsteTesta";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue("@Naziv", izmenjenaVrstaTesta.Naziv);
                komanda.Parameters.AddWithValue("@MinimalanBrojPoena",
                                                izmenjenaVrstaTesta.MinimalanBrojPoena);
                komanda.Parameters.AddWithValue("@IDVrsteTesta", id);

                konekcija.Open();

                int brojRedova = komanda.ExecuteNonQuery();

                return brojRedova > 0;
            }
        }
        public bool ObrisiVrstuTesta(int id)
        {
            using (SqlConnection konekcija = konekcijaSaBazom.KreirajKonekciju())
            {
                string upit = "DELETE FROM VRSTA_TESTA WHERE IDVrsteTesta = @IDVrsteTesta";

                SqlCommand komanda = new SqlCommand(upit, konekcija);

                komanda.Parameters.AddWithValue("@IDVrsteTesta", id);

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
                        // Vrsta testa se koristi u postojećim testiranjima
                        return false;
                    }

                    throw;
                }
            }
        }
    }
}
