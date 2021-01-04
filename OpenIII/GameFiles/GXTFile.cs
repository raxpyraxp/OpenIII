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
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenIII.GameFiles.GXT;

namespace OpenIII.GameFiles
{
    public enum GXTFileVersion
    {
        Unknown,
        III,
        VC,
        SA
    }

    /// <summary>
    /// An implementation for viewing or ediding text dictionaries (.GXT)
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы со словарями текстовых строк используемых игрой (.GXT)
    /// </summary>
    public class GXTFile : GameFile
    {
        /// <summary>
        /// Type name of the <see cref="FXTFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Наименование типа элемента файловой системы <see cref="FXTFile"/>
        /// </summary>
        public override string Type { get => "GXT text dictionary"; }

        public GXTFileBlock MainBlock { get; set; }

        public GXTFileVersion FileVersion {
            get {
                if (fileVersion == GXTFileVersion.Unknown)
                {
                    ReadVersionFromFile();
                }

                return fileVersion;
            }
        }

        private GXTFileVersion fileVersion;

        /// <summary>
        /// Default <see cref="GXTFile"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор по умолчанию для <see cref="GXTFile"/>
        /// </summary>
        /// <param name="filePath">A path to the <see cref="GXTFile"/></param>
        /// <param name="filePath" xml:lang="ru">Путь к файлу <see cref="GXTFile"/></param>
        public GXTFile(string filePath) : base(filePath) { }

        /// <summary>
        /// Parses all existing lines from the current <see cref="GXTFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение всех существующих в текущем файле <see cref="GXTFile"/> строк
        /// </summary>
        public void ParseData()
        {
            Stream stream = this.GetStream(FileMode.Open, FileAccess.Read);
            ReadVersionFromStream(stream);

            switch (FileVersion)
            {
                case GXTFileVersion.III:
                    stream.Seek(0, SeekOrigin.Begin);
                    MainBlock = new TKEYBlock(stream, true, FileVersion);
                    break;
                case GXTFileVersion.VC:
                    stream.Seek(0, SeekOrigin.Begin);
                    MainBlock = new TABLBlock(stream, FileVersion);
                    break;
                case GXTFileVersion.SA:
                    MainBlock = new TABLBlock(stream, FileVersion);
                    break;
                default:
                    break;
            }

            stream.Close();
        }

        public void ReadVersionFromFile()
        {
            Stream stream = GetStream(FileMode.Open, FileAccess.Read);
            ReadVersionFromStream(stream);
            stream.Close();
        }

        public void ReadVersionFromStream(Stream stream)
        {
            byte[] buf = new byte[4];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(buf, 0, buf.Length);

            if (Encoding.ASCII.GetString(buf) == "TKEY")
            {
                fileVersion = GXTFileVersion.III;
            }

            if (Encoding.ASCII.GetString(buf) == "TABL")
            {
                fileVersion = GXTFileVersion.VC;
            }

            if (BitConverter.ToInt32(buf, 0) == 0x080004)
            {
                fileVersion = GXTFileVersion.SA;
            }
        }
    }
}
