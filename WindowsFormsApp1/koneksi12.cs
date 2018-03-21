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
namespace CobaMySQLABD
{
    class koneksi12
    {

        public System.Data.SqlClient.SqlConnection GetConn()
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-KVQ3E1A" + "\"+RIKZAPEHAA ;  Initial Catalog=db_latihan; Integrated Security=True";
            return conn;
        }

    }
}
