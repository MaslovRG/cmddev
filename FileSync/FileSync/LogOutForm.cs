using System;
using System.Windows.Forms;

namespace FileSync
{
    public partial class LogOutForm : Form
    {
        private MainForm main;
        
        public LogOutForm(MainForm m)
        {
            InitializeComponent();
            main = m;
        }

        private void yesBtn_Click(object sender, EventArgs e)
        {
            main.Close();
	    Close();
        }

        private void noBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
