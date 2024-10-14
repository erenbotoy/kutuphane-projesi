using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kutuphane_otomasyonu
{
    public partial class Kullanici_EkleListele : Form
    {
        public Kullanici_EkleListele()
        {
            InitializeComponent();
        }
        
        SqlDataAdapter da;
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        



        void Ku_listele()
        {
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Kullanicilar", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds, "Kullanicilar");
            dataGridView1.DataSource = ds.Tables["Kullanicilar"];
            baglan.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CalisanMenu frm = new CalisanMenu();
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

        private void button1_Click(object sender, EventArgs e)
        {

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox1.Text) || string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(maskedTextBox2.Text) ||
                string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                DialogResult result = MessageBox.Show("Bazı alanlar boş bırakılmış.", "Uyarı");
                return; // Eğer alanlar boşsa işlemi sonlandır
            }

            string sifre = textBox3.Text;

            if (sifre.Length < 6 || sifre.Length > 18)
            {
                MessageBox.Show("Şifreniz koşulları karşılamıyor","!!!UYARI!!!");
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
            int dogumyili = dateTimePicker1.Value.Year;
            bool? durum;

            try
            {
                using (kimlik.KPSPublicSoapClient servis = new kimlik.KPSPublicSoapClient())
                {
                    durum = servis.TCKimlikNoDogrula(TCno, textBox1.Text, textBox2.Text, dogumyili);
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
                                baglan.Close();
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
                string insertSorgu = "INSERT INTO Kullanicilar (TCno, İsim, Soyİsim, Telno, Cinsiyet, Sifre, Dogumtarih,Kayittarih)" +
                   "VALUES (@TCno, @İsim, @Soyİsim, @Telno, @Cinsiyet, @Sifre, @Dogumtarih,@Kayittarih)";

                using (SqlCommand insertKomut = new SqlCommand(insertSorgu, baglan))
                {
                    insertKomut.Parameters.AddWithValue("@TCno", maskedTextBox1.Text);
                    insertKomut.Parameters.AddWithValue("@İsim", textBox1.Text);
                    insertKomut.Parameters.AddWithValue("@Soyİsim", textBox2.Text);
                    insertKomut.Parameters.AddWithValue("@Telno", maskedTextBox2.Text);
                    insertKomut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                    insertKomut.Parameters.AddWithValue("@Sifre", textBox3.Text);
                    insertKomut.Parameters.AddWithValue("@Dogumtarih", dateTimePicker1.Text);
                    insertKomut.Parameters.AddWithValue("@Kayittarih", dateTimePicker2.Text);
                    baglan.Open();
                    insertKomut.ExecuteNonQuery();
                    baglan.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    maskedTextBox1.Text = "";
                    maskedTextBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                }

                MessageBox.Show("Bilgiler doğru");


            }
            else
            {
                MessageBox.Show("Kayıtlarınızı kontrol edin");
            }
            Ku_listele();
            baglan.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(textBox3.UseSystemPasswordChar == true)
            {
                textBox3.UseSystemPasswordChar = false ;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true ;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Seçili Kullanıcıları silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        baglan.Open();

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            if (row.Cells["KU_ID"].Value != null && int.TryParse(row.Cells["KU_ID"].Value.ToString(), out int kitapId))
                            {
                                string sorgu = "DELETE FROM Kullanicilar WHERE KU_ID = @KU_ID";
                                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                                {
                                    komut.Parameters.AddWithValue("@KU_ID", kitapId);
                                    komut.ExecuteNonQuery();

                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                    maskedTextBox1.Text = "";
                                    maskedTextBox2.Text = "";
                                    textBox3.Text = "";
                                    comboBox1.Text = "";
                                    dateTimePicker1.Value = DateTime.Now;
                                }
                            }
                        }

                        baglan.Close();
                        MessageBox.Show("Seçili Kullanicilar silindi");
                        Ku_listele(); // Verileri tekrar yükleme işlemi
                    }
                    catch (SqlException )
                    {
                        MessageBox.Show("Hata: Emanet almış kullanıcıyı silemezsiniz","!!!UYARI!!!" );
                        textBox1.Text = "";
                        textBox2.Text = "";
                        maskedTextBox1.Text = "";
                        maskedTextBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir Kullanici seçin.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                string sifre = textBox3.Text;

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

                long TCno = long.Parse(maskedTextBox1.Text);
                int dogumyili = dateTimePicker1.Value.Year;
                bool? durum;

                try
                {
                    using (kimlik.KPSPublicSoapClient servis = new kimlik.KPSPublicSoapClient())
                    {
                        durum = servis.TCKimlikNoDogrula(TCno, textBox1.Text, textBox2.Text, dogumyili);
                        

                    }
                }

                catch
                {
                    durum = null;
                }

                if (durum == true)
                {
                    int KU_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["KU_ID"].Value);

                    string sorguu = "update Kullanicilar set TCno=@TCno,İsim=@İsim,Soyİsim=@Soyİsim,Telno=@Telno,Cinsiyet=@Cinsiyet, Sifre=@Sifre,Dogumtarih=@Dogumtarih where KU_ID=@KU_ID";

                    SqlCommand komutt = new SqlCommand(sorguu, baglan);
                    komutt.Parameters.AddWithValue("@KU_ID", KU_ID);
                    komutt.Parameters.AddWithValue("@TCno", maskedTextBox1.Text);
                    komutt.Parameters.AddWithValue("@İsim", textBox1.Text);
                    komutt.Parameters.AddWithValue("@Soyİsim", textBox2.Text);
                    komutt.Parameters.AddWithValue("@Telno", maskedTextBox2.Text);
                    komutt.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                    komutt.Parameters.AddWithValue("@Sifre", textBox3.Text);
                    komutt.Parameters.AddWithValue("@Dogumtarih", dateTimePicker1.Text);

                    baglan.Open();
                    komutt.ExecuteNonQuery();
                    MessageBox.Show("Değişiklik gerçekleştirildi.");
                    baglan.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    maskedTextBox1.Text = "";
                    maskedTextBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;

                }
                else
                {
                    MessageBox.Show("Kayıtlarınızı kontrol edin");
                }
            }
            Ku_listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int KU_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["KU_ID"].Value);

                string sorgu = "SELECT * FROM Kullanicilar WHERE KU_ID = @KU_ID";

                // SQL komutu oluştur
                SqlCommand komut = new SqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@KU_ID", KU_ID);

                // Veritabanını aç
                baglan.Open();

                // SQL sorgusunu çalıştır ve SqlDataReader'ı al
                SqlDataReader oku = komut.ExecuteReader();

                // Eğer bir satır varsa
                if (oku.Read())
                {
                    // Kitap bilgilerini TextBox'lara yerleştir
                    maskedTextBox1.Text = oku["TCno"].ToString();
                    textBox1.Text = oku["İsim"].ToString();
                    textBox2.Text = oku["Soyİsim"].ToString();
                    maskedTextBox2.Text = oku["Telno"].ToString();
                    comboBox1.Text = oku["Cinsiyet"].ToString();
                    textBox3.Text = oku["Sifre"].ToString();
                    dateTimePicker1.Text = oku["Dogumtarih"].ToString();
                }
                else
                {
                    MessageBox.Show("Veri bulunamadı.");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Hata oluştu: Tüm sütunu seçiniz","!!!UYARI!!!");
            }
            finally
            {
                // Veritabanını kapat
                baglan.Close();
            }
        }

        private void Kullanici_EkleListele_Load(object sender, EventArgs e)
        {
            Ku_listele();
        }

        private void maskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();

            //  da = new SqlDataAdapter("Select * from ogrenci where ortalama between " + textBox1.Text +" AND "+textBox2.Text , baglan);
            // da = new SqlDataAdapter("Select * from ogrenci where ortalama not between " + textBox1.Text + " AND " + textBox2.Text, baglan);
            da = new SqlDataAdapter("Select * from Kullanicilar where TCno LIKE'" + maskedTextBox4.Text + "%'", baglan);

            DataSet ds = new DataSet();
            da.Fill(ds, "Kullanicilar");
            dataGridView1.DataSource = ds.Tables["Kullanicilar"];

            baglan.Close();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string sifre = textBox3.Text;


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
