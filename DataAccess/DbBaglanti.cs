using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrakTakipSistemi
{
    public class DbBaglanti
    {
       public OleDbConnection baglanti()
        {
           OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\mruzgar\Desktop\AngunVeriTabani.mdb");
            baglan.Open();
            return baglan;
        }
    }
}
