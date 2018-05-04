using System;
using System.Linq;
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
        private SettingsFileType selectedTableType { get; set; }

        public MainForm()
        {
            InitializeComponent();
            model = new FileSyncMain(GetLocalSettingsPath(), null);
            loginForm = new LoginForm(this, model);
        }

        private string GetLocalSettingsPath()
        {
            string localSettingsFileName = "settings.xml";
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FileSync");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, localSettingsFileName);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginForm.ShowDialog();
            model = loginForm.model;
            logStt.Text = "Добрый день, " + model.UserLogin;

            // Show local groups
            ShowGroup(localTable, model.LocalGroups);

            // Show global groups
            ShowGroup(globalTable, model.GlobalGroups);
        }

        #region Display

        /// <summary>
        /// Show groups (applying for localTable and globalTable)
        /// </summary>
        /// <param name="view"></param>
        /// <param name="listGroup"></param>
        private void ShowGroup(DataGridView view, IReadOnlyList<IGroupData> listGroup)
        {
            view.Rows.Clear();
            view.RowCount = listGroup.Count;
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
            view.RowCount = 1;
            if (list.Count > 0)
            {
                view.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    view[0, i].Value = list[i].Name;
                    view[1, i].Value = list[i].Path;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="group"></param>
        private void CellContentClick(object sender, DataGridViewCellEventArgs e, IReadOnlyList<IGroupData> group)
        {
            var view = sender as DataGridView;
            if (view.RowCount > 0)
            {
                int index = view.CurrentCell.RowIndex;

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
            selectedTableType = SettingsFileType.Local;
            CellContentClick(sender, e, model.LocalGroups);
        }

        private void globalTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedTableType = SettingsFileType.Global;
            CellContentClick(sender, e, model.GlobalGroups);
        }

        private void propertyFileTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (propertyFileTable.RowCount == e.RowIndex + 1)
                    propertyFileTable.RowCount++;

                if (fileBrowser.ShowDialog() == DialogResult.OK)
                {
                    propertyFileTable[1, e.RowIndex].Value = fileBrowser.FileName;
                    propertyFileTable[0, e.RowIndex].Value = Path.GetFileName(fileBrowser.FileName);
                }
            }
        }

        private void propertyFolderTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (propertyFolderTable.RowCount == e.RowIndex + 1)
                    propertyFolderTable.RowCount++;

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    propertyFolderTable[1, e.RowIndex].Value = folderBrowser.SelectedPath;
                    propertyFolderTable[0, e.RowIndex].Value = Path.GetFileName(folderBrowser.SelectedPath);
                }
            }
        }

        #endregion

        #region Clear
        /// <summary>
        /// Clear table
        /// </summary>
        /// <param name="view"></param>
        private void ClearAllAfterAddOrDelete(DataGridView view)
        {
            view.Rows.Clear();
            view.RowCount = 1;
        }

        private void ClearAllAfterAddOrDelete()
        {
            nameGroupEdt.Clear();
            dataSyncEdt.Clear();
            ClearAllAfterAddOrDelete(propertyFileTable);
            ClearAllAfterAddOrDelete(propertyFolderTable);
        }
        #endregion

        #region Main operations with group: ADD, UPDATE, DELETE

        /// <summary>
        /// Get data from table
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private string[] getDataTable(DataGridView view)
        {
            var data = view.Rows
                .Cast<DataGridViewRow>()
                .Where(row => !string.IsNullOrWhiteSpace(row.Cells[1]?.Value as string))
                .Select(row => row.Cells[1].Value as string)
                .Distinct()
                .ToArray();

            if (data.Any())
                return data;
            else
                return null;
        }

        /// <summary>
        /// Add group by type
        /// </summary>
        /// <param name="type"></param>
        private void AddGroup(SettingsFileType type)
        {
            if (nameGroupEdt.Text != null)
            {
                // group name
                string name = nameGroupEdt.Text;

                // files in group
                string[] files = getDataTable(propertyFileTable);

                // folders in groups
                string[] folders = getDataTable(propertyFolderTable);

                // CASE: add group from global to local (solved)
                //if (selectedTableType == SettingsFileType.Global)
                //    model.NewGroup(name);
                //else
                //    model.NewGroup(name, files, folders);

                model.NewGroup(name, files, folders);

                // update view
                if (type == SettingsFileType.Local)
                    ShowGroup(localTable, model.LocalGroups);
                else
                    ShowGroup(globalTable, model.GlobalGroups);
            }
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
                model.DeleteGroup(name, true, false);
                model.DeleteGroup(name, false, true);
                ShowGroup(globalTable, model.GlobalGroups);
            }
            ClearAllAfterAddOrDelete();
        }

        /// <summary>
        /// Update group by type (apply for local only)
        /// </summary>
        private void UpdateGroup()
        {
            // group name
            string name = nameGroupEdt.Text;

            if (model.LocalGroups.ToArray()
                .Any(s => s.Name == name)
                )
            {
                model.DeleteGroup(name, true, false);
                AddGroup(SettingsFileType.Local);
            }
        }

        private void SyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                model.Syncronize();
                //MessageBox.Show(model.GlobalGroups.Count.ToString());
                ShowGroup(localTable, model.LocalGroups);
                ShowGroup(globalTable, model.GlobalGroups);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }

        private void AddGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AddGroup(SettingsFileType.Local);
                ClearAllAfterAddOrDelete();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }

        private void DeleteGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // solved!
            DeleteGroup(selectedTableType);
            ClearAllAfterAddOrDelete();
        }

        private void UpdateGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateGroup();
                ClearAllAfterAddOrDelete();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Ничего не изменяется!", "Сообщение");
            }
        }

        private void nameGroupEdt_TextChanged(object sender, EventArgs e)
        {
            dataSyncEdt.Clear();
            ClearAllAfterAddOrDelete(propertyFileTable);
            ClearAllAfterAddOrDelete(propertyFolderTable);
        }

        #endregion

        #region Other stuffs

        private void switchAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm_Load(sender, e);
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var log = new LogOutForm(this);
            log.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
            // write User's Guide about how to use program
            MessageBox.Show("DevSync version 1.0\n" +
                "Author: cmddev-2018-IU7-81\n" +
                "Source: https://github.com/MaslovRG/cmddev/", "Help");
        }

        #endregion
    }
}
