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
    /// <summary>
    /// A basic class for all file system elements including files and directories
    /// </summary>
    /// <summary xml:lang="ru">
    /// Базовый класс элемента файловой системы, который является базой для файлов и каталогов
    /// </summary>
    public abstract class FileSystemElement
    {
        /// <summary>
        /// Full absolute path to the <see cref="FileSystemElement"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Полный абсолютный путь к элементу файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        public string FullPath { get; protected set; }

        /// <summary>
        /// Name of the <see cref="FileSystemElement"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Имя элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Extension of the <see cref="FileSystemElement"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Расширение элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        public abstract string Extension { get; }

        /// <summary>
        /// Type name of the <see cref="FileSystemElement"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Наименование типа элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Small icon for the type of the <see cref="FileSystemElement"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Маленькая иконка типа элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        public Bitmap SmallIcon { get => GetIcon(IconSize.Small); }

        /// <summary>
        /// Large icon for the type of the <see cref="FileSystemElement"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Большая иконка типа элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        public Bitmap LargeIcon { get => GetIcon(IconSize.Large); }

        /// <summary>
        /// Default <see cref="FileSystemElement"/> constructor for the new element
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="FileSystemElement"/> для создания нового элемента файловой системы
        /// </summary>
        public FileSystemElement() { }

        /// <summary>
        /// Default <see cref="FileSystemElement"/> constructor for the existing element
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="FileSystemElement"/> для создания существующего элемента файловой системы
        /// </summary>
        /// <param name="path"><see cref="FileSystemElement"/> path</param>
        /// <param name="path" xml:lang="ru">Путь к <see cref="FileSystemElement"/></param>
        public FileSystemElement(string path)
        {
            FullPath = path;
        }

        /// <summary>
        /// Creates new <see cref="FileSystemElement"/> of it's type
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создание указателя на <see cref="FileSystemElement"/> того типа, к которому он отностся
        /// </summary>
        /// <param name="path"><see cref="FileSystemElement"/> path</param>
        /// <param name="path" xml:lang="ru">Путь к <see cref="FileSystemElement"/></param>
        /// <returns>
        /// Handle for the <see cref="FileSystemElement"/>
        /// </returns>
        /// <returns xml:lang="ru">
        /// Указатель на элемент файловой системы <see cref="FileSystemElement"/>
        /// </returns>
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
        /// Fetches the icon of the <see cref="FileSystemElement"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение иконки элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        /// <param name="size">Icon size</param>
        /// <param name="size" xml:lang="ru">Размер иконки</param>
        /// <returns>
        /// <see cref="Bitmap"/> icon
        /// </returns>
        /// <returns xml:lang="ru">
        /// Иконка <see cref="Bitmap"/>
        /// </returns>
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

        /// <summary>
        /// Fetches the icon of the <see cref="FileSystemElement"/> that is predefined for the file type in it's class
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение предопределённой в классе иконки элемента файловой системы <see cref="FileSystemElement"/>
        /// </summary>
        /// <param name="size">Icon size</param>
        /// <param name="size" xml:lang="ru">Размер иконки</param>
        /// <returns>
        /// <see cref="Bitmap"/> icon
        /// </returns>
        /// <returns xml:lang="ru">
        /// Иконка <see cref="Bitmap"/>
        /// </returns>
        public abstract Bitmap GetPredefinedIcon(IconSize size);
    }
}
