
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenIII.GameFiles
{
    public class OBJS : ConfigRow
    {
        public const string SectionName = "objs";
    }

    public class OBJSType1 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private int Flags { get; set; }


        public OBJSType1(int id, string modelName, string txdName, int meshCount, double drawDistance1, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.Flags = flags;
        }
    }

    public class OBJSType2 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private int Flags { get; set; }


        public OBJSType2(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.Flags = flags;
        }
    }

    public class OBJSType3 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private double DrawDistance3 { get; set; }

        private int Flags { get; set; }


        public OBJSType3(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, double drawDistance3, int flags)
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

    public class OBJSType4 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private double DrawDistance { get; set; }

        private int Flags { get; set; }


        public OBJSType4(int id, string modelName, string txdName, double drawDistance, int flags)
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
    }

    public class TOBJType1 : TOBJ
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private int Flags { get; set; }

        private int TimeOn { get; set; }

        private int TimeOff { get; set; }


        public TOBJType1(int id, string modelName, string txdName, int meshCount, double drawDistance1, int flags, int timeOn, int timeOff)
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
    }

    public class TOBJType2 : TOBJ
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private int Flags { get; set; }

        private int TimeOn { get; set; }

        private int TimeOff { get; set; }


        public TOBJType2(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, int flags, int timeOn, int timeOff)
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
    }

    public class TOBJType3 : TOBJ
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private double DrawDistance3 { get; set; }

        private int Flags { get; set; }

        private int TimeOn { get; set; }

        private int TimeOff { get; set; }


        public TOBJType3(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, double drawDistance3, int flags, int timeOn, int timeOff)
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

    public class TOBJType4 : TOBJ
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private double DrawDistance { get; set; }

        private int Flags { get; set; }

        private int TimeOn { get; set; }

        private int TimeOff { get; set; }


        public TOBJType4(int id, string modelName, string txdName, double drawDistance, int flags, int timeOn, int timeOff)
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
    }

    /// <summary>
    /// Lights.
    /// </summary>
    public class TwoDFXType1 : TwoDFX
    {
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

        public TwoDFXType1(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType, string corona, string shadow, double distance, double outerRange, double size, double innerRange, int shadowIntensity, int flash, int wet, int flare, int flags)
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
    }

    /// <summary>
    /// Particles.
    /// </summary>
    public class TwoDFXType2 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown { get; set; }

        private int TwoDFXType { get; set; }

        private int Particle { get; set; }

        private double StrengthX { get; set; }

        private double StrengthY { get; set; }

        private double StrengthZ { get; set; }

        private double Scale { get; set; }

        public TwoDFXType2(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType, int particle, double strengthX, double strengthY, double strengthZ, double scale)
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
    }

    /// <summary>
    /// Peds.
    /// </summary>
    public class TwoDFXType3 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown1 { get; set; }

        private int TwoDFXType { get; set; }

        private int Unknown2 { get; set; }

        private double Unk1 { get; set; }

        private double Unk2 { get; set; }

        private double Unk3 { get; set; }

        private int Unknown3 { get; set; }

        public TwoDFXType3(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, int unknown2, double unk1, double unk2, double unk3, int unknown3)
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
    }

    /// <summary>
    /// Sun Reflections.
    /// </summary>
    public class TwoDFXType4 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown1 { get; set; }

        private int TwoDFXType { get; set; }

        private int Behavior { get; set; }

        private double Unk1 { get; set; }

        private double Unk2 { get; set; }

        private double Unk3 { get; set; }

        private double RollX { get; set; }

        private double RollY { get; set; }

        private double RollZ { get; set; }

        public TwoDFXType4(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, int behavior, double unk1, double unk2, double unk3, double rollX, double rollY, double rollZ)
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
    }

    public class TwoDFXType5 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown { get; set; }

        private int TwoDFXType { get; set; }

        public TwoDFXType5(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType)
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
    }

    public class HIERType1 : HIER
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        public HIERType1(int id, string modelName, string txdName)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
        }
    }


    public class PEDS : ConfigRow
    {
        public const string SectionName = "peds";
    }

    public class PEDSType1 : PEDS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string DefaultPedtype { get; set; }

        private string Behavior { get; set; }

        private string AnimGroup { get; set; }

        private int CarsCanDrive { get; set; }

        public PEDSType1(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, int CarsCanDrive)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = AnimGroup;
            this.CarsCanDrive = CarsCanDrive;
        }
    }

    public class PEDSType2 : PEDS
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

        public PEDSType2(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, int CarsCanDrive, string animFile, int radioStation1, int radioStation2)
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
    }

    public class PEDSType3 : PEDS
    {
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

        public PEDSType3(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, int carsCanDrive, int flags, string animFile, int radioStation1, int radioStation2, string voiceArchive, string voice1, string voice2)
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
    }

    public class WEAPType1 : WEAP
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string AnimationName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance { get; set; }


        public WEAPType1(int id, string modelName, string txdName, string animationName, int meshCount, double drawDistance)
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
    }

    public class ANIMType1 : ANIM
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string AnimationName { get; set; }

        private double DrawDistance { get; set; }

        private int Flags { get; set; }

        public ANIMType1(int id, string modelName, string txdName, string animationName, double drawDistance, int flags)
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
    }

    public class TXDPType1 : TXDP
    {
        private string TxdName;

        private string TxdParentName;

        public TXDPType1(string txdName, string txdParentName)
        {
            this.TxdName = txdName;
            this.TxdParentName = txdParentName;
        }
    }


    public class HAND : ConfigRow
    {
        public const string SectionName = "hand";
    }

    public class HANDType1 : HAND
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Unknown { get; set; }

        public HANDType1(int id, string modelName, string txdName, string Unknown)
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
    }

    /// <summary>
    /// III cars (12 props)
    /// </summary>
    public class CARSType1 : CARS
    {
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

        public CARSType1(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, int comprules, int wheelModelId, double wheelScale)
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
    }

    /// <summary>
    /// III boat, train and heli (10 props)
    /// </summary>
    public class CARSType2 : CARS
    {
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

        public CARSType2(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, int comprules)
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
    }

    /// <summary>
    /// III plane (11 props)
    /// </summary>
    public class CARSType3 : CARS
    {
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

        private int LODModelId { get; set; }

        public CARSType3(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, int comprules, int lodModelId)
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
    }

    /// <summary>
    /// VC plane (12 props)
    /// </summary>
    public class CARSType4 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private int Comprules { get; set; }

        private int LODModelId { get; set; }

        public CARSType4(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules, int lodModelId)
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
    }

    /// <summary>
    /// VC cars (13 props)
    /// </summary>
    public class CARSType5 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private int Comprules { get; set; }

        private int WheelModelId { get; set; }

        private double WheelScale { get; set; }

        public CARSType5(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules, int wheelModelId, double wheelScale)
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
    }

    /// <summary>
    /// VC boat and heli (11 props)
    /// </summary>
    public class CARSType6 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private int Comprules { get; set; }

        public CARSType6(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules)
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
    }

    /// <summary>
    /// VC bike (13 props)
    /// </summary>
    public class CARSType7 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private int Comprules { get; set; }

        private int SteeringAngle { get; set; }

        private double WheelScale { get; set; }

        public CARSType7(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, int comprules, int steeringAngle, double wheelScale)
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
    }

    /// <summary>
    /// SA cars and other types (15 props)
    /// </summary>
    public class CARSType8 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Flags { get; set; }

        private int Comprules { get; set; }

        private int WheelId { get; set; }

        private double WheelScaleFront { get; set; }

        private double WheelScaleRear { get; set; }

        private int WheelUpgradeClass { get; set; }

        public CARSType8(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int flags, int comprules, int wheelId, double wheelScaleFront, double wheelScaleRear, int wheelUpgradeClass)
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
            this.WheelId = wheelId;
            this.WheelScaleFront = wheelScaleFront;
            this.WheelScaleRear = wheelScaleRear;
            this.WheelUpgradeClass = wheelUpgradeClass;
        }
    }

    /// <summary>
    /// SA boat (11 props)
    /// </summary>
    public class CARSType9 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Flags { get; set; }

        private int Comprules { get; set; }

        public CARSType9(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int flags, int comprules)
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
        protected List<PATHNode> Nodes = new List<PATHNode>();

        public override string ToString()
        {
            string nodes = null;
            var result = this.GetType()
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Select(field => field.GetValue(this))     
                             .ToList();

            // удалёна коллекция узлов путей, так как узлы выводятся отдельно
            result.RemoveAt(result.Count - 1);

            foreach (var node in this.Nodes)
            {
                nodes += "\t" + node.ToString() + "\r\n";
            }

            return string.Join(", ", result) + "\r\n" + nodes;
        }
    }

    public class PATHType1 : PATH
    {
        private string GroupType { get; set; }

        private int Id { get; set; }

        private string ModelName { get; set; }

        public PATHType1(string groupType, int id, string modelName, PATHNode[] nodes)
        {
            this.GroupType = groupType;
            this.Id = id;
            this.ModelName = modelName;
            this.Nodes = nodes.OfType<PATHNode>().ToList();
        }
    }

    public class PATHType2 : PATH
    {
        private string GroupType { get; set; }

        private int Delimiter { get; set; }

        public PATHType2(string groupType, int delimiter, PATHNode[] nodes)
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

        /// <summary>
        /// Создаёт строку из всех параметров, разделённых запятыми
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = this.GetType()
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Select(field => field.GetValue(this))
                             .ToArray();
            return string.Join(", ", result);
        }
    }
}
