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

using OpenIII.GameFiles.ConfigSections.IPL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenIII.GameFiles
{
    public class IPLFile : ConfigFile
    {
        public IPLFile(string filePath) : base(filePath) { }

        public void ParseData()
        {
            string lineIterator;
            StreamReader Reader = new StreamReader(this.FullPath);
            Exception exception = new Exception("Неизвестное количество параметров.");

            while (!Reader.EndOfStream)
            {
                lineIterator = Reader.ReadLine();
                List<string> paramsBuf;

                if (ExcludedSynbols.IndexOf(lineIterator) >= 0 || lineIterator.IndexOf('#') > -1)
                {
                    continue;
                }

                if (SectionNames.IndexOf(lineIterator) >= 0)
                {
                    ConfigSections.Add(new ConfigSection(lineIterator));
                    continue;
                }

                paramsBuf = new List<string>(lineIterator.Split(','));

                if (paramsBuf.Count < 2) continue;

                paramsBuf = CleanParams(paramsBuf);
                var configRows = ConfigSections.Last().ConfigRows;

                switch (ConfigSections.Last().Name)
                {
                    case INST.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 11:
                                configRows.Add(new INSTType3().Parse(paramsBuf));
                                break;
                            case 12:
                                configRows.Add(new INSTType1().Parse(paramsBuf));
                                break;
                            case 13:
                                configRows.Add(new INSTType2().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case AUZO.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 7:
                                configRows.Add(new AUZOType2().Parse(paramsBuf));
                                break;
                            case 9:
                                configRows.Add(new AUZOType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case CARS.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 12:
                                configRows.Add(new CARSType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case CULL.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 11:
                                if (Int32.TryParse(paramsBuf[3], out int _))
                                {
                                    configRows.Add(new CULLType2().Parse(paramsBuf));
                                }
                                else
                                {
                                    configRows.Add(new CULLType1().Parse(paramsBuf));
                                }
                                break;
                            case 13:
                                configRows.Add(new CULLType3().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case ENEX.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 18:
                                configRows.Add(new ENEXType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case GRGE.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 11:
                                configRows.Add(new GRGEType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case JUMP.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 16:
                                configRows.Add(new JUMPType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case OCCL.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 7:
                                configRows.Add(new OCCLType1().Parse(paramsBuf));
                                break;
                            case 9:
                                configRows.Add(new OCCLType2().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case PICK.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 4:
                                configRows.Add(new PICKType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case TCYC.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 11:
                                configRows.Add(new TCYCType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                    case ZONE.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 10:
                                configRows.Add(new ZONEType1().Parse(paramsBuf));
                                break;
                        }
                        break;
                }
            }

            Reader.Close();
        }
    }
}
