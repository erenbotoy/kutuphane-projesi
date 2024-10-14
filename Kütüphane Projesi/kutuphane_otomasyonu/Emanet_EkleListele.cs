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
    public partial class Emanet_EkleListele : Form
    {
        public Emanet_EkleListele()
        {
            InitializeComponent();
        }
        
        SqlDataAdapter da;
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        
       


        void E_listele()
        {
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Kitaplar", baglan);
            SqlDataAdapter daa = new SqlDataAdapter("Select * from Kullanicilar", baglan);
            SqlDataAdapter daaa = new SqlDataAdapter("Select * from Emanet ", baglan);
            DataSet ds = new DataSet();
            DataSet dss = new DataSet();
            DataSet dsss = new DataSet();
            da.Fill(ds, "Kitaplar");
            daa.Fill(dss, "Kullanicilar");
            daaa.Fill(dsss, "Emanet");
            dataGridView1.DataSource = ds.Tables["Kitaplar"];
            dataGridView2.DataSource = dss.Tables["Kullanicilar"];
            dataGridView3.DataSource = dsss.Tables["Emanet"];


            baglan.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CalisanMenu frm = new CalisanMenu();
            frm.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                {
                    komut.Parameters.AddWithValue("@Kid", kitapID);

                    // Veritabanını aç
                    baglan.Open();

                    // SQL sorgusunu çalıştır ve SqlDataReader'ı al
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        // Eğer bir satır varsa
                        if (oku.Read())
                        {
                            // Kitap bilgilerini TextBox'lara yerleştir
                            textBox1.Text = oku["KitapAdi"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen Kitap ID bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception )
            {
                // Hata durumunda kullanıcıya bilgi ver
                MessageBox.Show("Bir hata oluştu: Tüm sütunu seçiniz","!!!UYARI!!!" );
            }
            finally
            {
                // Veritabanını kapat
                baglan.Close();
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Seçili satırın Kitap ID'sini al
                int KU_ID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["KU_ID"].Value);

                // Kitap bilgilerini getirmek için SQL sorgusu
                string sorgu = "SELECT * FROM Kullanicilar WHERE KU_ID = @KU_ID";

                // SQL komutu oluştur
                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                {
                    komut.Parameters.AddWithValue("@KU_ID", KU_ID);

                    // Veritabanını aç
                    baglan.Open();

                    // SQL sorgusunu çalıştır ve SqlDataReader'ı al
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        // Eğer bir satır varsa
                        if (oku.Read())
                        {
                            // Kitap bilgilerini TextBox'lara yerleştir
                            maskedTextBox2.Text = oku["TCno"].ToString();
                            textBox3.Text = oku["İsim"].ToString();
                            textBox4.Text = oku["Soyİsim"].ToString();
                            maskedTextBox1.Text = oku["Telno"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen Kullanıcı ID bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception )
            {
                // Hata durumunda kullanıcıya bilgi ver
                MessageBox.Show("Bir hata oluştu: Tüm sütunu seçiniz", "!!!UYARI!!!");
            }
            finally
            {
                // Veritabanını kapat
                baglan.Close();
            }

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    int Kid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Kid"].Value);
                    int KU_ID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["KU_ID"].Value);

                    string kontrolSorgu = "SELECT Durum FROM Kitaplar WHERE Kid = @Kid";
                    
                    using (SqlCommand kontrolKomut = new SqlCommand(kontrolSorgu, baglan))
                    {
                        kontrolKomut.Parameters.AddWithValue("@Kid", Kid);
                        kontrolKomut.Parameters.AddWithValue("@KU_ID", KU_ID); // KU_ID parametresini ekle
                        baglan.Open();
                        object durum = kontrolKomut.ExecuteScalar();
                        baglan.Close();

                        if (durum != null && durum.ToString() == "Mevcut")
                        {
                            // Kitap mevcut, emanet işlemi gerçekleştir
                            string sorgu = "INSERT INTO Emanet (Kid,KU_ID,KitapAdi, TCno, İsim, Soyİsim, Telno, Alinan_tarih, Teslim_tarih) " +
                                "VALUES (@Kid,@KU_ID,@KitapAdi, @TCno, @İsim, @Soyİsim, @Telno, @Alinan_tarih, @Teslim_tarih) " +
                                "UPDATE Kitaplar SET Durum = 'Mevcut Değil' WHERE Kid = @Kid";

                            using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                            {
                                komut.Parameters.AddWithValue("@Kid", Kid);
                                komut.Parameters.AddWithValue("@KU_ID", KU_ID);
                                komut.Parameters.AddWithValue("@KitapAdi", textBox1.Text);
                                komut.Parameters.AddWithValue("@TCno", maskedTextBox2.Text);
                                komut.Parameters.AddWithValue("@İsim", textBox3.Text);
                                komut.Parameters.AddWithValue("@Soyİsim", textBox4.Text);
                                komut.Parameters.AddWithValue("@Telno", maskedTextBox1.Text);
                                komut.Parameters.AddWithValue("@Alinan_tarih", dateTimePicker1.Text);
                                komut.Parameters.AddWithValue("@Teslim_tarih", dateTimePicker2.Text);

                                baglan.Open();
                                komut.ExecuteNonQuery();
                                baglan.Close();
                                MessageBox.Show("Emanet verildi.");
                                textBox1.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                maskedTextBox1.Text = "";
                                maskedTextBox2.Text = "";
                                dateTimePicker2.Value = DateTime.Now;
                                dateTimePicker1.Value = DateTime.Now;
                                E_listele();
                            }
                        }

                        else
                        {
                            MessageBox.Show("Mevcut bir kitap seçiniz.", "!! Uyarı !!");
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Bir kullanıcı seçiniz.", "!! Uyarı !! ");
                }
            }
            else
            {
                MessageBox.Show("Bir kitap seçiniz", "!! Uyarı !!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Seçili emaneti almak istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    baglan.Open();

                    foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                    {
                        if (row.Cells["EID"].Value != null && int.TryParse(row.Cells["EID"].Value.ToString(), out int EID))
                        {



                            if (row.Cells["Kid"].Value != null && int.TryParse(row.Cells["Kid"].Value.ToString(), out int Kid))
                            {
                                // UPDATE sorgusu
                                string updateSorgu = "UPDATE Kitaplar SET Durum ='Mevcut' WHERE Kid=@Kid";
                                using (SqlCommand updateKomut = new SqlCommand(updateSorgu, baglan))
                                {
                                    updateKomut.Parameters.AddWithValue("@Kid", Kid);
                                    updateKomut.ExecuteNonQuery();
                                }

                            }

                            // DELETE sorgusu
                            string deleteSorgu = "DELETE FROM Emanet WHERE EID = @EID";
                            using (SqlCommand deleteKomut = new SqlCommand(deleteSorgu, baglan))
                            {
                                deleteKomut.Parameters.AddWithValue("@EID", EID);
                                deleteKomut.ExecuteNonQuery();
                            }

                        }
                    }

                    baglan.Close();
                    MessageBox.Show("Seçili emanet alındı");
                    E_listele();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Emanet seçiniz.", "!! UYARI !!");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Emanet_EkleListele_Load(object sender, EventArgs e)
        {
            E_listele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();

            //  da = new SqlDataAdapter("Select * from ogrenci where ortalama between " + textBox1.Text +" AND "+textBox2.Text , baglan);
            // da = new SqlDataAdapter("Select * from ogrenci where ortalama not between " + textBox1.Text + " AND " + textBox2.Text, baglan);
            da = new SqlDataAdapter("Select * from Kitaplar where KitapAdi LIKE'" + textBox2.Text + "%'", baglan);

            DataSet ds = new DataSet();
            da.Fill(ds, "Kitaplar");
            dataGridView1.DataSource = ds.Tables["Kitaplar"];

            baglan.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
           
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();

            //  da = new SqlDataAdapter("Select * from ogrenci where ortalama between " + textBox1.Text +" AND "+textBox2.Text , baglan);
            // da = new SqlDataAdapter("Select * from ogrenci where ortalama not between " + textBox1.Text + " AND " + textBox2.Text, baglan);
            da = new SqlDataAdapter("Select * from Kullanicilar where TCno LIKE'" + maskedTextBox3.Text + "%'", baglan);
           

            DataSet ds = new DataSet();
           
            da.Fill(ds, "Kullanicilar");
           
            dataGridView2.DataSource = ds.Tables["Kullanicilar"];
            

            baglan.Close();
        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
 
        }

        private void maskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("Select * from Emanet where TCno LIKE'" + maskedTextBox4.Text + "%'", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds, "Emanet");
            dataGridView3.DataSource = ds.Tables["Emanet"];
        }
    }
}
