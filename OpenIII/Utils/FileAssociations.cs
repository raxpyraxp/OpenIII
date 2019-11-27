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
            MessageBox.Show("Unsupported file format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
