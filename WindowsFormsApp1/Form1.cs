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
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static string konekInfo = "datasource=localhost; port=3306; username=root; password=; database=siap";
        MySqlConnection sambung = new MySqlConnection(konekInfo);
        
        public Form1()
        {
            InitializeComponent();
            
            
            
        }

        private void buttonPass(object sender, EventArgs e) { textBox2.PasswordChar = '*'; }
        private void textUserEnter(object sender, EventArgs e) {
            if (textBox1.Text == "Username") {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
            }

        private void textUserLeave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Username";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textPassEnter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Password")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Boolean masuk = false;
            string nama="";
            int id =0;
            ////disini ADA MASALAH
            sambung.Open();
            string query = "select id,nama from user where id= '" + textBox1.Text + "'and password='" + textBox2.Text + "';";
            
            MySqlCommand cmd = new MySqlCommand(query, sambung);
            MySqlDataReader rdr = cmd.ExecuteReader();

            textBox1.Text = "Username";
            textBox1.ForeColor = Color.Silver;

            textBox2.Text = "Password";
            textBox2.ForeColor = Color.Silver;


            while (rdr.Read())
            {
                masuk = true;
                id = (int)rdr[0];
                nama = (string)rdr[1];
            }
            if (masuk) {
                MessageBox.Show("Halo "+ nama);
                rdr.Close();
                sambung.Close();
                sambung.Open();
                
                MySqlCommand run = new MySqlCommand("UPDATE user SET onload = 'available' WHERE id = " + id + ";", sambung);
                run.ExecuteNonQuery();
                sambung.Close();
                this.Hide();
                Form3 f2 = new Form3(nama);
                f2.ShowDialog();
                this.Show();
            }
            else {
                MessageBox.Show("Maaf, mungkin password salah :(");
                rdr.Close();
                sambung.Close();
            }
            

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
        }

    }
}
