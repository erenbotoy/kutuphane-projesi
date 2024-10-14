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
    public partial class YoneticiMenu : Form
    {
        public YoneticiMenu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AnaMenu frm = new AnaMenu();
            frm.Show();
            this.Hide();
        }

        private void alta_al2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ekrankapa_2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ke_Click(object sender, EventArgs e)
        {
            Calisan_EkleListele frm = new Calisan_EkleListele();
            frm.Show();
            this.Hide();
        }
    }
}
