namespace OpenIII.GameFiles.ConfigSections.IPL
{
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