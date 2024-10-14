using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphane_otomasyonu
{
    public partial class CalisanMenu : Form
    {
        public CalisanMenu()
        {
            InitializeComponent();
        }



        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_ke_Click(object sender, EventArgs e)
        {
            Kitap_EkleListele frm = new Kitap_EkleListele();
            frm.Show();
            this.Hide();
        }

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AnaMenu frm = new AnaMenu();
            frm.Show();
            this.Close();
        }

        private void btn_ue_Click(object sender, EventArgs e)
        {
            Kullanici_EkleListele frm = new Kullanici_EkleListele();
            frm.Show();
            this.Hide();
        }

        private void btn_ee_Click(object sender, EventArgs e)
        {
            Emanet_EkleListele frm = new Emanet_EkleListele();
            frm.Show();
            this.Hide();
        }
    }
}
