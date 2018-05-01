using System;
using System.IO;
using System.Windows.Forms;
using FileSyncSDK;
using FileSyncSDK.Interfaces;

namespace FileSync
{
    public partial class MainForm : Form
    {
        private LoginForm loginForm;
        private IMain model { get; set; }

        public MainForm()
        {
            InitializeComponent();
            model = new FileSyncMain(Directory.GetCurrentDirectory(), null);
            loginForm = new LoginForm(this, model);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginForm.ShowDialog();
            model = loginForm.model;
            TreePath.ShowDirectory(localTree, model.LocalSettingsPath);
            TreePath.ShowDirectory(globalTree, model.ServiceFolderPath);
            logStt.Text = "Добрый день, " + model.UserLogin;
        }

        private void setPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                model.LocalSettingsPath = folderBrowser.SelectedPath;
                TreePath.ShowDirectory(localTree, model.LocalSettingsPath);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Syncronize();
        }

        private void switchAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm_Load(sender, e);
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var log = new LogOutForm(this);
            log.ShowDialog();
        }
    }
}
