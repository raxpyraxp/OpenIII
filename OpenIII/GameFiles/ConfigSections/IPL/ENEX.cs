namespace OpenIII.GameFiles.ConfigSections.IPL
{
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
}
