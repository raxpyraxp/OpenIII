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
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    /// <summary>
    /// IMG/CDIMAGE version 1 archive files for RenderWare-based GTA games implementation that is used in GTA III and GTA: Vice City
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с IMG/CDIMAGE архивами версии 1 для игр GTA на движке RenderWare которые используются в GTA III и GTA: Vice City
    /// </summary>
    class ArchiveFileV1 : ArchiveFile
    {
        /// <summary>
        /// Table of contents file extension
        /// </summary>
        /// <summary xml:lang="ru">
        /// Расширение файла оглавления архива
        /// </summary>
        public const string DirSuffix = "dir";

        /// <summary>
        /// Size of the <see cref="GameFile"/> offset section in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции смещения файла <see cref="GameFile"/> в байтах
        /// </summary>
        public const int OFFSET_ENTRY_BYTE_SIZE = 4;

        /// <summary>
        /// Size of the <see cref="GameFile"/> size section in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции размера файла <see cref="GameFile"/> в байтах
        /// </summary>
        public const int SIZE_ENTRY_BYTE_SIZE = 4;

        /// <summary>
        /// Size of the <see cref="GameFile"/> name section in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции имени файла <see cref="GameFile"/> в байтах
        /// </summary>
        public const int FILENAME_ENTRY_BYTE_SIZE = 24;

        /// <summary>
        /// Size of the whole <see cref="GameFile"/> entry in the file allocation table in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Суммарный размер секции файла <see cref="GameFile"/> в таблице размещения файлов в байтах
        /// </summary>
        public const int DIR_ENTRY_SIZE = OFFSET_ENTRY_BYTE_SIZE +
                                          FILENAME_ENTRY_BYTE_SIZE +
                                          SIZE_ENTRY_BYTE_SIZE;

        /// <summary>
        /// Default begining of data section offset
        /// In the V1 it defaults to 0 because whole file is used for data only
        /// </summary>
        /// <summary xml:lang="ru">
        /// Смещение начала блока данных по умолчанию
        /// В архиве первой версии начало блока данных эквивалентно началу файла так как весь файл используется для хранения данных
        /// </summary>
        public override int FILE_SECTION_START { get => 0; }

        /// <summary>
        /// <see cref="ArchiveFile"/> version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Версия архива <see cref="ArchiveFile"/>
        /// </summary>
        public override ArchiveFileVersion ImgVersion { get => ArchiveFileVersion.V1; }

        /// <summary>
        /// Total files count in the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Количество файлов в архиве <see cref="ArchiveFile"/>
        /// </summary>
        public override long TotalFiles { get => DirFile.Length / DIR_ENTRY_SIZE; }

        /// <summary>
        /// Table of contents .dir file handle
        /// </summary>
        /// <summary xml:lang="ru">
        /// Указатель на .dir файл, содержащий таблицу размещения файлов
        /// </summary>
        public GameFile DirFile { get => new GameFile(GetDirFilePath(FullPath)); }

        /// <summary>
        /// <see cref="ArchiveFile"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="filePath"><see cref="ArchiveFile"/> path</param>
        /// <param name="filePath" xml:lang="ru">Путь к архиву <see cref="ArchiveFile"/></param>
        public ArchiveFileV1(string filePath) : base(filePath)
        {

        }

        /// <summary>
        /// Gets .dir file path based on <see cref="ArchiveFile"/> path
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение пути к .dir-файлу по пути архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="path"><see cref="ArchiveFile"/> path</param>
        /// <param name="path" xml:lang="ru">Путь к архиву <see cref="ArchiveFile"/></param>
        /// <returns>
        /// .dir file path
        /// </returns>
        /// <returns xml:lang="ru">
        /// Путь к .dir файлу
        /// </returns>
        public static string GetDirFilePath(string path)
        {
            // Just replace extension to dir in the original file path
            return path.Remove(path.Length - DirSuffix.Length) + DirSuffix;
        }

        /// <summary>
        /// Gets file handles list to access all archived <see cref="GameFile"/> files
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение списка указателей на все архивированные файлы <see cref="GameFile"/>
        /// </summary>
        /// <returns>
        /// List of handles to all archived <see cref="GameFile"/> files
        /// </returns>
        /// <returns xml:lang="ru">
        /// Список указателей на все архивированные файлы <see cref="GameFile"/>
        /// </returns>
        public override List<FileSystemElement> GetFileList()
        {
            long filesCount = TotalFiles;

            Stream stream = DirFile.GetStream(FileMode.Open, FileAccess.Read);
            List<FileSystemElement> fileList = new List<FileSystemElement>();
            
            int size, offset, read = 1;
            
            byte[] buf;
            
            string filename;

            while (read > 0 && filesCount > fileList.Count)
            {
                buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                offset = BitConverter.ToInt32(buf, 0) * SECTOR_SIZE;

                buf = new byte[SIZE_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                size = BitConverter.ToInt32(buf, 0) * SECTOR_SIZE;

                buf = new byte[FILENAME_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                filename = Encoding.ASCII.GetString(buf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));

                fileList.Add(new GameFile(offset, size, filename, this));
            }

            stream.Close();

            return fileList;
        }

        /// <summary>
        /// Creates new <see cref="GameFile"/> entry
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создаёт запись о файле <see cref="GameFile"/>
        /// </summary>
        /// <param name="offset">Offset for the new <see cref="GameFile"/></param>
        /// <param name="offset" xml:lang="ru">Смещение для нового <see cref="GameFile"/></param>
        /// <param name="length">Size of the <see cref="GameFile"/></param>
        /// <param name="length" xml:lang="ru">Размер файла <see cref="GameFile"/></param>
        /// <param name="filename">Name of the <see cref="GameFile"/></param>
        /// <param name="filename" xml:lang="ru">Имя файла <see cref="GameFile"/></param>
        /// <returns>
        /// Entry in bytes block
        /// </returns>
        /// <returns xml:lang="ru">
        /// Блок данных с записью о файле
        /// </returns>
        public byte[] CreateNewEntry(int offset, long length, string filename)
        {
            MemoryStream stream = new MemoryStream();
            byte[] buf;
            byte[] entry = new byte[DIR_ENTRY_SIZE];

            buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
            buf = BitConverter.GetBytes((int)Math.Ceiling((double)offset / SECTOR_SIZE));
            stream.Write(buf, 0, buf.Length);

            buf = new byte[SIZE_ENTRY_BYTE_SIZE];
            buf = BitConverter.GetBytes((int)Math.Ceiling((double)length / SECTOR_SIZE));
            stream.Write(buf, 0, buf.Length);

            buf = new byte[FILENAME_ENTRY_BYTE_SIZE];
            Encoding.ASCII.GetBytes(filename + "\0").CopyTo(buf, 0);
            stream.Write(buf, 0, FILENAME_ENTRY_BYTE_SIZE);

            stream.Flush();
            stream.ToArray().CopyTo(entry, 0);
            stream.Close();
            return entry;
        }

        /// <summary>
        /// Adds new table of contents entry for the new <paramref name="file"/> in defined <paramref name="offset"/> to the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Добавляет новую запись о файле <paramref name="file"/> в таблицу размещения файлов в архиве <see cref="ArchiveFile"/> с заданным смещением <paramref name="offset"/>
        /// </summary>
        /// <param name="offset">Starting offset of a <paramref name="file"/></param>
        /// <param name="offset" xml:lang="ru">Смещение начала файла <paramref name="file"/></param>
        /// <param name="file"><see cref="GameFile"/> that needs new entry to be defined</param>
        /// <param name="file" xml:lang="ru">Файл <see cref="GameFile"/>, запись о котором необходимо добавить в таблицу</param>
        public override void AddNewFileEntry(int offset, GameFile file)
        {
            Stream stream = DirFile.GetStream(FileMode.Append, FileAccess.Write);
            byte[] newEntry = CreateNewEntry(offset, file.Length, file.Name);

            stream.Write(newEntry, 0, newEntry.Length);
            stream.Flush();
            stream.Close();
        }

        /// <summary>
        /// Deletes the table of contents entry for the <see cref="GameFile"/> from the current <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет запись о файле <see cref="GameFile"/> из текущего архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="file"><see cref="GameFile"/> which entry needs to be deleted</param>
        /// <param name="file" xml:lang="ru">Файл <see cref="GameFile"/>, запись которого необходимо удалить</param>
        public override void DeleteFileEntry(GameFile entry)
        {
            List<FileSystemElement> entries = GetFileList();

            Stream stream = DirFile.GetStream(FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[4];

            foreach (GameFile dirEntry in entries)
            {
                if (!dirEntry.Equals(entry))
                {
                    byte[] newEntry = CreateNewEntry(dirEntry.Offset, dirEntry.Length, dirEntry.Name);
                    stream.Write(newEntry, 0, newEntry.Length);
                }
            }

            stream.Flush();
            stream.Close();
        }
    }
}
