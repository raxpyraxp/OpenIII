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

using System.Collections.Generic;
using System.IO;
using System.Drawing;
using OpenIII.Utils;

namespace OpenIII.GameFiles
{
    /// <summary>
    /// An implementation for working with game directories
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с каталогами игры
    /// </summary>
    public class GameDirectory : FileSystemElement
    {
        /// <summary>
        /// A <see cref="DirectoryInfo"/> element associated with current directory
        /// </summary>
        /// <summary xml:lang="ru">
        /// Информация о текущей директории <see cref="DirectoryInfo"/>
        /// </summary>
        private DirectoryInfo DirectoryInfo;

        /// <summary>
        /// The name of the <see cref="GameDirectory"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Имя директории <see cref="GameDirectory"/>
        /// </summary>
        public override string Name { get => DirectoryInfo.Name; }

        /// <summary>
        /// The extension of the <see cref="GameDirectory"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Расширение директории <see cref="GameDirectory"/>
        /// </summary>
        public override string Extension { get => DirectoryInfo.Extension; }

        /// <summary>
        /// Default <see cref="GameDirectory"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="GameDirectory"/> по умолчанию
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="path" xml:lang="ru">Путь к директории</param>
        public GameDirectory(string path) : base(path)
        {
            this.DirectoryInfo = new DirectoryInfo(FullPath);
        }

        /// <summary>
        /// Creates the handle <see cref="GameDirectory"/> to the direcory under <paramref name="path"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создание указателя на директорию <see cref="GameDirectory"/> по пути <paramref name="path"/>
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="path" xml:lang="ru">Путь к директории</param>
        /// <returns>
        /// <see cref="GameDirectory"/> handle
        /// </returns>
        /// <returns xml:lang="ru">
        /// Указатель на директорию <see cref="GameDirectory"/>
        /// </returns>
        public static new GameDirectory CreateInstance(string path)
        {
            return new GameDirectory(path);
        }

        /// <summary>
        /// Fetches the icon of the <see cref="GameDirectory"/> that is predefined for the file type in it's class
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение предопределённой в классе иконки директории <see cref="GameDirectory"/>
        /// </summary>
        /// <param name="size">Icon size</param>
        /// <param name="size" xml:lang="ru">Размер иконки</param>
        /// <returns>
        /// <see cref="Bitmap"/> icon
        /// </returns>
        /// <returns xml:lang="ru">
        /// Иконка <see cref="Bitmap"/>
        /// </returns>
        public override Bitmap GetPredefinedIcon(IconSize size)
        {
            return Properties.Resources.Folder;
        }

        /// <summary>
        /// Fetches all file handles for all child <see cref="GameFile"/> files in the current <see cref="GameDirectory"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение указателей на все дочерние файлы <see cref="GameFile"/> находящиеся в текущей директории <see cref="GameDirectory"/>
        /// </summary>
        /// <returns>
        /// List of <see cref="GameFile"/> handles
        /// </returns>
        /// <returns xml:lang="ru">
        /// Список указателей <see cref="GameFile"/>
        /// </returns>
        public List<GameFile> GetFiles()
        {
            List<GameFile> gameFiles = new List<GameFile>();

            foreach (FileInfo file in DirectoryInfo.GetFiles())
            {
                gameFiles.Add(GameFile.CreateInstance(Path.Combine(file.DirectoryName, file.Name)));
            }

            return gameFiles;
        }

        /// <summary>
        /// Fetches all directory handles for all child <see cref="GameDirectory"/> directories in the current <see cref="GameDirectory"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение указателей на все дочерние директории <see cref="GameDirectory"/> находящиеся в текущей директории <see cref="GameDirectory"/>
        /// </summary>
        /// <returns>
        /// List of <see cref="GameDirectory"/> handles
        /// </returns>
        /// <returns xml:lang="ru">
        /// Список указателей <see cref="GameDirectory"/>
        /// </returns>
        public List<GameDirectory> GetDirectories()
        {
            List<GameDirectory> gameDirectories = new List<GameDirectory>();

            foreach (DirectoryInfo dir in DirectoryInfo.GetDirectories())
            {
                gameDirectories.Add(GameDirectory.CreateInstance(dir.FullName));
            }

            return gameDirectories;
        }

        /// <summary>
        /// Fetches all handles for all child <see cref="FileSystemElement"/> elements in the current <see cref="GameDirectory"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение указателей на все дочерние элементы <see cref="FileSystemElement"/> находящиеся в текущей директории <see cref="GameDirectory"/>
        /// </summary>
        /// <returns>
        /// List of <see cref="FileSystemElement"/> handles
        /// </returns>
        /// <returns xml:lang="ru">
        /// Список указателей <see cref="FileSystemElement"/>
        /// </returns>
        public List<FileSystemElement> GetContent()
        {
            List<FileSystemElement> resources = new List<FileSystemElement>();
            resources.AddRange(GetDirectories());
            resources.AddRange(GetFiles());

            return resources;
        }
    }
}
