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
using System.Linq;
using System.Text;
using OpenIII.GameFiles;

namespace OpenIII.Utils
{
    /// <summary>
    /// A class that defines comparator for files and directories sorting
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс, определяющий компаратор для сортировки файлов и директорий
    /// </summary>
    class FileNameSortComparer : IComparer<FileSystemElement>
    {
        /// <summary>
        /// Compare two <see cref="FileSystemElement"/> file system elements
        /// </summary>
        /// <summary xml:lang="ru">
        /// Сравнить два элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        /// <param name="left">Left file system element</param>
        /// <param name="left" xml:lang="ru">Левый элемент файловой системы</param>
        /// <param name="right">Right file system element</param>
        /// <param name="right" xml:lang="ru">Правый элемент файловой системы</param>
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
