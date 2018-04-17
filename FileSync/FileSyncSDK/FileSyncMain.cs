using System;
using System.Collections.Generic;
using System.Text;
using FileSyncSDK.Interfaces;
using FileSyncSDK.Implementations;
using FileSyncSDK.Enums;
using System.IO;
using System.Linq;

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

        private void ResetCloudService()
        {
            if (localSettings != null && localSettings.CloudService != null)
                cloudService = localSettings.CloudService;
            else
                cloudService = new CloudService();
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

                ResetCloudService();
            }
        }

        public string UserLogin
        {
            get
            {
                return cloudService.UserLogin;
            }

            set
            {
                cloudService.UserLogin = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return cloudService.UserPassword;
            }

            set
            {
                cloudService.UserPassword = value;
            }

        }

        public string ServiceName
        {
            get
            {
                return cloudService.ServiceName;
            }

            set
            {
                cloudService.ServiceName = value;
            }
        }

        public string ServiceFolderPath
        {
            get
            {
                return cloudService.ServiceFolderPath;
            }

            set
            {
                cloudService.ServiceFolderPath = value;
            }
        }

        public IReadOnlyList<IGroup> GlobalGroups
        {
            get
            {
                return globalSettings?.Groups.ToList();
            }
        }

        public IReadOnlyList<IGroup> LocalGroups
        {
            get
            {
                return localSettings?.Groups.ToList();
            }
        }

        public IProgress<IProgressData> ProgressView
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
            }
        }

        private ISettings localSettings = null;
        private ISettings globalSettings = null;
        private ICloudService cloudService = null;
        private IProgress<IProgressData> progress = null;

        public void DeleteGroup(string name, bool local, bool global)
        {
            if (local)
            {
                IGroup group = localSettings.Groups.SingleOrDefault(g => g.Name == name);
                if (group != null)
                    localSettings.Groups.Remove(group);
            }

            if (global)
            {
                using (ISession session = cloudService.OpenSession(progress))
                {
                    globalSettings = session.GlobalSettings;
                    IGroup group = globalSettings.Groups.SingleOrDefault(g => g.Name == name);
                    if (group != null)
                        session.DeleteGroup(group);
                }
            }
        }

        public void NewGroup(string name, string[] files, string[] folders)
        {
            IGroup group = new Group(name, files, folders);
            if (localSettings.Groups.Contains(group))
                throw new ArgumentException("Group with that name already exists.");

            localSettings.Groups.Add(group);
        }

        public void GetData()
        {
            using (ISession session = cloudService.OpenSession(progress))
            {
                globalSettings = session.GlobalSettings;
            }
        }

        public void Syncronize()
        {
            using (ISession session = cloudService.OpenSession(progress))
            {
                globalSettings = session.GlobalSettings;
                foreach (IGroup localGroup in localSettings.Groups)
                {
                    IGroup globalGroup = globalSettings.Groups.SingleOrDefault(g => g.Name == localGroup.Name);
                    session.SyncronizeGroups(localGroup, globalGroup);
                }
            }
        }

        public bool CloudLoginSuccess()
        {
            try
            {
                using (ISession session = cloudService.OpenSession())
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
