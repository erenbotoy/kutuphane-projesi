using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphane_otomasyonu
{
    public partial class YoneticiGiris : Form
    {
        public YoneticiGiris()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AnaMenu frm = new AnaMenu();
            frm.Show();
            this.Hide();
        }

        private void alta_al2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        private void ekrankapa_2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void btn_giris_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select * From Yonetici where TCno='" + txtbx_ya.Text +
                "' AND Sifre='" + txtbx_sifre.Text + "'";

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Giriş başarılı!");
                

                string rol = dr["rol"].ToString().Trim();
                if (rol == "Yonetici")
                {
                    YoneticiMenu frm = new YoneticiMenu();
                    frm.Show();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("TC no ve şifre hatalı");
            }


            con.Close();
        }

        private void txtbx_sifre_Enter(object sender, EventArgs e)
        {
            txtbx_sifre.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtbx_sifre.isPassword == true)
            {
                txtbx_sifre.isPassword = false;
            }
            else
            {
                txtbx_sifre.isPassword = true;
            }
        }
    }
}
