namespace OpenIII.GameFiles.ConfigSections.IDE
{
    public class OBJS : ConfigRow
    {
        public const string SectionName = "objs";
    }

    public class OBJSType1 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private int Flags { get; set; }

        public OBJSType1() { }

        public OBJSType1(int id, string modelName, string txdName, int meshCount, double drawDistance1, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.Flags = flags;
        }
    }

    public class OBJSType2 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private int Flags { get; set; }

        public OBJSType2() { }

        public OBJSType2(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.Flags = flags;
        }
    }

    public class OBJSType3 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance1 { get; set; }

        private double DrawDistance2 { get; set; }

        private double DrawDistance3 { get; set; }

        private int Flags { get; set; }

        public OBJSType3() { }

        public OBJSType3(int id, string modelName, string txdName, int meshCount, double drawDistance1, double drawDistance2, double drawDistance3, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.MeshCount = meshCount;
            this.DrawDistance1 = drawDistance1;
            this.DrawDistance2 = drawDistance2;
            this.DrawDistance3 = drawDistance3;
            this.Flags = flags;
        }
    }

    public class OBJSType4 : OBJS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private double DrawDistance { get; set; }

        private int Flags { get; set; }

        public OBJSType4() { }

        public OBJSType4(int id, string modelName, string txdName, double drawDistance, int flags)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DrawDistance = drawDistance;
            this.Flags = flags;
        }
    }
}