using System;
using System.Diagnostics;
using System.Windows.Forms;
using FileSyncSDK.Interfaces;

namespace FileSync
{
    public partial class LoginForm : Form
    {
        private MainForm mainForm;
        public IMain model { get; set; }

        public LoginForm(MainForm m, IMain iMain)
        {
            InitializeComponent();

            mainForm = m;
            mainForm.Enabled = false;
            model = iMain;

            // check exist file settings
            // if does, show saved login and password
            // else show default
            if (model.UserLogin != null)
                usernameEdt.Text = model.UserLogin;
            else
            {
                usernameEdt.Text = "Username";
                passwordEdt.Text = "Password";
            }
            serverPathEdt.Text = model.ServiceFolderPath;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                model.ServiceFolderPath = serverPathEdt.Text;
                model.UserLogin = usernameEdt.Text;
                model.UserPassword = passwordEdt.Text;
                model.GetData();
                mainForm.Enabled = true;
                Close();
            }
            catch (ArgumentNullException)
            {
                ErrorMsg("Поля ввода не могут быть пустыми");
            }
            catch (Exception excp)
            {
                ErrorMsg(excp.Message);
            }
            //// just for test
            //model.UserLogin = usernameEdt.Text;
            //model.UserPassword = passwordEdt.Text;
            //mainForm.Enabled = true;
            //Close();
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mega.nz");
        }

        private void ErrorMsg(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
