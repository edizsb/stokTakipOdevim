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
    public partial class Form6 : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;");



        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void üRÜNSİLToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void üRÜNSİLToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

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

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int urunId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);

                    string deleteQuery = "DELETE FROM urunler WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@id", urunId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Ürün tamamen silindi.");
                    button2_Click(null, null); // Listeyi güncelle
                }
                else
                {
                    MessageBox.Show("Lütfen silinecek bir ürün seçin.");
                }
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
                string urunAdi = textBox1.Text;
                int silinecekAdet = Convert.ToInt32(textBox2.Text);

                // Mevcut adet bilgisini al
                string selectQuery = "SELECT adet FROM urunler WHERE urun_adi = @adi";
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                selectCmd.Parameters.AddWithValue("@adi", urunAdi);

                connection.Open();
                object result = selectCmd.ExecuteScalar();
                connection.Close();

                if (result != null)
                {
                    int mevcutAdet = Convert.ToInt32(result);
                    int yeniAdet = mevcutAdet - silinecekAdet;

                    if (yeniAdet < 0)
                    {
                        MessageBox.Show("Hata: Stoktaki adetten fazla ürün silinemez.");
                        return;
                    }

                    string updateQuery = "UPDATE urunler SET adet = @yeniAdet WHERE urun_adi = @adi";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@yeniAdet", yeniAdet);
                    updateCmd.Parameters.AddWithValue("@adi", urunAdi);

                    connection.Open();
                    updateCmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Adet güncellendi.");
                    button2_Click(null, null); // Grid güncelle
                }
                else
                {
                    MessageBox.Show("Ürün bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
