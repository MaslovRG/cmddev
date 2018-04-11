using System;
using System.Collections.Generic;
using System.Text;
using FileSyncSDK.Interfaces;
using FileSyncSDK.Implementations;
using FileSyncSDK.Enums;

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

        public string LocalSettingsPath
        {
            get
            {
                return localSettings.FilePath;
            }

            set
            {
                if (localSettings == null)
                    localSettings = new Settings(SettingsFileType.Local, value);
                else
                    localSettings.FilePath = value;
            }
        }

        public string UserLogin
        {
            get
            {
                return login;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                login = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return password;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                password = value;
            }
        }

        public string ServiceName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServiceFolderPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IReadOnlyList<IGroup> GlobalGroups => throw new NotImplementedException();

        public IReadOnlyList<IGroup> LocalGroups => throw new NotImplementedException();

        public IProgress<IProgressData> ProgressView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private ISettings localSettings = null;
        private ISettings globalSettings = null;
        private string login = null;
        private string password = null;
        private string serviceFolder = null;
        private string serviceName = null;

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
            using (ISyncronizer syncronizer = new Syncronizer(serviceName, login, password, serviceFolder))
            {
                // TODO
            }
        }

        public void Syncronize()
        {
            throw new NotImplementedException();
        }

        public bool CloudLoginSuccess()
        {
            try
            {
                using (ISyncronizer test = new Syncronizer(serviceName, login, password, serviceFolder))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
