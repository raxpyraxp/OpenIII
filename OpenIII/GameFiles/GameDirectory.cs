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
    public class GameDirectory : FileSystemElement
    {
        /// <summary>
        /// Информация о директории
        /// </summary>
        private DirectoryInfo DirectoryInfo;

        /// <summary>
        /// Имя директории
        /// </summary>
        public override string Name { get => DirectoryInfo.Name; }

        /// <summary>
        /// Расширение директории
        /// </summary>
        public override string Extension { get => DirectoryInfo.Extension; }

        /// <summary>
        /// Создаёт новую директорию
        /// </summary>
        /// <param name="path"></param>
        public GameDirectory(string path) : base(path)
        {
            this.DirectoryInfo = new DirectoryInfo(FullPath);
        }

        /// <summary>
        /// Создаёт экземпляр
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static new GameDirectory CreateInstance(string path)
        {
            return new GameDirectory(path);
        }

        public override Bitmap GetPredefinedIcon(IconSize size)
        {
            return Properties.Resources.Folder;
        }

        /// <summary>
        /// Возвращает все файлы из директории
        /// </summary>
        /// <returns></returns>
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
        /// Возвращает все директории из директории
        /// </summary>
        /// <returns></returns>
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
        /// Возвращает все элементы, находящиеся в директории
        /// </summary>
        /// <returns></returns>
        public List<FileSystemElement> GetContent()
        {
            List<FileSystemElement> resources = new List<FileSystemElement>();
            resources.AddRange(GetDirectories());
            resources.AddRange(GetFiles());

            return resources;
        }
    }
}
