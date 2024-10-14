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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kutuphane_otomasyonu
{
    public partial class KullaniciGiris : Form
    {
        public KullaniciGiris()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;

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

        private void ekrankapa_2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static class Kullanici_ID
        {
             public static int Id; // veya uygun türde bir alan/özellik
                                        // Diğer özellikleri ekleyin
        }

        private void btn_giris_Click(object sender, EventArgs e)
        {


            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select * From Kullanicilar where TCno='" + txtbx_ya.Text +
                "' AND Sifre='" + txtbx_sifre.Text + "'";

            

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Giriş başarılı!");
                Kullanici_ID.Id = (int)dr["KU_ID"];
                


                string rol = dr["rol"].ToString().Trim();
                if (rol == "Kullanici")
                {
                    KullaniciMenu frm = new KullaniciMenu();
                    frm.Show();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("TC no  ve şifre hatalı");
                linkLabel2.Visible = true;
            }


            con.Close();
    }
        


        private void txtbx_sifre_Enter(object sender, EventArgs e)
        {
            txtbx_sifre.Text = "";
        }

        private void txtbx_ya_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kullanici_Kayitolma frm = new Kullanici_Kayitolma();
            frm.Show();
            this.Hide(); ;
            
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

        private void KullaniciGiris_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KullanıcıSifreDegistirme frm = new KullanıcıSifreDegistirme();
            frm.Show();
        }

        private void txtbx_ya_Enter(object sender, EventArgs e)
        {

        }
    }
}
