using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphane_otomasyonu
{
    public partial class Kullanici_Kayitolma : Form
    {
        public Kullanici_Kayitolma()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(textBox1.UseSystemPasswordChar == true)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
            }
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox1.Text) || string.IsNullOrEmpty(textBox3.Text) ||
               string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(maskedTextBox2.Text) ||
               string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(textBox1.Text) ||
               string.IsNullOrEmpty(dateTimePicker2.Text))
            {
                DialogResult result = MessageBox.Show("Bazı alanlar boş bırakılmış.", "Uyarı");
                return; // Eğer alanlar boşsa işlemi sonlandır
            }


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

            // TC Kimlik Kontrolü
            long TCno = long.Parse(maskedTextBox1.Text);
            int dogumyili = dateTimePicker2.Value.Year;
            bool? durum;

            try
            {
                using (kimlik.KPSPublicSoapClient servis = new kimlik.KPSPublicSoapClient())
                {
                    durum = servis.TCKimlikNoDogrula(TCno, textBox3.Text, textBox6.Text, dogumyili);
                    string sorgu = "SELECT TCno FROM Kullanicilar WHERE TCno=@TCno";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                    {
                        komut.Parameters.AddWithValue("@TCno", maskedTextBox1.Text);

                        baglan.Open();

                        using (SqlDataReader reader = komut.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Eğer TCno zaten varsa
                                MessageBox.Show("Bu TC no zaten kayıtlı.");
                                return;
                            }

                        }

                        baglan.Close();
                    }


                }
            }
            catch
            {
                durum = null;
            }

            if (durum == true)
            {
                // Eğer TCno yoksa, yeni kayıt ekleyebilirsiniz
                string insertSorgu = "INSERT INTO Kullanicilar (TCno, İsim, Soyİsim, Telno, Cinsiyet, Sifre, Dogumtarih)" +
                   "VALUES (@TCno, @İsim, @Soyİsim, @Telno, @Cinsiyet, @Sifre, @Dogumtarih)";

                using (SqlCommand insertKomut = new SqlCommand(insertSorgu, baglan))
                {
                    insertKomut.Parameters.AddWithValue("@TCno", maskedTextBox1.Text);
                    insertKomut.Parameters.AddWithValue("@İsim", textBox3.Text);
                    insertKomut.Parameters.AddWithValue("@Soyİsim", textBox6.Text);
                    insertKomut.Parameters.AddWithValue("@Telno", maskedTextBox2.Text);
                    insertKomut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                    insertKomut.Parameters.AddWithValue("@Sifre", textBox1.Text);
                    insertKomut.Parameters.AddWithValue("@Dogumtarih", dateTimePicker2.Text);
                    baglan.Open();
                    insertKomut.ExecuteNonQuery();
                    baglan.Close();
                }

                MessageBox.Show("Bilgiler doğru. Kaydınız gerçekleşmiştir");
                KullaniciGiris frm = new KullaniciGiris();
                frm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Kayıtlarınızı kontrol edin");
            }


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            KullaniciGiris frm = new KullaniciGiris();
            frm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
    }
   }

