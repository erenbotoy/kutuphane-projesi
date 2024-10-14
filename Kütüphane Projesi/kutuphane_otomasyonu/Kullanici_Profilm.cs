using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static kutuphane_otomasyonu.KullaniciGiris;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kutuphane_otomasyonu
{
    public partial class Kullanici_Profilm : Form
    {
        public Kullanici_Profilm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            KullaniciMenu frm = new KullaniciMenu();
            frm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (groupBox2.Visible == false)
            {
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible &= false;
            }
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        SqlDataReader dr;
        SqlCommand cmd;






        public void button2_Click(object sender, EventArgs e)
        {
            
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {

            string sifre = textBox6.Text;

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

            if (textBox5.Text == textBox6.Text)
            {
                MessageBox.Show("Yeni şifreniz mevcut şifre ile yeni şifreniz aynı olamaz", "!!!UYARI!!!");
            }

            else if (textBox6.Text != textBox7.Text)
            {
                label4.Visible = true;
            }

            else
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;

                cmd.CommandText = "Select * From Kullanicilar where Sifre='" + textBox5.Text + "'";

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string sorgu = "update Kullanicilar set Sifre=@Sifre where KU_ID=@KU_ID";
                    SqlCommand komut = new SqlCommand(sorgu, baglan);
                    komut.Parameters.AddWithValue("@KU_ID", Kullanici_ID.Id);
                    komut.Parameters.AddWithValue("@Sifre", textBox6.Text);


                    baglan.Open();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Şifreniz başarılı Şekilde güncellenmiştir!");
                    baglan.Close();
                    KullaniciGiris frm = new KullaniciGiris();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Şifrenizi yanlış girdiniz");
                }
            }

            con.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(groupBox3.Visible == false)
            {
                groupBox3.Visible = true;
            }
            else 
            { 
                groupBox3.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;

            cmd.CommandText = "Select * From Kullanicilar where TCno='" + maskedTextBox9.Text + "'";

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string sorgu = "update Kullanicilar set Telno=@Telno where KU_ID=@KU_ID";
                SqlCommand komut = new SqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@KU_ID", Kullanici_ID.Id);
                komut.Parameters.AddWithValue("@Telno", maskedTextBox8.Text);


                baglan.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Telefon numaranız başarılı Şekilde güncellenmiştir!");
                baglan.Close();
                groupBox3.Visible=false;

                string sorguu = "Select * From Kullanicilar where KU_ID=@KU_ID";

                // SQL komutu oluştur
                SqlCommand komutt = new SqlCommand(sorguu, baglan);
                komutt.Parameters.AddWithValue("@KU_ID", Kullanici_ID.Id);

                // Veritabanını aç
                baglan.Open();

                // SQL sorgusunu çalıştır ve SqlDataReader'ı al
                SqlDataReader oku = komutt.ExecuteReader();

                // Eğer bir satır varsa
                if (oku.Read())
                {
                    // Kullanıcı bilgilerini TextBox'lara yerleştir
                    maskedTextBox2.Text = oku["TCno"].ToString();
                    textBox1.Text = oku["İsim"].ToString();
                    textBox2.Text = oku["Soyİsim"].ToString();
                    maskedTextBox1.Text = oku["Telno"].ToString();
                    textBox3.Text = oku["Cinsiyet"].ToString();
                    textBox8.Text = oku["Sifre"].ToString();
                    textBox4.Text = oku["Dogumtarih"].ToString();
                }

                // Veritabanını kapat
                baglan.Close();

            }
            else
            {
                MessageBox.Show("Bilgileri yanlış girdiniz");
            }
        con.Close();
        }

        private void Kullanici_Profilm_Load(object sender, EventArgs e)
        {
            // Kullanici_Profilm sınıfındaki kullaniciId'yi al


            string sorgu = "Select * From Kullanicilar where KU_ID=@KU_ID";

            // SQL komutu oluştur
            SqlCommand komut = new SqlCommand(sorgu, baglan);
            komut.Parameters.AddWithValue("@KU_ID", Kullanici_ID.Id);

            // Veritabanını aç
            baglan.Open();

            // SQL sorgusunu çalıştır ve SqlDataReader'ı al
            SqlDataReader oku = komut.ExecuteReader();

            // Eğer bir satır varsa
            if (oku.Read())
            {
                // Kullanıcı bilgilerini TextBox'lara yerleştir
                maskedTextBox2.Text = oku["TCno"].ToString();
                textBox1.Text = oku["İsim"].ToString();
                textBox2.Text = oku["Soyİsim"].ToString();
                maskedTextBox1.Text = oku["Telno"].ToString();
                textBox3.Text = oku["Cinsiyet"].ToString();
                textBox8.Text = oku["Sifre"].ToString();
                textBox4.Text = oku["Dogumtarih"].ToString();
            }

            // Veritabanını kapat
            baglan.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void maskedTextBox5_TextChanged(object sender, EventArgs e)
        {
            string sifre = textBox1.Text;


            // Şifre uzunluğu kontrolü
            if (sifre.Length < 6 || sifre.Length > 12)
            {
                label2.ForeColor = Color.Red;
                //return;
            }
            else
            {
                label2.ForeColor = Color.Green;

            }

            // En az 1 adet büyük harf kontrolü
            if (!sifre.Any(char.IsUpper))
            {
                label3.ForeColor = Color.Red;
                //return;
            }
            else
            {
                label3.ForeColor = Color.Green;

            }

            // Özel karakter içerme kontrolü
            if (!sifre.Any(c => "!@#$%^&*()-_+=".Contains(c)))
            {
                label4.ForeColor = Color.Red;
                //return;
            }
            else
            {
                label4.ForeColor = Color.Green;

            }
            return;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string sifre = textBox6.Text;


            // Şifre uzunluğu kontrolü
            if (sifre.Length < 6 || sifre.Length > 18)
            {
                label10.ForeColor = Color.Red;
                //return;
            }
            else
            {
                label10.ForeColor = Color.Green;

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
