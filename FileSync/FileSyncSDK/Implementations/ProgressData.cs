using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSyncSDK.Implementations
{
    internal class ProgressData : IProgressData
    {
        public ProgressData(SyncStage stage, IGroupData group = null, string info = null)
        {
            Stage = stage;
            Group = group;
            Info = info;
        }

        public SyncStage Stage { get; set; }

        public IGroupData Group { get; set; }

        public string Info { get; set; }
    }
}
