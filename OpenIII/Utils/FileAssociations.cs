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
                    gxtEditorWindow.OpenFile(new GXTFile(file.FullPath));
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
