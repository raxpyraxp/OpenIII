using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                case ".fxt":
                    new GXTEditorWindow().Show();
                    break;
                case ".ide":
                case ".ipl":
                case ".dat":
                case ".txt":
                case ".log":
                case ".cfg":
                case ".ini":
                case ".zon":
                    //TextEditorWindow.GetInstance().SetTextArea(TextFile.GetContent(file.FullPath));
                    //TextEditorWindow.GetInstance().ShowDialog();
                    var window = new DataEditorWindow();
                    var textFile = new TextFile(file.FullPath);
                    var result = textFile.ParseData(file.FullPath);
                    
                    for (int i = 0; i < result.Count; i++)
                    {
                        var arr = result[i].ToArray();
                        window.songsDataGridView.Rows.Add(arr);
                    }

                    window.Show();

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
