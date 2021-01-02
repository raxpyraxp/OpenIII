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

using OpenIII.GameFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenIII.GameDefinitions
{
    class Game
    {
        /// <summary>
        /// Singleton instance of current game
        /// </summary>
        /// <summary xml:lang="ru">
        /// Инстанс определения текущей игры
        /// </summary>
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game("");
                }

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Private instance of current game
        /// </summary>
        /// <summary xml:lang="ru">
        /// Приватный инстанс текущей игры
        /// </summary>
        private static Game instance;

        /// <summary>
        /// List of game definition types found through reflection
        /// </summary>
        /// <summary xml:lang="ru">
        /// Список найденных через reflection определений игр
        /// </summary>
        private static List<Type> GameDefinitions = Assembly.GetAssembly(typeof(Game)).GetTypes().Where(
            definition => definition.IsClass && !definition.IsAbstract && definition.IsSubclassOf(typeof(Game))).ToList();

        /// <summary>
        /// Specifies if current game instance is a valid game definition
        /// </summary>
        /// <summary xml:lang="ru">
        /// Флаг, указывающий что инстанс игры является корректным
        /// </summary>
        public virtual bool IsDefined { get => false; }

        /// <summary>
        /// Name of the game
        /// </summary>
        /// <summary xml:lang="ru">
        /// Название игры
        /// </summary>
        public virtual string Name { get => "Unknown"; }

        /// <summary>
        /// Path of the game
        /// </summary>
        /// <summary xml:lang="ru">
        /// Путь к игре
        /// </summary>
        public virtual string Path { get; }

        /// <summary>
        /// Supported IMG archive version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Поддерживаемая версия IMG архива
        /// </summary>
        public virtual ArchiveFileVersion ImgVersion { get => ArchiveFileVersion.Unknown; }

        /// <summary>
        /// Supported GXT version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Поддерживаемая версия GXT
        /// </summary>
        public virtual GXTFileVersion GxtVersion { get => GXTFileVersion.Unknown; }

        /// <summary>
        /// Default constructor for game instance
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="path">Game path</param>
        /// <param name="path" xml:lang="ru">Путь к игре</param>
        public Game(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Get correct game instance from directory path
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение корректного инстанса определения игры по пути к ней
        /// </summary>
        /// <param name="path">Game path</param>
        /// <param name="path" xml:lang="ru">Путь к игре</param>
        /// <returns>Instance of a game definition or a base <see cref="Game"/> instance if no supported game is found in path</returns>
        /// <returns xml:lang="ru">Инстанс определения игры либо базовый инстанс <see cref="Game"/> в случае если игра не найдена</returns>
        public static Game ObtainGameDefinitionFromPath(string path)
        {
            foreach (Type definition in GameDefinitions)
            {
                if ((bool) definition.GetMethod("IsGameExistInPath").Invoke(null, new object[] { path }))
                {
                    return (Game)Activator.CreateInstance(definition, new object[] { path });
                }
            }

            return new Game(path);
        }

        /// <summary>
        /// Is this game instance valid for specified directory path
        /// This is a dummy method for a <see cref="Game"/> class. It's always returns false
        /// </summary>
        /// <summary xml:lang="ru">
        /// Указывает, что данный инстанс определения игры является корректным для игры, которая находится по указанному пути
        /// Данный метод для <see cref="Game"/> является заглушкой и всегда возвращает false
        /// </summary>
        /// <param name="path">Game path</param>
        /// <param name="path" xml:lang="ru">Путь к игре</param>
        /// <returns>false for <see cref="Game"/></returns>
        /// <returns xml:lang="ru">false для <see cref="Game"/></returns>
        public static bool IsGameExistInPath(string path)
        {
            return false;
        }
    }
}
