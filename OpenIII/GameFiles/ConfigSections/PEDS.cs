using OpenIII.GameDefinitions;

namespace OpenIII.GameFiles
{
    public class PEDS : ConfigRow
    {
        public const string SectionName = "peds";
    }

    public class PEDSType1 : PEDS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string DefaultPedtype { get; set; }

        private string Behavior { get; set; }

        private string AnimGroup { get; set; }

        private Flag CarsCanDrive { get; set; }

        public PEDSType1() { }

        public PEDSType1(int id, string modelName, string txdName, string defaultPedtype, string behavior, string animGroup, Flag carsCanDrive)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = animGroup;
            this.CarsCanDrive = carsCanDrive;
        }
    }

    public class PEDSType2 : PEDS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string DefaultPedtype { get; set; }

        private string Behavior { get; set; }

        private string AnimGroup { get; set; }

        private Flag CarsCanDrive { get; set; }

        private string AnimFile { get; set; }

        private RadioStationVC RadioStation1 { get; set; }

        private RadioStationVC RadioStation2 { get; set; }

        public PEDSType2() { }

        public PEDSType2(int id, string modelName, string txdName, string defaultPedtype, string behavior, string animGroup, Flag carsCanDrive, string animFile, RadioStationVC radioStation1, RadioStationVC radioStation2)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = animGroup;
            this.CarsCanDrive = carsCanDrive;
            this.AnimFile = animFile;
            this.RadioStation1 = radioStation1;
            this.RadioStation2 = radioStation2;
        }
    }

    public class PEDSType3 : PEDS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string DefaultPedtype { get; set; }

        private string Behavior { get; set; }

        private string AnimGroup { get; set; }

        private Flag CarsCanDrive { get; set; }

        private Flag Flags { get; set; }

        private string AnimFile { get; set; }

        private RadioStationSA RadioStation1 { get; set; }

        private RadioStationSA RadioStation2 { get; set; }

        private string VoiceArchive { get; set; }

        private string Voice1 { get; set; }

        private string Voice2 { get; set; }

        public PEDSType3() { }

        public PEDSType3(int id, string modelName, string txdName, string defaultPedtype, string behavior, string AnimGroup, Flag carsCanDrive, Flag flags, string animFile, RadioStationSA radioStation1, RadioStationSA radioStation2, string voiceArchive, string voice1, string voice2)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.DefaultPedtype = defaultPedtype;
            this.Behavior = behavior;
            this.AnimGroup = AnimGroup;
            this.CarsCanDrive = carsCanDrive;
            this.Flags = flags;
            this.AnimFile = animFile;
            this.RadioStation1 = radioStation1;
            this.RadioStation2 = radioStation2;
            this.VoiceArchive = voiceArchive;
            this.Voice1 = voice1;
            this.Voice2 = voice2;
        }
    }
}