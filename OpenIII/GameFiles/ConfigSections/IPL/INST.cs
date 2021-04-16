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
}