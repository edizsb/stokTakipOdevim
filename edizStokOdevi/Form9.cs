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
    public partial class Form9 : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;");

        public Form9()
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

        private void Form9_Load(object sender, EventArgs e)
        {

            try
            {
                string query = "SELECT urun_adi, fiyat FROM urunler";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                listBox1.Items.Clear();

                while (reader.Read())
                {
                    string urunAdi = reader["urun_adi"].ToString();
                    decimal fiyat = Convert.ToDecimal(reader["fiyat"]);
                    string gosterilecek = $"{urunAdi} - fiyat: {fiyat} TL";

                    listBox1.Items.Add(gosterilecek);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }

            // ListView hazırla
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Ürün", 150);
            listView1.Columns.Add("Miktar", 70);








        }

        private void button1_Click(object sender, EventArgs e)
        {


            foreach (var seciliUrun in listBox1.SelectedItems)
            {
                string tamYazi = seciliUrun.ToString();
                int miktar = (int)numericUpDown1.Value;

                ListViewItem item = new ListViewItem(tamYazi);
                item.SubItems.Add(miktar.ToString());
                listView1.Items.Add(item);
            }

            listBox1.ClearSelected();
            numericUpDown1.Value = 1;

            // 🔢 Toplamı hesapla ve label6'ya yaz
            decimal toplamTutar = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                string tamYazi = item.SubItems[0].Text;
                string urunAdi = tamYazi.Split('-')[0].Trim();
                int miktar = Convert.ToInt32(item.SubItems[1].Text);

                string query = "SELECT fiyat FROM urunler WHERE urun_adi = @adi";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@adi", urunAdi);

                connection.Open();
                object fiyatObj = cmd.ExecuteScalar();
                connection.Close();

                if (fiyatObj != null)
                {
                    decimal fiyat = Convert.ToDecimal(fiyatObj);
                    toplamTutar += fiyat * miktar;
                }
            }

            label6.Text = $"Toplam: {toplamTutar} TL";


        }

        private void button2_Click(object sender, EventArgs e)
        {






            try
            {
                decimal toplamTutar = 0;

                foreach (ListViewItem item in listView1.Items)
                {
                    string tamYazi = item.SubItems[0].Text; // "çikolata - fiyat: 12 TL"
                    string urunAdi = tamYazi.Split('-')[0].Trim();
                    int miktar = Convert.ToInt32(item.SubItems[1].Text);

                    // Ürün ID ve fiyatı al
                    string selectQuery = "SELECT id, fiyat, adet FROM urunler WHERE urun_adi = @adi";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                    selectCmd.Parameters.AddWithValue("@adi", urunAdi);

                    connection.Open();
                    SqlDataReader reader = selectCmd.ExecuteReader();
                    int urunId = 0;
                    decimal fiyat = 0;
                    int mevcutAdet = 0;

                    if (reader.Read())
                    {
                        urunId = Convert.ToInt32(reader["id"]);
                        fiyat = Convert.ToDecimal(reader["fiyat"]);
                        mevcutAdet = Convert.ToInt32(reader["adet"]);
                    }
                    connection.Close();

                    // 1️⃣ Stoktan düş
                    int yeniAdet = mevcutAdet - miktar;
                    if (yeniAdet < 0) yeniAdet = 0; // eksiye düşmesin

                    string updateQuery = "UPDATE urunler SET adet = @yeniAdet WHERE id = @id";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@yeniAdet", yeniAdet);
                    updateCmd.Parameters.AddWithValue("@id", urunId);

                    connection.Open();
                    updateCmd.ExecuteNonQuery();
                    connection.Close();

                    // 2️⃣ Satış kaydını yaz
                    decimal urunToplam = fiyat * miktar;
                    toplamTutar += urunToplam;

                    string insertQuery = "INSERT INTO satislar (urun_id, musteri_id, personel_id, satis_tarihi, miktar, toplam_fiyat) VALUES (@uid, NULL, NULL, @tarih, @miktar, @toplam)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@uid", urunId);
                    insertCmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                    insertCmd.Parameters.AddWithValue("@miktar", miktar);
                    insertCmd.Parameters.AddWithValue("@toplam", urunToplam);

                    connection.Open();
                    insertCmd.ExecuteNonQuery();
                    connection.Close();
                }

                // Bilgilendirme
                MessageBox.Show($"{toplamTutar} TL'lik satış yapıldı.");
                label6.Text = "Ürün seçiniz...";
                listView1.Items.Clear();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Satış hatası: " + ex.Message);
            }





        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
