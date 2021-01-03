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
    /// IMG/CDIMAGE version 2 archive files for RenderWare-based GTA games implementation that is used in GTA: San Andreas
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с IMG/CDIMAGE архивами версии 1 для игр GTA на движке RenderWare которые используются в GTA: San Andreas
    /// </summary>
    class ArchiveFileV2 : ArchiveFile
    {
        // Directory entry constants

        /// <summary>
        /// Size of the <see cref="GameFile"/> offset section in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции смещения файла <see cref="GameFile"/> в байтах
        /// </summary>
        public const int OFFSET_ENTRY_BYTE_SIZE = 4;

        /// <summary>
        /// Size of the <see cref="GameFile"/> streaming size section in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции размера потока файла <see cref="GameFile"/> в байтах
        /// </summary>
        public const int STREAMING_ENTRY_BYTE_SIZE = 2;

        /// <summary>
        /// Size of the <see cref="GameFile"/> size section in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции размера файла <see cref="GameFile"/> в байтах
        /// </summary>
        /// <remarks>
        /// Refered as not used at least in PC version
        /// </remarks>
        /// <remarks xml:lang="ru">
        /// В некоторых источниках утверждается, что данная секция не используется по крайней мере в ПК-версии
        /// </remarks>
        public const int SIZE_ENTRY_BYTE_SIZE = 2;

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
        public const int DIR_ENTRY_SIZE =
            OFFSET_ENTRY_BYTE_SIZE +
            FILENAME_ENTRY_BYTE_SIZE +
            SIZE_ENTRY_BYTE_SIZE +
            STREAMING_ENTRY_BYTE_SIZE;


        // Header constants

        /// <summary>
        /// Size of the version section of the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции версии архива <see cref="ArchiveFile"/>
        /// </summary>
        public static int VERSION_SIZE = 4;

        /// <summary>
        /// Size of the number of files section of the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции количества файлов в архиве <see cref="ArchiveFile"/>
        /// </summary>
        public static int NUMBER_OF_ENTRIES_SIZE = 4;

        /// <summary>
        /// Size of the header section of the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер секции заголовка архива <see cref="ArchiveFile"/>
        /// </summary>
        public static int HEADER_SIZE = VERSION_SIZE + NUMBER_OF_ENTRIES_SIZE;

        /// <summary>
        /// Default begining of data section offset
        /// In the V2 this is the starting point of the first file in the default San Andreas IMG file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Смещение начала блока данных по умолчанию
        /// Во второй версии архива указывает на смещение первого файла в нетронутом IMG архиве San Andreas
        /// </summary>
        public override int FILE_SECTION_START { get => 0x7F800; }

        /// <summary>
        /// <see cref="ArchiveFile"/> version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Версия архива <see cref="ArchiveFile"/>
        /// </summary>
        public override ArchiveFileVersion ImgVersion { get => ArchiveFileVersion.V2; }

        /// <summary>
        /// Total files count in the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Количество файлов в архиве <see cref="ArchiveFile"/>
        /// </summary>
        public override long TotalFiles { get => ReadTotalFilesFromArchive(); }

        /// <summary>
        /// <see cref="ArchiveFile"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="filePath"><see cref="ArchiveFile"/> path</param>
        /// <param name="filePath" xml:lang="ru">Путь к архиву <see cref="ArchiveFile"/></param>
        public ArchiveFileV2(string filePath) : base(filePath)
        {
        }

        public new static ArchiveFile Create(string path)
        {
            // Create .img file
            FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            
            // Version 2 signature
            stream.Write(Encoding.ASCII.GetBytes("VER2"), 0, 4);

            // Number of entries
            stream.Write(new byte[] { 0x0, 0x0, 0x0, 0x0 }, 0, 4);

            // Dir section
            // TODO: make a static constant
            stream.Seek(0x7F800 - 1, SeekOrigin.Begin);
            stream.WriteByte(0);

            stream.Flush();
            stream.Close();
            
            return new ArchiveFileV2(path);
        }

        /// <summary>
        /// Reads the version from the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Читает версию напрямую из файла архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="imgFile"><see cref="ArchiveFile"/> version of which should be read</param>
        /// <param name="imgFile" xml:lang="ru">Архив <see cref="ArchiveFile"/> версию которого нужно прочитать</param>
        /// <returns>
        /// <paramref name="imgFile"/> version
        /// </returns>
        /// <returns xml:lang="ru">
        /// Версия архива <paramref name="imgFile"/>
        /// </returns>
        public static ArchiveFileVersion ReadVersionFromArchive(GameFile imgFile)
        {
            Stream stream = imgFile.GetStream(FileMode.Open, FileAccess.Read);
            byte[] buf = new byte[VERSION_SIZE];

            stream.Read(buf, 0, buf.Length);

            string versionHeader = Encoding.ASCII.GetString(buf);

            stream.Close();

            return versionHeader.IndexOf("VER2") != -1 ?
                ArchiveFileVersion.V2 :
                ArchiveFileVersion.Unknown;
        }

        /// <summary>
        /// Reads the total files from the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Чтение количества файлов напрямую из файла архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="imgFile"><see cref="ArchiveFile"/> total files of which should be read</param>
        /// <param name="imgFile" xml:lang="ru">Архив <see cref="ArchiveFile"/> количество файлов которого нужно прочитать</param>
        /// <returns>
        /// <paramref name="imgFile"/> version
        /// </returns>
        /// <returns xml:lang="ru">
        /// Версия архива <paramref name="imgFile"/>
        /// </returns>
        private long ReadTotalFilesFromArchive()
        {
            Stream stream = GetStream(FileMode.Open, FileAccess.Read);
            byte[] buf = new byte[NUMBER_OF_ENTRIES_SIZE];

            stream.Seek(VERSION_SIZE, SeekOrigin.Begin);
            stream.Read(buf, 0, buf.Length);

            int totalFiles = BitConverter.ToInt32(buf, 0);

            stream.Close();

            return totalFiles;
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
            long filesCount = ReadTotalFilesFromArchive();

            Stream stream = GetStream(FileMode.Open, FileAccess.Read);
            List<FileSystemElement> fileList = new List<FileSystemElement>();
            int read = 1;
            byte[] buf;

            // Skipping header
            stream.Seek(HEADER_SIZE, SeekOrigin.Begin);

            while (read > 0 && filesCount > fileList.Count)
            {
                buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                int offset = BitConverter.ToInt32(buf, 0) * SECTOR_SIZE;

                buf = new byte[STREAMING_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                int size = BitConverter.ToInt16(buf, 0) * SECTOR_SIZE;

                buf = new byte[SIZE_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                // It was never used in production game release, so we just skip this for now
                //int size = BitConverter.ToInt16(buf, 0) * SECTOR_SIZE;

                buf = new byte[FILENAME_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                string filename = Encoding.ASCII.GetString(buf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));

                fileList.Add(new GameFile(offset, size, filename, this));
            }

            stream.Close();

            return fileList;
        }

        /// <summary>
        /// Gets handle to the first <see cref="GameFile"/> in the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение указателя на первый файл <see cref="GameFile"/> в архиве <see cref="ArchiveFile"/>
        /// </summary>
        /// <returns>
        /// Handle of the first <see cref="GameFile"/>
        /// </returns>
        /// <returns xml:lang="ru">
        /// Указатель на первый файл <see cref="GameFile"/>
        /// </returns>
        public GameFile GetFirstFile()
        {
            GameFile firstFile = null;

            foreach (GameFile file in GetFileList())
            {
                if (firstFile == null || file.Offset < firstFile.Offset)
                {
                    firstFile = file;
                }
            }

            return firstFile;
        }

        /// <summary>
        /// Gets offset of the first <see cref="GameFile"/> in the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение смещения первого файла <see cref="GameFile"/> в архиве <see cref="ArchiveFile"/>
        /// </summary>
        /// <returns>
        /// Offset of the first <see cref="GameFile"/>
        /// </returns>
        /// <returns xml:lang="ru">
        /// Смещение первого файла <see cref="GameFile"/>
        /// </returns>
        public int GetFirstFileOffset()
        {
            GameFile file = GetFirstFile();

            if (file == null)
            {
                // TODO: this is wrong, but we assume that file is valid and doesn't contain any file.
                // Offset in IMG is int, so it is required to be int
                return (int)Length;
            }
            else
            {
                return GetFirstFile().Offset;
            }
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

            buf = new byte[STREAMING_ENTRY_BYTE_SIZE];
            buf = BitConverter.GetBytes((ushort)Math.Ceiling((double)length / SECTOR_SIZE));
            stream.Write(buf, 0, buf.Length);

            // It was never used in production game release, so we just write it with zeroes
            buf = new byte[SIZE_ENTRY_BYTE_SIZE];
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
        /// Creates new <see cref="ArchiveFile"/> header
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создаёт новый заголовок архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="numberOfFiles">New file count</param>
        /// <param name="numberOfFiles" xml:lang="ru">Новое количество файлов</param>
        /// <returns>
        /// Header in bytes block
        /// </returns>
        /// <returns xml:lang="ru">
        /// Блок данных с заголовком
        /// </returns>
        public byte[] CreateHeader(int numberOfFiles)
        {
            MemoryStream stream = new MemoryStream();
            byte[] buf;
            byte[] header = new byte[HEADER_SIZE];

            // Writing version
            buf = new byte[VERSION_SIZE];
            Encoding.ASCII.GetBytes("VER2").CopyTo(buf, 0);
            stream.Write(buf, 0, buf.Length);

            // Copy and modify total files
            buf = new byte[NUMBER_OF_ENTRIES_SIZE];
            BitConverter.GetBytes(numberOfFiles).CopyTo(buf, 0);
            stream.Write(buf, 0, NUMBER_OF_ENTRIES_SIZE);

            stream.Flush();
            stream.ToArray().CopyTo(header, 0);
            stream.Close();
            return header;
        }

        /// <summary>
        /// Creates new file table section of the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создаёт новую таблицу размещения файлов в архиве <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="entries">List of files in the archive</param>
        /// <param name="entries" xml:lang="ru">Список файлов в архиве</param>
        /// <returns>
        /// Table section in bytes block
        /// </returns>
        /// <returns xml:lang="ru">
        /// Блок данных с таблицей размещения файлов
        /// </returns>
        public byte[] CreateFileTableSection(List<GameFile> entries)
        {
            MemoryStream tempStream = new MemoryStream();
            byte[] fileTableSection;
            byte[] header;
            byte[] newEntry;

            // Create archive version header
            header = CreateHeader(entries.Count);
            tempStream.Write(header, 0, header.Length);

            // Copy file entries
            foreach (GameFile entry in entries)
            {
                newEntry = CreateNewEntry(entry.Offset, entry.Length, entry.Name);
                tempStream.Write(newEntry, 0, newEntry.Length);
            }

            tempStream.Flush();
            fileTableSection = tempStream.ToArray();
            tempStream.Close();
            return fileTableSection;
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
        /// <returns><see cref="GameFile"/> entry for a new file</returns>
        public override GameFile AddNewFileEntry(int offset, GameFile file)
        {
            List<GameFile> entries = new List<GameFile>();
            int firstFileOffset = GetFirstFileOffset();
            byte[] buf;
            byte[] fileTableSection;
            int read = 1;

            // TODO: We should find better solution for this mess
            foreach (GameFile entry in GetFileList())
            {
                entries.Add(entry);
            }

            file.PrepareForArchiving(offset);
            entries.Add(file);

            fileTableSection = CreateFileTableSection(entries);

            // Check if new IMG header doesn't overlap the first file
            if (fileTableSection.Length > firstFileOffset)
            {
                // We should correctly remove it and write it to the end
                GameFile firstFile = GetFirstFile();
                GameFile tmpFile = new GameFile(Path.GetTempFileName());
                firstFile.Extract(tmpFile.FullPath);
                firstFile.Delete();
                InsertFile(tmpFile);
                tmpFile.Delete();

                // Try to add file entry again
                return AddNewFileEntry(offset, file);
            }
            else
            {
                MemoryStream headerStream = new MemoryStream(fileTableSection);
                Stream archiveStream = GetStream(FileMode.Open, FileAccess.ReadWrite);

                while (headerStream.Position < headerStream.Length)
                {
                    buf = new byte[SECTOR_SIZE];
                    read = headerStream.Read(buf, 0, SECTOR_SIZE);
                    archiveStream.Write(buf, 0, read);
                }

                headerStream.Close();
                archiveStream.Close();

                return new GameFile(offset, (int)file.Length, file.Name, this);
            }
        }

        /// <summary>
        /// Deletes the table of contents entry for the <see cref="GameFile"/> from the current <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет запись о файле <see cref="GameFile"/> из текущего архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="file"><see cref="GameFile"/> which entry needs to be deleted</param>
        /// <param name="file" xml:lang="ru">Файл <see cref="GameFile"/>, запись которого необходимо удалить</param>
        public override void DeleteFileEntry(GameFile file)
        {
            List<GameFile> entries = new List<GameFile>();
            byte[] buf;
            byte[] fileTableSection;
            int read = 1;

            // TODO: We should find better solution for this mess
            foreach (GameFile entry in GetFileList())
            {
                if (!file.Equals(entry))
                {
                    entries.Add(entry);
                }
            }

            fileTableSection = CreateFileTableSection(entries);
            MemoryStream headerStream = new MemoryStream(fileTableSection);
            Stream archiveStream = GetStream(FileMode.Open, FileAccess.ReadWrite);

            while (headerStream.Position < headerStream.Length)
            {
                buf = new byte[SECTOR_SIZE];
                read = headerStream.Read(buf, 0, SECTOR_SIZE);
                archiveStream.Write(buf, 0, read);
            }

            headerStream.Close();
            archiveStream.Close();
        }
    }
}
