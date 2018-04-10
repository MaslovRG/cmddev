using System;
using System.Windows.Forms;

namespace FileSync
{
    public partial class MainForm : Form
    {
        private LoginForm loginForm;

        public MainForm()
        {
            InitializeComponent();
            loginForm = new LoginForm(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginForm.ShowDialog();
        }
    }
}
