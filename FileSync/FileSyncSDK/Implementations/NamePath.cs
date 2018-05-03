using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSyncSDK.Implementations
{
    internal class NamePath : INamePath
    {
        public NamePath(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; private set; }

        public string Path { get; set; }
    }
}
