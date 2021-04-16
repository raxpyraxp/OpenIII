namespace OpenIII.GameFiles.ConfigSections.IPL
{
    public class GRGE : ConfigRow
    {
        public const string SectionName = "grge";
    }

    public class GRGEType1 : GRGE
    {
        private double Pos1 { get; set; }

        private double Pos2 { get; set; }

        private double Pos3 { get; set; }

        private double LineX { get; set; }

        private double LineY { get; set; }

        private double CubeX { get; set; }

        private double CubeY { get; set; }

        private double CubeZ { get; set; }

        private int Flag { get; set; }

        private int Type { get; set; }

        private string Name { get; set; }
    }
}
