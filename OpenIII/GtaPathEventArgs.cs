using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenIII
{
    public class GtaPathEventArgs : EventArgs
    {
        public string Path { get; }

        public GtaPathEventArgs(string path)
        {
            Path = path;
        }
    }
}
