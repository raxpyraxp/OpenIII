using System;
using System.IO;

namespace OpenIII.GameFiles
{
    public class GameFile : GameResource
    {
        public GameFile(string path) : base(path)
        {
        }

        public static new GameFile createInstance(string path)
        {
            switch(getExtension(path))
            {
                case "img":
                    return ArchiveFile.createInstance(path);
                default:
                    return new GameFile(path);
            }
        }

        public override string getName()
        {
            return new FileInfo(FullPath).Name;
        }

        public override string getExtension()
        {
            return new FileInfo(FullPath).Extension;
        }

        public static string getExtension(string path)
        {
            return new FileInfo(path).Extension;
        }
    }

    /*class App {
        static void Main() {
            TextFile file = new TextFile();

            List<string[]> result = file.ParseData("water.dat");

            result.ForEach(delegate (String[] item)
            {
                foreach (string i in item)
                {
                    Console.WriteLine(i);
                }
            });
        }
    }*/
}