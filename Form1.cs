using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ders2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("user=root; password=; port=3306; server=localhost; database=SQLDataDemo");
        MySqlCommand cmd;
        DataTable dt;
        CurrencyManager cm;
        MySqlDataReader mydr;
        

        
             
  
 
 

private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();

            text_verisi();

            

            con.Close();
            

        }

        public void text_verisi()
        {


   cmd = new MySqlCommand("Select * from Userinfo", con);
 
            //datatable olstr
            dt = new DataTable();

            //execute reader
            dt.Load(cmd.ExecuteReader());
            //veriler datagridview girilecekü
            dataGridView1.DataSource = dt;
            cm = (CurrencyManager)BindingContext[dt];

            //cm sınıfını cagirip dt ile eslestiricegiz
            textBox2.Text = "" + (cm.Position + 1) + "/" + cm.Count;

             textBox3.DataBindings.Add("Text", dt, "Name");
            textBox4.DataBindings.Add("Text", dt, "Surname");
            textBox5.DataBindings.Add("Text", dt, "Email");
            textBox6.DataBindings.Add("Text", dt, "ID");

                
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString("ID"));
            }





        }

        public void comboBox1_TextChanged()
        {


        }


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cm.Position--;
            textBox2.Text = "" + (cm.Position + 1) + "/" + cm.Count;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cm.Position=0;
            textBox2.Text = "" + (cm.Position + 1) + "/" + cm.Count;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cm.Position++;
            textBox2.Text = "" + (cm.Position + 1) + "/" + cm.Count;


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            cm.Position = cm.Count;
            textBox2.Text = "" + (cm.Position + 1) + "/" + cm.Count;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cm.Position++;
           
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            


        }

        private void button6_Click(object sender, EventArgs e)
        {
              cm.Position--;
         }

        private void button5_Click(object sender, EventArgs e)
        {
 
            cm.Position++;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //name
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //surname
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //email
        }
    }
}
