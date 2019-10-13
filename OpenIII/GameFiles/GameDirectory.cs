using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    public class GameDirectory
    {
        public string FullPath { get; }

        public GameDirectory(string path)
        {
            FullPath = path;
        }

        public List<GameFile> getFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(FullPath);
            List<GameFile> gameFiles = new List<GameFile>();

            foreach (FileInfo file in dir.GetFiles())
            {
                gameFiles.Add(GameFile.createInstance(Path.Combine(file.DirectoryName, file.Name)));
            }

            return gameFiles;
        }
    }
}
