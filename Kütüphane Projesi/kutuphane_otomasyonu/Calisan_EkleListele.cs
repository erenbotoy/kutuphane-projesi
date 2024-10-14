using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kutuphane_otomasyonu
{
    public partial class Calisan_EkleListele : Form
    {
        public Calisan_EkleListele()
        {
            InitializeComponent();
        }

        SqlDataAdapter da;
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");

        void C_listele()
        {
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Calisanlar", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds, "Calisanlar");
            dataGridView1.DataSource = ds.Tables["Calisanlar"];
            baglan.Close();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            YoneticiMenu frm = new YoneticiMenu();
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

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
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
            long TC = long.Parse(maskedTextBox1.Text);
            int Kayıt_Tarihi = dateTimePicker1.Value.Year;
            bool? durum;

            try
            {
                using (kimlik.KPSPublicSoapClient servis = new kimlik.KPSPublicSoapClient())
                {
                    durum = servis.TCKimlikNoDogrula(TC, textBox1.Text, textBox2.Text, Kayıt_Tarihi);
                    string sorgu = "SELECT TCno FROM Calisanlar WHERE TCno=@TCno";

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
                string insertSorgu = "INSERT INTO Calisanlar (TCno, İsim, Soyisim, Telno, Cinsiyet, Sifre, Kayıt_Tarihi,Dogumtarihi)" +
                   "VALUES (@TCno, @İsim, @Soyisim, @Telno, @Cinsiyet, @Sifre, @Kayıt_Tarihi,@Dogumtarihi)";

                using (SqlCommand insertKomut = new SqlCommand(insertSorgu, baglan))
                {
                    insertKomut.Parameters.AddWithValue("@TCno", maskedTextBox1.Text);
                    insertKomut.Parameters.AddWithValue("@İsim", textBox1.Text);
                    insertKomut.Parameters.AddWithValue("@Soyisim", textBox2.Text);
                    insertKomut.Parameters.AddWithValue("@Telno", maskedTextBox2.Text);
                    insertKomut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                    insertKomut.Parameters.AddWithValue("@Sifre", textBox3.Text);
                    insertKomut.Parameters.AddWithValue("@Kayıt_Tarihi", dateTimePicker2.Text);
                    insertKomut.Parameters.AddWithValue("@Dogumtarihi", dateTimePicker1.Text); 
                    baglan.Open();
                    insertKomut.ExecuteNonQuery();
                    
                    
                }
                MessageBox.Show("Bilgiler doğru");
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

            C_listele();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(textBox3.UseSystemPasswordChar == true)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

                long TC = long.Parse(maskedTextBox1.Text);
                int Kayıt_Tarihi = dateTimePicker1.Value.Year;
                bool? durum;

                try
                {
                    using (kimlik.KPSPublicSoapClient servis = new kimlik.KPSPublicSoapClient())
                    {
                        durum = servis.TCKimlikNoDogrula(TC, textBox1.Text, textBox2.Text, Kayıt_Tarihi);
           
                    }
                }

                catch
                {
                    durum = null;
                }

                if (durum == true)
                {
                    int CID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CID"].Value);

                    string sorgu = "update Calisanlar set TCno=@TCno,İsim=@İsim,Soyİsim=@Soyİsim,Telno=@Telno,Cinsiyet=@Cinsiyet, Sifre=@Sifre,Kayıt_Tarihi=@Kayıt_Tarihi where CID=@CID";

                    SqlCommand komut = new SqlCommand(sorgu, baglan);
                    komut.Parameters.AddWithValue("@CID", CID);
                    komut.Parameters.AddWithValue("@TCno", maskedTextBox1.Text);
                    komut.Parameters.AddWithValue("@İsim", textBox1.Text);
                    komut.Parameters.AddWithValue("@Soyİsim", textBox2.Text);
                    komut.Parameters.AddWithValue("@Telno", maskedTextBox2.Text);
                    komut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                    komut.Parameters.AddWithValue("@Sifre", textBox3.Text);
                    komut.Parameters.AddWithValue("@Kayıt_Tarihi", dateTimePicker1.Text);

                    baglan.Open();
                    komut.ExecuteNonQuery();
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
                    baglan.Close();
                }
                else
                {
                    MessageBox.Show("Bilgilerinizi Kontrol Ediniz", "!!!UYARI!!!");
                }

            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek bir Calışan seçin.");
            }


            C_listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int CID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CID"].Value);

                string sorgu = "SELECT * FROM Calisanlar WHERE CID = @CID";

                // SQL komutu oluştur
                SqlCommand komut = new SqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@CID", CID);

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
                    dateTimePicker1.Text = oku["Dogumtarihi"].ToString();
                    dateTimePicker2.Text = oku["Kayıt_Tarihi"].ToString();
                }
                else
                {
                    MessageBox.Show("Veri bulunamadı.");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Hata oluştu:Tüm sütunu seçiniz", "!!!UYARI!!!" );
            }
            finally
            {
                // Veritabanını kapat
                baglan.Close();
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void Calisan_EkleListele_Load(object sender, EventArgs e)
        {
            C_listele();
        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();

            //  da = new SqlDataAdapter("Select * from ogrenci where ortalama between " + textBox1.Text +" AND "+textBox2.Text , baglan);
            // da = new SqlDataAdapter("Select * from ogrenci where ortalama not between " + textBox1.Text + " AND " + textBox2.Text, baglan);
            da = new SqlDataAdapter("Select * from Calisanlar where TCno LIKE'" + maskedTextBox4.Text + "%'", baglan);

            DataSet ds = new DataSet();
            da.Fill(ds, "Calisanlar");
            dataGridView1.DataSource = ds.Tables["Calisanlar"];
         

            baglan.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Seçili Çalışanı silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    {
                        baglan.Open();

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            if (row.Cells["CID"].Value != null && int.TryParse(row.Cells["CID"].Value.ToString(), out int CID))
                            {
                                string sorgu = "DELETE FROM Calisanlar WHERE CID = @CID";
                                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                                {
                                    komut.Parameters.AddWithValue("@CID", CID);
                                    komut.ExecuteNonQuery();

                                    
                                }
                            }
                        }

                        baglan.Close();
                        MessageBox.Show("Seçili Calisanlar silindi");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        maskedTextBox1.Text = "";
                        maskedTextBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now;
                        C_listele(); // Verileri tekrar yükleme işlemi
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir Calisan seçin.");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string sifre = textBox3.Text;


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
