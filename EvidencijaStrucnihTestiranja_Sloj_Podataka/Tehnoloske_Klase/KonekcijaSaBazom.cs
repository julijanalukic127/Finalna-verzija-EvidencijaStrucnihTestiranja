using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Kreiranje SQL konekcije na osnovu konfigurisanog konekcionog stringa.
    /// Saradnici: DB klase sloja podataka.
    /// </summary>
    public class KonekcijaSaBazom
    {
        private readonly string connectionString;

        public KonekcijaSaBazom(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection KreirajKonekciju()
        {
            return new SqlConnection(this.connectionString);
        }
    }
}
