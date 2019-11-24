using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenIII.GameFiles;

namespace OpenIII
{
    public partial class FileBrowserWindow : Form
    {
        private ArchiveFile archiveFile;

        public FileBrowserWindow(ArchiveFile file)
        {
            InitializeComponent();

            archiveFile = file;
            SetListView(archiveFile.readImgFileList());
            SetTotalFiles(archiveFile.TotalFiles);
        }

        public void SetListView(List<ArchiveEntry> list)
        {
            fileListView.Items.Clear();

            foreach (ArchiveEntry entry in list)
            {
                ListViewItem item = new ListViewItem(entry.filename);
                item.Tag = entry;
                fileListView.Items.Add(item);
            }
        }

        public void SetTotalFiles(long totalFiles)
        {
            totalFilesLabel.Text = totalFiles.ToString();
        }

        private void fileListViewDoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                ArchiveEntry entry = (ArchiveEntry)item.Tag;
                entry.extract(@"D:\Documents\" + entry.filename);
            }
        }
    }
}
