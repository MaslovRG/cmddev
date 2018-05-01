using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FileSyncSDK;
using FileSyncSDK.Enums;
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
            logStt.Text = "Добрый день, " + model.UserLogin;

            // Show local groups
            ShowGroup(localTable, model.LocalGroups);

            // Show global groups
        //    ShowGroup(globalTable, model.GlobalGroups);
        }

        /// <summary>
        /// Show groups (applying for localTable and globalTable)
        /// </summary>
        /// <param name="view"></param>
        /// <param name="listGroup"></param>
        private void ShowGroup(DataGridView view, IReadOnlyList<IGroupData> listGroup)
        {
            view.Rows.Clear();
            for (int i = 0; i < listGroup.Count; i++)
            {
                view[0, i].Value = listGroup[i].Name;
                view[1, i].Value = listGroup[i].LastSync.ToString();
            }
        }

        /// <summary>
        /// Show List<INamePath>
        /// </summary>
        /// <param name="view"></param>
        /// <param name="list"></param>
        private void ShowNamePath(DataGridView view, IReadOnlyList<INamePath> list)
        {
            view.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                view[0, i].Value = list[i].Name;
                view[1, i].Value = list[i].Path;
            }
        }

        /// <summary>
        /// Show group's properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="group"></param>
        private void CellContentClick(object sender, DataGridViewCellEventArgs e, IReadOnlyList<IGroupData> group)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                // main stuff: Group's detail
                nameGroupEdt.Text = group[index].Name;
                dataSyncEdt.Text = group[index].LastSync.ToString();

                // files
                ShowNamePath(propertyFileTable, group[index].Files);

                // folders
                ShowNamePath(propertyFolderTable, group[index].Folders);
            }
        }

        private void localTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CellContentClick(sender, e, model.LocalGroups);
        }

        private void globalTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CellContentClick(sender, e, model.GlobalGroups);
        }

        /// <summary>
        /// Add group by type
        /// </summary>
        /// <param name="type"></param>
        private void AddGroup(SettingsFileType type)
        {
            // group name
            string name = nameGroupEdt.Text;

            // files in group
            string[] files = null;
            if (propertyFileTable.RowCount > 0)
            {
                files = new string[propertyFileTable.RowCount];
                for (int i = 0; i < propertyFileTable.RowCount; i++)
                {
                    files[i] = propertyFileTable[0, i].Value.ToString();
                }
            }

            // folders in groups
            string[] folders = null;
            if (propertyFolderTable.RowCount > 0)
            {
                files = new string[propertyFolderTable.RowCount];
                for (int i = 0; i < propertyFolderTable.RowCount; i++)
                {
                    files[i] = propertyFolderTable[0, i].Value.ToString();
                }
            }

            model.NewGroup(name, files, folders);

            // update view
            if (type == SettingsFileType.Local)
                ShowGroup(localTable, model.LocalGroups);
            else
                ShowGroup(globalTable, model.GlobalGroups);
        }

        /// <summary>
        /// Delete group by type
        /// </summary>
        /// <param name="type"></param>
        private void DeleteGroup(SettingsFileType type)
        {
            string name = nameGroupEdt.Text;
            if (type == SettingsFileType.Local)
            {
                model.DeleteGroup(name, true, false);
                ShowGroup(localTable, model.LocalGroups);
            }
            else
            {
                model.DeleteGroup(name, false, true);
                ShowGroup(globalTable, model.GlobalGroups);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            string controlName = menu.SourceControl.Name;

            var type = (controlName == "localTable") ? SettingsFileType.Local : SettingsFileType.Global;
            AddGroup(type);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            string controlName = menu.SourceControl.Name;

            var type = (controlName == "localTable") ? SettingsFileType.Local : SettingsFileType.Global;
            DeleteGroup(type);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            model.Syncronize();
        }

        private void setPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                model.LocalSettingsPath = folderBrowser.SelectedPath;
                ShowGroup(localTable, model.LocalGroups);
            }
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

        private void propertyFileTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    propertyFileTable[1, e.RowIndex].Value = folderBrowser.SelectedPath;
                }
            }
        }

        private void propertyFolderTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    propertyFolderTable[1, e.RowIndex].Value = folderBrowser.SelectedPath;
                }
            }
        }
    }
}
