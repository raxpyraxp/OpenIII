using OpenIII.Forms;

namespace OpenIII
{
    public partial class DataEditorWindow : BaseWindow
    {
        public DataEditorWindow()
        {
            InitializeComponent();

            songsDataGridView.ColumnCount = 35;
        }
    }
}
