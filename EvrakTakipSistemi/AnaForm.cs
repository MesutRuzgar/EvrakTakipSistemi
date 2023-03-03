using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace EvrakTakipSistemi
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        DbBaglanti bgl = new DbBaglanti();

        private void AnaForm_Load(object sender, EventArgs e)
        {
            Listele();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mskVkn.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbxAd.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbxVergiYili.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbxFaaliyetBelgesiTarih.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            tbxİmzaSirkusuTarih.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            rtbxFirmaYetkili.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            Listele();

        }

        private void Listele()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select * from tbl_evraklar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
