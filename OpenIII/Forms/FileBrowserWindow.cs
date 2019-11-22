using System;
using System.Collections.Generic;
using System.Drawing;
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

            this.rootDir = rootDir;
            SetFileListView(rootDir.getContent());
            SetDirListView(rootDir);
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

        public void SetFileListView(List<GameResource> list)
        {
            fileListView.Items.Clear();
            fileListView.SmallImageList = new ImageList();
            fileListView.LargeImageList = new ImageList();

            foreach (GameResource resource in list)
            {
                ListViewItem item = new ListViewItem(resource.Name);
                
                // Determine image key to show icon
                string imageKey = resource is GameDirectory ? "dir" : "file." + resource.Extension;
                fileListView.SmallImageList.Images.Add(imageKey, resource.SmallIcon);
                fileListView.LargeImageList.Images.Add(imageKey, resource.LargeIcon);

                item.Tag = resource;
                item.ImageKey = imageKey;
                
                fileListView.Items.Add(item);
            }
        }

        public void SetDirListView(GameDirectory rootdir)
        {
            fileTreeView.Nodes.Clear();
            fileTreeView.ImageList = new ImageList();

            fileTreeView.ImageList.Images.Add("dir", rootDir.SmallIcon);

            TreeNode item = new TreeNode(rootdir.Name);
            item.Tag = rootdir;
            item.ImageKey = "dir";

            if (rootdir.getDirectories().Count != 0)
            {
                // To make node expandable we're adding an empty element.
                // When user expands it, we're removing this and query the actual child dir list
                item.Nodes.Add("");
            }

            fileTreeView.Nodes.Add(item);
            item.Expand();
        }

        public TreeNode[] GetNodesList(List<GameDirectory> list)
        {
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (GameDirectory dir in list)
            {
                TreeNode item = new TreeNode(dir.Name);
                item.Tag = dir;
                item.ImageKey = "dir";

                if (dir.getDirectories().Count != 0)
                {
                    // To make node expandable we're adding an empty element.
                    // When user expands it, we're removing this and query the actual child dir list
                    item.Nodes.Add("");
                }

                nodes.Add(item);
            }

            return nodes.ToArray();
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

        private void fileTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            fileTreeView.BeginUpdate();
            GameDirectory dir = (GameDirectory)e.Node.Tag;
            e.Node.Nodes.Clear();
            e.Node.Nodes.AddRange(GetNodesList(dir.getDirectories()));
            fileTreeView.EndUpdate();
        }

        private void fileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GameDirectory dir = (GameDirectory)e.Node.Tag;
            SetFileListView(dir.getContent());
        }
    }
}
