using System;
using System.Collections.Generic;
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

        private string LocalWorkingPath { get; set; }
        private string GlobalWorkingPath { get; set; }

        public MainForm()
        {
            InitializeComponent();
            model = new FileSyncMain(Directory.GetCurrentDirectory(), null);
            loginForm = new LoginForm(this, model);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                loginForm.ShowDialog();
                model = loginForm.model;

                logStt.Text = "Welcome, " + model.UserLogin;

                //LocalWorkingPath = model.LocalSettingsPath;
                //GlobalWorkingPath = model.ServiceFolderPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void setPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pathForm = new PathForm(this);
            pathForm.ShowDialog();
            //WorkingPath = pathForm.FolderPath;

            TreePath.ShowDirectory(localTree, model.LocalSettingsPath);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Syncronize();
        }

        private void switchAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm_Load(sender, e);
        }
    }
}
