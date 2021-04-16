namespace OpenIII.GameFiles.ConfigSections.IPL
{
    public class AUZO : ConfigRow
    {
        public const string SectionName = "auzo";
    }

    public class AUZOType1 : AUZO
    {
        private string Name { get; set; }

        private int Id { get; set; }

        private int Switch { get; set; }

        private double X1 { get; set; }

        private double Y1 { get; set; }

        private double Z1 { get; set; }

        private double X2 { get; set; }

        private double Y2 { get; set; }

        private double Z2 { get; set; }
    }

    public class AUZOType2 : AUZO
    {
        private string Name { get; set; }

        private int Id { get; set; }

        private int Switch { get; set; }

        private double X1 { get; set; }

        private double Y1 { get; set; }

        private double Z1 { get; set; }

        private double Radios { get; set; }
    }
}
