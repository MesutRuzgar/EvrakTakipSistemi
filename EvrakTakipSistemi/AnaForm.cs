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
            try
            {
                tbxId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                mskVkn.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbxAd.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbxVergiYili.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbxFaaliyetBelgesiTarih.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                tbxİmzaSirkusuTarih.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                rtbxFirmaYetkili.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Geçersiz işlem!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("insert into Tbl_Evraklar (VKN,FirmaAd,VergiLevhasiYili,FaaliyetBelgesiTarihi,ImzaSirküsüTarihi,FirmaYetkilileri) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskVkn.Text);
            komut.Parameters.AddWithValue("@p2", tbxAd.Text);
            komut.Parameters.AddWithValue("@p3", tbxVergiYili.Text);
            komut.Parameters.AddWithValue("@p4", tbxFaaliyetBelgesiTarih.Text);
            komut.Parameters.AddWithValue("@p5", tbxİmzaSirkusuTarih.Text);
            komut.Parameters.AddWithValue("@p6", rtbxFirmaYetkili.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Ekleme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
            bgl.baglanti().Close();
            Listele();

        }

        private void Temizle()
        {
            tbxId.Text = "";
            mskVkn.Text = "";
            tbxAd.Text = "";
            tbxVergiYili.Text = "";
            tbxFaaliyetBelgesiTarih.Text = "";
            tbxİmzaSirkusuTarih.Text = "";
            rtbxFirmaYetkili.Text = "";
        }


        private void Listele()
        {
            OleDbCommand komut = new OleDbCommand("select Id AS [Müşteri No],VKN,FirmaAd AS [Firma Adı],VergiLevhasiYili AS [Vergi Levhası Yılı],FaaliyetBelgesiTarihi AS [Faaliyet Belgesi Tarihi],ImzaSirküsüTarihi AS [İmza Sirküsü Tarihi],FirmaYetkilileri AS [Firma Yetkilileri] from tbl_evraklar", bgl.baglanti());
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            

        }

        private void Filtre()
        {
            OleDbCommand komut = new OleDbCommand("select Id AS [Müşteri No],VKN,FirmaAd AS [Firma Adı],VergiLevhasiYili AS [Vergi Levhası Yılı],FaaliyetBelgesiTarihi AS [Faaliyet Belgesi Tarihi],ImzaSirküsüTarihi AS [İmza Sirküsü Tarihi],FirmaYetkilileri AS [Firma Yetkilileri] from tbl_evraklar where FirmaAd like '%" + tbxSearch.Text + "%'", bgl.baglanti());
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            //text change kullanmamın sebebi her bir harfe bastığımda filtreleme yaptırmak istemem
            Filtre();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("Delete from Tbl_Evraklar where Id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tbxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Silme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
            Listele();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("update Tbl_Evraklar set VKN=@p1,FirmaAd=@p2,VergiLevhasiYili=@p3,FaaliyetBelgesiTarihi=@p4,ImzaSirküsüTarihi=@p5,FirmaYetkilileri=@p6 where Id=@p7", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskVkn.Text);
            komut.Parameters.AddWithValue("@p2", tbxAd.Text);
            komut.Parameters.AddWithValue("@p3", tbxVergiYili.Text);
            komut.Parameters.AddWithValue("@p4", tbxFaaliyetBelgesiTarih.Text);
            komut.Parameters.AddWithValue("@p5", tbxİmzaSirkusuTarih.Text);
            komut.Parameters.AddWithValue("@p6", rtbxFirmaYetkili.Text);
            komut.Parameters.AddWithValue("@p7", tbxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
            Listele();

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
