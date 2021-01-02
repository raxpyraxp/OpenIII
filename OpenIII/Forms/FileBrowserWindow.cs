/*
 *  This file is a part of OpenIII project, the GTA modding tool.
 *  
 *  Copyright (C) 2019-2020 Savelii Morozov (Prographer)
 *  Email: morozov.salevii@gmail.com
 *  
 *  Copyright (C) 2019-2020 Sergey Filatov (raxp)
 *  Email: raxp.worm202@gmail.com
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using OpenIII.GameFiles;
using OpenIII.Utils;
using OpenIII.Forms;
using OpenIII.GameDefinitions;

namespace OpenIII
{
    /// <summary>
    /// File browser window, the main form of this app
    /// </summary>
    /// <summary xml:lang="ru">
    /// Файловый менеджер, главная форма приложения
    /// </summary>
    public partial class FileBrowserWindow : Form
    {
        /// <summary>
        /// File browser window singleton
        /// </summary>
        /// <summary xml:lang="ru">
        /// Синглтон для формы файлового менеджера
        /// </summary>
        private static FileBrowserWindow instance;

        /// <summary>
        /// Current file archive that the user is working with
        /// If no archive opened, <see cref="archiveFile"/> contains null.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Содержит указатель на текущий открытый архив с которым работает пользователь
        /// Если пользователь работает с файлами, то <see cref="archiveFile"/> содержит null.
        /// </summary>
        private ArchiveFile archiveFile;

        /// <summary>
        /// Current directory that the user is working with
        /// </summary>
        /// <summary xml:lang="ru">
        /// Содержит указатель на текущий каталог в котором работает пользователь
        /// </summary>
        private GameDirectory rootDir;

        /// <summary>
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public FileBrowserWindow()
        {
            InitializeComponent();
            gameToolStripStatusLabel.Text = Game.Instance.Name;
        }

        /// <summary>
        /// Create the instance of this form if no other instances created and return it
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создать инстанс формы если он ещё не создан и вернуть его
        /// </summary>
        /// <returns>
        /// Current form instance
        /// </returns>
        /// <returns xml:lang="ru">
        /// Текущий инстанс формы
        /// </returns>
        public static FileBrowserWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new FileBrowserWindow();
            }

            return instance;
        }

        /// <summary>
        /// Open directory in the file browser window
        /// </summary>
        /// <summary xml:lang="ru">
        /// Открыть каталог в файловом менеджере
        /// </summary>
        /// <param name="rootDir">Directory to be opened</param>
        /// <param name="rootDir" xml:lang="ru">Каталог который необходимо открыть</param>
        public void OpenDir(GameDirectory rootDir)
        {
            this.rootDir = rootDir;
            archiveFile = null;
            SetFileListView(rootDir.GetContent());
            SetDirListView(rootDir);
        }

        /// <summary>
        /// Open archive in the file browser window
        /// </summary>
        /// <summary xml:lang="ru">
        /// Открыть архив в файловом менеджере
        /// </summary>
        /// <param name="archive">Archive to be opened</param>
        /// <param name="archive" xml:lang="ru">Архив который необходимо открыть</param> 
        public void OpenArchive(ArchiveFile archive)
        {
            archiveFile = archive;
            SetFileListView(archiveFile.GetFileList());
            SetTotalFiles(archiveFile.TotalFiles);
            fileTreeView.SelectedNode = null;
        }

        /// <summary>
        /// Sets the new file list on the form
        /// </summary>
        /// <summary xml:lang="ru">
        /// Показать новый список файлов на форме
        /// </summary>
        /// <param name="list">File list</param>
        /// <param name="list" xml:lang="ru">Список файлов</param> 
        public void SetFileListView(List<FileSystemElement> list)
        {
            UseWaitCursor = true;
            Application.DoEvents();
            fileListView.BeginUpdate();

            fileListView.Items.Clear();
            fileListView.SmallImageList = new ImageList();
            fileListView.LargeImageList = new ImageList();

            list.Sort(new FileNameSortComparer());

            foreach (FileSystemElement resource in list)
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

        /// <summary>
        /// Sets the new directory tree on the form
        /// </summary>
        /// <summary xml:lang="ru">
        /// Показать новое дерево каталогов на форме
        /// </summary>
        /// <param name="rootdir">Root directory</param>
        /// <param name="rootdir" xml:lang="ru">Корневой каталог</param> 
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

        /// <summary>
        /// Gets <see cref="TreeNode"/> array from the <see cref="GameDirectory"/> list to apply
        /// it to the child hives of the directory tree
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение массива <see cref="TreeNode"/> из списка <see cref="GameDirectory"/> для
        /// последующего использования в дочерних ветках дерева каталогов
        /// </summary>
        /// <param name="list">Directory list</param>
        /// <param name="list" xml:lang="ru">Список каталогов</param> 
        /// <returns>Tree node list</returns>
        /// <returns xml:lang="ru">Список элементов дерева</returns>
        public TreeNode[] GetNodesList(List<GameDirectory> list)
        {
            list.Sort(new FileNameSortComparer());
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (GameDirectory dir in list)
            {
                nodes.Add(CreateNode(dir));
            }

            return nodes.ToArray();
        }

        /// <summary>
        /// Gets <see cref="TreeNode"/> from the <see cref="GameDirectory"/> object to apply
        /// it to the child hives of the directory tree
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение <see cref="TreeNode"/> из <see cref="GameDirectory"/> для
        /// последующего использования в дочерних ветках дерева каталогов
        /// </summary>
        /// <param name="dir">Directory</param>
        /// <param name="dir" xml:lang="ru">Каталог</param> 
        /// <returns>Tree node</returns>
        /// <returns xml:lang="ru">Элемент дерева</returns>
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

        /// <summary>
        /// Sets total file count in the toolbar
        /// </summary>
        /// <summary xml:lang="ru">
        /// Показать общее количество файлов в тулбаре
        /// </summary>
        /// <param name="totalFiles">Files count</param>
        /// <param name="totalFiles" xml:lang="ru">Количество файлов</param> 
        public void SetTotalFiles(long totalFiles)
        {
            totalFilesLabel.Text = totalFiles.ToString();
        }

        /// <summary>
        /// Expand the <see cref="TreeNode"/> that is attached to the specified <see cref="GameDirectory"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Раскрыть <see cref="TreeNode"/>, которая закреплена за указанным каталогом <see cref="GameDirectory"/>
        /// </summary>
        /// <remarks>
        /// <see cref="ExpandDirectoryNode"/> calls itself to expand parent directory too, so it returns the <see cref="TreeNode"/>
        /// that was expanded. Then it searches for the <see cref="TreeNode"/> attached to the <see cref="GameDirectory"/>
        /// in children of the returned <see cref="TreeNode"/>, expands and selects it.
        /// </remarks>
        /// <remarks>
        /// <see cref="ExpandDirectoryNode"/> вызывает саму себя для того, чтобы также раскрыть родительский каталог. После раскрытия
        /// функция возвращает раскрытую <see cref="TreeNode"/>. После получения раскрытой ветки функция ищет в ней дочернюю ветку
        /// <see cref="TreeNode"/>, которая закреплена за <see cref="GameDirectory"/>, после чего раскрывает и выделяет её.
        /// </remarks>
        /// <param name="dir">Directory</param>
        /// <param name="dir" xml:lang="ru">Каталог</param> 
        /// <returns>Opened tree node</returns>
        /// <returns xml:lang="ru">Раскрытая ветвь дерева</returns>
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

        /// <summary>
        /// File list view dobule click event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события двойного нажатия на файл
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void OnFileListViewDoubleClick(object sender, EventArgs e)
        {
            if (archiveFile == null)
            {
                // If browsing directory
                if (fileListView.SelectedItems.Count == 1)
                {
                    FileSystemElement resource = (FileSystemElement)fileListView.SelectedItems[0].Tag;

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
                if (fileListView.SelectedItems.Count == 1)
                {
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
                else if (fileListView.SelectedItems.Count > 1)
                {
                    List<GameFile> entries = new List<GameFile>();
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        foreach (ListViewItem entry in fileListView.SelectedItems)
                        {
                            entries.Add((GameFile)entry.Tag);
                        }

                        foreach (GameFile entry in entries)
                        {
                            entry.Extract(dialog.SelectedPath + '\\' + entry.Name);
                        }

                        MessageBox.Show("Done!");
                    }

                    return;
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// File tree view expand event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события раскрытия ветки в дереве каталогов
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
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

        /// <summary>
        /// File tree view select event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события выбора каталога в дереве каталогов
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void OnFileTreeViewDirSelect(object sender, TreeViewEventArgs e)
        {
            GameDirectory dir = (GameDirectory)e.Node.Tag;
            archiveFile = null;
            SetFileListView(dir.GetContent());
        }

        /// <summary>
        /// Exit menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Выход"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void OnExitMenuItemClick(object sender, EventArgs e)
        {
            AppDefs.ExitFromApp();
        }

        /// <summary>
        /// About menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "О программе"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void OnAboutMenuItemClick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Set game path menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Выбрать путь к игре"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void SetGamePathMenuItemClick(object sender, EventArgs e)
        {
            SetGamePathWindow window = new SetGamePathWindow();
            window.OnGtaPathSet += OnGtaPathChanged;
            window.ShowDialog();
        }

        /// <summary>
        /// Game path changed event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события выбора нового пути игры
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void OnGtaPathChanged(object sender, PathEventArgs e)
        {
            Properties.Settings.Default.GTAPath = e.Path;
            Properties.Settings.Default.Save();

            Game game = Game.ObtainGameDefinitionFromPath(e.Path);
            Game.Instance = game;

            gameToolStripStatusLabel.Text = game.Name;

            OpenDir(new GameDirectory(e.Path));
        }

        /// <summary>
        /// Refresh file list to catch all changes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обновить окно файлов
        /// </summary>
        private void RefreshFileList()
        {
            if (archiveFile == null)
            {
                OpenDir(rootDir);
            }
            else
            {
                OpenArchive(archiveFile);
            }
        }

        /// <summary>
        /// About menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "О программе"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        /// <summary>
        /// Insert new file to archive menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Добавить файл в архив..."
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All Files|*.*";
            dialog.Title = "Insert File...";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (archiveFile != null)
                {
                    archiveFile.InsertFile(new GameFile(dialog.FileName));
                }
            }

            RefreshFileList();
        }

        /// <summary>
        /// Delete file menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Удалить файл..."
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameFile resource = (GameFile)fileListView.SelectedItems[0].Tag;
            DialogResult dialogResult = MessageBox.Show("Do you really want to delete selected file?", "Some Title", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                resource.Delete();
                RefreshFileList();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "";
            dialog.Filter = "GXT file (*.gxt)|*.gxt|FXT file (*.fxt)|*.fxt";
            dialog.FilterIndex = 2;
            dialog.Title = "Save as...";
            DialogResult result = dialog.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                    GameFile file = new GameFile(dialog.FileName);

                    switch (file.Extension)
                    {
                        case ".fxt":
                            FXTFile fxtFile = new FXTFile(file.FullPath);
                            FXTEditorWindow fxtEditorWindow = new FXTEditorWindow();

                            try
                            {
                                fxtFile.SaveFile();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            
                            fxtEditorWindow.OpenFile(fxtFile);
                            fxtEditorWindow.Show();
                            break;
                        case ".gxt":
                            
                            break;
                        default:
                            MessageBox.Show("Unknown file extention!");
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
