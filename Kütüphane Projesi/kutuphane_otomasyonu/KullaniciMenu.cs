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
    public partial class KullaniciMenu : Form
    {
        public KullaniciMenu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AnaMenu frm = new AnaMenu();
            frm.Show();
            this.Close();
        }

        private void alta_al2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ekrankapa_2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Kullanici_KitapListele frm = new Kullanici_KitapListele();
            frm.Show();
            this.Hide();
        }

        private void btn_ke_Click(object sender, EventArgs e)
        {
            Kullanici_Profilm frm = new Kullanici_Profilm();
            frm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void KullaniciMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
