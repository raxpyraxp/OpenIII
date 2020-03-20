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
using System.IO;
using System.Drawing;
using OpenIII.Utils;

namespace OpenIII.GameFiles
{
    public abstract class FileSystemElement
    {
        /// <summary>
        /// Полный путь к файлу
        /// </summary>
        public string FullPath { get; protected set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Расширение файла
        /// </summary>
        public abstract string Extension { get; }

        /// <summary>
        /// Маленькая иконка
        /// </summary>
        public Bitmap SmallIcon { get => GetIcon(IconSize.Small); }

        /// <summary>
        /// Большая иконка
        /// </summary>
        public Bitmap LargeIcon { get => GetIcon(IconSize.Large); }

        public FileSystemElement() { }

        public FileSystemElement(string path)
        {
            FullPath = path;
        }

        /// <summary>
        /// Создаёт экземпляр
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileSystemElement CreateInstance(string path)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return GameDirectory.CreateInstance(path);
            }
            else
            {
                return GameFile.CreateInstance(path);
            }

        }

        /// <summary>
        /// Получить иконку
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Bitmap GetIcon(IconSize size)
        {
            // Uncomment this to use predefined png icons
            //return GetIcon(size);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT
                && Environment.OSVersion.Version.Major > 5)
            {
                // Obtain system icons from WinAPI on Vista+
                return IconsFetcher.GetIcon(FullPath, size);
            }
            else
            {
                // Use predefined icons from app resources on XP/Mono
                return GetPredefinedIcon(size);
            }
        }

        public abstract Bitmap GetPredefinedIcon(IconSize size);
    }
}
