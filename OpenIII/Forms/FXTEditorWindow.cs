using OpenIII.GameFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenIII.Forms
{
    public partial class FXTEditorWindow : BaseWindow
    {
        private bool isFileEdited = false;
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

        private void AddRow(string key, string value)
        {
            CurrentFile.Items.Add(new FXTFileItem(key, value));
        }

        private void DeleteRow()
        {
            CurrentFile.Items.RemoveAt(DataGridView.SelectedCells[0].RowIndex);
        }

        private void DeleteRow(uint index)
        {
            CurrentFile.Items.RemoveAt((int)index);
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
                if (isFileEdited == true)
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

            isFileEdited = false;
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
            isFileEdited = true;
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRow(null, null);
            isFileEdited = true;
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRow();
            isFileEdited = true;
        }
    }
}
