using System;
using System.Windows.Forms;

namespace FileSync
{
    public partial class PathForm : Form
    {
        private MainForm main;
        public string FolderPath { get; set; }

        public PathForm(MainForm m)
        {
            InitializeComponent();
            main = m;
            //main.Enabled = false;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (pathBrowser.ShowDialog(this) == DialogResult.OK)
            {
                folderPath.Text = pathBrowser.SelectedPath;
            }
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            if (folderPath.Text != "")
            {
                FolderPath = folderPath.Text;
                //main.Enabled = true;
                Close();
            }
            else
            {
                MessageBox.Show("Path can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
