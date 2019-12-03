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
        public GameDirectory(string path) : base(path)
        {
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
            DirectoryInfo dir = new DirectoryInfo(FullPath);
            List<GameFile> gameFiles = new List<GameFile>();

            foreach (FileInfo file in dir.GetFiles())
            {
                gameFiles.Add(GameFile.CreateInstance(Path.Combine(file.DirectoryName, file.Name)));
            }

            return gameFiles;
        }

        public List<GameDirectory> GetDirectories()
        {
            DirectoryInfo rootdir = new DirectoryInfo(FullPath);
            List<GameDirectory> gameDirectories = new List<GameDirectory>();

            foreach (DirectoryInfo dir in rootdir.GetDirectories())
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

        public override string GetName()
        {
            return new DirectoryInfo(FullPath).Name;
        }

        public override string GetExtension()
        {
            return new DirectoryInfo(FullPath).Extension;
        }
    }
}
