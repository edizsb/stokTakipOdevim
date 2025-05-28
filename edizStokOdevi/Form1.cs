using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace edizStokOdevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {




            // Bağlantı dizesi
            string connectionString = @"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;Trust Server Certificate=True";

            try
            {
                // SQL bağlantısını oluşturma
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Bağlantıyı aç
                    connection.Open();
                    Console.WriteLine("Bağlantı başarılı!");

                    // Veri almak için SQL sorgusu
                    string query = "SELECT * FROM TabloAdi"; // Tablo adını buraya yazın

                    // Veriyi almak için SqlDataAdapter kullanma
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();

                    // Veriyi doldurma
                    adapter.Fill(dataTable);

                    // Veriyi ekrana yazdırma
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            Console.Write(item + "\t");
                        }
                        Console.WriteLine();
                    }

                    connection.Close();
                    Console.WriteLine("Bağlantı kapatıldı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

            Console.ReadLine();










        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Bağlantı dizesi
            string connectionString = @"Data Source=DESKTOP-VRAQO4S;Initial Catalog=edizsb;Integrated Security=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // Kullanıcı adı ve şifreyi al
                    string kullaniciAdi = textBox1.Text;
                    string sifre = textBox2.Text;

                    // Sorgu: kullanıcı adı ve şifreyi kontrol et
                    string query = "SELECT rol FROM personel WHERE kullanici_adi = @kullaniciAdi AND sifre = @sifre";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                        command.Parameters.AddWithValue("@sifre", sifre);

                        // Veri çekme
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string rol = result.ToString();

                            // Rol kontrolü
                            if (rol == "admin")
                            {
                                MessageBox.Show("Admin olarak giriş yapıldı!");
                                Form2 form2 = new Form2();
                                form2.Show();
                                this.Hide();
                            }
                            else if (rol == "personel")
                            {
                          

                                MessageBox.Show("Personel olarak giriş yapıldı!");
                                Form2 form2 = new Form2();
                                form2.Show();
                                this.Hide();


                            }
                            else
                            {
                                MessageBox.Show("Rol bilgisi bulunamadı.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }










}
    



