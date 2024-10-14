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
using static kutuphane_otomasyonu.KullaniciGiris;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace kutuphane_otomasyonu
{
    public partial class KullanıcıSifreDegistirme : Form
    {
        public KullanıcıSifreDegistirme()
        {
            InitializeComponent();
        }

        private void KullanıcıSifreDegistirme_Load(object sender, EventArgs e)
        {

        }

        private void ekrankapa_2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void alta_al2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            KullaniciGiris frm = new KullaniciGiris();
            frm.Show();
            this.Hide();
        }

        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        SqlDataReader dr;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");

        private void button3_Click(object sender, EventArgs e)
        {

            string sifre = textBox1.Text;

            if (sifre.Length < 6 || sifre.Length > 18)
            {
                MessageBox.Show("Şifreniz koşulları karşılamıyor", "!!!UYARI!!!");
                return;
            }

            // En az 1 adet büyük harf kontrolü
            if (!sifre.Any(char.IsUpper))
            {
                MessageBox.Show("Şifreniz koşulları karşılamıyor", "!!!UYARI!!!");
                return;
            }


            // Özel karakter içerme kontrolü
            if (!sifre.Any(c => "!@#$%^&*()-_+=".Contains(c)))
            {
                MessageBox.Show("Şifreniz koşulları karşılamıyor", "!!!UYARI!!!");
                return;
            }
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;

            

            cmd.CommandText = "Select * From Kullanicilar where TCno='" + maskedTextBox9.Text + "'";

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string sorgu = "update Kullanicilar set Sifre=@Sifre where TCno=@TCno";
                SqlCommand komut = new SqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@TCno",maskedTextBox9.Text);
                komut.Parameters.AddWithValue("@Sifre", textBox1.Text);

                baglan.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Şifreniz başarılı bir Şekilde güncellenmiştir!");
                baglan.Close();
                KullaniciGiris frm = new KullaniciGiris();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bilgileriniz yanlıştır");
            }
            con.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.UseSystemPasswordChar == true)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar= true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sifre = textBox1.Text;


            // Şifre uzunluğu kontrolü
            if (sifre.Length < 6 || sifre.Length > 18)
            {
                label11.ForeColor = Color.Red;
                //return;
            }
            else
            {
                label11.ForeColor = Color.Green;

            }

            // En az 1 adet büyük harf kontrolü
            if (!sifre.Any(char.IsUpper))
            {
                label17.ForeColor = Color.Red;
                //return;
            }
            else
            {
                label17.ForeColor = Color.Green;

            }

            // Özel karakter içerme kontrolü
            if (!sifre.Any(c => "!@#$%^&*()-_+=".Contains(c)))
            {
                label13.ForeColor = Color.Red;
                //return;
            }
            else
            {
                label13.ForeColor = Color.Green;

            }
            return;
        }
    }
}
