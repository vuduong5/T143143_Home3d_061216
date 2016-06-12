using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
namespace T143143_Homework03_051516
{
    class CheckID
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DELL\SQLEXPRESS;Initial Catalog=Vocabulary;User ID=sa;Password=123456");
        SqlCommand command;
        int id = 0;
        List<string> listAgain, listGood, listEasy;
        public CheckID(List<string> Again, List<string> Good, List<string> Easy)
        {
            this.listAgain = Again;
            this.listGood = Good;
            this.listEasy = Easy;
        }
        public int loadCheck() 
        {
            int idload = 0;
            int idAgain = pushAgain(listAgain);
            if (idAgain == 0)
            {
                int idGood = pushGood(listGood);
                if (idGood == 0)
                {
                    int idEasy = pushEasy(listEasy);
                    if (idEasy == 0)
                    {
                        idload = 0;
                    }
                    else 
                    {
                        idload = idEasy;
                    }
                }
                    
                else 
                {
                    idload = idGood;
                }
            }
            else 
            {
                idload = idAgain;
            }
            return idload;
             
        }
        private int pushAgain(List<string> list) 
        {
            bool flag = false;
            int id_a = 0;
            foreach (string a in list)
            {
                flag = checkDate(int.Parse(a));
                if (flag == true)
                    id_a = int.Parse(a);
                    list.Remove(a);
                    removeDate(id_a);
                    break;
            }

            return id_a;
        }
        private int pushGood(List<string> list)
        {
            bool flag = false;
            int id_a = 0;
            foreach (string a in list)
            {
                flag = checkDate(int.Parse(a));
                if (flag == true)
                {
                    id_a = int.Parse(a);
                    list.Remove(a);
                    removeDate(id_a);
                    break;
                }
                   
            }

            return id_a;
        }
        private int pushEasy(List<string> list)
        {
            bool flag = false;
            int id_a = 0;
            foreach (string a in list)
            {
                flag = checkDate(int.Parse(a));
                if (flag == true)
                {
                    id_a = int.Parse(a);
                    list.Remove(a);
                    removeDate(id_a);
                    break;
                    
                }
               
            }

            return id_a;
        }
        private bool checkDate(int id) 
        {
            bool flag = false;
            string tuvung = "";
            DateTime date = new DateTime();
            int i;
            string sql = "SELECT id,TimeReturn FROM word WHERE ID = " + id;
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                tuvung = reader[1].ToString();
                date = Convert.ToDateTime(tuvung);
            }
            reader.Close();
            conn.Close();
            i = date.Minute - DateTime.Now.Minute ;
            if (date == DateTime.Now || i < 0)
            {
                flag = true;
            }
            else 
            {
                flag = false;
            }
            return flag;
        }
        private void removeDate(int id) 
        {
            string sql = "update word set timereturn = null WHERE ID = " + id;
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            command = new SqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

    }
}
