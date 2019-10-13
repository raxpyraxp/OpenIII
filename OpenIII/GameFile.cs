using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenIII {
    class GameFile {
        public string filePath;
    }

    class App {
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
    }
}