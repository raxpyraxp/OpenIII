using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using OpenIII.GameFiles;
using OpenIII.Utils;
using OpenIII.Forms;

namespace OpenIII
{
    public partial class FileBrowserWindow : Form
    {
        private static FileBrowserWindow instance;

        private ArchiveFile archiveFile;
        private GameDirectory rootDir;
        public AboutWindow aboutWindow;

        public FileBrowserWindow()
        {
            InitializeComponent();
        }

        public static FileBrowserWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new FileBrowserWindow();
            }

            return instance;
        }

        public void OpenDir(GameDirectory rootDir)
        {
            this.rootDir = rootDir;
            archiveFile = null;
            SetFileListView(rootDir.GetContent());
            SetDirListView(rootDir);
        }

        public void OpenArchive(ArchiveFile archive)
        {
            archiveFile = archive;
            SetFileListView(archiveFile.GetFileList());
            SetTotalFiles(archiveFile.TotalFiles);
            fileTreeView.SelectedNode = null;
        }

        public void SetFileListView(List<GameResource> list)
        {
            UseWaitCursor = true;
            Application.DoEvents();
            fileListView.BeginUpdate();

            fileListView.Items.Clear();
            fileListView.SmallImageList = new ImageList();
            fileListView.LargeImageList = new ImageList();

            foreach (GameResource resource in list)
            {
                ListViewItem item = new ListViewItem(resource.Name);
                
                // Determine image key to show icon
                string imageKey = resource is GameDirectory ? "dir" : "file." + resource.Extension;

                if (!fileListView.SmallImageList.Images.ContainsKey(imageKey))
                {
                    fileListView.SmallImageList.Images.Add(imageKey, resource.SmallIcon);
                    fileListView.LargeImageList.Images.Add(imageKey, resource.LargeIcon);
                }

                item.Tag = resource;
                item.ImageKey = imageKey;

                fileListView.Items.Add(item);
            }

            fileListView.EndUpdate();
            UseWaitCursor = false;
            Application.DoEvents();
        }

        public void SetDirListView(GameDirectory rootdir)
        {
            UseWaitCursor = true;
            Application.DoEvents();
            fileTreeView.BeginUpdate();

            fileTreeView.Nodes.Clear();
            fileTreeView.ImageList = new ImageList();
            fileTreeView.ImageList.Images.Add("dir", rootDir.SmallIcon);

            fileTreeView.Nodes.Add(CreateNode(rootDir));
            fileTreeView.Nodes[0].Expand();

            fileTreeView.EndUpdate();
            UseWaitCursor = false;
            Application.DoEvents();
        }

        public TreeNode[] GetNodesList(List<GameDirectory> list)
        {
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (GameDirectory dir in list)
            {
                nodes.Add(CreateNode(dir));
            }

            return nodes.ToArray();
        }

        public TreeNode CreateNode(GameDirectory dir)
        {
            TreeNode item = new TreeNode(dir.Name);
            item.Tag = dir;
            item.ImageKey = "dir";

            if (dir.GetDirectories().Count != 0)
            {
                // To make node expandable we're adding an empty element.
                // When user expands it, we're removing this and query the actual child dir list
                item.Nodes.Add("");
            }

            return item;
        }

        public void SetTotalFiles(long totalFiles)
        {
            totalFilesLabel.Text = totalFiles.ToString();
        }

        public TreeNode ExpandDirectoryNode(GameDirectory dir)
        {
            DirectoryInfo info = new DirectoryInfo(dir.FullPath);

            if (dir.FullPath != rootDir.FullPath)
            {
                // If current dir is not root game dir, expand parent dir first
                TreeNode openedNode = ExpandDirectoryNode(new GameDirectory(info.Parent.FullName));

                // Find associated tree node and expand it
                foreach (TreeNode node in openedNode.Nodes)
                {
                    GameDirectory dirNode = (GameDirectory)node.Tag;

                    if (dirNode.FullPath == dir.FullPath)
                    {
                        // AfterSelect is a temporary solution. We need some other more appropriate solution
                        node.Expand();
                        fileTreeView.AfterSelect -= OnFileTreeViewDirSelect;
                        fileTreeView.SelectedNode = node;
                        fileTreeView.AfterSelect += OnFileTreeViewDirSelect;
                        return node;
                    }
                }

                return null;
            }
            else
            {
                // If this is root dir, expand and select it
                fileTreeView.Nodes[0].Expand();
                fileTreeView.AfterSelect -= OnFileTreeViewDirSelect;
                fileTreeView.SelectedNode = fileTreeView.Nodes[0];
                fileTreeView.AfterSelect += OnFileTreeViewDirSelect;
                return fileTreeView.Nodes[0];
            }
        }

        private void OnFileListViewDoubleClick(object sender, EventArgs e)
        {
            if (archiveFile == null)
            {
                // If browsing directory
                if (fileListView.SelectedItems.Count == 1)
                {
                    GameResource resource = (GameResource)fileListView.SelectedItems[0].Tag;

                    if (resource is GameDirectory)
                    {
                        GameDirectory dir = (GameDirectory)resource;
                        ExpandDirectoryNode(dir);
                        SetFileListView(dir.GetContent());
                    }
                    else
                    {
                        FileAssociations.OpenFile((GameFile)resource);
                    }
                }
            }
            else
            {
                // If browsing archive
                GameFile entry = (GameFile)fileListView.SelectedItems[0].Tag;
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = entry.FullPath;
                dialog.Filter = "All Files|*.*";
                dialog.Title = "Extract To...";
                DialogResult result = dialog.ShowDialog();
                
                if (result == DialogResult.OK)
                {
                    entry.Extract(dialog.InitialDirectory + dialog.FileName);
                }
            }
        }

        private void OnFileTreeViewExpand(object sender, TreeViewCancelEventArgs e)
        {
            GameDirectory dir = (GameDirectory)e.Node.Tag;

            UseWaitCursor = true;
            Application.DoEvents();
            fileTreeView.BeginUpdate();

            e.Node.Nodes.Clear();
            e.Node.Nodes.AddRange(GetNodesList(dir.GetDirectories()));

            fileTreeView.EndUpdate();
            UseWaitCursor = false;
            Application.DoEvents();
        }

        private void OnFileTreeViewDirSelect(object sender, TreeViewEventArgs e)
        {
            GameDirectory dir = (GameDirectory)e.Node.Tag;
            archiveFile = null;
            SetFileListView(dir.GetContent());
        }

        private void OnExitMenuItemClick(object sender, EventArgs e)
        {
            AppDefs.ExitFromApp();
        }

        private void OnAboutMenuItemClick(object sender, EventArgs e)
        {
            aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
