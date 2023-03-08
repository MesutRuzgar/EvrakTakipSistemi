using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EvrakTakipSistemi
{
    public partial class IletisimFormu : Form
    {
       
        public IletisimFormu()
        {
         
            InitializeComponent();
        }
        public string id;
        DbBaglanti bgl = new DbBaglanti();
        private void IletisimFormu_Load(object sender, EventArgs e)
        {
            tbxId.Text = id;
            SqlCommand cmd = new SqlCommand("GETCOMMUNICATION ",bgl.baglanti());
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.CommandType= CommandType.StoredProcedure;
           
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbxVKN.Text = dr[1].ToString();
                tbxAd.Text = dr[2].ToString();
                mskTel.Text = dr[3].ToString();
                tbxEmail.Text = dr[4].ToString();

            }
            bgl.baglanti().Close();


        }
        
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
          
            
        }
    }
}
