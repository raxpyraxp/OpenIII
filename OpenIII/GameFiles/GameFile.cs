using System;
using System.IO;

namespace OpenIII.GameFiles
{
    public class GameFile
    {
        public string filePath { get; }

        public GameFile(string filePath)
        {
            this.filePath = filePath;
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