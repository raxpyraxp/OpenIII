using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using OpenIII.Utils;

namespace OpenIII.GameFiles
{
    public class GameDirectory : GameResource
    {
        public override string Name { get => directoryInfo.Name; }
        public override string Extension { get => directoryInfo.Extension; }
        private DirectoryInfo directoryInfo;

        public GameDirectory(string path) : base(path)
        {
            this.directoryInfo = new DirectoryInfo(FullPath);
        }

        public static new GameDirectory CreateInstance(string path)
        {
            return new GameDirectory(path);
        }

        public override Bitmap GetIcon(IconSize size)
        {
            return Properties.Resources.Folder;
        }

        public List<GameFile> GetFiles()
        {
            List<GameFile> gameFiles = new List<GameFile>();

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                gameFiles.Add(GameFile.CreateInstance(Path.Combine(file.DirectoryName, file.Name)));
            }

            return gameFiles;
        }

        public List<GameDirectory> GetDirectories()
        {
            List<GameDirectory> gameDirectories = new List<GameDirectory>();

            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                gameDirectories.Add(GameDirectory.CreateInstance(dir.FullName));
            }

            return gameDirectories;
        }

        public List<GameResource> GetContent()
        {
            List<GameResource> resources = new List<GameResource>();
            resources.AddRange(GetDirectories());
            resources.AddRange(GetFiles());

            return resources;
        }
    }
}
