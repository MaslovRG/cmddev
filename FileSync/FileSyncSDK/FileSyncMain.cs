using System;
using System.Collections.Generic;
using System.Text;
using FileSyncSDK.Interfaces;
using FileSyncSDK.Implementations;

namespace FileSyncSDK
{
    public class FileSyncMain : IMain
    {
        /// <summary>
        /// Конструктор основного класса программы FileSync.
        /// </summary>
        /// <param name="localSettingsPath">Значение параметра LocalSettingsPath.</param>
        /// <param name="progressView">Значение параметра ProgessView, может быть null.</param>
        /// <exception cref="ArgumentNullException">localSettingsPath null.</exception>
        public FileSyncMain(string localSettingsPath, IProgress<IProgressData> progressView)
        {
            LocalSettingsPath = localSettingsPath;
            ProgressView = progressView;
        }

        public string LocalSettingsPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CloudLogin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CloudPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IProgress<IProgressData> ProgressView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IReadOnlyList<IGroup> GlobalGroups => throw new NotImplementedException();

        public IReadOnlyList<IGroup> LocalGroups => throw new NotImplementedException();

        private ISettings localSettings = null;
        private ISettings globalSettings = null;
        private ISyncronizer syncronizer = null;

        public void DeleteGroup(string name, bool local, bool global)
        {
            throw new NotImplementedException();
        }

        public void NewGroup(string name, string[] files, string[] folders)
        {
            throw new NotImplementedException();
        }

        public void GetData()
        {
            throw new NotImplementedException();
        }

        public void Syncronize()
        {
            throw new NotImplementedException();
        }
    }
}
