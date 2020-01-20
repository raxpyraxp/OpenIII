using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenIII.GameFiles;

namespace OpenIII.Utils
{
    class FileNameSortComparer : IComparer<FileSystemElement>
    {
        public int Compare(FileSystemElement left, FileSystemElement right)
        {
            int comparison = 0;
            
            // Directory is in priority, so it must be higher than file
            if (left is GameDirectory)
            {
                comparison -= 2;
            }

            if (right is GameDirectory)
            {
                comparison += 2;
            }

            // Then compare file names
            comparison += string.Compare(left.Name, right.Name);

            return comparison;
        }
    }
}
