namespace OpenIII.GameFiles
{
    public class HIER : ConfigRow
    {
        public const string SectionName = "hier";
    }

    public class HIERType1 : HIER
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        public HIERType1(int id, string modelName, string txdName)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
        }
    }
}