namespace OpenIII.GameFiles.ConfigSections.IPL
{
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
}
