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
using System.IO;
using SpeechLib;
namespace T143143_Homework03_051516
{
    public partial class Form1 : Form
    {
        frmImportFile Import = new frmImportFile();
        // connect sqlsever
        SqlConnection conn;
        //SqlConnection conn = new SqlConnection(@"Data Source=DELL\SQLEXPRESS;Initial Catalog=Vocabulary;User ID=sa;Password=123456");
        //SqlConnection conn = new SqlConnection(@"data source=.\SQLEXPRESS;Initial Catalog=Vocabulary;integrated security = SSPI");
        
        SqlCommand command;
        TimeReturn timer = new TimeReturn();
        string imgLoc = "";
        int i_IDchinh = 1;
        int i_IDswap = 1;
        int i_IDcheck = 0;
        List<string> listEasy = new List<string>();
        List<string> listGood = new List<string>();
        List<string> listAgain = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        //from load
        private void Form1_Load(object sender, EventArgs e)
        {
            Import.frmImportFile_Load(null,null);
            conn = new SqlConnection(Import.connect());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelStudy2.Show();
            addLabel();
            addPicture();
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            panelAnswer.Show();
           // addPicture();
            SpeakEL(selectSql("TV"));

        }

        private void btnAgain_Click(object sender, EventArgs e)
        {
            bool flag = false;
            panelAnswer.Visible = false;
            timer.timeSetAgain(i_IDchinh);
            listAgain.Add("" + i_IDchinh);
            listAgain.Reverse();
            i_IDcheck = loadCheck();
            if (i_IDcheck == 0)
            {
                i_IDchinh = i_IDswap;
                i_IDchinh++;
                i_IDswap = i_IDchinh;
             
            }
            else
            {
                i_IDchinh = i_IDcheck;
            }
            addLabel();
            addPicture();

        }

    

        private void btnGood_Click(object sender, EventArgs e)
        {
            panelAnswer.Visible = false;
            timer.timeSetGood(i_IDchinh);
            listGood.Add("" + i_IDchinh);
            listGood.Reverse();
            i_IDcheck = loadCheck();
            if (i_IDcheck == 0)
            {
                i_IDchinh = i_IDswap;
                i_IDchinh++;
                i_IDswap = i_IDchinh;

            }
            else
            {
                i_IDchinh = i_IDcheck;
            }
            addLabel();
            addPicture();

        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            panelAnswer.Visible = false;
            timer.timeSetEasy(i_IDchinh);
            listEasy.Add("" + i_IDchinh);
            listEasy.Reverse();
            i_IDcheck = loadCheck();

            if (i_IDcheck == 0)
            {
                i_IDchinh = i_IDswap;
                i_IDchinh++;
                i_IDswap = i_IDchinh;

            }
            else
            {
                i_IDchinh = i_IDcheck;
            }  
            addLabel();
            addPicture();


        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            frmCaiDat caidat = new frmCaiDat();
            caidat.ShowDialog();
        }

        private void btnOption_Click(object sender, EventArgs e)
        {

        }

        private void btnVoice_Click(object sender, EventArgs e)
        {
            SpeakEL(selectSql("TV"));
        }
         
        private string selectSql(string text)
        {
            string tuvung = "";
            string phatam = "";
            string explain = "";
            string type = "";
            string nghia = "";
            string tudongnghia = "";
            string sql = "SELECT id,word,pronounce,Meaning,Type,Explain_EL,synonym FROM word WHERE ID = " + i_IDchinh;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
             
                tuvung= reader[1].ToString();
                phatam = "/ " + reader[2].ToString() + " /";
                nghia = reader[3].ToString();               
                type = "/ "+reader[4].ToString()+" /";              
                explain = reader[5].ToString();
                tudongnghia = reader[6].ToString();
            }
               
            else
            {
                MessageBox.Show("this is does not exits");
            }
            conn.Close();
            if(text == "TV")
            {
             return tuvung;
            }
            if (text == "PA")
            {
                return phatam;
            }
            if (text == "EX")
            {
                return explain;
            }
            if (text == "TY")
            {
                return type;
            }
            if (text == "mean")
            {
                return nghia;
            }
            if(text == "TuDN")
            {
                return tudongnghia;
            }
            return null;
          
           
        }
        private void addPicture() 
        {
            string sql = "SELECT id,Image FROM word WHERE ID = " + i_IDchinh;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                byte[] img = (byte[])(reader[1]);
                if (img == null)
                {
                    pictureStudy.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pictureStudy.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            else 
            {
                MessageBox.Show("this is does not exits");
            }
            conn.Close();
        } 
        private string biendoi(string tu)
        {
            string sTutrong = "" ;
            Random random1 = new Random();
            var chars = tu.ToCharArray();
            int idem = chars.Length;
            if (idem > 10)
            {
                for (int i = 0; i < 8; i++)
                {
                    chars[random1.Next(1, 8)] = '_';
                }
            }
            if (idem <= 10 && idem >= 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    chars[random1.Next(1, 5)] = '_';
                }
            }
            if(idem < 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    chars[random1.Next(1, 3)] = '_';
                }
            }     
            sTutrong = new string(chars); 
            return sTutrong;
        }
        private void addLabel() 
        {
            lbTuVung.Text = biendoi(selectSql("TV"));
            lbTuVungAnswer.Text = selectSql("TV");         
            lbNghia.Text = selectSql("mean");
            lbPhatAm.Text = selectSql("TY")+"  "+selectSql("PA");
            lbViDu.Text = selectSql("EX");
           // lbType.Text = selectSql("TY");
            lbTuDongNghia.Text = selectSql("TuDN");
            SpeakEL(selectSql("TV"));
        }
        private void SpeakEL(string text)
        {
            SpVoice voice = new SpVoice();
            voice.Speak(text, SpeechVoiceSpeakFlags.SVSFDefault);
        }
        //---------------------------check id ---------------------------------------------------
        int idAgain = 0, idload = 0, idGood = 0, idEasy = 0;
        private int loadCheck()
        {
            idload = 0;
            idAgain = pushAgain();
            if (idAgain == 0)
            {
                 idGood = pushGood();
                if (idGood == 0)
                {
                    int idEasy = pushEasy();
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
        private int pushAgain()
        {
            bool flag = false;
            int id_a = 0;
            foreach (string a in listAgain)
            {
                flag = checkDate(int.Parse(a));
                if (flag == true)
                {
                    id_a = int.Parse(a);
                    listAgain.Remove(a);
                    removeDate(id_a);
                    break;
                }
               
            }

            return id_a;
        }
        private int pushGood()
        {
            bool flag = false;
            int id_a = 0;
            foreach (string a in listGood)
            {
                flag = checkDate(int.Parse(a));
                if (flag == true)
                {
                    id_a = int.Parse(a);
                    listGood.Remove(a);
                    removeDate(id_a);
                    break;
                }

            }

            return id_a;
        }
        private int pushEasy()
        {
            bool flag = false;
            int id_a = 0;
            foreach (string a in listEasy)
            {
                flag = checkDate(int.Parse(a));
                if (flag == true)
                {
                    id_a = int.Parse(a);
                    listEasy.Remove(a);
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
            int m,h;
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
            m = date.Minute - DateTime.Now.Minute;
            h = date.Hour - DateTime.Now.Hour;
            if (date == DateTime.Now || (m <= 0 && h == 0)||(m <= 0 && h<0))
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

        private void btnImport_Click_1(object sender, EventArgs e)
        {
            frmImportFile frmImport = new frmImportFile();
            frmImport.ShowDialog();
        }
    }
}
