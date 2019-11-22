using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    public abstract class GameResource
    {
        public string FullPath { get; }
        public string Name { get => getName(); }
        public string Extension { get => getExtension(); }

        public GameResource(string path)
        {
            FullPath = path;
        }

        public GameResource createInstance(string path)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return GameDirectory.createInstance(path);
            }
            else
            {
                return GameFile.createInstance(path);
            }

        }

        public abstract string getName();

        public abstract string getExtension();
    }
}
