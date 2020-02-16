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

namespace otobusdeneme
{
    public partial class Form1 : Form
    {



        veritabani vt = new veritabani();
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

           

            plakadoldur();
            illerigetir();

            koltukdoldur();

        }


        public void plakadoldur()
        {

            SqlConnection baglan = new SqlConnection(vt.baglanticumlesi);

            SqlDataAdapter adaptor = new SqlDataAdapter("select Otobus_id,Otobus_Plaka from Otobus_Tablo", baglan);

            DataTable tablo = new DataTable();

            adaptor.Fill(tablo);
            comboBoxplaka.DataSource = tablo;

            comboBoxplaka.DisplayMember = "Otobus_Plaka";

            comboBoxplaka.ValueMember = "Otobus_id";

           


        }

        public void illerigetir()
        {

            SqlConnection baglan = new SqlConnection(vt.baglanticumlesi);

            SqlDataAdapter adaptor = new SqlDataAdapter("select ilID,ilAd from il", baglan);

            DataTable tablo = new DataTable();
            DataTable tablo2 = new DataTable();
            adaptor.Fill(tablo);
            adaptor.Fill(tablo2);
            comboBoxnereden.DataSource = tablo;
            comboBoxnereden.DisplayMember = "ilAd";

            comboBoxnereden.ValueMember = "ilID";

            comboBoxnereye.DataSource = tablo2;
            comboBoxnereye.DisplayMember = "ilAd";
            comboBoxnereye.ValueMember = "ilID";





        }


        public void koltukdoldur()
        {

            for (int i = 0; i < 20; i++)
            {


                Button b = new Button();


                b.Name = i.ToString();

                b.Text = (i + 1).ToString();
                b.Size = new Size(30, 40);

                b.Parent = flowLayoutPanel1;
                b.BackColor = Color.Green;

                b.Click += new EventHandler(yesert);

            }

            for (int i = 20; i < 40; i++)
            {


                Button b = new Button();


                b.Name = i.ToString();

                b.Text = (i + 1).ToString();
                
                b.Size = new Size(30, 40);
                b.BackColor = Color.Green;
                b.Parent = flowLayoutPanel2;

                b.Click += new EventHandler(yesert);


            }







        }


        public void koltuksec(object o, EventArgs a)
        {

            Button b = (Button)o;
            b.BackColor = Color.Yellow;



        }


        public void yesert(object o, EventArgs a)
        {

            Button c = (Button)o;


            if (c.BackColor == Color.Yellow)
            {


                c.BackColor = Color.Green;


            }

            else
            {

                c.BackColor = Color.Yellow;



            }


        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           try
           
               {

                  DialogResult sonuc = MessageBox.Show("Seçili Koltuğu Satın Almak İstiyormusunuz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(sonuc==DialogResult.Yes)
            {
                SqlConnection baglan = new SqlConnection(vt.baglanticumlesi);

                SqlCommand emir = new SqlCommand("insert into sefer_tablo (Sefer_Otobus,Sefer_Nereden,Sefer_Nereye,Sefer_Saat,Sefer_Koltuk,cinsiyet,tarih) values (@plaka,@nereden,@nereye,@saat,@koltuk,@cinsiyet,@tarih)", baglan);

                emir.CommandType = CommandType.StoredProcedure;

                emir.CommandText = "aynıveriyiengelleme";
                emir.Parameters.AddWithValue("@plaka", comboBoxplaka.SelectedValue);

                emir.Parameters.AddWithValue("@nereden", comboBoxnereden.SelectedValue);
                emir.Parameters.AddWithValue("@nereye", comboBoxnereye.SelectedValue);
                emir.Parameters.AddWithValue("@saat", listBox1.SelectedItem);
                emir.Parameters.AddWithValue("@tarih", maskedTextBox1.Text);
                foreach (Button gez in flowLayoutPanel1.Controls)
                {

                    if (gez.BackColor == Color.Yellow)
                    {

                        emir.Parameters.AddWithValue("@koltuk", gez.Text);

                    }

                }

                foreach (Button gez in flowLayoutPanel2.Controls)
                {

                    if (gez.BackColor == Color.Yellow)
                    {

                        emir.Parameters.AddWithValue("@koltuk", gez.Text);

                    }

                }



                if (radioButton1.Checked == true)
                {

                   emir.Parameters.AddWithValue("@cinsiyet", radioButton1.Text);

                }

                else if (radioButton2.Checked == true)
                {

                    emir.Parameters.AddWithValue("@cinsiyet", radioButton2.Text);


                }

                baglan.Open();
                emir.ExecuteNonQuery();

                baglan.Close();



                MessageBox.Show("Satış Gerçekleştirildi");


           



             }



               }





            catch(Exception)
           {


               MessageBox.Show("Boş Alanları Doldurunuz");


           }







            }


              




          






                
          

           

           


           
           



          






       



        
            

           
 


       

        private void satışYapılanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();

            this.Close();
           
          



        }

        private void button2_Click(object sender, EventArgs e)
        {




            try
            {
                SqlConnection baglan = new SqlConnection(vt.baglanticumlesi);

                SqlCommand emir = new SqlCommand("select Sefer_Koltuk from sefer_tablo where Sefer_Otobus = @pl and Sefer_Saat =@sat and tarih=@tar", baglan);

                emir.Parameters.AddWithValue("@pl", comboBoxplaka.SelectedValue);

                emir.Parameters.AddWithValue("@sat", listBox1.SelectedItem);

                emir.Parameters.AddWithValue("@tar", maskedTextBox1.Text);
                baglan.Open();
                SqlDataReader oku = emir.ExecuteReader();



                while (oku.Read())
                {


                    foreach (Button gez in flowLayoutPanel1.Controls)
                    {

                        if (gez.Text == oku.GetInt32(0).ToString())
                        {

                            gez.BackColor = Color.Red;
                            gez.Enabled = false;

                        }



                    }




                    foreach (Button gez in flowLayoutPanel2.Controls)
                    {

                        if (gez.Text == oku.GetInt32(0).ToString())
                        {

                            gez.BackColor = Color.Red;
                            gez.Enabled = false;

                        }




                    }



                }


            }


               catch(Exception)
            {


                MessageBox.Show("Diğer Alanları Doldurunuz");


            }



            

        }

    }
}
