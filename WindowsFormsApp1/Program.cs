using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using CobaMySQLABD;

namespace WindowsFormsApp1
{
     class Program{
        koneksi12 konn = new koneksi12();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
         void Main()
        {
            System.Data.SqlClient.SqlConnection sambung = konn.GetConn();

            //System.Data.SqlClient.SqlConnection sambung = new System.Data.SqlClient.SqlConnection();
            //   sambung.ConnectionString = "Data Source=DESKTOP-KVQ3E1A" + "\"RIKZAPEHAA ; Initial Catalog=siap; Integrated Security=True";
            //          return sambung;
            //            string konekInfo = "datasource=localhost; port=3306; username=root; password=; database=siap";
            //          MySqlConnection sambung = new MySqlConnection(konekInfo);
            //dapetin user aktif
            int iduser = 0;
            string nama = "";

            sambung.Open();
            SqlCommand getuser = new SqlCommand(
                "select id,nama from user where onload = 'available' ", sambung);
            SqlDataReader get = getuser.ExecuteReader();
            while (get.Read())
            {
                iduser = (int)get[0];
                nama = (string)get[1];
            }
            get.Close();
            sambung.Close();
            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (iduser == 0) { Application.Run(new Form1()); }
            else { Application.Run(new Form3(nama)); }
        //------>   // Application.Run(new Form2());

        }
    }

    internal class koneksi
    {
        public koneksi()
        {
        }
    }
}
