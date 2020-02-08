using OpenIII.Forms;
using System.Windows.Forms;

namespace OpenIII.GameFiles
{
    public partial class GXTEditorWindow : BaseWindow
    {
        private GXTFile CurrentFile;
        public GXTEditorWindow()
        {
            InitializeComponent();
        }

        public void OpenFile(GXTFile file)
        {
            file.ParseData();

            CurrentFile = file;

            foreach (GXTFileBlockEntry item in CurrentFile.Blocks[0].Entries)
            {
                listBox1.Items.Add(item.Name);
            }

            dataGridView1.ColumnCount = 1;

            foreach (GXTFileBlockEntry item in CurrentFile.Blocks[1].Entries)
            {
                dataGridView1.Rows.Add(item.Name);
            }
        }
    }
}
