namespace OpenIII.GameFiles.ConfigSections.IPL
{
    public class TCYC : ConfigRow
    {
        public const string SectionName = "tcyc";
    }

    public class TCYCType1 : TCYC
    {
        private double X1 { get; set; }

        private double Y1 { get; set; }

        private double Z1 { get; set; }

        private double X2 { get; set; }

        private double Y2 { get; set; }

        private double Z2 { get; set; }

        private int farClip { get; set; }

        private int extraColor { get; set; }

        private int extraColorintensity { get; set; }

        private int falloffDist { get; set; }

        //private int unused { get; set; }

        private double lodDistMult { get; set; }
    }
}
