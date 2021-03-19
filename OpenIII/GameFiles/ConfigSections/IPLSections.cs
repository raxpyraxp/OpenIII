namespace OpenIII.GameFiles
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
}
