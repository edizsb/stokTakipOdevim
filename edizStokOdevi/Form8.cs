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

namespace edizStokOdevi
{
    public partial class Form8 : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;");
        public Form8()
        {
            InitializeComponent();
        }

        private void üRÜNLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void üRÜNEKŞEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
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

        private void aNASAYFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // GridView'e tüm ürünleri bas
                string query = "SELECT * FROM urunler";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                dataGridView1.DataSource = dt;

                // label5 – Toplam ürün adedi
                string toplamQuery = "SELECT SUM(adet) FROM urunler";
                SqlCommand toplamCmd = new SqlCommand(toplamQuery, connection);
                connection.Open();
                object toplamSonuc = toplamCmd.ExecuteScalar();
                connection.Close();
                label5.Text = toplamSonuc != DBNull.Value ? toplamSonuc.ToString() : "0";

                // label6 – Farklı kategori sayısı
                string kategoriQuery = "SELECT COUNT(DISTINCT kategori_id) FROM urunler";
                SqlCommand kategoriCmd = new SqlCommand(kategoriQuery, connection);
                connection.Open();
                object kategoriSonuc = kategoriCmd.ExecuteScalar();
                connection.Close();
                label6.Text = kategoriSonuc.ToString();

                // label7 – Bitmek üzere olan ürün (minimum adetli)
                string minQuery = "SELECT TOP 1 urun_adi FROM urunler ORDER BY adet ASC";
                SqlCommand minCmd = new SqlCommand(minQuery, connection);
                connection.Open();
                object minSonuc = minCmd.ExecuteScalar();
                connection.Close();
                label7.Text = minSonuc?.ToString() ?? "Yok";

                // label8 – En çok bulunan ürün (maksimum adetli)
                string maxQuery = "SELECT TOP 1 urun_adi FROM urunler ORDER BY adet DESC";
                SqlCommand maxCmd = new SqlCommand(maxQuery, connection);
                connection.Open();
                object maxSonuc = maxCmd.ExecuteScalar();
                connection.Close();
                label8.Text = maxSonuc?.ToString() ?? "Yok";
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }


        private void StokEtiketleriniGuncelle()
        {
            try
            {
                // label5 – Toplam ürün adedi
                string toplamQuery = "SELECT SUM(adet) FROM urunler";
                SqlCommand toplamCmd = new SqlCommand(toplamQuery, connection);
                connection.Open();
                object toplamSonuc = toplamCmd.ExecuteScalar();
                connection.Close();
                label5.Text = toplamSonuc != DBNull.Value ? toplamSonuc.ToString() : "0";

                // label6 – Farklı kategori sayısı
                string kategoriQuery = "SELECT COUNT(DISTINCT kategori_id) FROM urunler";
                SqlCommand kategoriCmd = new SqlCommand(kategoriQuery, connection);
                connection.Open();
                object kategoriSonuc = kategoriCmd.ExecuteScalar();
                connection.Close();
                label6.Text = kategoriSonuc.ToString();

                // label7 – Bitmek üzere olan ürün
                string minQuery = "SELECT TOP 1 urun_adi FROM urunler ORDER BY adet ASC";
                SqlCommand minCmd = new SqlCommand(minQuery, connection);
                connection.Open();
                object minSonuc = minCmd.ExecuteScalar();
                connection.Close();
                label7.Text = minSonuc?.ToString() ?? "Yok";

                // label8 – En çok bulunan ürün
                string maxQuery = "SELECT TOP 1 urun_adi FROM urunler ORDER BY adet DESC";
                SqlCommand maxCmd = new SqlCommand(maxQuery, connection);
                connection.Open();
                object maxSonuc = maxCmd.ExecuteScalar();
                connection.Close();
                label8.Text = maxSonuc?.ToString() ?? "Yok";
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Label verileri çekilirken hata: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM urunler ORDER BY fiyat ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM urunler ORDER BY fiyat DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            
                StokEtiketleriniGuncelle();


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
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM urunler ORDER BY adet ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM urunler ORDER BY adet DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
