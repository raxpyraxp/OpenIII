/*
 *  This file is a part of OpenIII project, the GTA modding tool.
 *  
 *  Copyright (C) 2019-2020 Savelii Morozov (Prographer)
 *  Email: morozov.salevii@gmail.com
 *  
 *  Copyright (C) 2019-2020 Sergey Filatov (raxp)
 *  Email: raxp.worm202@gmail.com
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace OpenIII.GameFiles
{
    public class IDEFile : GameFile
    {
        // нужно будет реализовать в виде перечисления для использования в других классах
        public static List<string> sectionNames = new List<string> {
            "objs", "tobj", "hier", "cars", "peds", "path", "2dfx", "weap", "anim", "txdp"
        };

        public IDEFile(string filePath) : base(filePath) { }

        public List<ConfigSection> ConfigSections = new List<ConfigSection>();

        public void ParseData()
        {
            string lineIterator;
            StreamReader Reader = new StreamReader(this.FullPath);

            while ((lineIterator = Reader.ReadLine()) != null)
            {
                List<string> paramsBuf;

                if ((lineIterator.IndexOf('#') + lineIterator.IndexOf(';') + lineIterator.IndexOf("end")) == 0)
                {
                    continue;
                }

                if (sectionNames.IndexOf(lineIterator) != -1)
                {
                    ConfigSections.Add(new ConfigSection(lineIterator));
                    continue;
                }

                paramsBuf = new List<string>(lineIterator.Split(','));

                for (int i = 0; i < paramsBuf.Count; i++)
                {
                    paramsBuf[i] = paramsBuf[i].Trim();
                }

                switch (paramsBuf.Count)
                {
                    // определение с пятью аргументами встречается только в SA
                    case 5:
                        break;
                    
                    case 6:
                        ConfigSections[ConfigSections.Count - 1].ConfigRows.Add(new ConfigRow(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2],
                            Int32.Parse(paramsBuf[3]),
                            float.Parse(paramsBuf[4]),
                            Int32.Parse(paramsBuf[5])
                        ));
                        break;
                    
                    case 7:
                        break;
                    
                    case 8:
                        break;
                }
            }

            Reader.Close();
        }
    }


    public class ConfigSection
    {

        public string Name { get; set; }

        public List<ConfigRow> ConfigRows = new List<ConfigRow>();

        public ConfigSection(string name)
        {
            this.Name = name;
        }
    }


    public class ConfigRow
    {
        public int Id { get; set; }

        public string ModelName { get; set; }

        public string TxdName { get; set; }

        public int MeshCount { get; set; }

        public float DrawDistance { get; set; }

        public int Flags { get; set; }


        public ConfigRow(int id, string modelName, string txdName, int meshCount, float drawDistance, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance = drawDistance;
            this.Flags = flags;
        }
    }
}
