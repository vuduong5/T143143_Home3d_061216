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
namespace T143143_Homework03_051516
{
    public partial class frmCaiDat : Form
    {
        public frmCaiDat()
        {
            InitializeComponent();
        }
        //SqlConnection conn = new SqlConnection(@"Data Source=DELL\SQLEXPRESS;Initial Catalog=Vocabulary;User ID=sa;Password=123456");
        frmImportFile import = new frmImportFile();
        SqlConnection conn;
        SqlCommand command;
        string imgLoc = "";
        private void btnBrower_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif | All Files (*.*)|*.*";
                dlg.Title = "Select Vocabulary";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    pictureBox1.ImageLocation = imgLoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                string sql = "insert into Word(id,word,pronounce,synonym,Meaning,Type,Explain_EL,Image) values (" + txtID.Text + ",N'" + txtWord.Text + "',N'" + txtPronouce.Text +"','"+ txtSynonym.Text+"',N'" + txtMean.Text + "','" + txtType.Text + "','" + txtExplain.Text + "',@img)";
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@img", img));
                int x = command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show(x.ToString() + " Record(s) (save).");

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT id,word,pronounce,Meaning,Type,Explain_EL,Image,synonym FROM word WHERE word = '" + txtWord.Text+"'";
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    txtID.Text = reader[0].ToString();
                    txtWord.Text = reader[1].ToString();
                    txtPronouce.Text = reader[2].ToString();
                    txtMean.Text = reader[3].ToString();
                    txtType.Text = reader[4].ToString();
                    txtExplain.Text = reader[5].ToString();
                    txtSynonym.Text = reader[7].ToString();
                    byte[] img = (byte[])(reader[6]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                else
                {
                    MessageBox.Show("this is does not exits");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void frmCaiDat_Load(object sender, EventArgs e)
        {
            import.frmImportFile_Load(null, null);
            conn = new SqlConnection(@"" + import.connect());
            btnBrower.Click += new EventHandler(btnBrower_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnShow.Click += new EventHandler(btnShow_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
             
                string sql = "update word set word = '"+txtWord.Text+"', pronounce = '"+txtPronouce.Text+"', synonym = N'"+txtSynonym.Text+"', Meaning = N'"+txtMean.Text+"', Type = '"+txtType.Text+"', Explain_EL = '"+txtExplain.Text+"' where id =" +txtID.Text;
                                                   
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                command = new SqlCommand(sql, conn);             
                int x = command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show(x.ToString() + " Edit successfully");

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
