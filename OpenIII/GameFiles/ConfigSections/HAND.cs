namespace OpenIII.GameFiles
{
    public class HAND : ConfigRow
    {
        public const string SectionName = "hand";
    }

    public class HANDType1 : HAND
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Unknown { get; set; }

        public HANDType1() { }

        public HANDType1(int id, string modelName, string txdName, string Unknown)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Unknown = Unknown;
        }
    }
}