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

namespace OpenIII
{
    /// <summary>
    /// Class that defines event arguments that are used in events when new game path selected
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс, определяющий аргументы события, которые используются при выборе нового пути к игре
    /// </summary>
    public class PathEventArgs : EventArgs
    {
        /// <summary>
        /// Selected game path
        /// </summary>
        /// <summary xml:lang="ru">
        /// Выбранный путь к игре
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Default <see cref="PathEventArgs"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="PathEventArgs"/> по умолчанию
        /// </summary>
        /// <param name="path">Path of the game</param>
        /// <param name="path" xml:lang="ru">Путь к игре</param>
        public PathEventArgs(string path)
        {
            Path = path;
        }
    }
}
