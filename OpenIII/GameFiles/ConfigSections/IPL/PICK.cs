namespace OpenIII.GameFiles.ConfigSections.IPL
{   
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
}
