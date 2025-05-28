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

namespace edizStokOdevi
{
    public partial class Form5 : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;");


        public Form5()
        {
            InitializeComponent();
        }

        private void aNASAYFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void üRÜNLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void üRÜNSİLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
            this.Hide();
        }

        private void üRÜNLERToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void üRÜNSİLToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void sTOKTAKİBİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void sATIŞYAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }

        private void çIKIŞYAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Çıkış Yapıldı.");

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void aNASAYFAToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }



        private void ListeleUrunler()
        {
            try
            {
                string query = @"SELECT u.id, u.urun_adi, k.kategori_adi, u.adet, u.fiyat
                         FROM urunler u
                         JOIN kategoriler k ON u.kategori_id = k.id";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // Formdan verileri al
                string urunAdi = textBox1.Text;
                int kategoriId = Convert.ToInt32(comboBox1.SelectedValue); // ÖNEMLİ: kategori ID geliyor
                int adet = Convert.ToInt32(textBox2.Text);
                decimal fiyat = Convert.ToDecimal(textBox3.Text);

                // SQL sorgusu
                string query = "INSERT INTO urunler (urun_adi, kategori_id, fiyat, adet, durum) VALUES (@adi, @kategori, @fiyat, @adet, 1)";
                SqlCommand cmd = new SqlCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@adi", urunAdi);
                cmd.Parameters.AddWithValue("@kategori", kategoriId);
                cmd.Parameters.AddWithValue("@fiyat", fiyat);
                cmd.Parameters.AddWithValue("@adet", adet);

                // Bağlantıyı aç ve sorguyu çalıştır
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Ürün başarıyla eklendi.");
                ListeleUrunler(); // datagrid'i güncelle
            }
            catch (Exception ex)
            {
                connection.Close(); // Hata durumunda da kapat
                MessageBox.Show("Hata: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
                string query = "SELECT * FROM urunler";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT id, kategori_adi FROM kategoriler";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "kategori_adi";
                comboBox1.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategori yüklenemedi: " + ex.Message);
            }
        }
    }
}
