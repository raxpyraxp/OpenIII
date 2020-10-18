﻿/*
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenIII.GameFiles
{
    public class IDEFile : GameFile
    {
        public static List<string> SectionNames = new List<string> {
            "objs", "tobj", "hier", "cars", "peds", "path", "2dfx", "weap", "anim", "txdp"
        };

        public static List<string> ExcludedSynbols = new List<string>
        {
            "end", "#", ";"
        };

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

        public static List<string> CleanParams(List<string> listParams)
        {
            List<string> result = new List<string>();

            foreach (string param in listParams)
            {
                result.Add(param.Replace("\"", "").Trim());
            }

            return result;
        }

        public IDEFile(string filePath) : base(filePath) { }

        public List<ConfigSection> ConfigSections = new List<ConfigSection>();

        public void ParseData()
        {
            string lineIterator;
            CultureInfo culture = CultureInfo.InvariantCulture;
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
                                        double.Parse(nodesParamsBuf[3], culture),
                                        double.Parse(nodesParamsBuf[4], culture),
                                        double.Parse(nodesParamsBuf[5], culture),
                                        double.Parse(nodesParamsBuf[6], culture),
                                        Int32.Parse(nodesParamsBuf[7]),
                                        Int32.Parse(nodesParamsBuf[8])
                                    ));

                                    if (parsedNodes.Count == 12) break;

                                    lineIterator = Reader.ReadLine();
                                    nodesParamsBuf = new List<string>(lineIterator.Split(','));
                                }

                                ConfigSections.Last().ConfigRows.Add(new PATH(
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
                                        double.Parse(nodesParamsBuf[3], culture),
                                        double.Parse(nodesParamsBuf[4], culture),
                                        double.Parse(nodesParamsBuf[5], culture),
                                        Int32.Parse(nodesParamsBuf[7]),
                                        Int32.Parse(nodesParamsBuf[8]),
                                        double.Parse(nodesParamsBuf[6], culture)
                                    ));

                                    if (parsedNodes.Count == 12) break;

                                    lineIterator = Reader.ReadLine();
                                    nodesParamsBuf = new List<string>(lineIterator.Split(','));
                                }

                                ConfigSections.Last().ConfigRows.Add(new PATH(
                                    paramsBuf[0],
                                    Int32.Parse(paramsBuf[1]),
                                    parsedNodes.ToArray()
                                ));
                                break;
                        }
                        break;
                    case OBJS.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 5:
                                ConfigSections.Last().ConfigRows.Add(new OBJS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    double.Parse(paramsBuf[3], culture),
                                    Int32.Parse(paramsBuf[4])
                                ));
                                break;

                            case 6:
                                ConfigSections.Last().ConfigRows.Add(new OBJS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    Int32.Parse(paramsBuf[3]),
                                    double.Parse(paramsBuf[4], culture),
                                    Int32.Parse(paramsBuf[5])
                                ));
                                break;

                            case 7:
                                ConfigSections.Last().ConfigRows.Add(new OBJS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    Int32.Parse(paramsBuf[3]),
                                    double.Parse(paramsBuf[4], culture),
                                    double.Parse(paramsBuf[5], culture),
                                    Int32.Parse(paramsBuf[6])
                                ));
                                break;

                            case 8:
                                ConfigSections.Last().ConfigRows.Add(new OBJS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    Int32.Parse(paramsBuf[3]),
                                    double.Parse(paramsBuf[4], culture),
                                    double.Parse(paramsBuf[5], culture),
                                    double.Parse(paramsBuf[6], culture),
                                    Int32.Parse(paramsBuf[7])
                                ));
                                break;
                        }
                    break;

                    case TOBJ.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 7:
                                ConfigSections.Last().ConfigRows.Add(new TOBJ(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    float.Parse(paramsBuf[3]),
                                    Int32.Parse(paramsBuf[4]),
                                    Int32.Parse(paramsBuf[5]),
                                    Int32.Parse(paramsBuf[6])
                                ));
                                break;

                            case 8:
                                ConfigSections.Last().ConfigRows.Add(new TOBJ(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    Int32.Parse(paramsBuf[3]),
                                    float.Parse(paramsBuf[4]),
                                    Int32.Parse(paramsBuf[5]),
                                    Int32.Parse(paramsBuf[6]),
                                    Int32.Parse(paramsBuf[7])
                                ));
                                break;

                            case 9:
                                ConfigSections.Last().ConfigRows.Add(new TOBJ(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    Int32.Parse(paramsBuf[3]),
                                    float.Parse(paramsBuf[4]),
                                    float.Parse(paramsBuf[5]),
                                    Int32.Parse(paramsBuf[6]),
                                    Int32.Parse(paramsBuf[7]),
                                    Int32.Parse(paramsBuf[8])
                                ));
                                break;

                            case 10:
                                ConfigSections.Last().ConfigRows.Add(new TOBJ(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    Int32.Parse(paramsBuf[3]),
                                    float.Parse(paramsBuf[4]),
                                    float.Parse(paramsBuf[5]),
                                    float.Parse(paramsBuf[6]),
                                    Int32.Parse(paramsBuf[7]),
                                    Int32.Parse(paramsBuf[8]),
                                    Int32.Parse(paramsBuf[9])
                                ));
                                break;
                        }
                    break;

                    case TwoDFX.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 20:
                                ConfigSections.Last().ConfigRows.Add(new TwoDFX(
                                    Int32.Parse(paramsBuf[0]),
                                    double.Parse(paramsBuf[1], culture),
                                    double.Parse(paramsBuf[2], culture),
                                    double.Parse(paramsBuf[3], culture),
                                    Int32.Parse(paramsBuf[4]),
                                    Int32.Parse(paramsBuf[5]),
                                    Int32.Parse(paramsBuf[6]),
                                    Int32.Parse(paramsBuf[7]),
                                    Int32.Parse(paramsBuf[8]),
                                    paramsBuf[9],
                                    paramsBuf[10],
                                    double.Parse(paramsBuf[11], culture),
                                    double.Parse(paramsBuf[12], culture),
                                    double.Parse(paramsBuf[13], culture),
                                    double.Parse(paramsBuf[14], culture),
                                    Int32.Parse(paramsBuf[15]),
                                    Int32.Parse(paramsBuf[16]),
                                    Int32.Parse(paramsBuf[17]),
                                    Int32.Parse(paramsBuf[18]),
                                    Int32.Parse(paramsBuf[19])
                                ));
                                break;
                            case 14:
                                // Добавили проверку, так как у первого и второго типа одинаковое количество параметров.
                                // У первого типа параметр 2DFXType всегда 1
                                if (paramsBuf[8] == "1")
                                {
                                    ConfigSections.Last().ConfigRows.Add(new TwoDFX(
                                        Int32.Parse(paramsBuf[0]),
                                        double.Parse(paramsBuf[1], culture),
                                        double.Parse(paramsBuf[2], culture),
                                        double.Parse(paramsBuf[3], culture),
                                        Int32.Parse(paramsBuf[4]),
                                        Int32.Parse(paramsBuf[5]),
                                        Int32.Parse(paramsBuf[6]),
                                        Int32.Parse(paramsBuf[7]),
                                        Int32.Parse(paramsBuf[8]),
                                        Int32.Parse(paramsBuf[9]),
                                        double.Parse(paramsBuf[10], culture),
                                        double.Parse(paramsBuf[11], culture),
                                        double.Parse(paramsBuf[12], culture),
                                        double.Parse(paramsBuf[13], culture)
                                    ));
                                }
                                else
                                {
                                    ConfigSections.Last().ConfigRows.Add(new TwoDFX(
                                        Int32.Parse(paramsBuf[0]),
                                        double.Parse(paramsBuf[1], culture),
                                        double.Parse(paramsBuf[2], culture),
                                        double.Parse(paramsBuf[3], culture),
                                        Int32.Parse(paramsBuf[4]),
                                        Int32.Parse(paramsBuf[5]),
                                        Int32.Parse(paramsBuf[6]),
                                        Int32.Parse(paramsBuf[7]),
                                        Int32.Parse(paramsBuf[8]),
                                        Int32.Parse(paramsBuf[9]),
                                        double.Parse(paramsBuf[10], culture),
                                        double.Parse(paramsBuf[11], culture),
                                        double.Parse(paramsBuf[12], culture),
                                        Int32.Parse(paramsBuf[13])
                                    ));
                                }
                                break;
                            case 16:
                                ConfigSections.Last().ConfigRows.Add(new TwoDFX(
                                    Int32.Parse(paramsBuf[0]),
                                    double.Parse(paramsBuf[1], culture),
                                    double.Parse(paramsBuf[2], culture),
                                    double.Parse(paramsBuf[3], culture),
                                    Int32.Parse(paramsBuf[4]),
                                    Int32.Parse(paramsBuf[5]),
                                    Int32.Parse(paramsBuf[6]),
                                    Int32.Parse(paramsBuf[7]),
                                    Int32.Parse(paramsBuf[8]),
                                    Int32.Parse(paramsBuf[9]),
                                    double.Parse(paramsBuf[10], culture),
                                    double.Parse(paramsBuf[11], culture),
                                    double.Parse(paramsBuf[12], culture),
                                    double.Parse(paramsBuf[13], culture),
                                    double.Parse(paramsBuf[14], culture),
                                    double.Parse(paramsBuf[15], culture)
                                ));
                                break;
                            case 9:
                                ConfigSections.Last().ConfigRows.Add(new TwoDFX(
                                    Int32.Parse(paramsBuf[0]),
                                    double.Parse(paramsBuf[1], culture),
                                    double.Parse(paramsBuf[2], culture),
                                    double.Parse(paramsBuf[3], culture),
                                    Int32.Parse(paramsBuf[4]),
                                    Int32.Parse(paramsBuf[5]),
                                    Int32.Parse(paramsBuf[6]),
                                    Int32.Parse(paramsBuf[7]),
                                    Int32.Parse(paramsBuf[8])
                                ));
                                break;
                        }
                    break;

                    case HIER.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new HIER(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2]
                        ));
                    break;

                    case PEDS.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 7:
                                ConfigSections.Last().ConfigRows.Add(new PEDS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    paramsBuf[3],
                                    paramsBuf[4],
                                    paramsBuf[5],
                                    Convert.ToInt32(paramsBuf[6], 16)
                                ));
                                break;
                            case 10:
                                ConfigSections.Last().ConfigRows.Add(new PEDS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    paramsBuf[3],
                                    paramsBuf[4],
                                    paramsBuf[5],
                                    Convert.ToInt32(paramsBuf[6], 16),
                                    paramsBuf[7],
                                    Int32.Parse(paramsBuf[8]),
                                    Int32.Parse(paramsBuf[9])
                                 ));
                                break;
                            case 14:
                                ConfigSections.Last().ConfigRows.Add(new PEDS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    paramsBuf[3],
                                    paramsBuf[4],
                                    paramsBuf[5],
                                    Convert.ToInt32(paramsBuf[6], 16),
                                    Int32.Parse(paramsBuf[7]),
                                    paramsBuf[8],
                                    Int32.Parse(paramsBuf[9]),
                                    Int32.Parse(paramsBuf[10]),
                                    paramsBuf[11],
                                    paramsBuf[12],
                                    paramsBuf[13]
                                 ));
                                break;
                            default:
                                throw exception;
                        }
                    break;

                    case WEAP.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new WEAP(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2],
                            paramsBuf[3],
                            Int32.Parse(paramsBuf[4]),
                            double.Parse(paramsBuf[5], culture)
                        ));
                        break;

                    case ANIM.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new ANIM(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2],
                            paramsBuf[3],
                            double.Parse(paramsBuf[4], culture),
                            Int32.Parse(paramsBuf[5])
                        ));
                        break;

                    case TXDP.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new TXDP(
                            paramsBuf[0],
                            paramsBuf[1]
                        ));
                        break;

                    case HAND.SectionName:
                        ConfigSections.Last().ConfigRows.Add(new HAND(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2],
                            paramsBuf[3]
                        ));
                        break;

                    case CARS.SectionName:
                        switch (paramsBuf.Count)
                        {
                            case 13:
                                if (paramsBuf[3] == VehicleClassVC.Bike)
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARS(
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
                                        Convert.ToInt32(paramsBuf[10], 16),
                                        Int32.Parse(paramsBuf[11]),
                                        double.Parse(paramsBuf[12], culture)
                                    ));
                                }
                                else
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARS(
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
                                        Convert.ToInt32(paramsBuf[10], 16),
                                        double.Parse(paramsBuf[12], culture),
                                        Int32.Parse(paramsBuf[11])
                                    ));
                                }
                                break;
                            case 12:
                                if (int.TryParse(paramsBuf[11], out _))
                                {
                                    // VC
                                    ConfigSections.Last().ConfigRows.Add(new CARS(
                                        Int32.Parse(paramsBuf[0]),
                                        paramsBuf[1],
                                        paramsBuf[2],
                                        paramsBuf[3],
                                        paramsBuf[4],
                                        paramsBuf[5],
                                        paramsBuf[6],
                                        Int32.Parse(paramsBuf[8]),
                                        Int32.Parse(paramsBuf[9]),
                                        Convert.ToInt32(paramsBuf[10], 16),
                                        Int32.Parse(paramsBuf[11])
                                    ));
                                }
                                else
                                {
                                    // III
                                    ConfigSections.Last().ConfigRows.Add(new CARS(
                                        Int32.Parse(paramsBuf[0]),
                                        paramsBuf[1],
                                        paramsBuf[2],
                                        paramsBuf[3],
                                        paramsBuf[4],
                                        paramsBuf[5],
                                        paramsBuf[6],
                                        Int32.Parse(paramsBuf[7]),
                                        Int32.Parse(paramsBuf[8]),
                                        Convert.ToInt32(paramsBuf[9], 16),
                                        Int32.Parse(paramsBuf[10]),
                                        double.Parse(paramsBuf[11], culture)
                                    ));
                                }
                                break;
                            case 11:
                                if (Int32.TryParse(paramsBuf[10], out _))
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARS(
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
                                }
                                else
                                {
                                    ConfigSections.Last().ConfigRows.Add(new CARS(
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
                                        Convert.ToInt32(paramsBuf[10], 16)
                                    ));
                                }
                                break;
                            case 10:
                                ConfigSections.Last().ConfigRows.Add(new CARS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    paramsBuf[3],
                                    paramsBuf[4],
                                    paramsBuf[5],
                                    paramsBuf[6],
                                    Int32.Parse(paramsBuf[7]),
                                    Int32.Parse(paramsBuf[8]),
                                    Int32.Parse(paramsBuf[9])
                                ));
                                break;
                            case 15:
                                ConfigSections.Last().ConfigRows.Add(new CARS(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    paramsBuf[3],
                                    paramsBuf[4],
                                    paramsBuf[5],
                                    paramsBuf[6],
                                    paramsBuf[7],
                                    Int32.Parse(paramsBuf[8]),
                                    Convert.ToByte(paramsBuf[9], 16),
                                    Convert.ToInt32(paramsBuf[10], 16),
                                    Int32.Parse(paramsBuf[11]),
                                    double.Parse(paramsBuf[12], culture),
                                    double.Parse(paramsBuf[13], culture),
                                    Int32.Parse(paramsBuf[14])
                                ));
                                break;
                        }
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
        protected List<string> NonRequiredParams = new List<string>();

        public override string ToString()
        {
            var result = this.GetType()
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             // If parameter is not required and null, don't include it in the string
                             .Select(field => (!NonRequiredParams.Contains(field.Name) || field.GetValue(this) != null) ? field.GetValue(this) : null)
                             .ToArray();
            return string.Join(", ", result);
        }
    }

    public class OBJS : ConfigRow
    {
        public const string SectionName = "objs";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private double DrawDistance3 { get; set; }

        private double DrawDistance { get; set; }

        private int Flags { get; set; }

        private new List<string> NonRequiredParams = new List<string>()
        { 
            "DrawDistance",
            "DrawDistance1",
            "DrawDistance2",
            "DrawDistance3"
        };

        public OBJS(int id, string modelName, string txdName, int meshCount, double drawDistance1, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.Flags = flags;
        }

        public OBJS(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.Flags = flags;
        }

        public OBJS(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, double drawDistance3, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.DrawDistance3 = drawDistance3;
            this.Flags = flags;
        }

        public OBJS(int id, string modelName, string txdName, double drawDistance, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DrawDistance = drawDistance;
            this.Flags = flags;
        }
    }

    public class TOBJ : ConfigRow
    {
        public const string SectionName = "tobj";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private float DrawDistance { get; set; }

        private float DrawDistance1 { get; set; }

        private float DrawDistance2 { get; set; }

        private float DrawDistance3 { get; set; }

        private int Flags { get; set; }

        private int TimeOn { get; set; }

        private int TimeOff { get; set; }

        private new List<string> NonRequiredParams = new List<string>()
        {
            "DrawDistance",
            "DrawDistance1",
            "DrawDistance2",
            "DrawDistance3"
        };

        public TOBJ(int id, string modelName, string txdName, int meshCount, float drawDistance1, int flags, int timeOn, int timeOff)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.Flags = flags;
            this.TimeOn = timeOn;
            this.TimeOff = timeOff;
        }

        public TOBJ(int id, string modelName, string txdName, int meshCount, float drawDistance1, float drawDistance2, int flags, int timeOn, int timeOff)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.Flags = flags;
            this.TimeOn = timeOn;
            this.TimeOff = timeOff;
        }

        public TOBJ(int id, string modelName, string txdName, int meshCount, float drawDistance1, float drawDistance2, float drawDistance3, int flags, int timeOn, int timeOff)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.DrawDistance3 = drawDistance3;
            this.Flags = flags;
            this.TimeOn = timeOn;
            this.TimeOff = timeOff;
        }

        public TOBJ(int id, string modelName, string txdName, float drawDistance, int flags, int timeOn, int timeOff)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DrawDistance = drawDistance;
            this.Flags = flags;
            this.TimeOn = timeOn;
            this.TimeOff = timeOff;
        }
    }

    public class TwoDFX : ConfigRow
    {
        public const string SectionName = "2dfx";

        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown { get; set; }

        private int TwoDFXType { get; set; }

        private string Corona { get; set; }

        private string Shadow { get; set; }

        private double Distance { get; set; }

        private double OuterRange { get; set; }

        private double Size { get; set; }

        private double InnerRange { get; set; }

        private int ShadowIntensity { get; set; }

        private int Flash { get; set; }

        private int Wet { get; set; }

        private int Flare { get; set; }

        private int Flags { get; set; }

        private int Particle { get; set; }

        private double StrengthX { get; set; }

        private double StrengthY { get; set; }

        private double StrengthZ { get; set; }

        private double Scale { get; set; }

        private int Unknown1 { get; set; }

        private int Unknown2 { get; set; }

        private int Behavior { get; set; }

        private double Unk1 { get; set; }

        private double Unk2 { get; set; }

        private double Unk3 { get; set; }

        private int Unknown3 { get; set; }

        private double RollX { get; set; }

        private double RollY { get; set; }

        private double RollZ { get; set; }

        private new List<string> NonRequiredParams = new List<string>()
        {
            "Corona",
            "Shadow",
            "Distance",
            "OuterRange",
            "Size",
            "InnerRange",
            "ShadowIntensity",
            "Flash",
            "Wet",
            "Flare",
            "Flags",
            "Particle",
            "StrengthX",
            "StrengthY",
            "StrengthZ",
            "Scale",
            "Unknown",
            "Unk1",
            "Unk2",
            "Unk3",
            "Unknown1",
            "Unknown2",
            "Behavior",
            "RotX",
            "RotY",
            "RotZ"
        };

        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType, string corona, string shadow, double distance, double outerRange, double size, double innerRange, int shadowIntensity, int flash, int wet, int flare, int flags)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown = unknown;
            this.TwoDFXType = twoDFXType;
            this.Corona = corona;
            this.Shadow = shadow;
            this.Distance = distance;
            this.OuterRange = outerRange;
            this.Size = size;
            this.InnerRange = innerRange;
            this.ShadowIntensity = shadowIntensity;
            this.Flash = flash;
            this.Wet = wet;
            this.Flare = flare;
            this.Flags = flags;
        }

        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType, int particle, double strengthX, double strengthY, double strengthZ, double scale)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown = unknown;
            this.TwoDFXType = twoDFXType;
            this.Particle = particle;
            this.StrengthX = strengthX;
            this.StrengthY = strengthY;
            this.StrengthZ = strengthZ;
            this.Scale = scale;
        }

        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, int unknown2, double unk1, double unk2, double unk3, int unknown3)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown1 = unknown1;
            this.TwoDFXType = twoDFXType;
            this.Unknown2 = unknown2;
            this.Unk1 = unk1;
            this.Unk2 = unk2;
            this.Unk3 = unk3;
            this.Unknown3 = unknown3;
        }

        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, int behavior, double unk1, double unk2, double unk3, double rollX, double rollY, double rollZ)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown1 = unknown1;
            this.TwoDFXType = twoDFXType;
            this.Behavior = behavior;
            this.Unk1 = unk1;
            this.Unk2 = unk2;
            this.Unk3 = unk3;
            this.RollX = rollX;
            this.RollY = rollY;
            this.RollZ = rollZ;
        }

        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown = unknown;
            this.TwoDFXType = twoDFXType;
        }
    }

    public class HIER : ConfigRow
    {
        public const string SectionName = "hier";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        public HIER(int id, string modelName, string txdName)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
        }
    }

    public class PEDS : ConfigRow
    {
        public const string SectionName = "peds";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string DefaultPedtype { get; set; }

        private string Behavior { get; set; }

        private string AnimGroup { get; set; }

        private int CarsCanDrive { get; set; }

        private int Flags { get; set; }

        private string AnimFile { get; set; }

        private int RadioStation1 { get; set; }

        private int RadioStation2 { get; set; }

        private string VoiceArchive { get; set; }

        private string Voice1 { get; set; }

        private string Voice2 { get; set; }

        private new List<string> NonRequiredParams = new List<string>()
        {
            "AnimFile",
            "RadioStation1",
            "RadioStation2",
            "Flags",
            "VoiceArchive",
            "Voice1",
            "Voice2"
        };

        public PEDS(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, int CarsCanDrive)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = AnimGroup;
            this.CarsCanDrive = CarsCanDrive;
        }

        public PEDS(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, int CarsCanDrive, string animFile, int radioStation1, int radioStation2)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = AnimGroup;
            this.CarsCanDrive = CarsCanDrive;
            this.AnimFile = animFile;
            this.RadioStation1 = radioStation1;
            this.RadioStation2 = radioStation2;
        }

        public PEDS(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, int carsCanDrive, int flags, string animFile, int radioStation1, int radioStation2, string voiceArchive, string voice1, string voice2)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = AnimGroup;
            this.CarsCanDrive = carsCanDrive;
            this.Flags = flags;
            this.AnimFile = animFile;
            this.RadioStation1 = radioStation1;
            this.RadioStation2 = radioStation2;
            this.VoiceArchive = voiceArchive;
            this.Voice1 = voice1;
            this.Voice2 = voice2;
        }
    }

    public class WEAP : ConfigRow
    {
        public const string SectionName = "weap";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string AnimationName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance { get; set; }

        public WEAP(int id, string modelName, string txdName, string animationName, int meshCount, double drawDistance)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.AnimationName = animationName;
            this.MeshCount = meshCount;
            this.DrawDistance = drawDistance;
        }
    }

    public class ANIM : ConfigRow
    {
        public const string SectionName = "anim";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string AnimationName { get; set; }

        private double DrawDistance { get; set; }

        private int Flags { get; set; }

        public ANIM(int id, string modelName, string txdName, string animationName, double drawDistance, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.AnimationName = animationName;
            this.DrawDistance = drawDistance;
            this.Flags = flags;
        }
    }

    public class TXDP : ConfigRow
    {
        public const string SectionName = "txdp";

        private string TxdName;

        private string TxdParentName;

        public TXDP(string txdName, string txdParentName)
        {
            this.TxdName = txdName;
            this.TxdParentName = txdParentName;
        }
    }

    public class HAND : ConfigRow
    {
        public const string SectionName = "hand";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Unknown { get; set; }

        public HAND(int id, string modelName, string txdName, string Unknown)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Unknown = Unknown;
        }
    }

    public class CARS : ConfigRow
    {
        public const string SectionName = "cars";

        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private int Comprules { get; set; }

        private int WheelModelId { get; set; }

        private double WheelScale { get; set; }

        private int LODModelId { get; set; }

        private string Anims { get; set; }

        private int SteeringAngle { get; set; }

        private byte Flags { get; set; }

        private double WheelScaleFront { get; set; }

        private double WheelScaleRear { get; set; }

        private double WheelUpgradeClass { get; set; }

        private int Unknown { get; set; }


        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, int comprules, int wheelModelId, double wheelScale)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.WheelModelId = wheelModelId;
            this.WheelScale = wheelScale;
        }

        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, int comprules)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
        }

        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, int comprules, int lodModelId)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.LODModelId = lodModelId;
        }



        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules, int wheelModelId, double wheelScale)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.WheelModelId = wheelModelId;
            this.WheelScale = wheelScale;
        }

        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
        }

        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules, int lodModelId)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.LODModelId = lodModelId;
        }

        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules, double wheelScale, int steeringAngle)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.SteeringAngle = steeringAngle;
            this.WheelScale = wheelScale;
        }



        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, byte flags, int comprules, int wheelModelId, double wheelScaleFront, double wheelScaleRear, int unknown)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Flags = flags;
            this.Comprules = comprules;
            this.WheelModelId = wheelModelId;
            this.WheelScaleFront = wheelScaleFront;
            this.WheelScaleRear = wheelScaleRear;
            this.Unknown = unknown;
        }

        public CARS(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, byte flags, int comprules)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Flags = flags;
            this.Comprules = comprules;
        }
    }

    public class PATH : ConfigRow
    {
        public const string SectionName = "path";

        private List<PATHNode> Nodes = new List<PATHNode>();

        private string GroupType { get; set; }

        private int Id { get; set; }

        private string ModelName { get; set; }

        private int Delimiter { get; set; }


        public PATH(string groupType, int id, string modelName, PATHNode[] nodes)
        {
            this.GroupType = groupType;
            this.Id = id;
            this.ModelName = modelName;
            this.Nodes = nodes.OfType<PATHNode>().ToList();
        }

        public PATH(string groupType, int delimiter, PATHNode[] nodes)
        {
            this.GroupType = groupType;
            this.Delimiter = delimiter;
            this.Nodes = nodes.OfType<PATHNode>().ToList();
        }
    }

    public class PATHNode
    {
        private int NodeType { get; set; }

        private int NextNode { get; set; }

        private int IsCrossRoad { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private double Unknown { get; set; }

        private int LeftLanes { get; set; }

        private int RightLanes { get; set; }


        private double XRel { get; set; }

        private double YRel { get; set; }

        private double ZRel { get; set; }

        private double XAbs { get; set; }

        private double YAbs { get; set; }

        private double ZAbs { get; set; }

        private double Median { get; set; }

        private int SpeedLimit { get; set; }

        private int Flags { get; set; }

        private double SpawnRate { get; set; }


        public PATHNode(int nodeType, int nextNode, int isCrossRoad, double x, double y, double z, double unknown, int leftLanes, int rightLanes)
        {
            this.NodeType = nodeType;
            this.NextNode = nextNode;
            this.IsCrossRoad = isCrossRoad;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Unknown = unknown;
            this.LeftLanes = leftLanes;
            this.RightLanes = rightLanes;
        }

        public PATHNode(int nodeType, int nextNode, int isCrossRoad, double xRel, double yRel, double zRel, int leftLanes, int rightLanes, double median)
        {
            this.NodeType = nodeType;
            this.NextNode = nextNode;
            this.IsCrossRoad = isCrossRoad;
            this.XRel = xRel;
            this.YRel = yRel;
            this.ZRel = zRel;
            this.LeftLanes = leftLanes;
            this.RightLanes = rightLanes;
            this.Median = median;
        }
    }
}
