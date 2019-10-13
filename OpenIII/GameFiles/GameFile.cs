using System;
using System.IO;

namespace OpenIII.GameFiles
{
    public class GameFile
    {
        public string FilePath { get; }
        public string Extension { get => getExtension(FilePath); }

        public GameFile(string path)
        {
            FilePath = path;
        }

        public static GameFile createInstance(string path)
        {
            switch(getExtension(path))
            {
                case "img":
                    return ArchiveFile.createInstance(path);
                default:
                    throw new Exception("Invalid file type");
            }
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