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
using System.Globalization;
using System.IO;
using System.Linq;

namespace OpenIII.GameFiles
{
    public class IDEFile : GameFile
    {
        // нужно будет реализовать в виде перечисления для использования в других классах
        public static List<string> SectionNames = new List<string> {
            "objs", "tobj", "hier", "cars", "peds", "path", "2dfx", "weap", "anim", "txdp"
        };

        public static List<string> ExcludedSynbols = new List<string>
        {
            "end", "#", ";"
        };

        public IDEFile(string filePath) : base(filePath) { }

        public List<ConfigSection> ConfigSections = new List<ConfigSection>();

        public void ParseData()
        {
            string lineIterator;
            var culture = CultureInfo.InvariantCulture;
            StreamReader Reader = new StreamReader(this.FullPath);

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

                for (int i = 0; i < paramsBuf.Count; i++)
                {
                    if (paramsBuf[i].IndexOf('\"') >= 0)
                    {
                        paramsBuf[i] = paramsBuf[i].Replace('\"', ' ');
                    }

                    if (paramsBuf[i].IndexOf(' ') >= 0) {
                        paramsBuf[i] = paramsBuf[i].Trim();
                    }
                }

                switch (ConfigSections.Last().Name)
                {
                    case "objs":
                        switch (paramsBuf.Count)
                        {
                            case 5:
                                ConfigSections.Last().ConfigRows.Add(new OBJ(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    double.Parse(paramsBuf[3], culture),
                                    Int32.Parse(paramsBuf[4])
                                ));
                                break;

                            case 6:
                                ConfigSections.Last().ConfigRows.Add(new OBJ(
                                    Int32.Parse(paramsBuf[0]),
                                    paramsBuf[1],
                                    paramsBuf[2],
                                    Int32.Parse(paramsBuf[3]),
                                    double.Parse(paramsBuf[4], culture),
                                    Int32.Parse(paramsBuf[5])
                                ));
                                break;

                            case 7:
                                ConfigSections.Last().ConfigRows.Add(new OBJ(
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
                                ConfigSections.Last().ConfigRows.Add(new OBJ(
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

                    case "tobj":
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
                    case "2dfx":
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
                    case "hier":
                        ConfigSections.Last().ConfigRows.Add(new HIER(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2]
                        ));
                    break;
                    case "peds":
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
                                    0
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
                                    0,
                                    paramsBuf[7],
                                    Int32.Parse(paramsBuf[8]),
                                    Int32.Parse(paramsBuf[9])
                                 ));
                                break;
                            case 14:
                                break;
                        }
                    break;
                    case "weap":
                        ConfigSections.Last().ConfigRows.Add(new WEAP(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2],
                            paramsBuf[3],
                            Int32.Parse(paramsBuf[4]),
                            0
                        ));
                    break;
                    case "anim":
                        ConfigSections.Last().ConfigRows.Add(new ANIM(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2],
                            paramsBuf[3],
                            double.Parse(paramsBuf[4], culture),
                            Int32.Parse(paramsBuf[5])
                        ));
                    break;
                    case "txdp":
                        ConfigSections.Last().ConfigRows.Add(new TXDP(
                            paramsBuf[0],
                            paramsBuf[1]
                        ));
                    break;
                    case "hand":
                        ConfigSections.Last().ConfigRows.Add(new HAND(
                            Int32.Parse(paramsBuf[0]),
                            paramsBuf[1],
                            paramsBuf[2],
                            paramsBuf[3]
                        ));
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

    }

    public class OBJ : ConfigRow
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private double DrawDistance3 { get; set; }

        private int Flags { get; set; }


        // non-breakable objects and behaves equivalently to Type 1
        public OBJ(int id, string modelName, string txdName, double drawDistance1, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DrawDistance1 = drawDistance1;
            this.Flags = flags;
        }

        // non-breakable objects
        public OBJ(int id, string modelName, string txdName, int meshCount, double drawDistance1, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.Flags = flags;
        }

        // breakable objects
        public OBJ(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.Flags = flags;
        }

        // complex breakable objects
        public OBJ(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, double drawDistance3, int flags)
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
    }

    public class TOBJ : ConfigRow
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private float DrawDistance1 { get; set; }

        private float DrawDistance2 { get; set; }

        private float DrawDistance3 { get; set; }

        private int Flags { get; set; }

        private int TimeOn { get; set; }

        private int TimeOff { get; set; }


        // non-breakable objects and behaves equivalently to Type 1
        public TOBJ(int id, string modelName, string txdName, float drawDistance1, int flags, int timeOn, int timeOff)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DrawDistance1 = drawDistance1;
            this.Flags = flags;
            this.TimeOn = timeOn;
            this.TimeOff = timeOff;
        }

        // non-breakable objects
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

        // breakable objects
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

        // complex breakable objects
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
    }


    public class TwoDFX : ConfigRow {
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

        // type 1

        private int Unknown1 { get; set; }

        private int Particle { get; set; }

        private double StrengthX { get; set; }

        private double StrengthY { get; set; }

        private double StrengthZ { get; set; }

        private double Scale { get; set; }

        // type 2

        private int Unknown2 { get; set; }

        private int Unknown3 { get; set; }

        private double Unk1 { get; set; }

        private double Unk2 { get; set; }

        private double Unk3 { get; set; }

        private int Unknown4 { get; set; }

        // type 3

        private int Behavior { get; set; }

        private double RollX { get; set; }

        private double RollY { get; set; }

        private double RollZ { get; set; }


        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, string corona, string shadow, double distance, double outerRange, double size, double innerRange, int shadowIntensity, int flash, int wet, int flare, int flags)
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

        // type 1 Particles
        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, int particle, double strengthX, double strengthY, double strengthZ, double scale) {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown1 = unknown1;
            this.TwoDFXType = twoDFXType;
            this.Particle = particle;
            this.StrengthX = strengthX;
            this.StrengthY = strengthY;
            this.StrengthZ = strengthZ;
            this.Scale = scale;
        }

        // type 2

        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown2, int twoDFXType, int unknown3, double unk1, double unk2, double unk3, int unknown4)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown2 = unknown2;
            this.TwoDFXType = twoDFXType;
            this.Unknown3 = unknown3;
            this.Unk1 = unk1;
            this.Unk2 = unk2;
            this.Unk3 = unk3;
            this.Unknown4 = unknown4;
        }

        // type 3

        public TwoDFX(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType, int behavior, double unk1, double unk2, double unk3, double rollX, double rollY, double rollZ) {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown = unknown;
            this.TwoDFXType = twoDFXType;
            this.Behavior = behavior;
            this.Unk1 = unk1;
            this.Unk2 = unk2;
            this.Unk3 = unk3;
            this.RollX = rollX;
            this.RollY = rollY;
            this.RollZ = rollZ;
        }

        // type 4

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

    public class HIER : ConfigRow {
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
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string DefaultPedtype { get; set; }

        private string Behavior { get; set; }

        private string AnimGroup { get; set; }

        private int CarsCanDrive { get; set; }

        private string AnimFile { get; set; }

        private int RadioStation1 { get; set; }

        private int RadioStation2 { get; set; }

        private int Flags { get; set; }

        private string VoiceArchive { get; set; }

        private string Voice1 { get; set; }

        private string Voice2 { get; set; }


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

        public PEDS(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, int CarsCanDrive, int flags, string animFile, int radioStation1, int radioStation2, string voiceArchive, string voice1, string voice2)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = AnimGroup;
            this.CarsCanDrive = CarsCanDrive;
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
}
