using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
//using MySql.Data.MySqlClient;
using System.IO;
using System.Data.SqlClient;
using CobaMySQLABD;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        koneksi konn = new koneksi();

            //      koneksi12 konn2 = new koneksi12();
            System.Data.SqlClient.SqlConnection conn = konn.GetConn();
        //        System.Data.SqlClient.SqlConnection conn = konn2.GetConn();
        string konekInfo = "Data Source = DESKTOP - KVQ3E1A"+"\"+RIKZAPEHAA ; Initial Catalog=siap; Integrated Security=True";
        SqlConnection sambung = new SqlConnection(konekInfo);
        DateTime localdate = DateTime.Now;
        string targetPath = @"C:\Users\Public\SIAPupload";
        string targetPathMasuk = @"C:\Users\Public\SIAPupload\SuratMasuk";
        string targetPathKeluar = @"C:\Users\Public\SIAPupload\SuratKeluar";
        string onSurat = "";

        string destFile = "";
       string sourceFile = "";
       string fileName2 = "";
        string editor = "";

        public Form3(string login)
        {
            InitializeComponent();
            editor = login;
            Console.WriteLine("ini login "+login);
            Console.WriteLine("ini editor " + editor);
        }

        

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Visible = true;

            label23.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label31.Visible = false;

            richTextBox1.Visible = false;
            richTextBox2.Visible = false;
            richTextBox3.Visible = false;
            richTextBox4.Visible = false;
            richTextBox5.Visible = false;
            richTextBox6.Visible = false;
            richTextBox7.Visible = false;
            richTextBox8.Visible = false;
            richTextBox9.Visible = false;
            updateBut.Visible = false;
            deleteBut.Visible = false;

            datelabel.Text = "Hari ini  " +localdate.Day.ToString() + " / " + localdate.Month.ToString() + " / " + localdate.Year.ToString();

           
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }
            if (!System.IO.Directory.Exists(targetPathKeluar))
            {
                System.IO.Directory.CreateDirectory(targetPathKeluar);
            }
            if (!System.IO.Directory.Exists(targetPathMasuk))
            {
                System.IO.Directory.CreateDirectory(targetPathMasuk);
            }
        }

        private void resetField()
        {
            ikelompok.Text = "";
            inomor.Text = "";
            idd.Text = "";
            imm.Text = "";
            iyyyy.Text = "";
            idari.Text = "";
            ikepada.Text = "";
            ihal.Text = "";
            ifile.Text = "";
            ifilename.Text = "null";
            ilokasi.Text = "";
            iketerangan.Text = "";
            sourceFile = "";

        }

            private void sMasukB_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Visible = false;

            //tanggal hari ini
            string tglinput = "";
            tglinput = localdate.Day.ToString() + " / " + localdate.Month.ToString() + " / " + localdate.Year.ToString();

            //dari kolom date
            string tanggaljadi = idd.Text.ToString() + " / " + imm.Text.ToString() + " / " + iyyyy.Text.ToString();

            sambung.Open();
            SqlCommand intrans = new SqlCommand(
               " INSERT INTO `suratmasuk` (`ID`, `KELOMPOK`, `NOMOR`, `TANGGAL`, `TGLINPUT`, `DARI`, `KEPADA`, `HAL`, `KETERANGAN`, `FILE`, `LOKASI`, `EDITOR`) VALUES(NULL, '" + ikelompok.Text.ToString() + "', '" + inomor.Text.ToString() + "', '" + tanggaljadi + "', '" + tglinput + "', '" + idari.Text.ToString() + "', '" + ikepada.Text.ToString() + "', '" + ihal.Text.ToString() + "', '" + iketerangan.Text.ToString() + "', '" + ifilename.Text.ToString() + "', '" + ilokasi.Text.ToString() + "', '" + editor + "');", sambung);
            intrans.ExecuteNonQuery();
            sambung.Close();
            ///
           // button1_Click(sender, e);
            MessageBox.Show("Input Surat Masuk berhasil");

            try {

                //System.IO.Path.Combine(targetPath, fileName2);
                //System.IO.File.Copy(sourceFile, destFile, true);
                System.IO.File.Copy(sourceFile, System.IO.Path.Combine(targetPathMasuk, fileName2), true);
            }
            catch (Exception ex) { }

            
            resetField();


        }


        private void makeTrue() {

            label23.Visible = true;
            label24.Visible = true;
            label25.Visible = true;
            label26.Visible = true;
            label27.Visible = true;
            label28.Visible = true;
            label29.Visible = true;
            label30.Visible = true;
            label31.Visible = true;

            richTextBox1.Visible = true;
            richTextBox2.Visible = true;
            richTextBox3.Visible = true;
            richTextBox4.Visible = true;
            richTextBox5.Visible = true;
            richTextBox6.Visible = true;
            richTextBox7.Visible = true;
            richTextBox8.Visible = true;
            richTextBox9.Visible = true;
            updateBut.Visible = true;
            deleteBut.Visible = true;
        }

        private void aMasukB_Click(object sender, EventArgs e)
        {
            
            resetrichTB();
            onSurat = "masuk";

            dataGridView1.DataSource = null;
            dataGridView1.Visible = true;

            makeFalse();

            input.Visible = true;
            sKeluarB.Visible = false;
            sMasukB.Visible = false;

            //tampilin seuratmasuk ke gridview
            sambung.Open();
           SqlDataAdapter list = new SqlDataAdapter("SELECT `KELOMPOK`, `NOMOR`,  `TANGGAL` as `TGL SURAT`, `DARI` as `PENGIRIM`, `KEPADA` as `PENERIMA`, `HAL`, `KETERANGAN` as `ISI SINGKAT` , `FILE`, `LOKASI`, `EDITOR`, `TGLINPUT` as `TGL ENTRY` FROM `suratmasuk`", sambung);
            DataTable table = new DataTable();

            list.Fill(table);
            dataGridView1.DataSource = table;
            sambung.Close();
        }

        private void aKeluarB_Click(object sender, EventArgs e)
        {
            
            resetrichTB();
            onSurat = "keluar";

            dataGridView1.DataSource = null;
            dataGridView1.Visible = true;

            makeFalse();


            input.Visible = true;
            sKeluarB.Visible = false;
            sMasukB.Visible = false;

            //tampilin suratkeluar ke gridview
            sambung.Open();
            SqlDataAdapter list = new SqlDataAdapter("SELECT `KELOMPOK`, `NOMOR`, `TANGGAL` as `TGL SURAT`, `DARI` as `PENGIRIM`, `KEPADA` as `PENERIMA`, `HAL`, `KETERANGAN` as `ISI SINGKAT`, `FILE`, `LOKASI`, `EDITOR`, `TGLINPUT` as `TGL ENTRY` FROM `suratkeluar`", sambung);
            DataTable table = new DataTable();

            list.Fill(table);
            dataGridView1.DataSource = table;
            sambung.Close();
        }

        private void makeFalse() {
            label23.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label31.Visible = false;

            richTextBox1.Visible = false;
            richTextBox2.Visible = false;
            richTextBox3.Visible = false;
            richTextBox4.Visible = false;
            richTextBox5.Visible = false;
            richTextBox6.Visible = false;
            richTextBox7.Visible = false;
            richTextBox8.Visible = false;
            richTextBox9.Visible = false;
            updateBut.Visible = false;
            deleteBut.Visible = false;
        }

        private void input_Click(object sender, EventArgs e)
        {

            resetrichTB();
            dataGridView1.DataSource = null;
            dataGridView1.Visible = false;

            makeFalse();

            input.Visible = false;
            sKeluarB.Visible = true;
            sMasukB.Visible = true;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            
            DialogResult pilihan = dlgBukaFile.ShowDialog();
            if (pilihan == DialogResult.OK)
            {
                // tbNamaFile.Text = dlgBukaFile.SafeFileName;
                ifile.Text = dlgBukaFile.FileName; // <--source file
                ifilename.Text = dlgBukaFile.SafeFileName; // <--simpan db
                sourceFile = dlgBukaFile.FileName;
                Console.WriteLine(dlgBukaFile.FileName);
                fileName2 = dlgBukaFile.SafeFileName;
            }
            

            // Use Path class to manipulate file and directory paths.
            //  string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            destFile = System.IO.Path.Combine(targetPath, fileName2);
            // Console.WriteLine(sourceFile);


            
        }

        private void sKeluarB_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ifilename 1= "+ifilename);
            dataGridView1.DataSource = null;
            dataGridView1.Visible = false;

            //tanggal hari ini
            string tglinput = "";
            tglinput = localdate.Day.ToString() + " / " + localdate.Month.ToString() + " / " + localdate.Year.ToString();

            //dari kolom date
            string tanggaljadi = idd.Text.ToString() + " / " + imm.Text.ToString() + " / " + iyyyy.Text.ToString();

            sambung.Open();
            SqlCommand intrans = new SqlCommand(
                " INSERT INTO `suratkeluar` (`ID`, `KELOMPOK`, `NOMOR`, `TANGGAL`, `TGLINPUT`, `DARI`, `KEPADA`, `HAL`, `KETERANGAN`, `FILE`, `LOKASI`, `EDITOR`) VALUES(NULL, '" + ikelompok.Text.ToString() + "', '" + inomor.Text.ToString() + "', '" + tanggaljadi + "', '" + tglinput + "', '" + idari.Text.ToString() + "', '" + ikepada.Text.ToString() + "', '" + ihal.Text.ToString() + "', '" + iketerangan.Text.ToString() + "', '" + ifilename.Text.ToString() + "', '" + ilokasi.Text.ToString() + "', '" + editor + "');", sambung);
            
            intrans.ExecuteNonQuery();
            Console.WriteLine("ifilename 2= " + ifilename);
            sambung.Close();
            ///
           // button1_Click(sender, e);
            MessageBox.Show("Input Surat Keluar berhasil");
            try {
                //System.IO.File.Copy(sourceFile, destFile, true); 
                System.IO.File.Copy(sourceFile, System.IO.Path.Combine(targetPathKeluar, fileName2), true);

            }
            catch (Exception ex ) { }
            
            resetField();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sambung.Open();
            SqlCommand logut = new SqlCommand(
                "UPDATE `user` SET `onload` = 'na'", sambung);
            logut.ExecuteNonQuery();
            sambung.Close();
            //  Form1 f1 = new Form1();
            // f1.ShowDialog();
            this.Close();
        }

        private void clearBut_Click(object sender, EventArgs e)
        {
            resetField();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            makeTrue();


            //dapetin id buku

            string kelompokklik = dataGridView1.SelectedCells[0].Value.ToString();
            string nomorsuratklik = dataGridView1.SelectedCells[1].Value.ToString();
            string tanggalklik = dataGridView1.SelectedCells[2].Value.ToString();
            string dariklik = dataGridView1.SelectedCells[3].Value.ToString();
            string kepadaklik = dataGridView1.SelectedCells[4].Value.ToString();
            string halklik = dataGridView1.SelectedCells[5].Value.ToString();
            string keteranganklik = dataGridView1.SelectedCells[6].Value.ToString();
            string fileklik = dataGridView1.SelectedCells[7].Value.ToString();
            string lokasiklik = dataGridView1.SelectedCells[8].Value.ToString();


            //rubah Rich TB
            richTextBox1.Text = kelompokklik;
            richTextBox2.Text = nomorsuratklik;
            richTextBox3.Text = tanggalklik;
            richTextBox4.Text = dariklik;
            richTextBox5.Text = kepadaklik;
            richTextBox6.Text = halklik;
            richTextBox7.Text = keteranganklik;
            richTextBox8.Text = fileklik;
            richTextBox9.Text = lokasiklik;

            
        }

        private void resetrichTB() {


            //reset rich
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
            richTextBox6.Text = "";
            richTextBox7.Text = "";
            richTextBox8.Text = "";
            richTextBox9.Text = "";
            onSurat = "";
        }

        private void updateBut_Click(object sender, EventArgs e)
        {

            

            if (onSurat.Equals("masuk")) {
                try
                {

                    //dapetin nomor sebagai primary
                    string nomorsuratRT = dataGridView1.SelectedCells[1].Value.ToString();
                    Console.WriteLine("masuk update");
                    Console.WriteLine(onSurat);

                    Console.WriteLine("pada if masuk");
                    //update data
                    sambung.Open();
                    SqlCommand rubahnilai = new SqlCommand(
                        //  "UPDATE `buku` SET `status` = 'TERSEDIA' WHERE `buku`.`id` =" + idbuku + ";", sambung 
                        "UPDATE `suratmasuk` SET `KELOMPOK`= '" + richTextBox1.Text + "',`NOMOR`='" + richTextBox2.Text + "',`TANGGAL`='" + richTextBox3.Text + "',`DARI`='" + richTextBox4.Text + "',`KEPADA`='" + richTextBox5.Text + "',`HAL`='" + richTextBox6.Text + "',`KETERANGAN`='" + richTextBox7.Text + "',`FILE`='" + richTextBox8.Text + "',`LOKASI`='" + richTextBox9.Text + "',`EDITOR`='" + editor + "'  WHERE `suratmasuk`.`NOMOR`='" + nomorsuratRT + "';", sambung
                        );
                    rubahnilai.ExecuteNonQuery();
                    sambung.Close();
                    //


                    //reload
                    resetrichTB();
                    aMasukB_Click(sender, e);
                    //
                }
                catch (Exception ex)
                {
                    sambung.Close();
                    MessageBox.Show("Update Fail, sorry");

                }

            }
            else if (onSurat.Equals("keluar")) {


                try
                {
                    //dapetin nomor sebagai primary
                    string nomorsuratRT = dataGridView1.SelectedCells[1].Value.ToString();
                    Console.WriteLine("masuk update");
                    Console.WriteLine(onSurat);

                    Console.WriteLine("pada if keluar");
                    //update data
                    sambung.Open();
                    SqlCommand rubahnilai = new SqlCommand(
                        //  "UPDATE `buku` SET `status` = 'TERSEDIA' WHERE `buku`.`id` =" + idbuku + ";", sambung 
                        "UPDATE `suratkeluar` SET `KELOMPOK`='" + richTextBox1.Text + "',`NOMOR`='" + richTextBox2.Text + "',`TANGGAL`='" + richTextBox3.Text + "',`DARI`='" + richTextBox4.Text + "',`KEPADA`='" + richTextBox5.Text + "',`HAL`='" + richTextBox6.Text + "',`KETERANGAN`='" + richTextBox7.Text + "',`FILE`='" + richTextBox8.Text + "',`LOKASI`='" + richTextBox9.Text + "',`EDITOR`='" + editor + "'  WHERE `suratkeluar`.`NOMOR`='" + nomorsuratRT + "';", sambung
                        );
                    rubahnilai.ExecuteNonQuery();
                    sambung.Close();
                    //

                    //reload
                    resetrichTB();
                    aKeluarB_Click(sender, e);
                    //
                }

                catch (Exception ex)
                {
                    sambung.Close();
                    MessageBox.Show("Update Fail, sorry");

                }

            }



        }

        private void deleteBut_Click(object sender, EventArgs e)
        {


            if (onSurat.Equals("masuk"))
            {
                try
                {
                    //buang dari list
                    sambung.Open();
                    SqlCommand buang = new SqlCommand(
                        "DELETE FROM `suratmasuk` WHERE `suratmasuk`.`NOMOR`='" + dataGridView1.SelectedCells[1].Value.ToString() + "';", sambung);
                    buang.ExecuteNonQuery();
                    sambung.Close();
                    ///
                    MessageBox.Show("Surat Nomor : \n" + dataGridView1.SelectedCells[1].Value.ToString() + " \n  Berhasil dihapus");

                    //delete dari hardisk
                    string file = dataGridView1.SelectedCells[7].Value.ToString();
                    if (System.IO.File.Exists(@"C:\Users\Public\SIAPupload\SuratMasuk\"+file)) {

                       

                        try {
                            System.IO.File.Delete(@"C:\Users\Public\SIAPupload\SuratMasuk\" + file);
                        }
                        catch {
                        }
                    }
                    
                    //reload
                    resetrichTB();
                    aMasukB_Click(sender, e);
                    //
                }
                catch (Exception ex)
                {
                    sambung.Close();
                    MessageBox.Show("delete fail");

                }

            }
            else if (onSurat.Equals("keluar"))
            {

                //buang dari list
                try
                {
                    sambung.Open();
                    SqlCommand buang = new SqlCommand(
                        "DELETE FROM `suratkeluar` WHERE `suratkeluar`.`NOMOR`='" + dataGridView1.SelectedCells[1].Value.ToString() + "';", sambung);
                    buang.ExecuteNonQuery();
                    sambung.Close();
                    ///
                    MessageBox.Show("Surat Nomor : \n" + dataGridView1.SelectedCells[1].Value.ToString() + " \n  Berhasil dihapus");

                    //delete dari hardisk
                    string file = dataGridView1.SelectedCells[7].Value.ToString();
                    if (System.IO.File.Exists(@"C:\Users\Public\SIAPupload\SuratKeluar\" + file))
                    {

                        try
                        {
                            System.IO.File.Delete(@"C:\Users\Public\SIAPupload\SuratKeluar\" + file);
                        }
                        catch
                        {
                        }
                    }
                    //reload
                    resetrichTB();
                    aKeluarB_Click(sender, e);
                    //
                }
                catch (Exception ex) {
                    sambung.Close();
                    MessageBox.Show("Delete fail");
                }                
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}


///localdate.ToString()