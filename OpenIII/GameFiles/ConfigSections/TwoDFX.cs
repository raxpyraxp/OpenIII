namespace OpenIII.GameFiles
{
    public class TwoDFX : ConfigRow
    {
        public const string SectionName = "2dfx";
    }

    /// <summary>
    /// Lights.
    /// </summary>
    public class TwoDFXType1 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown { get; set; }

        private int TwoDFXType { get; set; }

        private string Corona { get; set; }

        private string Shadow { get; set; }

        private double Distance { get; set; }

        private double OuterRange { get; set; }

        private double Size { get; set; }

        private double InnerRange { get; set; }

        private int ShadowIntensity { get; set; }

        private int Flash { get; set; }

        private int Wet { get; set; }

        private int Flare { get; set; }

        private int Flags { get; set; }

        public TwoDFXType1(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType, string corona, string shadow, double distance, double outerRange, double size, double innerRange, int shadowIntensity, int flash, int wet, int flare, int flags)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown = unknown;
            this.TwoDFXType = twoDFXType;
            this.Corona = corona;
            this.Shadow = shadow;
            this.Distance = distance;
            this.OuterRange = outerRange;
            this.Size = size;
            this.InnerRange = innerRange;
            this.ShadowIntensity = shadowIntensity;
            this.Flash = flash;
            this.Wet = wet;
            this.Flare = flare;
            this.Flags = flags;
        }
    }

    /// <summary>
    /// Particles.
    /// </summary>
    public class TwoDFXType2 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown { get; set; }

        private int TwoDFXType { get; set; }

        private int Particle { get; set; }

        private double StrengthX { get; set; }

        private double StrengthY { get; set; }

        private double StrengthZ { get; set; }

        private double Scale { get; set; }

        public TwoDFXType2(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType, int particle, double strengthX, double strengthY, double strengthZ, double scale)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown = unknown;
            this.TwoDFXType = twoDFXType;
            this.Particle = particle;
            this.StrengthX = strengthX;
            this.StrengthY = strengthY;
            this.StrengthZ = strengthZ;
            this.Scale = scale;
        }
    }

    /// <summary>
    /// Peds.
    /// </summary>
    public class TwoDFXType3 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown1 { get; set; }

        private int TwoDFXType { get; set; }

        private int Unknown2 { get; set; }

        private double Unk1 { get; set; }

        private double Unk2 { get; set; }

        private double Unk3 { get; set; }

        private int Unknown3 { get; set; }

        public TwoDFXType3(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, int unknown2, double unk1, double unk2, double unk3, int unknown3)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown1 = unknown1;
            this.TwoDFXType = twoDFXType;
            this.Unknown2 = unknown2;
            this.Unk1 = unk1;
            this.Unk2 = unk2;
            this.Unk3 = unk3;
            this.Unknown3 = unknown3;
        }
    }

    /// <summary>
    /// Sun Reflections.
    /// </summary>
    public class TwoDFXType4 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown1 { get; set; }

        private int TwoDFXType { get; set; }

        private int Behavior { get; set; }

        private double Unk1 { get; set; }

        private double Unk2 { get; set; }

        private double Unk3 { get; set; }

        private double RollX { get; set; }

        private double RollY { get; set; }

        private double RollZ { get; set; }

        public TwoDFXType4(int id, double x, double y, double z, int r, int g, int b, int unknown1, int twoDFXType, int behavior, double unk1, double unk2, double unk3, double rollX, double rollY, double rollZ)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown1 = unknown1;
            this.TwoDFXType = twoDFXType;
            this.Behavior = behavior;
            this.Unk1 = unk1;
            this.Unk2 = unk2;
            this.Unk3 = unk3;
            this.RollX = rollX;
            this.RollY = rollY;
            this.RollZ = rollZ;
        }
    }

    public class TwoDFXType5 : TwoDFX
    {
        private int Id { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private int R { get; set; }

        private int G { get; set; }

        private int B { get; set; }

        private int Unknown { get; set; }

        private int TwoDFXType { get; set; }

        public TwoDFXType5(int id, double x, double y, double z, int r, int g, int b, int unknown, int twoDFXType)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.Unknown = unknown;
            this.TwoDFXType = twoDFXType;
        }
    }
}