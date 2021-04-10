namespace OpenIII.GameFiles
{
    public class CARS : ConfigRow
    {
        public const string SectionName = "cars";
    }

    /// <summary>
    /// III cars (12 props)
    /// </summary>
    public class CARSType1 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private Flag Comprules { get; set; }

        private int WheelModelId { get; set; }

        private double WheelScale { get; set; }

        public CARSType1() { }

        public CARSType1(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, Flag comprules, int wheelModelId, double wheelScale)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.WheelModelId = wheelModelId;
            this.WheelScale = wheelScale;
        }
    }

    /// <summary>
    /// III boat, train and heli (10 props)
    /// </summary>
    public class CARSType2 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private Flag Comprules { get; set; }

        public CARSType2() { }

        public CARSType2(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, Flag comprules)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
        }
    }

    /// <summary>
    /// III plane (11 props)
    /// </summary>
    public class CARSType3 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private Flag Comprules { get; set; }

        private int LODModelId { get; set; }

        public CARSType3() { }

        public CARSType3(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string vehicleClass, int frequency, int level, Flag comprules, int lodModelId)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.LODModelId = lodModelId;
        }
    }

    /// <summary>
    /// VC plane (12 props)
    /// </summary>
    public class CARSType4 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private Flag Comprules { get; set; }

        private int LODModelId { get; set; }

        public CARSType4() { }

        public CARSType4(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, Flag comprules, int lodModelId)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.LODModelId = lodModelId;
        }
    }

    /// <summary>
    /// VC cars (13 props)
    /// </summary>
    public class CARSType5 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private Flag Comprules { get; set; }

        private int WheelModelId { get; set; }

        private double WheelScale { get; set; }

        public CARSType5() { }
            
        public CARSType5(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, Flag comprules, int wheelModelId, double wheelScale)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.WheelModelId = wheelModelId;
            this.WheelScale = wheelScale;
        }
    }

    /// <summary>
    /// VC boat and heli (11 props)
    /// </summary>
    public class CARSType6 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private Flag Comprules { get; set; }

        public CARSType6() { }

        public CARSType6(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, Flag comprules)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
        }
    }

    /// <summary>
    /// VC bike (13 props)
    /// </summary>
    public class CARSType7 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Level { get; set; }

        private Flag Comprules { get; set; }

        private int SteeringAngle { get; set; }

        private double WheelScale { get; set; }

        public CARSType7() { }

        public CARSType7(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int level, Flag comprules, int steeringAngle, double wheelScale)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Level = level;
            this.Comprules = comprules;
            this.SteeringAngle = steeringAngle;
            this.WheelScale = wheelScale;
        }
    }

    /// <summary>
    /// SA cars and other types (15 props)
    /// </summary>
    public class CARSType8 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Flags { get; set; }

        private Flag Comprules { get; set; }

        private int WheelId { get; set; }

        private double WheelScaleFront { get; set; }

        private double WheelScaleRear { get; set; }

        private int WheelUpgradeClass { get; set; }

        public CARSType8() { }

        public CARSType8(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int flags, Flag comprules, int wheelId, double wheelScaleFront, double wheelScaleRear, int wheelUpgradeClass)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Flags = flags;
            this.Comprules = comprules;
            this.WheelId = wheelId;
            this.WheelScaleFront = wheelScaleFront;
            this.WheelScaleRear = wheelScaleRear;
            this.WheelUpgradeClass = wheelUpgradeClass;
        }
    }

    /// <summary>
    /// SA boat (11 props)
    /// </summary>
    public class CARSType9 : CARS
    {
        private int Id { get; set; }

        private string ModelName { get; set; }

        private string TxdName { get; set; }

        private string Type { get; set; }

        private string HandlingId { get; set; }

        private string GXTKey { get; set; }

        private string Anims { get; set; }

        private string VehicleClass { get; set; }

        private int Frequency { get; set; }

        private int Flags { get; set; }

        private Flag Comprules { get; set; }

        public CARSType9(int id, string modelName, string txdName, string type, string handlingId, string gxtKey, string anims, string vehicleClass, int frequency, int flags, Flag comprules)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.TxdName = txdName;
            this.Type = type;
            this.HandlingId = handlingId;
            this.GXTKey = gxtKey;
            this.Anims = anims;
            this.VehicleClass = vehicleClass;
            this.Frequency = frequency;
            this.Flags = flags;
            this.Comprules = comprules;
        }
    }
}