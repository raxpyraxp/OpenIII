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
using System.IO;
using OpenIII.GameFiles;

namespace OpenIII.Utils
{
    /// <summary>
    /// A special stream for managing files directly from the archive file
    /// </summary>
    /// <summary xml:lang="ru">
    /// Специальный вид потока для работы с файлом напрямую в архиве
    /// </summary>
    class ArchiveStream : FileStream
    {
        /// <summary>
        /// Handle of the archived <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Указатель на архивированный файл <see cref="GameFile"/>
        /// </summary>
        public GameFile File { get; private set; }

        /// <summary>
        /// Length of the archived <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер архивированного файла <see cref="GameFile"/>
        /// </summary>
        public override long Length { get => File != null ? File.Length : 0; }

        /// <summary>
        /// Current position relative to the <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Текущая позиция относительно файла <see cref="GameFile"/>
        /// </summary>
        public override long Position {
            get => base.Position - File.Offset;
            set => base.Position = value + File.Offset;
        }

        /// <summary>
        /// Constructior for the <see cref="ArchiveStream"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для потока <see cref="ArchiveStream"/>
        /// </summary>
        /// <param name="gameFile">Archived file</param>
        /// <param name="gameFile" xml:lang="ru">Архивированный файл</param>
        /// <param name="mode">File access mode</param>
        /// <param name="mode" xml:lang="ru">Метод доступа к файлу</param>
        /// <param name="access">File access permissions</param>
        /// <param name="access" xml:lang="ru">Разрешения доступа к файлу</param>
        public ArchiveStream(GameFile gameFile, FileMode mode, FileAccess access)
            : base(gameFile.ParentArchive.FullPath, mode, access)
        {
            File = gameFile;
            base.Seek(File.Offset, SeekOrigin.Begin);
        }

        /// <summary>
        /// Move current position to the <paramref name="offset"/> relative to the <paramref name="origin"/> of the <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Переместить текущую позицию на смещение <paramref name="offset"/> относительно <paramref name="origin"/> файла <see cref="GameFile"/>
        /// </summary>
        /// <param name="offset">New offset</param>
        /// <param name="offset" xml:lang="ru">Новое смещение</param>
        /// <param name="origin">Relative position where to move from</param>
        /// <param name="origin" xml:lang="ru">Позиция относительно которой необходимо переместить текущую позицию</param>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return base.Seek(offset + File.Offset, origin);
        }
    }
}
