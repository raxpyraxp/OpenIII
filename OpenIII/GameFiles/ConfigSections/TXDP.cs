namespace OpenIII.GameFiles
{
    public class TXDP : ConfigRow
    {
        public const string SectionName = "txdp";
    }

    public class TXDPType1 : TXDP
    {
        private string TxdName;

        private string TxdParentName;

        public TXDPType1(string txdName, string txdParentName)
        {
            this.TxdName = txdName;
            this.TxdParentName = txdParentName;
        }
    }
}