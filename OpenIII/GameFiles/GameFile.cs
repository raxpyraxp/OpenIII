using System;
using System.IO;

namespace OpenIII.GameFiles
{
    public class GameFile : GameResource
    {
        public GameFile(string path) : base(path)
        {
        }

        public static new GameFile CreateInstance(string path)
        {
            switch(GetExtension(path))
            {
                case "img":
                    return ArchiveFile.CreateInstance(path);
                default:
                    return new GameFile(path);
            }
        }

        public override string GetName()
        {
            return new FileInfo(FullPath).Name;
        }

        public override string GetExtension()
        {
            return new FileInfo(FullPath).Extension;
        }

        public static string GetExtension(string path)
        {
            return new FileInfo(path).Extension;
        }
    }
}