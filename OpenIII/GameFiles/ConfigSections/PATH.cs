using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenIII.GameFiles.ConfigSections
{
    public class PATH : ConfigRow
    {
        public const string SectionName = "path";
        protected List<PATHNode> Nodes = new List<PATHNode>();

        public override string ToString()
        {
            string nodes = null;
            var result = this.GetType()
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Select(field => field.GetValue(this))
                             .ToList();

            // удалена коллекция узлов путей, так как узлы выводятся отдельно
            result.RemoveAt(result.Count - 1);

            foreach (var node in this.Nodes)
            {
                nodes += "\t" + node.ToString() + "\r\n";
            }

            return string.Join(", ", result) + "\r\n" + nodes;
        }
    }

    public class PATHType1 : PATH
    {
        private string GroupType { get; set; }

        private int Id { get; set; }

        private string ModelName { get; set; }

        public PATHType1(string groupType, int id, string modelName, PATHNode[] nodes)
        {
            this.GroupType = groupType;
            this.Id = id;
            this.ModelName = modelName;
            this.Nodes = nodes.OfType<PATHNode>().ToList();
        }
    }

    public class PATHType2 : PATH
    {
        private string GroupType { get; set; }

        private int Delimiter { get; set; }

        public PATHType2(string groupType, int delimiter, PATHNode[] nodes)
        {
            this.GroupType = groupType;
            this.Delimiter = delimiter;
            this.Nodes = nodes.OfType<PATHNode>().ToList();
        }
    }

    public class PATHNode
    {
        private int NodeType { get; set; }

        private int NextNode { get; set; }

        private int IsCrossRoad { get; set; }

        private double X { get; set; }

        private double Y { get; set; }

        private double Z { get; set; }

        private double Unknown { get; set; }

        private int LeftLanes { get; set; }

        private int RightLanes { get; set; }

        private double XRel { get; set; }

        private double YRel { get; set; }

        private double ZRel { get; set; }

        private double XAbs { get; set; }

        private double YAbs { get; set; }

        private double ZAbs { get; set; }

        private double Median { get; set; }

        private int SpeedLimit { get; set; }

        private int Flags { get; set; }

        private double SpawnRate { get; set; }

        public PATHNode(int nodeType, int nextNode, int isCrossRoad, double x, double y, double z, double unknown, int leftLanes, int rightLanes)
        {
            this.NodeType = nodeType;
            this.NextNode = nextNode;
            this.IsCrossRoad = isCrossRoad;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Unknown = unknown;
            this.LeftLanes = leftLanes;
            this.RightLanes = rightLanes;
        }

        public PATHNode(int nodeType, int nextNode, int isCrossRoad, double xRel, double yRel, double zRel, int leftLanes, int rightLanes, double median)
        {
            this.NodeType = nodeType;
            this.NextNode = nextNode;
            this.IsCrossRoad = isCrossRoad;
            this.XRel = xRel;
            this.YRel = yRel;
            this.ZRel = zRel;
            this.LeftLanes = leftLanes;
            this.RightLanes = rightLanes;
            this.Median = median;
        }

        /// <summary>
        /// Создаёт строку из всех параметров, разделённых запятыми
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = this.GetType()
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Select(field => field.GetValue(this))
                             .ToArray();
            return string.Join(", ", result);
        }
    }
}