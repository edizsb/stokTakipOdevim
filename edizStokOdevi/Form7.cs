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
    public partial class Form7 : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;");

        public Form7()
        {
            InitializeComponent();
        }




        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void sTOKTAKİBİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sATIŞYAPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();


        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Çıkış Yapıldı.");

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM personel";
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
                string ad = textBox2.Text;
                string soyad = textBox3.Text;
                string pozisyon = comboBox2.Text;
                string kullaniciAdi = textBox4.Text;
                string sifre = textBox5.Text;
                string rol = comboBox1.Text;

                string query = "INSERT INTO personel (ad, soyad, pozisyon, kullanici_adi, sifre, rol) VALUES (@ad, @soyad, @pozisyon, @kullaniciAdi, @sifre, @rol)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@ad", ad);
                cmd.Parameters.AddWithValue("@soyad", soyad);
                cmd.Parameters.AddWithValue("@pozisyon", pozisyon);
                cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@sifre", sifre);
                cmd.Parameters.AddWithValue("@rol", rol);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Personel başarıyla eklendi.");
                button1_Click(null, null); // listeyi yenile
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string kullaniciAdi = textBox1.Text;

                string query = "DELETE FROM personel WHERE kullanici_adi = @kullaniciAdi";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);

                connection.Open();
                int sonuc = cmd.ExecuteNonQuery();
                connection.Close();

                if (sonuc > 0)
                {
                    MessageBox.Show("Personel silindi.");
                    button1_Click(null, null); // listeyi yenile
                }
                else
                {
                    MessageBox.Show("Belirtilen kullanıcı adına sahip personel bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
