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
using System.Windows.Forms.DataVisualization.Charting;

namespace edizStokOdevi
{
    public partial class Form2 : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;");

        public Form2()
        {



            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void EnCokSatilanTop5UrunGoster()
        {
            try
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();
                chart1.Legends.Clear();

                // Grafik başlığı
                chart1.Titles.Add("En Çok Satılan Ürünler");

                // Legend ayarları (sağda gösterim)
                Legend legend = new Legend("UrunListesi");
                legend.Docking = Docking.Right;
                legend.Alignment = StringAlignment.Near;
                legend.Font = new Font("Segoe UI", 9);
                legend.IsDockedInsideChartArea = false;
                chart1.Legends.Add(legend);

                // Tek bir seri
                Series seri = new Series();
                seri.ChartType = SeriesChartType.Column;
                seri.IsValueShownAsLabel = false;
                seri.IsVisibleInLegend = false; // Series adı görünmesin
                chart1.Series.Add(seri);

                // Renkler
                Color[] renkler = { Color.Green, Color.Gold, Color.Orange, Color.Red, Color.SaddleBrown };

                // Veri çek
                string query = @"
            SELECT TOP 5 u.urun_adi, SUM(s.miktar) AS toplam_satis
            FROM satislar s
            JOIN urunler u ON u.id = s.urun_id
            GROUP BY u.urun_adi
            ORDER BY toplam_satis DESC
        ";

                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int index = 0;
                while (reader.Read() && index < renkler.Length)
                {
                    string urunAdi = reader["urun_adi"].ToString();
                    int toplamSatis = Convert.ToInt32(reader["toplam_satis"]);

                    // Veri noktası
                    DataPoint point = new DataPoint();
                    point.AxisLabel = urunAdi; // ✔ Alt eksende ürün adı yazsın
                    point.YValues[0] = toplamSatis;
                    point.Color = renkler[index];
                    point.LegendText = urunAdi; // ✔ Legend’da da ürün adı yazsın
                    point.IsVisibleInLegend = true;

                    seri.Points.Add(point);
                    index++;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Grafik hatası: " + ex.Message);
            }
        }





        private void Form2_Load(object sender, EventArgs e)
        {
            EnCokSatilanTop5UrunGoster();


            try
            {
                string query = "SELECT SUM(toplam_fiyat) FROM satislar";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                object sonuc = cmd.ExecuteScalar();
                connection.Close();

                if (sonuc != DBNull.Value && sonuc != null)
                {
                    decimal toplamSatis = Convert.ToDecimal(sonuc);
                    label27.Text = $" {toplamSatis} TL";
                }
                else
                {
                    label27.Text = ": 0 TL";
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }









            try
            {
                string query = "SELECT SUM(adet) FROM urunler";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                object sonuc = cmd.ExecuteScalar();
                connection.Close();

                if (sonuc != DBNull.Value && sonuc != null)
                {
                    int toplamAdet = Convert.ToInt32(sonuc);
                    label14.Text = $" {toplamAdet}";
                }
                else
                {
                    label14.Text = " 0";
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }







            try
            {
                int toplamAdet = 0;
                int maxAdet = 10000;

                string query = "SELECT SUM(adet) FROM urunler";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                object sonuc = cmd.ExecuteScalar();
                connection.Close();

                if (sonuc != DBNull.Value && sonuc != null)
                    toplamAdet = Convert.ToInt32(sonuc);

                // Yüzdelik hesapla
                int yuzde = (int)((toplamAdet / (double)maxAdet) * 100);

                // Label'a yaz
                label9.Text = $"Yüzde {yuzde}";

                // ProgressBar'a aktar
                progressBar1.Maximum = 100;
                progressBar1.Value = Math.Min(yuzde, 100); // 100'ü geçmesin
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void pERSONELLERToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void aCIKTEMAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
