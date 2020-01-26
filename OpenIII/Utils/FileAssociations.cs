using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenIII.Forms;
using OpenIII.GameFiles;

namespace OpenIII.Utils
{
    class FileAssociations
    {
        public static void OpenFile(GameFile file)
        {
            // TODO: создать объект с расширениями файлов
            switch (file.Extension.ToLower())
            {
                case ".gxt":
                    GXTEditorWindow gxtEditorWindow = new GXTEditorWindow();
                    gxtEditorWindow.Show();
                    break;
                case ".fxt":
                    FXTEditorWindow fxtEditorWindow = new FXTEditorWindow();
                    fxtEditorWindow.OpenFile(new FXTFile(file.FullPath));
                    fxtEditorWindow.Show();
                    break;
                case ".ide":
                case ".ipl":
                case ".dat":
                case ".txt":
                case ".log":
                case ".cfg":
                case ".ini":
                case ".zon":
                    DataEditorWindow dataEditorWindow = new DataEditorWindow();
                    dataEditorWindow.OpenFile(new TextFile(file.FullPath));
                    dataEditorWindow.Show();
                    break;
                case ".img":
                    FileBrowserWindow.GetInstance().OpenArchive((ArchiveFile)file);
                    break;
                default:
                    MessageBox.Show("Unsupported file type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
