using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FileSync
{
    public partial class LoginForm : Form
    {
        private MainForm main;
        private string Username { get; set; }
        private string Password { get; set; }

        public LoginForm(MainForm m)
        {
            InitializeComponent();
            main = m;
            main.Enabled = false;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            Username = usernameEdt.Text;
            Password = passwordEdt.Text;

            if (check_user())
            {
                main.Enabled = true;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid username/password. Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// check valid Username and Password
        /// </summary>
        /// <returns>
        /// true if valid user and password
        /// else return false
        /// </returns>
        private bool check_user()
        {
            if (Username == "admin" && Password == "123")
                return true;
            return false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mega.nz");
        }
    }
}
