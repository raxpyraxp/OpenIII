using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    public class GameDirectory : GameResource
    {
        public GameDirectory(string path) : base(path)
        {
        }

        public static new GameDirectory createInstance(string path)
        {
            return new GameDirectory(path);
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

        public List<GameDirectory> getDirectories()
        {
            DirectoryInfo rootdir = new DirectoryInfo(FullPath);
            List<GameDirectory> gameDirectories = new List<GameDirectory>();

            foreach (DirectoryInfo dir in rootdir.GetDirectories())
            {
                gameDirectories.Add(GameDirectory.createInstance(dir.FullName));
            }

            return gameDirectories;
        }

        public List<GameResource> getContent()
        {
            List<GameResource> resources = new List<GameResource>();
            resources.AddRange(getDirectories());
            resources.AddRange(getFiles());

            return resources;
        }

        public override string getName()
        {
            return new DirectoryInfo(FullPath).Name;
        }

        public override string getExtension()
        {
            return new DirectoryInfo(FullPath).Extension;
        }
    }
}
