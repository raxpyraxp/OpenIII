using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenIII
{
    public class PathEventArgs : EventArgs
    {
        public string Path { get; }

        public PathEventArgs(string path)
        {
            Path = path;
        }
    }
}
