using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EvrakTakipSistemi
{
    public class DbBaglanti
    {
       public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("server=RUZGAR;user id=sa;password=123456;initial catalog=dbAngun;");
            baglan.Open();
            return baglan;
        }
    }
}
