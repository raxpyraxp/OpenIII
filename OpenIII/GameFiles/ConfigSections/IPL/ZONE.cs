namespace OpenIII.GameFiles.ConfigSections.IPL
{
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
}
