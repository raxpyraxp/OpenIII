using OpenIII.Forms;
using OpenIII.GameFiles;
using System.Linq;

namespace OpenIII
{
    public partial class DataEditorWindow : BaseWindow
    {
        private static DataEditorWindow instance;
        public static DataEditorWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new DataEditorWindow();
            }

            return instance;
        }

        public void OpenFile(TextFile file)
        {
            var result = file.ParseData(file.FullPath);

            DataGridView.Rows.Clear();

            for (int i = 0; i < result.Count; i++)
            {
                var arr = result[i].ToArray();
                DataGridView.Rows.Add(arr);
            }
        }

        public DataEditorWindow()
        {
            InitializeComponent();

            SetColumnCount(35);
        }

        public void SetColumnCount(int columnCount) {
            DataGridView.ColumnCount = columnCount;
        }
    }
}
