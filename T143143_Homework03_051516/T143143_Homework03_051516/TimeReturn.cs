using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
namespace T143143_Homework03_051516
{
    class TimeReturn
    {
       // SqlConnection conn = new SqlConnection(@"Data Source=DELL\SQLEXPRESS;Initial Catalog=Vocabulary;User ID=sa;Password=123456");
        frmImportFile import = new frmImportFile();
        SqlConnection conn ;
        SqlCommand command;
        private void connect() 
        {
            import.frmImportFile_Load(null,null);
            conn = new SqlConnection(@"" + import.connect());
        }
        public void timeSetAgain(int id)
        {
            connect();
            try
            {
               // (datet = new DateTime(datet.Year,datet.Month,datet.Day,datet.Hour,datet.Minute+1,datet.Second))
                if (DateTime.Now.Second <= 58)
                {
                    string sql = "update word set timeReturn = '"
                        + DateTime.Now.Year + "-" + DateTime.Now.Month + "-"
                        + DateTime.Now.Day + " " + DateTime.Now.Hour + ":"
                        + (DateTime.Now.Minute+1) + ":" + DateTime.Now
                        .Second + "' where id = " + id;
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    string sql = "update word set timeReturn = '"
                            + DateTime.Now.Year + "-" + DateTime.Now.Month + "-"
                            + DateTime.Now.Day + " " + (DateTime.Now.Hour + 1) + ":"
                            + (59-DateTime.Now.Minute) + ":" + DateTime.Now
                            .Second + "' where id = " + id;
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();  
            }
        }
        public void timeSetGood(int id)
        {
            connect();
            try
            {
                if (DateTime.Now.Minute <= 49)
                {
                    string sql = "update word set timeReturn = '"
                        + DateTime.Now.Year + "-" + DateTime.Now.Month + "-"
                        + DateTime.Now.Day + " " + DateTime.Now.Hour + ":"
                        + (DateTime.Now.Minute + 10) + ":" + DateTime.Now
                        .Second + "' where id = " + id;
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    string sql = "update word set timeReturn = '"
                            + DateTime.Now.Year + "-" + DateTime.Now.Month + "-"
                            + DateTime.Now.Day + " " + (DateTime.Now.Hour + 1) + ":"
                            + (59 - DateTime.Now.Minute) + ":" + DateTime.Now
                            .Second + "' where id = " + id;
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }
        public void timeSetEasy(int id)
        {
            connect();
            try
            {
                if (DateTime.Now.Hour <= 22)
                {
                    string sql = "update word set timeReturn = '"
                        + DateTime.Now.Year + "-" + DateTime.Now.Month + "-"
                        + DateTime.Now.Day + " " + (DateTime.Now.Hour+1) + ":"
                        + DateTime.Now.Minute + ":" + DateTime.Now
                        .Second + "' where id = " + id;
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                else 
                {
                    string sql = "update word set timeReturn = '"
                            + DateTime.Now.Year + "-" + DateTime.Now.Month + "-"
                            + (DateTime.Now.Day+1) + " " + (23 - DateTime.Now.Hour) + ":"
                            + DateTime.Now.Minute  + ":" + DateTime.Now
                            .Second + "' where id = " + id;
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }
    }
}
