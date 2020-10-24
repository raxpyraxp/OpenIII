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
using System.ComponentModel;
using System.IO;

namespace OpenIII.GameFiles
{
    /// <summary>
    /// An implementation for viewing or ediding CLEO text dictionaries (.FXT)
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы со словарями текстовых строк используемых CLEO-скриптами (.FXT)
    /// </summary>
    public class FXTFile : GameFile
    {
        /// <summary>
        /// Standard header for each FXT file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Стандартная строка заголовка с которой начинается каждый FXT файл
        /// </summary>
        public static string Headers = "# File created or edited by OpenIII.";

        /// <summary>
        /// List of the <see cref="FXTFileItem"/> text lines from the current <see cref="FXTFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Список текстовых строк <see cref="FXTFileItem"/> из текущего файла <see cref="FXTFile"/>
        /// </summary>
        public BindingList<FXTFileItem> Items = new BindingList<FXTFileItem>();

        /// <summary>
        /// Default <see cref="FXTFile"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор по умолчанию для <see cref="FXTFile"/>
        /// </summary>
        /// <param name="filePath">A path to the <see cref="FXTFile"/></param>
        /// <param name="filePath" xml:lang="ru">Путь к файлу <see cref="FXTFile"/></param>
        public FXTFile(string filePath) : base(filePath) { }

        /// <summary>
        /// Parses all existing lines from the current <see cref="FXTFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение всех существующих в текущем файле <see cref="FXTFile"/> строк
        /// </summary>
        /// <returns>
        /// List of all existing <see cref="FXTFileItem"/> lines
        /// </returns>
        /// <returns xml:lang="ru">
        /// Список всех существующих строк <see cref="FXTFileItem"/>
        /// </returns>
        public BindingList<FXTFileItem> ParseData()
        {
            string lineIterator = null;
            StreamReader Reader = new StreamReader(this.FullPath);
            BindingList<FXTFileItem> data = new BindingList<FXTFileItem>();

            while (!Reader.EndOfStream)
            {
                Reader.ReadLine();

                if (lineIterator != "" && Char.IsLetterOrDigit(lineIterator[0]))
                {
                    data.Add(new FXTFileItem(
                        lineIterator.Substring(0, lineIterator.IndexOf(" ")), lineIterator.Substring(lineIterator.IndexOf(" ") + 1))
                    );
                }
            }

            this.Items = data;
            Reader.Close();

            return data;
        }

        public void SaveFile()
        {
            string data = this.ToString();

            try
            {
                StreamWriter streamWriter = new StreamWriter(this.GetStream(FileMode.Create, FileAccess.Write));
                streamWriter.WriteLine(FXTFile.Headers);
                streamWriter.Write(data);
                streamWriter.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            this.isFileEdited = false;
        }

        /// <summary>
        /// Gets all parsed lines from the current <see cref="FXTFile"/> in <see cref="String"/> format
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение всех прочитанных из файла <see cref="FXTFile"/> строк в текстовом формате <see cref="String"/>
        /// </summary>
        /// <returns>
        /// All key-value lines in <see cref="String"/>
        /// </returns>
        /// <returns xml:lang="ru">
        /// Список всех существующих строк в формате имя-значение строкой
        /// </returns>
        public override string ToString()
        {
            string buf = null;

            foreach (FXTFileItem item in this.Items)
            {
                buf += item.Key + " " + item.Value + '\n';
            }

            return buf;
        }

        /// <summary>
        /// Creates new <see cref="FXTFileItem"/> and adds it to the list
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создание нового элемента <see cref="FXTFileItem"/> и добавление его в спиок строк
        /// </summary>
        /// <param name="key">A key for the new line. Allowed characters are capitalized latin letters, numbers and underscore</param>
        /// <param name="value">A value for the new line</param>
        /// <param name="key" xml:lang="ru">Ключ для новой строки. Допускаются заглавные латинские символы, цифры и нижнее подчёркивание</param>
        /// <param name="value" xml:lang="ru">Значение для новой строки</param>
        public void AddItem(string key, string value)
        {
            this.Items.Add(new FXTFileItem(key, value));
        }
    }

    /// <summary>
    /// An implementation for working with the CLEO text line
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с текстовой строкой для скриптов CLEO
    /// </summary>
    public class FXTFileItem
    {
        /// <summary>
        /// Maximum allowed length for the key
        /// </summary>
        /// <summary xml:lang="ru">
        /// Максимально допустимая длина ключа
        /// </summary>
        public static int MAX_KEY_LENGTH = 8;

        /// <summary>
        /// A key that is used in the script to request line output in the game
        /// </summary>
        /// <summary xml:lang="ru">
        /// Ключ, который используется скриптом для запроса вывода строки в игре
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// A value that is shown in the game
        /// </summary>
        /// <summary xml:lang="ru">
        /// Значение, которое будет видеть игрок
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Default <see cref="FXTFileItem"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="FXTFileItem"/> по умолчанию
        /// </summary>
        /// <param name="key">A key for the new line. Allowed characters are capitalized latin letters, numbers and underscore</param>
        /// <param name="value">A value for the new line</param>
        /// <param name="key" xml:lang="ru">Ключ для новой строки. Допускаются заглавные латинские символы, цифры и нижнее подчёркивание</param>
        /// <param name="value" xml:lang="ru">Значение для новой строки</param>
        public FXTFileItem(string key, string value) {
            this.Key = key;
            this.Value = value;
        }
    }
}
