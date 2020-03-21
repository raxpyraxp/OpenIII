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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenIII.GameFiles
{
    /// <summary>
    /// Enum with names of the section from the text file
    /// </summary>
    /// <summary xml:lang="ru">
    /// Перечисление с названиями секций в текстовом файле
    /// </summary>
    public enum SectionName
    {
        /// <summary>
        /// Static objects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция статических объектов
        /// </summary>
        objs,

        /// <summary>
        /// Time-dependent objects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция объектов, зависящих от игрового времени
        /// </summary>
        tobj,

        /// <summary>
        /// Transport paths section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция путей транспорта
        /// </summary>
        path,

        /// <summary>
        /// 2D effects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция двумерных эффектов
        /// </summary>
        twoDfx,

        /// <summary>
        /// Vehicle color section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция цветов транспорта
        /// </summary>
        col,

        /// <summary>
        /// Static objects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция статических объектов
        /// </summary>
        inst,

        /// <summary>
        /// Pickups section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция пикапов
        /// </summary>
        pick,

        /// <summary>
        /// Cull zones section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция специальных зон
        /// </summary>
        cull,

        /// <summary>
        /// Animated objects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция анимированных объектов
        /// </summary>
        anim,

        /// <summary>
        /// Weapon objects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция объектов оружия
        /// </summary>
        weap,

        /// <summary>
        /// Peds section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция педов
        /// </summary>
        peds,

        /// <summary>
        /// Vehicles section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция транспортных средств
        /// </summary>
        cars,

        /// <summary>
        /// Cutscene objects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция объектов для катсцен
        /// </summary>
        heir,

        /// <summary>
        /// Textures section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция текстур
        /// </summary>
        txdp,

        /// <summary>
        /// Sounds section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция звуков
        /// </summary>
        auzo,

        /// <summary>
        /// Time cycle section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция зон для модификации параметров погоды
        /// </summary>
        tcyc,

        /// <summary>
        /// Stunt jumps section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция уникальных прыжков
        /// </summary>
        jump,

        /// <summary>
        /// Unused section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция НЕ ИСПОЛЬЗУЕТСЯ
        /// </summary>
        enex,

        /// <summary>
        /// Garages section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция гаражей
        /// </summary>
        grge,

        /// <summary>
        /// Unused section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция НЕ ИСПОЛЬЗУЕТСЯ
        /// </summary>
        mult,

        /// <summary>
        /// Occlusion objects section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция объектов окклюзии
        /// </summary>
        occl,

        /// <summary>
        /// Zone section
        /// </summary>
        /// <summary xml:lang="ru">
        /// Секция зон
        /// </summary>
        zone
    }

    /// <summary>
    /// Text file section
    /// </summary>
    /// <summary xml:lang="ru">
    /// Секция текстового файла
    /// </summary>
    public class TextFileSection
    {
        /// <summary>
        /// Section name
        /// </summary>
        /// <summary xml:lang="ru">
        /// Название секции
        /// </summary>
        private static string Name;

        /// <summary>
        /// List of <see cref="TextFileSectionItem"/> from the text file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Список элементов <see cref="TextFileSectionItem"/> секции текстового файла
        /// </summary>
        private static List<TextFileSectionItem> Items;

        /// <summary>
        /// Constructior for the <see cref="TextFileSection"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для <see cref="TextFileSection"/>
        /// </summary>
        /// <param name="sectionName">Name of the section</param>
        /// <param name="sectionName" xml:lang="ru">Название секции</param>
        public TextFileSection(string sectionName)
        {
            Name = sectionName;
        }

        /// <summary>
        /// Adds new <paramref name="parameter"/> to the current <see cref="TextFileSection"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Добавляет новый параметр <paramref name="parameter"/> в текущую секцию <see cref="TextFileSection"/>
        /// </summary>
        /// <param name="parameter">File section element to be added</param>
        /// <param name="parameter" xml:lang="ru">Элемент секции который необходимо добавить</param>
        public static void AddItem(TextFileSectionItem parameter) { }

        /// <summary>
        /// Deletes <paramref name="parameter"/> from the current <see cref="TextFileSection"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет параметр <paramref name="parameter"/> из текущей секции <see cref="TextFileSection"/>
        /// </summary>
        /// <param name="parameter">File section element to be deleted</param>
        /// <param name="parameter" xml:lang="ru">Элемент секции который необходимо удалить</param>
        public static void RemoveItem(TextFileSectionItem parameter) { }
    }

    /// <summary>
    /// Element of the text file section
    /// </summary>
    /// <summary xml:lang="ru">
    /// Элемент секции текстового файла
    /// </summary>
    public class TextFileSectionItem
    {
        /// <summary>
        /// Constructior for the <see cref="TextFileSectionItem"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для <see cref="TextFileSectionItem"/>
        /// </summary>
        /// <param name="param1">First value in the line</param>
        /// <param name="param1" xml:lang="ru">Первое значение в строке</param>
        /// <param name="param2">Second value in the line</param>
        /// <param name="param2" xml:lang="ru">Второе значение в строке</param>
        /// <param name="param3">Third value in the line</param>
        /// <param name="param3" xml:lang="ru">Третье значение в строке</param>
        public TextFileSectionItem(string param1, string param2, string param3) { }

        /// <summary>
        /// Constructior for the <see cref="TextFileSectionItem"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для <see cref="TextFileSectionItem"/>
        /// </summary>
        /// <param name="param1">First value in the line</param>
        /// <param name="param1" xml:lang="ru">Первое значение в строке</param>
        /// <param name="param2">Second value in the line</param>
        /// <param name="param2" xml:lang="ru">Второе значение в строке</param>
        /// <param name="param3">Third value in the line</param>
        /// <param name="param3" xml:lang="ru">Третье значение в строке</param>
        /// <param name="param4">Fourth value in the line</param>
        /// <param name="param4" xml:lang="ru">Четвёртое значение в строке</param>
        public TextFileSectionItem(string param1, string param2, string param3, string param4) { }

        /// <summary>
        /// Constructior for the <see cref="TextFileSectionItem"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для <see cref="TextFileSectionItem"/>
        /// </summary>
        /// <param name="param1">First value in the line</param>
        /// <param name="param1" xml:lang="ru">Первое значение в строке</param>
        /// <param name="param2">Second value in the line</param>
        /// <param name="param2" xml:lang="ru">Второе значение в строке</param>
        /// <param name="param3">Third value in the line</param>
        /// <param name="param3" xml:lang="ru">Третье значение в строке</param>
        /// <param name="param4">Fourth value in the line</param>
        /// <param name="param4" xml:lang="ru">Четвёртое значение в строке</param>
        /// <param name="param5">Fifth value in the line</param>
        /// <param name="param5" xml:lang="ru">Пятое значение в строке</param>
        public TextFileSectionItem(string param1, string param2, string param3, string param4, string param5) { }
    }

    /// <summary>
    /// An implementation for viewing or ediding text or configuration files in the game
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с текстовыми конфигурационными файлами игры
    /// </summary>
    public class TextFile : GameFile
    {
        /// <summary>
        /// A list of <see cref="TextFileSection"/> sections in the file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Список секций <see cref="TextFileSection"/> текстового файла
        /// </summary>
        private static List<TextFileSection> Sections;

        /// <summary>
        /// <see cref="TextFile"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор текстового файла <see cref="TextFile"/>
        /// </summary>
        /// <param name="filePath"><see cref="TextFile"/> path</param>
        /// <param name="filePath" xml:lang="ru">Путь к файлу <see cref="TextFile"/></param>
        public TextFile(string filePath) : base(filePath) { }

        /// <summary>
        /// Creates the handle <see cref="TextFile"/> to the file under <paramref name="path"/> in the file system
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создаёт указатель на файл <see cref="TextFile"/> по пути <paramref name="path"/>
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="path" xml:lang="ru">Путь к файлу</param>
        /// <returns>
        /// <see cref="TextFile"/> handle
        /// </returns>
        /// <returns xml:lang="ru">
        /// Указатель на файл <see cref="TextFile"/>
        /// </returns>
        public static new TextFile CreateInstance(string path)
        {
            return new TextFile(path);
        }

        /// <summary>
        /// Reads the file content under <paramref name="filePath"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Читает содержимое файла по пути <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="filePath" xml:lang="ru">Путь к файлу</param>
        /// <returns>
        /// File contents in <see cref="String"/>
        /// </returns>
        /// <returns xml:lang="ru">
        /// Содержимое файла в <see cref="String"/>
        /// </returns>
        public static string GetContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Parses all configuration sections from file under <paramref name="filePath"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Читает все конфигурационные секции из файла по пути <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="filePath" xml:lang="ru">Путь к файлу</param>
        /// <returns>
        /// File contents in array of <see cref="String"/>
        /// </returns>
        /// <returns xml:lang="ru">
        /// Содержимое файла в массиве строк <see cref="String"/>
        /// </returns>
        public List<String[]> ParseData(string path)
        {
            string iteratableLine;
            List<string> listOfRows = new List<string>();
            List<string[]> listOfArrayParams = new List<string[]>();
            StreamReader Reader = null;

            if (File.Exists(path))
            {
                Reader = new StreamReader(path);
            }
            else
            {
                throw new Exception("File does not exist!");
            }

            // определяем тип обрабатываемого файла
            while ((iteratableLine = Reader.ReadLine()) != null)
            {
                String[] namesOfSectionItem = { "ENDWEAPONDATA", "end", "objs", "tobj", "path", "2dfx", "col", "inst", "pick", "cull", "anim", "weap", "path", "peds", "cars", "hier" };
                
                if (iteratableLine != "" && Char.IsLetterOrDigit(iteratableLine[0]) && Array.IndexOf(namesOfSectionItem, iteratableLine) < 0)
                {
                    iteratableLine = Regex.Replace(iteratableLine, @",", " ");
                    listOfRows.Add(Regex.Replace(iteratableLine, @"\s+", ","));
                }
            }

            listOfRows.ForEach(delegate (String row)
            {
                listOfArrayParams.Add(row.Split(','));
            });

            return listOfArrayParams;
        }
    }
}