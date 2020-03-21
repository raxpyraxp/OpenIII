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

namespace OpenIII.GameFiles
{
    /// <summary>
    /// An implementation for viewing or ediding text dictionaries (.GXT)
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы со словарями текстовых строк используемых игрой (.GXT)
    /// </summary>
    public class GXTFile : GameFile
    {
        /// <summary>
        /// Size of the TABL block name
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер названия блока TABL
        /// </summary>
        public const int TABL_BLOCK_NAME_SIZE = 4;

        /// <summary>
        /// Size of the TABL block size
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер области в которой находится размер блока TABL
        /// </summary>
        public const int TABL_BLOCK_SIZE_SIZE = 4;


        /// <summary>
        /// Size of the TABL entry name
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер названия элемента в блоке TABL
        /// </summary>
        public const int TABL_ENTRY_NAME_SIZE = 4;

        /// <summary>
        /// Size of the TABL entry offset
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер смещения элемента в блоке TABL
        /// </summary>
        public const int TABL_ENTRY_OFFSET_SIZE = 4;

        /// <summary>
        /// List of the <see cref="GXTFileBlock"/> blocks from the current <see cref="GXTFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Список блоков <see cref="GXTFileBlock"/> из текущего файла <see cref="GXTFile"/>
        /// </summary>
        public List<GXTFileBlock> Blocks = new List<GXTFileBlock>();

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

            byte[] buf;
            string tablBlockName, tkeyBlockName, tdatBlockName;
            int tablBlockSize, tkeyBlockSize, tdatBlockSize;

            // Создаём блок TABL
            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tablBlockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tablBlockSize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock tablBlock = new GXTFileBlock(tablBlockName, tablBlockSize, new List<GXTFileBlockEntry>());

            // Находим элементы блока TABL
            for (int i = 0; i < tablBlockSize; i += (TABL_BLOCK_NAME_SIZE + TABL_BLOCK_SIZE_SIZE + TABL_ENTRY_OFFSET_SIZE))
            {
                tablBlock.Entries.Add(tablBlock.GetEntry(stream));
            }

            Blocks.Add(tablBlock);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tkeyBlockName = Encoding.ASCII.GetString(buf);


            // Создаём блок TKEY
            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tkeyBlockSize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock tkeyBlock = new GXTFileBlock(tkeyBlockName, tkeyBlockName, tkeyBlockSize, new List<GXTFileBlockEntry>());

            stream.Read(buf, 0, buf.Length);

            // Находим элементы блока TKEY
            for (int i = 0; i < tkeyBlockSize; i += (TABL_BLOCK_NAME_SIZE + TABL_BLOCK_SIZE_SIZE + TABL_ENTRY_OFFSET_SIZE))
            {
                tkeyBlock.Entries.Add(tkeyBlock.GetEntry(stream));
            }

            Blocks.Add(tkeyBlock);


            // Создаём блок TDAT
            //buf = new byte[4];
            //stream.Read(buf, 0, buf.Length);
            //tdatBlockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tdatBlockSize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock tdatBlock = new GXTFileBlock("", tdatBlockSize, new List<GXTFileBlockEntry>());

            string tmp = "";

            for (int i = 0; i < tkeyBlock.Entries.Count; i++)
            {
                while (Encoding.ASCII.GetString(buf) != "\0\0")
                {
                    buf = new byte[2];
                    stream.Read(buf, 0, buf.Length);
                    tmp += Encoding.ASCII.GetString(buf);
                }

                tmp = tmp.Replace("\0", "");

                tdatBlock.Entries.Add(new GXTFileBlockEntry(tmp, 0));

                tmp = "";

                buf = new byte[0];
                stream.Read(buf, 0, buf.Length);
            }

            Blocks.Add(tdatBlock);
        }
    }

    /// <summary>
    /// An implementation for managing GXT blocks
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с блоками GXT
    /// </summary>
    public class GXTFileBlock
    {
        // public string EntryName;

        /// <summary>
        /// Size of the block
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер блока
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Name of the block
        /// </summary>
        /// <summary xml:lang="ru">
        /// Название блока
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the block (again?)
        /// TODO: fix
        /// </summary>
        /// <summary xml:lang="ru">
        /// Название блока (опять?)
        /// TODO: Ещё раз пересмотреть необходимость
        /// </summary>
        public string EntryName { get; set; }

        /// <summary>
        /// List of child <see cref="GXTFileBlockEntry"/> entries
        /// </summary>
        /// <summary xml:lang="ru">
        /// Список дочерних элементов <see cref="GXTFileBlockEntry"/>
        /// </summary>
        public List<GXTFileBlockEntry> Entries { get; set; }

        /// <summary>
        /// Constructior for TABL type block initialization
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для создания блока типа TABL
        /// </summary>
        /// <param name="name">Block name</param>
        /// <param name="size">Block size</param>
        /// <param name="entries">List of child <see cref="GXTFileBlockEntry"/> entries</param>
        /// <param name="name" xml:lang="ru">Название блока</param>
        /// <param name="size" xml:lang="ru">Размер блока</param>
        /// <param name="entries" xml:lang="ru">Список дочерних элементов <see cref="GXTFileBlockEntry"/></param>
        public GXTFileBlock(string name, int size, List<GXTFileBlockEntry> entries)
        {
            this.Name = name;
            this.Size = size;
            this.Entries = entries;
        }

        /// <summary>
        /// Constructior for TKEY type block initialization
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для создания блока типа TKEY
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="name">Block name</param>
        /// <param name="size">Block size</param>
        /// <param name="entries">List of child <see cref="GXTFileBlockEntry"/> entries</param>
        /// <param name="entryName" xml:lang="ru"></param>
        /// <param name="name" xml:lang="ru">Название блока</param>
        /// <param name="size" xml:lang="ru">Размер блока</param>
        /// <param name="entries" xml:lang="ru">Список дочерних элементов <see cref="GXTFileBlockEntry"/></param>
        public GXTFileBlock(string entryName, string name, int size, List<GXTFileBlockEntry> entries)
        {
            this.EntryName = entryName;
            this.Name = name;
            this.Size = size;
            this.Entries = entries;
        }

        /// <summary>
        /// Gets the next entry in block
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получает следующий дочерний элемент в блоке
        /// </summary>
        /// <param name="stream">GXT file stream</param>
        /// <param name="stream" xml:lang="ru">Поток GXT файла</param>
        public GXTFileBlockEntry GetEntry(Stream stream)
        {
            string blockName;
            int entryOffset;
            byte[] buf;

            buf = new byte[8];
            stream.Read(buf, 0, buf.Length);
            blockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            entryOffset = BitConverter.ToInt32(buf, 0);

            return new GXTFileBlockEntry(blockName, entryOffset);
        }
    }

    /// <summary>
    /// An implementation for managing GXT entries
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с элементами GXT
    /// </summary>
    public class GXTFileBlockEntry
    {
        /// <summary>
        /// Offset of the entry in block
        /// </summary>
        /// <summary xml:lang="ru">
        /// Смещение элемента в блоке
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Name of the entry
        /// </summary>
        /// <summary xml:lang="ru">
        /// Имя элемента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructior for GXT element
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для создания GXT элемента
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <param name="offset">Entry offset in block</param>
        /// <param name="name" xml:lang="ru">Название элемента</param>
        /// <param name="offset" xml:lang="ru">Смещение элемента в блоке</param>
        public GXTFileBlockEntry(string name, int offset)
        {
            this.Name = name;
            this.Offset = offset;
        }
    }
}
