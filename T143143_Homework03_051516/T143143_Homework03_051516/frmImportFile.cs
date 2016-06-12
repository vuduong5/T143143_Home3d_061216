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
    public partial class frmImportFile : Form
    {

        private string connectionData = "";
        private string textDataSouce = "";
        private string textUserID = "";
        private string textPassword = "";

        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataReader reader;
        string sql = "";
        private string connectionString = "";
        public frmImportFile()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                connectionString = "Data Source="+txtDatasouce.Text+";User ID="+txtUserID.Text+";Password="+txtPassword.Text+"";
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "EXEC sp_databases";
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();
                reader.Dispose();
                conn.Close();
                conn.Dispose();
                txtDatasouce.Enabled = false;
                txtPassword.Enabled = false;
                txtUserID.Enabled = false;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnRestore.Enabled = true;
            
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string connect() 
        {
            connectionData = "Data Source=" + txtDatasouce.Text + ";Initial Catalog=Vocabulary;User ID=" + txtUserID.Text + ";Password=" + txtPassword.Text + "";
            return connectionData;

        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            txtDatasouce.Enabled = true;
            txtPassword.Enabled = true;
            txtUserID.Enabled = true;
            btnRestore.Enabled = false;
            btnConnect.Enabled = true;
        
        }

        public void frmImportFile_Load(object sender, EventArgs e)
        {
            btnDisconnect.Enabled = false;
            btnRestore.Enabled = false;
            loadSetting();
            
        }
        private void loadSetting() 
        {
            this.txtDatasouce.Text = Properties.Settings.Default.textDataSouce;
            this.txtPassword.Text = Properties.Settings.Default.textPassword;
            this.txtUserID.Text = Properties.Settings.Default.textUserID;
            
        }
        private void saveSetting()
        {
            Properties.Settings.Default.textDataSouce = this.txtDatasouce.Text;
            Properties.Settings.Default.textPassword = this.txtPassword.Text;
            Properties.Settings.Default.textUserID = this.txtUserID.Text;
            Properties.Settings.Default.Save();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Backup Files (*.bak)|*.bak| All Files (*.*)|*.*";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog() == DialogResult.OK) 
            {
                txtRestoreFile.Text = dlg.FileName;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
     
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "Restore Database " + txtNameRestore.Text + " FROM Disk = '" + txtRestoreFile.Text + "'";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                MessageBox.Show("Successfully restore Database");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void frmImportFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveSetting();
        }
    }
}
