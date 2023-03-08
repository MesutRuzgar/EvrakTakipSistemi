using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvrakTakipSistemi
{
    public partial class IletısımKayıt : Form
    {
        public IletısımKayıt()
        {
            InitializeComponent();
        }
     
        
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                AnaForm anaForm = new AnaForm();
                AnaForm.tel=this.mskTelefon.Text;
                AnaForm.email = this.tbxEmail.Text;

                
                DialogResult = DialogResult.OK;
                MessageBox.Show("İletişim bilgileri başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch 
            {
           
                MessageBox.Show("İletişim bilgileri kaydedilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

       
    }
}
