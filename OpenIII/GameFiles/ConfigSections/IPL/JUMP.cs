namespace OpenIII.GameFiles.ConfigSections.IPL
{
    public class JUMP : ConfigRow
    {
        public const string SectionName = "jump";
    }

    public class JUMPType1 : JUMP
    {
        private double StartLowerX { get; set; }

        private double StartLowerY { get; set; }

        private double StartLowerZ { get; set; }

        private double StartUpperX { get; set; }

        private double StartUpperY { get; set; }

        private double StartUpperZ { get; set; }

        private double TargetLowerX { get; set; }

        private double TargetLowerY { get; set; }

        private double TargetLowerZ { get; set; }

        private double TargetUpperX { get; set; }

        private double TargetUpperY { get; set; }

        private double TargetUpperZ { get; set; }

        private double CameraX { get; set; }

        private double CameraY { get; set; }

        private double CameraZ { get; set; }

        private int Reward { get; set; }
    }
}
