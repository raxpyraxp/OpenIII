namespace OpenIII.GameFiles
{
    public class WEAP : ConfigRow
    {
        public const string SectionName = "weap";
    }

    public class WEAPType1 : WEAP
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string AnimationName { get; set; }

        private int MeshCount { get; set; }

        private double DrawDistance { get; set; }

        public WEAPType1() { }


        public WEAPType1(int id, string modelName, string txdName, string animationName, int meshCount, double drawDistance)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.AnimationName = animationName;
            this.MeshCount = meshCount;
            this.DrawDistance = drawDistance;
        }
    }
}