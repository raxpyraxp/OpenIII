using OpenIII.GameFiles;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenIII.Forms
{
    public partial class FXTEditorWindow : OpenIII.Forms.BaseWindow
    {
        private const string Headers = "# Hello!";
        private FXTFile CurrentFile;
        private static FXTEditorWindow instance;

        public FXTEditorWindow()
        {
            InitializeComponent();
        }

        public static FXTEditorWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new FXTEditorWindow();
            }

            return instance;
        }

        public void OpenFile(FXTFile file)
        {
            file.ParseData();

            CurrentFile = file;

            foreach (FXTFileItem item in file.Items)
            {
                DataGridView.Rows.Add(item.GetKey(), item.GetValue());
            }
        }

        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string data;
            data = CurrentFile.DataToString();

            StreamWriter streamWriter = new StreamWriter(CurrentFile.FullPath);
            streamWriter.WriteLine(Headers);
            streamWriter.Write(data);
            streamWriter.Close();
        }

        /*
        public FXTFile GetData()
        {
            FXTFile data = new FXTFile();
            List<string> parameters = new List<string>();

            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    parameters.Add(cell.Value == null ? "" : cell.Value.ToString());
                }

                data.AddItem(parameters[0], parameters[1]);
                parameters.Clear();
            }

            return data;
        }
        */
    }
}
