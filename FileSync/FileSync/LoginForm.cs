using System;
using System.Diagnostics;
using System.Windows.Forms;
using FileSyncSDK.Exceptions;
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
            {
                usernameEdt.Text = model.UserLogin;
                passwordEdt.Text = model.UserPassword;
            }
            else
            {
                usernameEdt.Text = "Username";
                passwordEdt.Text = "password";
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            model.UserLogin = usernameEdt.Text;
            model.UserPassword = passwordEdt.Text;

            if (model.CloudLoginSuccess())
            {
                try
                {
                    model.ServiceFolderPath = serverPathEdt.Text;
                }
                catch (InvalidServiceFolderPathException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                mainForm.Enabled = true;
                Close();
            }
            else
            {
                MessageBox.Show((new ServiceSignInFailedException()).Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
