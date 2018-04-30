using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using FileSyncSDK.Exceptions;
using FileSyncSDK.Interfaces;
using FileSyncSDK.Enums;

namespace FileSync
{
    public partial class LoginForm : Form
    {
        private MainForm mainForm;
        public IMain model { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }

        public LoginForm(MainForm m, IMain iMain)
        {
            InitializeComponent();

            mainForm = m;
            mainForm.Enabled = false;
            model = iMain;

            // check exist file settings
            // if does, show saved login and password
            // else add an example
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            model.UserLogin = usernameEdt.Text;
            model.UserPassword = passwordEdt.Text;

            if (model.CloudLoginSuccess())
            {
                mainForm.Enabled = true;
                Close();
            }
            else
                MessageBox.Show((new ServiceSignInFailedException()).Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mega.nz");
        }
    }
}
