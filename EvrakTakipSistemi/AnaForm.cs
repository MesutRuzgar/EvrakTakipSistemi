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
    public partial class AnaForm : Form
    {
        public AnaForm()
        {

            InitializeComponent();

        }

        DbBaglanti bgl = new DbBaglanti();

        public static string tel;
        public static string email;


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
            catch
            {

                MessageBox.Show("Geçersiz işlem!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(tbxAd.Text) && string.IsNullOrEmpty(mskVkn.Text))
            {
                MessageBox.Show("Lütfen VKN ve Firma Ad bölümlerini doldurduğunuzdan emin olunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(tbxAd.Text))
            {
                MessageBox.Show("Lütfen Firma Ad bölümünü doldurduğunuzdan emin olunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxAd.Focus();
            }
            else if (string.IsNullOrEmpty(mskVkn.Text))
            {
                MessageBox.Show("Lütfen VKN bölümünü doldurduğunuzdan emin olunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskVkn.Focus();
            }
            else if (mskVkn.Text.Length < 10)
            {
                MessageBox.Show("Lütfen geçerli bir VKN giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskVkn.Focus();
            }

            else
            {
                SqlCommand komut = new SqlCommand("ADDCUSTOMER", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.AddWithValue("@TaxIdentificationNumber", mskVkn.Text);
                komut.Parameters.AddWithValue("@CompanyName", tbxAd.Text);
                DialogResult result = MessageBox.Show("İletişim bilgileri eklemek istiyor musunuz?", "Bilgilendirme Penceresi ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    IletısımKayıt fr = new IletısımKayıt();
                    DialogResult result2 = fr.ShowDialog();
                    if (result2 == DialogResult.OK)
                    {
                        

                        komut.Parameters.AddWithValue("@Phone", tel);
                        komut.Parameters.AddWithValue("@Email", email);
                    }

                    else
                    {
                        komut.Parameters.AddWithValue("@Phone", DBNull.Value);
                        komut.Parameters.AddWithValue("@Email", DBNull.Value);
                    }
                    
                }
                else
                {
                    komut.Parameters.AddWithValue("@Phone", DBNull.Value);
                    komut.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(tbxVergiYili.Text))
                {
                    komut.Parameters.AddWithValue("@TaxPlateYear", tbxVergiYili.Text);
                }
                else
                {
                    komut.Parameters.AddWithValue("@TaxPlateYear", DBNull.Value);
                }


                if (!string.IsNullOrEmpty(tbxFaaliyetBelgesiTarih.Text))
                {
                    komut.Parameters.AddWithValue("@ActivityCertificateDate", tbxFaaliyetBelgesiTarih.Text);
                }
                else
                {
                    komut.Parameters.AddWithValue("@ActivityCertificateDate", DBNull.Value);
                }

                if (!string.IsNullOrEmpty(tbxİmzaSirkusuTarih.Text))
                {
                    komut.Parameters.AddWithValue("@SignatureCircularDate", tbxİmzaSirkusuTarih.Text);
                }
                else
                {
                    komut.Parameters.AddWithValue("@SignatureCircularDate", DBNull.Value);
                }
                komut.Parameters.AddWithValue("@CompanyOfficials", rtbxFirmaYetkili.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Ekleme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                bgl.baglanti().Close();
                Listele();

            }



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
            SqlCommand komut = new SqlCommand("GETALL", bgl.baglanti());
            komut.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Gecerlimi();

        }

        private void Gecerlimi()
        {
            string dbFaaliyetYili;
            string dbImzaYili;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewCellStyle renk = new DataGridViewCellStyle();
                DateTime bugun = DateTime.Now;

                if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[5].Value.ToString()))

                {
                    //imza sirküleri tarihi için
                    dbImzaYili = dataGridView1.Rows[i].Cells["İMZA SİRKÜLER TARİHİ"].Value.ToString();
                    DateTime dtDbImzaYili = DateTime.Parse(dbImzaYili);
                    if (dtDbImzaYili >= bugun)
                    {
                        renk.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        renk.BackColor = Color.Firebrick;
                    }
                    dataGridView1.Rows[i].Cells["İMZA SİRKÜLER TARİHİ"].Style.BackColor = renk.BackColor;
                }

                if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["FAALİYET BELGESİ TARİHİ"].Value.ToString()))
                {
                    //faaliyet belgesi tarihi için
                    dbFaaliyetYili = dataGridView1.Rows[i].Cells["FAALİYET BELGESİ TARİHİ"].Value.ToString();
                    DateTime dtDbFaaliyet = DateTime.Parse(dbFaaliyetYili);
                    DateTime gecerliTarih = dtDbFaaliyet.AddDays(60);
                    if (gecerliTarih >= bugun)
                    {
                        renk.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        renk.BackColor = Color.Firebrick;
                    }
                    dataGridView1.Rows[i].Cells["FAALİYET BELGESİ TARİHİ"].Style.BackColor = renk.BackColor;
                }

                if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["VERGİ LEVHASI YILI"].Value.ToString()))
                {
                    //vergi levhası yılı için
                    int buYil = bugun.Year;
                    int vergiYili = buYil - 1;
                    if (dataGridView1.Rows[i].Cells["VERGİ LEVHASI YILI"].Value.ToString() == Convert.ToString(vergiYili))
                    {
                        renk.BackColor = Color.YellowGreen;

                    }
                    else
                    {
                        renk.BackColor = Color.Firebrick;
                    }
                    dataGridView1.Rows[i].Cells["VERGİ LEVHASI YILI"].Style.BackColor = renk.BackColor;
                }


                else
                {
                    renk.BackColor = Color.White;
                }

            }
        }


        private void Filtre()
        {
            //SqlCommand komut = new SqlCommand("select Id AS [Müşteri No],VKN,FirmaAd AS [Firma Adı],VergiLevhasiYili AS [Vergi Levhası Yılı],FaaliyetBelgesiTarihi AS [Faaliyet Belgesi Tarihi],ImzaSirküsüTarihi AS [İmza Sirküsü Tarihi],FirmaYetkilileri AS [Firma Yetkilileri] from Customers where FirmaAd like '%" + tbxSearch.Text + "%'", bgl.baglanti());
            //SqlDataAdapter da = new SqlDataAdapter(komut);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //Gecerlimi();
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            //text change kullanmamın sebebi her bir harfe bastığımda filtreleme yaptırmak istemem
            Filtre();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from Customers where Id=@p1", bgl.baglanti());
            if (string.IsNullOrEmpty(tbxId.Text))
            {
                MessageBox.Show("Lütfen silmek istediğiniz firmayı seçtiğinizden emin olunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                komut.Parameters.AddWithValue("@p1", tbxId.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Silme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                Listele();
            }


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxId.Text))
            {
                SqlCommand komut = new SqlCommand("update Customers set TaxIdentificationNumber=@p1,CompanyName=@p2,TaxPlateYear=@p3,ActivityCertificateDate=@p4,SignatureCircularDate=@p5,CompanyOfficials=@p6 where Id=@p7", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", mskVkn.Text);
                komut.Parameters.AddWithValue("@p2", tbxAd.Text);
                if (!string.IsNullOrEmpty(tbxVergiYili.Text))
                {
                    komut.Parameters.AddWithValue("@p3", tbxVergiYili.Text);
                }
                else
                {
                    komut.Parameters.AddWithValue("@p3", DBNull.Value);
                }

                if (!string.IsNullOrEmpty(tbxFaaliyetBelgesiTarih.Text))
                {
                    komut.Parameters.AddWithValue("@p4", tbxFaaliyetBelgesiTarih.Text);
                }
                else
                {
                    komut.Parameters.AddWithValue("@p4", DBNull.Value);
                }

                if (!string.IsNullOrEmpty(tbxİmzaSirkusuTarih.Text))
                {
                    komut.Parameters.AddWithValue("@p5", tbxİmzaSirkusuTarih.Text);
                }
                else
                {
                    komut.Parameters.AddWithValue("@p5", DBNull.Value);
                }
                komut.Parameters.AddWithValue("@p6", rtbxFirmaYetkili.Text);
                komut.Parameters.AddWithValue("@p7", tbxId.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Güncelleme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                Listele();
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz firmayı seçtiğinizden emin olunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            //datagridviev üzerinde sütun baslığına tıklayıp programın hata vermesini önlemek amacıyla bu özelligi iptal ettim
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void tbxIletisim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxId.Text))
            {
                MessageBox.Show("Lütfen bir müşteri seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                IletisimFormu frm = new IletisimFormu();
                frm.id = tbxId.Text;
                frm.Show();
            }

        }
    }
}
