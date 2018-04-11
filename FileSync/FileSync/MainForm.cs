using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FileSync
{
    public partial class MainForm : Form
    {
        private LoginForm loginForm;
        
        private string WorkingPath { get; set; }

        public MainForm()
        {
            InitializeComponent();
            loginForm = new LoginForm(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginForm.ShowDialog();
            logStt.Text = "Welcome, " + loginForm.Username;

            var pathForm = new PathForm(this);
            pathForm.ShowDialog();
            WorkingPath = pathForm.FolderPath;
            
            ShowDirectory(treePath, WorkingPath);
        }

        private void ShowDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles())
                    currentNode.Nodes.Add(new TreeNode(file.Name));
            }

            treeView.Nodes.Add(node);
        }
    }
}
