using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenIII.GameFiles;

namespace OpenIII
{
    public partial class MainWindow : Form
    {
        private ArchiveFile archiveFile;

        public MainWindow(ArchiveFile file)
        {
            InitializeComponent();

            archiveFile = file;
            SetListView(archiveFile.readImgFileList());
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

        private void fileListView_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                ArchiveEntry entry = (ArchiveEntry)item.Tag;
                entry.extract(@"D:\Documents\" + entry.filename);
            }
        }
    }
}
