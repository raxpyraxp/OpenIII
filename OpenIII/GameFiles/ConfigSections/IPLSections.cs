namespace OpenIII.GameFiles.ConfigSections.IPL
{
    public class INST : ConfigRow
    {
        public const string SectionName = "inst";
    }

    public class INSTType1 : INST
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private double PosX { get; set; }

        private double PosY { get; set; }

        private double PosZ { get; set; }

        private double ScaleX { get; set; }

        private double ScaleY { get; set; }

        private double ScaleZ { get; set; }

        private double RotX { get; set; }

        private double RotY { get; set; }

        private double RotZ { get; set; }

        private double RotW { get; set; }

        public INSTType1(int id, string modelName, double posX, double posY, double posZ, double scaleX, double scaleY, double scaleZ, double rotX, double rotY, double rotZ, double rotW)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.PosX = posX;
            this.PosY = posY;
            this.PosZ = posZ;
            this.ScaleX = scaleX;
            this.ScaleY = scaleY;
            this.ScaleZ = scaleZ;
            this.RotX = rotX;
            this.RotY = rotY;
            this.RotZ = rotZ;
            this.RotW = rotW;
        }
    }


    public class INSTType2 : INST
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private int Interior { get; set; }

        private double PosX { get; set; }

        private double PosY { get; set; }

        private double PosZ { get; set; }

        private double ScaleX { get; set; }

        private double ScaleY { get; set; }

        private double ScaleZ { get; set; }

        private double RotX { get; set; }

        private double RotY { get; set; }

        private double RotZ { get; set; }

        private double RotW { get; set; }

        public INSTType2(int id, string modelName, int interior, double posX, double posY, double posZ, double scaleX, double scaleY, double scaleZ, double rotX, double rotY, double rotZ, double rotW)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.Interior = interior;
            this.PosX = posX;
            this.PosY = posY;
            this.PosZ = posZ;
            this.ScaleX = scaleX;
            this.ScaleY = scaleY;
            this.ScaleZ = scaleZ;
            this.RotX = rotX;
            this.RotY = rotY;
            this.RotZ = rotZ;
            this.RotW = rotW;
        }
    }


    public class INSTType3 : INST
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private int Interior { get; set; }

        private double PosX { get; set; }

        private double PosY { get; set; }

        private double PosZ { get; set; }

        private double RotX { get; set; }

        private double RotY { get; set; }

        private double RotZ { get; set; }

        private double RotW { get; set; }

        private int Lod { get; set; }

        public INSTType3(int id, string modelName, int interior, double posX, double posY, double posZ, double rotX, double rotY, double rotZ, double rotW, int lod)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.Interior = interior;
            this.PosX = posX;
            this.PosY = posY;
            this.PosZ = posZ;
            this.RotX = rotX;
            this.RotY = rotY;
            this.RotZ = rotZ;
            this.RotW = rotW;
            this.Lod = lod;
        }
    }


    public class AUZO : ConfigRow
    {
        public const string SectionName = "auzo";
    }

    public class AUZOType1 : AUZO
    {
        private string Name { get; set; }

        private int Id { get; set; }

        private int Switch { get; set; }

        private double X1 { get; set; }

        private double Y1 { get; set; }

        private double Z1 { get; set; }

        private double X2 { get; set; }

        private double Y2 { get; set; }

        private double Z2 { get; set; }
    }

    public class AUZOType2 : AUZO
    {
        private string Name { get; set; }

        private int Id { get; set; }

        private int Switch { get; set; }

        private double X1 { get; set; }

        private double Y1 { get; set; }

        private double Z1 { get; set; }

        private double Radios { get; set; }
    }

    // TODO: классы секций IDE- и IPL-файлов соответствуют и не могут быть определены в обоих файлах

    public class CULL : ConfigRow
    {
        public const string SectionName = "cull";
    }

    public class CULLType1 : CULL
    {
        private double CenterX { get; set; }

        private double CenterY { get; set; }

        private double CenterZ { get; set; }

        private double X1 { get; set; }

        private double Y1 { get; set; }

        private double Z1 { get; set; }

        private double X2 { get; set; }

        private double Y2 { get; set; }

        private double Z2 { get; set; }

        private int Attribute { get; set; }

        private int WantedLevelDrop { get; set; }
    }

    public class CULLType2 : CULL
    {
        private double CenterX { get; set; }

        private double CenterY { get; set; }

        private double CenterZ { get; set; }

        private int Unknown1 { get; set; }

        private double Lenght { get; set; }

        private double Bottom { get; set; }

        private double Width { get; set; }

        private int Unknown2 { get; set; }

        private double Top { get; set; }

        private int Flag { get; set; }

        private int Unknown3 { get; set; }
    }

    public class CULLType3 : CULL
    {
        private double CenterX { get; set; }

        private double CenterY { get; set; }

        private double CenterZ { get; set; }

        private int Unknown1 { get; set; }

        private double Lenght { get; set; }

        private double Bottom { get; set; }

        private double Width { get; set; }

        private int Unknown2 { get; set; }

        private double Top { get; set; }

        private double Vx { get; set; }

        private double Vy { get; set; }

        private double Vz { get; set; }

        private double Cm { get; set; }
    }


    public class ENEX : ConfigRow
    {
        public const string SectionName = "enex";
    }

    public class ENEXType1 : ENEX
    {
        private double X1 { get; set; }

        private double Y1 { get; set; }

        private double Z1 { get; set; }

        private double EnterAngle { get; set; }

        private double SizeX { get; set; }

        private double SizeY { get; set; }

        private double SizeZ { get; set; }

        private double X2 { get; set; }

        private double Y2 { get; set; }

        private double Z2 { get; set; }

        private double ExitAngle { get; set; }

        private int TargetInterior { get; set; }

        private int Flags { get; set; }

        private string Name { get; set; }

        private int Sky { get; set; }

        private int NumPedsToSpawn { get; set; }

        private int TimeOn { get; set; }

        private int TimeOff { get; set; }
    }

    public class CARS : ConfigRow
    {
        public const string SectionName = "cars";
    }

    public class CARSType1 : CARS
    {
        private double PosX { get; set; }

        private double PosY { get; set; }

        private double PosZ { get; set; }

        private double Angle { get; set; }

        private int CarID { get; set; }

        private int PrimCol { get; set; }

        private int SecCol { get; set; }

        private int ForceSpawn { get; set; }

        private int Alarm { get; set; }

        private int DoorLock { get; set; }

        private int Unknown1 { get; set; }

        private int Unknown2 { get; set; }
    }

    public class TCYC : ConfigRow
    {
        public const string SectionName = "tcyc";
    }

    public class TCYCType1
    {
        private int X1 { get; set; }
        
        private int Y1 { get; set; }
        
        private int Z1 { get; set; }
        
        private int X2 { get; set; }
        
        private int Y2 { get; set; }
        
        private int Z2 { get; set; }
        
        private int farClip { get; set; }
        
        private int extraColor { get; set; }
        
        private int extraColorintensity { get; set; }
        
        private int falloffDist { get; set; }
        
        private int unused { get; set; }
        
        private int lodDistMult { get; set; }
    }

    public class GRGE : ConfigRow
    {
        public const string SectionName = "grge";
    }

    public class GRGEType1 : GRGE
    {
        private double Pos1 { get; set; }

        private double Pos2 { get; set; }

        private double Pos3 { get; set; }

        private double LineX { get; set; }

        private double LineY { get; set; }

        private double CubeX { get; set; }

        private double CubeY { get; set; }

        private double CubeZ { get; set; }

        private int Flag { get; set; }

        private int Type { get; set; }

        private string Name { get; set; }
    }


    public class JUMP : ConfigRow
    {
        public const string SectionName = "jump";
    }

    public class JUMPType1 : JUMP
    {
        private double StartLowerX { get; set; }

        private double StartLowerY { get; set; }

        private double StartLowerZ { get; set; }

        private double StartUpperX { get; set; }

        private double StartUpperY { get; set; }

        private double StartUpperZ { get; set; }

        private double TargetLowerX { get; set; }

        private double TargetLowerY { get; set; }

        private double TargetLowerZ { get; set; }

        private double TargetUpperX { get; set; }

        private double TargetUpperY { get; set; }

        private double TargetUpperZ { get; set; }

        private double CameraX { get; set; }

        private double CameraY { get; set; }

        private double CameraZ { get; set; }

        private int Reward { get; set; }
    }


    public class OCCL : ConfigRow
    {
        public const string SectionName = "occl";
    }

    public class PICK : ConfigRow
    {
        public const string SectionName = "pick";
    }

    public class PICKType1 : PICK
    {
        private int Id { get; set; }

        private double MidX { get; set; }

        private double MidY { get; set; }

        private double BottomZ { get; set; }
    }


    public class ZONE : ConfigRow
    {
        public const string SectionName = "zone";
    }

    public class ZONEType1 : ZONE
    {
        private string Name { get; set; }

        private int Type { get; set; }
        
        private double X1 { get; set; }
        
        private double Y1 { get; set; }
        
        private double Z1 { get; set; }
        
        private double X2 { get; set; }
        
        private double Y2 { get; set; }
        
        private double Z2 { get; set; }
        
        private int Level { get; set; }
        
        private string Text { get; set; }
    }


    public class OCCLType1 : OCCL
    {
        private double MidX { get; set; }

        private double MidY { get; set; }

        private double BottomZ { get; set; }

        private double WidthX { get; set; }

        private double WidthY { get; set; }

        private double Height { get; set; }

        private double Rotation { get; set; }
    }

    public class OCCLType2 : OCCL
    {
        private double MidX { get; set; }

        private double MidY { get; set; }

        private double BottomZ { get; set; }

        private double WidthX { get; set; }

        private double WidthY { get; set; }

        private double Height { get; set; }

        private double Rotation { get; set; }

        // TODO: определить точное имя параметра
        private double Unknown1 { get; set; }

        // TODO: определить точное имя параметра
        private int Unknown2 { get; set; }
    }
}
