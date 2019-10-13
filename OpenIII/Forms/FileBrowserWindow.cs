using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenIII.GameFiles;

namespace OpenIII
{
    public partial class FileBrowserWindow : Form
    {
        private ArchiveFile archiveFile;
        private GameDirectory rootDir;

        public FileBrowserWindow(ArchiveFile file)
        {
            InitializeComponent();

            archiveFile = file;
            SetListView(archiveFile.readImgFileList());
            SetTotalFiles(archiveFile.TotalFiles);
        }

        public FileBrowserWindow(GameDirectory rootDir)
        {
            InitializeComponent();

            SetListView(rootDir.getFiles());
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

        public void SetListView(List<GameFile> list)
        {
            fileListView.Items.Clear();

            foreach (GameFile file in list)
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.Tag = file;
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
