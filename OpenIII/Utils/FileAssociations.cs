using System;
using System.Collections.Generic;
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
            switch (file.Extension)
            {
                case ".asi":
                    MessageBox.Show("This is .asi file", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
                default:
                    MessageBox.Show("This file doesn't support yet.");
                break;
            }
        }
    }
}
