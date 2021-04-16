namespace OpenIII.GameFiles.ConfigSections.IPL
{
    public class OCCL : ConfigRow
    {
        public const string SectionName = "occl";
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

        private double Unknown1 { get; set; }

        private int Unknown2 { get; set; }
    }
}
