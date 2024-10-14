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
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void alta_al2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ekrankapa_2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            var newButton = new Bunifu.Framework.UI.BunifuFlatButton();
            this.Controls.Add(newButton);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            CalisanGiris frm = new CalisanGiris();
            frm.Show();
            this.Hide();
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {
            Hakkinda frm = new Hakkinda();
            frm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            KullaniciGiris frm = new KullaniciGiris();
            frm.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            YoneticiGiris frm = new YoneticiGiris();
            frm.Show();
            this.Hide();
        }
    }
}
