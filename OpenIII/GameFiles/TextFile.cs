using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenIII.GameFiles
{
    public class TextFile : GameFile
    {
        public List<String[]> ParseData(string path)
        {
            string iteratableLine;
            List<string> listOfRows = new List<string>();
            List<string[]> listOfArrayParams = new List<string[]>();
            StreamReader Reader = null;

            if (File.Exists(path))
            {
                Reader = new StreamReader(path);
            }
            else
            {
                Console.WriteLine("Ошибка чтения файла");
                Environment.Exit(0);
            }

            // определяем тип обрабатываемого файла
            switch (Path.GetExtension(path))
            {
                case ".cfg":
                    while ((iteratableLine = Reader.ReadLine()) != null)
                    {
                        if (Char.IsLetterOrDigit(iteratableLine[0]))
                        {
                            listOfRows.Add(Regex.Replace(iteratableLine, @"\s+", " "));
                        }
                    }


                    listOfRows.ForEach(delegate (String row)
                    {
                        listOfArrayParams.Add(row.Split(' '));
                    });

                    break;

                case ".dat":
                    while ((iteratableLine = Reader.ReadLine()) != null)
                    {
                        if (iteratableLine != "" && Char.IsLetterOrDigit(iteratableLine[0]))
                        {
                            listOfRows.Add(Regex.Replace(iteratableLine, @"\s+", ""));
                        }
                    }

                    listOfRows.ForEach(delegate (String row)
                    {
                        listOfArrayParams.Add(row.Split(','));
                    });

                    break;
            }

            return listOfArrayParams;
        }
    }
}
