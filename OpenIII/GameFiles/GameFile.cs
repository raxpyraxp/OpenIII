using System;
using System.IO;

namespace OpenIII.GameFiles
{
    public class GameFile
    {
        public string FullPath { get; }
        public string Name { get => getName(FullPath); }
        public string Extension { get => getExtension(FullPath); }

        public GameFile(string path)
        {
            FullPath = path;
        }

        public static GameFile createInstance(string path)
        {
            switch(getExtension(path))
            {
                case "img":
                    return ArchiveFile.createInstance(path);
                default:
                    return new GameFile(path);
            }
        }

        public static string getName(string path)
        {
            return new FileInfo(path).Name;
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