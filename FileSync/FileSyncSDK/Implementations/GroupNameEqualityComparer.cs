using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSyncSDK.Implementations
{
    internal class GroupNameEqualityComparer : IEqualityComparer<IGroup>
    {
        public bool Equals(IGroup x, IGroup y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(IGroup obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
