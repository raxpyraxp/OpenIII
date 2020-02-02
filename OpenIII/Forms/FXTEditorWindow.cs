using OpenIII.GameFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenIII.Forms
{
    public partial class FXTEditorWindow : OpenIII.Forms.BaseWindow
    {
        private bool FileIsEdited = false;
        private const string Headers = "# Hello!";
        private BindingSource bindingSource = new BindingSource();
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

            DataGridView.DataSource = CurrentFile.Items;

            /*
            foreach (FXTFileItem item in file.Items)
            {
                DataGridView.Rows.Add(item.GetKey(), item.GetValue());
            }
            */
        }

        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string data = CurrentFile.DataToString();

            try
            {
                if (FileIsEdited == true)
                {
                    StreamWriter streamWriter = new StreamWriter(CurrentFile.GetStream(FileMode.Create, FileAccess.Write));
                    streamWriter.WriteLine(Headers);
                    streamWriter.Write(data);
                    streamWriter.Close();
                } else
                {
                    MessageBox.Show("File wasn't changed!");
                    return;
                }
            } catch (Exception exception)
            {
                MessageBox.Show("Can't save the file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("File saved successsfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FileIsEdited = false;
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CloseWindow();
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView.Rows[e.RowIndex].ErrorText = "";

            if (e.FormattedValue.ToString().Length > 7 && e.ColumnIndex == 0) {
                e.Cancel = true;
                DataGridView.Rows[e.RowIndex].ErrorText = "Key's name length can't be greater than 7!";
            }
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            FileIsEdited = true;
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
