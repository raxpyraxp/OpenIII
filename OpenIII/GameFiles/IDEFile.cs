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
using System.Linq;

namespace OpenIII.GameFiles
{
    public class IDEFile : ConfigFile
    {
        public enum CarGroupsIII : int
        {
            None            = 0b_0000_0000,
            Poorfamily      = 0b_0000_0001,
            Richfamily      = 0b_0000_0010,
            Executive       = 0b_0000_0100,
            Worker          = 0b_0000_1000,
            Special         = 0b_0001_0000,
            Big             = 0b_0010_0000,
            Taxi            = 0b_0100_0000
        };

        public enum CarGroupsVC : int
        {
            None            = 0b_0000_0000_0000,
            Normal          = 0b_0000_0000_0001,
            Poorfamily      = 0b_0000_0000_0010,
            Richfamily      = 0b_0000_0000_0100,
            Executive       = 0b_0000_0000_1000,
            Worker          = 0b_0000_0001_0000,
            Big             = 0b_0000_0010_0000,
            Taxi            = 0b_0000_0100_0000,
            Moped           = 0b_0000_1000_0000,
            Motorbike       = 0b_0001_0000_0000,
            Leisureboat     = 0b_0010_0000_0000,
            Workerboat      = 0b_0100_0000_0000,
        };

        public static class VehicleClassVC
        {
            public const string Car   = "car";

            public const string Boat  = "boat";

            public const string Train = "train";

            public const string Heli  = "heli";

            public const string Plane = "plane";

            public const string Bike  = "bike";
        }

        public IDEFile(string filePath) : base(filePath) { }

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

                switch (ConfigSections.Last().Name)
                {
                    // разбор секции PATH отличается от других, так как записи имеют отличный от остальных вид
                    case PATH.SectionName:
                        lineIterator = Reader.ReadLine();

                        List<PATHNode> parsedNodes = new List<PATHNode>();
                        List<string> nodesParamsBuf = new List<string>(lineIterator.Split(','));
                        nodesParamsBuf = CleanParams(nodesParamsBuf);

                        switch (paramsBuf.Count)
                        {
                            case 3:
                                while (nodesParamsBuf.Count == 9)
                                {
                                    parsedNodes.Add(new PATHNode(
                                        Int32.Parse(nodesParamsBuf[0]),
                                        Int32.Parse(nodesParamsBuf[1]),
                                        Int32.Parse(nodesParamsBuf[2]),
                                        double.Parse(nodesParamsBuf[3]),
                                        double.Parse(nodesParamsBuf[4]),
                                        double.Parse(nodesParamsBuf[5]),
                                        double.Parse(nodesParamsBuf[6]),
                                        Int32.Parse(nodesParamsBuf[7]),
                                        Int32.Parse(nodesParamsBuf[8])
                                    ));

                                    if (parsedNodes.Count == 12) break;

                                    lineIterator = Reader.ReadLine();
                                    nodesParamsBuf = new List<string>(lineIterator.Split(','));
                                }

                                ConfigSections.Last().ConfigRows.Add(new PATHType1(
                                    paramsBuf[0],
                                    Int32.Parse(paramsBuf[1]),
                                    paramsBuf[2],
                                    parsedNodes.ToArray()
                                ));
                                break;
                            case 2:
                                while (nodesParamsBuf.Count == 12)
                                {
                                    parsedNodes.Add(new PATHNode(
                                        Int32.Parse(nodesParamsBuf[0]),
                                        Int32.Parse(nodesParamsBuf[1]),
                                        Int32.Parse(nodesParamsBuf[2]),
                                        double.Parse(nodesParamsBuf[3]),
                                        double.Parse(nodesParamsBuf[4]),
                                        double.Parse(nodesParamsBuf[5]),
                                        Int32.Parse(nodesParamsBuf[7]),
                                        Int32.Parse(nodesParamsBuf[8]),
                                        double.Parse(nodesParamsBuf[6])
                                    ));

                                    if (parsedNodes.Count == 12) break;

                                    lineIterator = Reader.ReadLine();
                                    nodesParamsBuf = new List<string>(lineIterator.Split(','));
                                }

                                ConfigSections.Last().ConfigRows.Add(new PATHType2(
                                    paramsBuf[0],
                                    Int32.Parse(paramsBuf[1]),
                                    parsedNodes.ToArray()
                                ));
                                break;
                            default:
                                throw exception;
                        }
                        break;
                    case OBJS.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 5:
                                ConfigSections.Last().ConfigRows.Add(new OBJSType4().Parse(paramsBuf));
                                break;

                            case 6:
                                ConfigSections.Last().ConfigRows.Add(new OBJSType1().Parse(paramsBuf));
                                break;

                            case 7:
                                ConfigSections.Last().ConfigRows.Add(new OBJSType2().Parse(paramsBuf));
                                break;

                            case 8:
                                ConfigSections.Last().ConfigRows.Add(new OBJSType3().Parse(paramsBuf));
                                break;
                            default:
                                throw exception;
                        }
                    break;

                    case TOBJ.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 7:
                                ConfigSections.Last().ConfigRows.Add(new TOBJType4().Parse(paramsBuf));
                                break;

                            case 8:
                                ConfigSections.Last().ConfigRows.Add(new TOBJType1().Parse(paramsBuf));
                                break;

                            case 9:
                                ConfigSections.Last().ConfigRows.Add(new TOBJType2().Parse(paramsBuf));
                                break;

                            case 10:
                                ConfigSections.Last().ConfigRows.Add(new TOBJType3().Parse(paramsBuf));
                                break;
                            default:
                                throw exception;
                        }
                    break;

                    case TwoDFX.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 20:
                                ConfigSections.Last().ConfigRows.Add(new TwoDFXType1().Parse(paramsBuf));
                                break;
                            case 14:
                                // Добавили проверку, так как у первого и второго типа одинаковое количество параметров.
                                // У первого типа параметр 2DFXType всегда 1
                                if (paramsBuf[8] == "1")
                                {
                                    ConfigSections.Last().ConfigRows.Add(new TwoDFXType2().Parse(paramsBuf));
                                }
                                else
                                {
                                    ConfigSections.Last().ConfigRows.Add(new TwoDFXType3().Parse(paramsBuf));
                                }
                                break;
                            case 16:
                                ConfigSections.Last().ConfigRows.Add(new TwoDFXType4().Parse(paramsBuf));
                                break;
                            case 9:
                                ConfigSections.Last().ConfigRows.Add(new TwoDFXType5().Parse(paramsBuf));
                                break;
                            default:
                                throw exception;
                        }
                    break;

                    case HIER.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new HIERType1().Parse(paramsBuf));
                    break;

                    case PEDS.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 7:
                                ConfigSections.Last().ConfigRows.Add(new PEDSType1().Parse(paramsBuf));
                                break;
                            case 10:
                                ConfigSections.Last().ConfigRows.Add(new PEDSType2().Parse(paramsBuf));
                                break;
                            case 14:
                                ConfigSections.Last().ConfigRows.Add(new PEDSType3().Parse(paramsBuf));
                                break;
                            default:
                                throw exception;
                        }
                    break;

                    case WEAP.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new WEAPType1().Parse(paramsBuf));
                        break;

                    case ANIM.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new ANIMType1().Parse(paramsBuf));
                        break;

                    case TXDP.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new TXDPType1().Parse(paramsBuf));
                        break;

                    case HAND.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new HANDType1().Parse(paramsBuf));
                        break;

                    case CARS.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 10:
                                ConfigSections.Last().ConfigRows.Add(new CARSType2().Parse(paramsBuf));
                                break;
                            case 11:
                                if (Int32.TryParse(paramsBuf[7], out int _))
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARSType3().Parse(paramsBuf));
                                }
                                else
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARSType6().Parse(paramsBuf));
                                }

                                // TODO: Сделать проверку на соответствие CARSType9
                                /*
                                ConfigSections.Last().ConfigRows.Add(new CARSType9(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    paramsBuf[3],
                                    paramsBuf[4],
                                    paramsBuf[5],
                                    paramsBuf[6],
                                    paramsBuf[7],
                                    Int32.Parse(paramsBuf[8]),
                                    Int32.Parse(paramsBuf[9]),
                                    Int32.Parse(paramsBuf[10])
                                ));
                                */

                                break;
                            case 12:
                                if (Int32.TryParse(paramsBuf[11], out int _))
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARSType4().Parse(paramsBuf));
                                }
                                else
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARSType1().Parse(paramsBuf));
                                }
                                break;
                            case 13:
                                ConfigSections.Last().ConfigRows.Add(new CARSType5().Parse(paramsBuf));

                                // TODO: Сделать проверку на соответствие CARSType7
                                /*
                                ConfigSections.Last().ConfigRows.Add(new CARSType7(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    paramsBuf[3],
                                    paramsBuf[4],
                                    paramsBuf[5],
                                    paramsBuf[6],
                                    paramsBuf[7],
                                    Int32.Parse(paramsBuf[8]),
                                    Int32.Parse(paramsBuf[9]),
                                    Int32.Parse(paramsBuf[10]),
                                    Int32.Parse(paramsBuf[11]),
                                    double.Parse(paramsBuf[12])
                                ));
                                */
                                break;
                            case 15:
                                ConfigSections.Last().ConfigRows.Add(new CARSType8().Parse(paramsBuf));
                                break;
                            default:
                                throw exception;
                        }
                        break;
                }
            }

            Reader.Close();
        }
    }
}
