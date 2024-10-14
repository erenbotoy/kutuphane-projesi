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
    public partial class Kitap_EkleListele : Form
    {
        public Kitap_EkleListele()
        {
            InitializeComponent();
        }

        
        SqlDataAdapter da;

        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        

        void K_listele()
        {
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Kitaplar", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds, "Kitaplar");
            dataGridView1.DataSource = ds.Tables["Kitaplar"];
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

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) ||
            string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(comboBox1.Text) ||
            string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                DialogResult result = MessageBox.Show("Bazı alanlar boş bırakılmış. Yine de eklemek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    return; // Eklemeyi iptal et
                }
            }

            string sorgu = "INSERT INTO Kitaplar (KitapAdi, Yazar, BaskiYili, Kategori, Yayınevi)" +
                           "VALUES (@KitapAdi, @Yazar, @BaskiYili, @Kategori, @Yayınevi)";

            using (SqlCommand komut = new SqlCommand(sorgu, baglan))
            {
                komut.Parameters.AddWithValue("@KitapAdi", textBox1.Text);
                komut.Parameters.AddWithValue("@Yazar", textBox2.Text);
                komut.Parameters.AddWithValue("@BaskiYili", dateTimePicker1.Text);
                komut.Parameters.AddWithValue("@Kategori", comboBox1.Text);
                komut.Parameters.AddWithValue("@Yayınevi", textBox3.Text);

                baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();

                
            }

            MessageBox.Show("KAYIT BAŞARILI");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            K_listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int kitapID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Kid"].Value);

                string sorgu = "update Kitaplar set KitapAdi=@KitapAdi,Yazar=@Yazar,BaskiYili=@BaskiYili,Kategori=@Kategori,Yayınevi=@Yayınevi where Kid=@Kid";

                SqlCommand komut = new SqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@Kid", kitapID);
                komut.Parameters.AddWithValue("@KitapAdi", textBox1.Text);
                komut.Parameters.AddWithValue("@Yazar", textBox2.Text);
                komut.Parameters.AddWithValue("@BaskiYili", dateTimePicker1.Text);
                komut.Parameters.AddWithValue("@Kategori", comboBox1.Text);
                komut.Parameters.AddWithValue("@Yayınevi", textBox3.Text);

                baglan.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Değişiklik gerçekleştirildi.");
                baglan.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek bir kitap seçin.");
            }


            K_listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Seçili kitapları silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        baglan.Open();

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            if (row.Cells["Kid"].Value != null && int.TryParse(row.Cells["Kid"].Value.ToString(), out int kitapId))
                            {
                                string sorgu = "DELETE FROM Kitaplar WHERE Kid = @Kid";
                                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                                {
                                    komut.Parameters.AddWithValue("@Kid", kitapId);
                                    komut.ExecuteNonQuery();
                                }
                            }
                        }

                        MessageBox.Show("Seçili kitaplar silindi");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        baglan.Close();
                        K_listele(); // Verileri tekrar yükleme işlemi
                    }
                    catch (SqlException ex)
                    {
                        // SQL Server referans kısıtlama hatası
                        if (ex.Number == 547)
                        {
                            MessageBox.Show("Silme işlemi gerçekleştirilemedi. Emanet verilmiş kitabı silemezsiniz!","!!!Uyarı!!!");
                        }
                        else
                        {
                            MessageBox.Show("Hata oluştu: " + ex.Message);
                        }
                    }
                    finally
                    {
                        baglan.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir kitap seçin.");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Seçili satırın Kitap ID'sini al
                int kitapID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Kid"].Value);

                // Kitap bilgilerini getirmek için SQL sorgusu
                string sorgu = "SELECT * FROM Kitaplar WHERE Kid = @Kid";

                // SQL komutu oluştur
                SqlCommand komut = new SqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@Kid", kitapID);

                // Veritabanını aç
                baglan.Open();

                // SQL sorgusunu çalıştır ve SqlDataReader'ı al
                SqlDataReader oku = komut.ExecuteReader();

                // Eğer bir satır varsa
                if (oku.Read())
                {
                    // Kitap bilgilerini TextBox'lara yerleştir
                    textBox1.Text = oku["KitapAdi"].ToString();
                    textBox2.Text = oku["Yazar"].ToString();
                    dateTimePicker1.Text = oku["BaskiYili"].ToString();
                    comboBox1.Text = oku["Kategori"].ToString();
                    textBox3.Text = oku["Yayınevi"].ToString();
                }
                else
                {
                    MessageBox.Show("Seçilen kitap bulunamadı.");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Hata oluştu: Tüm sütunu seçiniz","!!!UYARI!!!" );
            }
            finally
            {
                // Veritabanını kapat
                baglan.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Kitap_EkleListele_Load(object sender, EventArgs e)
        {
            K_listele();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();

            //  da = new SqlDataAdapter("Select * from ogrenci where ortalama between " + textBox1.Text +" AND "+textBox2.Text , baglan);
            // da = new SqlDataAdapter("Select * from ogrenci where ortalama not between " + textBox1.Text + " AND " + textBox2.Text, baglan);
            da = new SqlDataAdapter("Select * from Kitaplar where KitapAdi LIKE'" + textBox4.Text + "%'", baglan);

            DataSet ds = new DataSet();
            da.Fill(ds, "Kitaplar");
            dataGridView1.DataSource = ds.Tables["Kitaplar"];

            baglan.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
