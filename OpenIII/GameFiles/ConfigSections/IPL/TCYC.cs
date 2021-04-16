namespace OpenIII.GameFiles.ConfigSections.IPL
{
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
}
