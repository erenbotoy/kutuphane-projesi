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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace kutuphane_otomasyonu
{
    public partial class Kullanici_KitapListele : Form
    {
        public Kullanici_KitapListele()
        {
            InitializeComponent();
        }
        
        CurrencyManager cm;
        SqlDataAdapter da;
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-UELODK0\\SQLEXPRESS;Initial Catalog=KUTUPHANE;Integrated Security=True");
        
        
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            // << En geriyi ifade eden buton 
            cm.Position = 0;
            label5.Text = (cm.Position + 1) + " / " + cm.Count;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // < Bir geriyi ifade eden buton
            cm.Position--;
            label5.Text = (cm.Position + 1) + " / " + cm.Count;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // > Bir ileriyi ifade eden buton
            cm.Position++;
            label5.Text = (cm.Position + 1) + " / " + cm.Count;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // >> En sonu ifade eden buton
            cm.Position = cm.Count - 1;
            label5.Text = (cm.Position + 1) + " / " + cm.Count;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void K_listele()
        {
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Kitaplar", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds, "Kitaplar");
            dataGridView1.DataSource = ds.Tables["Kitaplar"];
            cm = (CurrencyManager)BindingContext[ds.Tables["Kitaplar"]];
            baglan.Close();
        }
        private void Kullanici_KitapListele_Load(object sender, EventArgs e)
        {
            K_listele();
        }
    }
}
