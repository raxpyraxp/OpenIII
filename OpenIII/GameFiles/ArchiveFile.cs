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
using System.Threading;
using OpenIII.GameDefinitions;

namespace OpenIII.GameFiles
{
    /// <summary>
    /// Archive version
    /// </summary>
    /// <summary xml:lang="ru">
    /// Версия архива
    /// </summary>
    public enum ArchiveFileVersion
    {
        Unknown,
        V1,
        V2
    }

    /// <summary>
    /// IMG/CDIMAGE archive files for RenderWare-based GTA games implementation
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с IMG/CDIMAGE архивами для игр GTA на движке RenderWare
    /// </summary>
    public abstract class ArchiveFile : GameFile
    {
        /// <summary>
        /// Basic DVD-like sector size for archive data storage
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер сектора для хранения данных в архиве. Имеет тот же размер, что и сектор на DVD диске
        /// </summary>
        public const int SECTOR_SIZE = 2048;

        /// <summary>
        /// Default begining of data section offset
        /// </summary>
        /// <summary xml:lang="ru">
        /// Смещение начала блока данных по умолчанию
        /// </summary>
        /// <remarks>
        /// It is abstract because version 1 and version 2 have different offsets
        /// </remarks>
        /// <remarks xml:lang="ru">
        /// Это поле является абстрактным так как архивы разных версий имеют разные смещения этого блока
        /// </remarks>
        public abstract int FILE_SECTION_START { get; }

        /// <summary>
        /// <see cref="ArchiveFile"/> version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Версия архива <see cref="ArchiveFile"/>
        /// </summary>
        public abstract ArchiveFileVersion ImgVersion { get; }

        /// <summary>
        /// Total files count in the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Количество файлов в архиве <see cref="ArchiveFile"/>
        /// </summary>
        public abstract long TotalFiles { get; }

        /// <summary>
        /// <see cref="ArchiveFile"/> constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="filePath"><see cref="ArchiveFile"/> path</param>
        /// <param name="filePath" xml:lang="ru">Путь к архиву <see cref="ArchiveFile"/></param>
        public ArchiveFile(string filePath) : base(filePath) { }

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
        public abstract List<FileSystemElement> GetFileList();

        /// <summary>
        /// Gets the new offset in file that can be used to write new file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение нового смещения в архиве для записи нового файла
        /// </summary>
        /// <returns>
        /// Offset for the new <see cref="GameFile"/>
        /// </returns>
        /// <returns xml:lang="ru">
        /// Смещение для нового файла <see cref="GameFile"/>
        /// </returns>
        public int CalculateOffsetForNewEntry()
        {
            GameFile lastFile = null;

            foreach (GameFile file in GetFileList())
            {
                if (lastFile == null || file.Offset > lastFile.Offset)
                {
                    lastFile = file;
                }
            }

            if (lastFile != null)
            {
                // TODO: There might be an exception if file is too long.
                // In regular filesystem the GameFile can be very large,
                // so Length is long in OpenIII.
                // But the game itself can't handle long files because
                // it's by design of IMG file (length is 4 bytes).
                // The only way is to give user an error message
                // if file too long.
                // Moreover, game has it's own limits and all files are
                // usually less than 25-30MB anyway.
                return lastFile.Offset + (int)lastFile.Length;
            }
            else
            {
                return FILE_SECTION_START;
            }
        }

        /// <summary>
        /// Creates new handle instance to use archive from filesystem based on the archive version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создание нового указателя на архив, расположенный в файле на основании его версии
        /// </summary>
        /// <param name="path">Archive file path</param>
        /// <param name="path" xml:lang="ru">Путь к архиву</param>
        /// <returns>
        /// New <see cref="ArchiveFile"/> instance
        /// </returns>
        /// <returns xml:lang="ru">
        /// Новый инстанс <see cref="ArchiveFile"/>
        /// </returns>
        public static new ArchiveFile CreateInstance(string path)
        {
            ArchiveFileVersion version = ArchiveFileV2.ReadVersionFromArchive(new GameFile(path));

            // We've tried to extract the version from the archive file itself in readVersionFromImg().
            // If we've failed, then we're checking if we have a .dir file nearby.
            // That indicates that we deal with V1 archive.
            if (version == ArchiveFileVersion.Unknown)
            {
                version = File.Exists(ArchiveFileV1.GetDirFilePath(path)) ?
                    ArchiveFileVersion.V1 :
                    ArchiveFileVersion.Unknown;
            }

            switch (version)
            {
                case ArchiveFileVersion.V1:
                    return new ArchiveFileV1(path);
                case ArchiveFileVersion.V2:
                    return new ArchiveFileV2(path);
                default:
                    throw new Exception("Invalid archive version");
            }
        }

        /// <summary>
        /// Creates new handle instance to use archive that is archived based on the archive version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создание нового указателя на архив, который находится в другом архиве на основании его версии
        /// </summary>
        /// <param name="offset">Offset of the file in the archive</param>
        /// <param name="offset" xml:lang="ru">Смещение файла в архиве</param>
        /// <param name="size">File size in the archive</param>
        /// <param name="size" xml:lang="ru">Размер файла в архиве</param>
        /// <param name="filename">File name</param>
        /// <param name="filename" xml:lang="ru">Имя файла</param>
        /// <param name="parentFile"><see cref="ArchiveFile"/> where this file belongs to</param>
        /// <param name="parentFile" xml:lang="ru"><see cref="ArchiveFile"/>, в котором данный файл находится</param>
        /// <returns>
        /// New <see cref="ArchiveFile"/> instance
        /// </returns>
        /// <returns xml:lang="ru">
        /// Новый инстанс <see cref="ArchiveFile"/>
        /// </returns>
        /// <exception cref="NotImplementedException">Thrown on function call</exception>
        /// <exception cref="NotImplementedException" xml:lang="ru">Вызывается при вызове функции</exception>
        /// <remarks>
        /// This function is not implemented. It is required by <see cref="GameFile"/> to be implemented
        /// but this configuration is never used in the real life.
        /// </remarks>
        /// <remarks xml:lang="ru">
        /// Эта функция не реализована. Она определена в <see cref="GameFile"/> как абстрактная и её требуется
        /// определить для <see cref="ArchiveFile"/>. Однако в реальности конфигурация с вложенными IMG файлами
        /// нигде не используется.
        /// </remarks>

        public static new ArchiveFile CreateInstance(int offset, int size, string filename, ArchiveFile parentFile)
        {
            throw new NotImplementedException();
        }

        public static ArchiveFile Create(string path)
        {
            switch (Game.Instance.ImgVersion)
            {
                case ArchiveFileVersion.V1:
                    return ArchiveFileV1.Create(path);
                case ArchiveFileVersion.V2:
                    return ArchiveFileV2.Create(path);
                default:
                    throw new InvalidOperationException();
            }

        }

        /// <summary>
        /// Экспортирует файл из архива
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="destination"></param>
        public void ExtractFile(GameFile entry, string destination)
        {
            Stream stream = entry.GetStream(FileMode.Open, FileAccess.Read);
            FileStream destinationFile = new FileStream(destination, FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[SECTOR_SIZE];

            while (stream.Position < stream.Length)
            {
                int read = stream.Read(buf, 0, SECTOR_SIZE);
                destinationFile.Write(buf, 0, read);
            }

            destinationFile.Flush();
            destinationFile.Close();
            stream.Close();
        }

        public void ExtractFileAsync(GameFile entry, string destination, CancellationToken ct, UpdateProgressDelegate callback)
        {
            callback.Invoke(0, string.Format("Extracting {0}", entry.Name));
            Stream stream = entry.GetStream(FileMode.Open, FileAccess.Read);
            FileStream destinationFile = new FileStream(destination, FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[SECTOR_SIZE];

            while (stream.Position < stream.Length)
            {
                if (ct.IsCancellationRequested)
                {
                    destinationFile.Close();
                    File.Delete(destination);
                    return;
                }

                int read = stream.Read(buf, 0, SECTOR_SIZE);
                destinationFile.Write(buf, 0, read);
                callback.Invoke((int)((float)stream.Position / stream.Length * 100), string.Format("Extracting {0}", entry.Name));
            }

            destinationFile.Flush();
            destinationFile.Close();
            stream.Close();
        }

        /// <summary>
        /// Inserts new <see cref="GameFile"/> into current <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Добавляет новый файл <see cref="GameFile"/> в текущий архив <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="sourceFile"><see cref="GameFile"/> to be inserted into the <see cref="ArchiveFile"/></param>
        /// <param name="sourceFile" xml:lang="ru">Файл <see cref="GameFile"/>, который необходимо добавить в архив <see cref="ArchiveFile"/></param>
        public void InsertFile(GameFile sourceFile)
        {
            int offset = CalculateOffsetForNewEntry();
            AddNewFileEntry(offset, sourceFile);
            
            Stream sourceStream = sourceFile.GetStream(FileMode.Open, FileAccess.Read);
            Stream destinationStream = GetStream(FileMode.Open, FileAccess.Write);
            byte[] buf;

            destinationStream.Seek(offset, SeekOrigin.Begin);

            while (sourceStream.Position < sourceStream.Length)
            {
                buf = new byte[SECTOR_SIZE];
                int read = sourceStream.Read(buf, 0, SECTOR_SIZE);
                destinationStream.Write(buf, 0, SECTOR_SIZE);
            }

            destinationStream.Flush();
            destinationStream.Close();
            sourceStream.Close();
        }

        /// <summary>
        /// Deletes <see cref="GameFile"/> from the current <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет файл <see cref="GameFile"/> из текущего архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="entry"><see cref="GameFile"/> to be deleted</param>
        /// <param name="entry" xml:lang="ru">Файл <see cref="GameFile"/>, который необходимо удалить</param>
        public void DeleteFile(GameFile entry)
        {
            // We don't really know if we need to actually remove the file itself.
            // Maybe it is not needed here and we can just remove entry
            // just like in regular file system.
            // I'll leave this code commented here. Maybe we'll do this as a feature
            // with a flag. Who knows.
            /*Stream destinationStream = GetStream(FileMode.Open, FileAccess.Write);

            destinationStream.Seek(entry.Offset, SeekOrigin.Begin);

            while (destinationStream.Position < entry.Offset + entry.Length)
            {
                byte[] buf = new byte[SECTOR_SIZE];
                destinationStream.Write(buf, 0, SECTOR_SIZE);
            }

            destinationStream.Close();*/

            DeleteFileEntry(entry);
        }

        /// <summary>
        /// Replaces <paramref name="oldEntry"/> in the <see cref="ArchiveFile"/> with new file <paramref name="newEntry"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Заменяет файл <paramref name="oldEntry"/> в архиве <see cref="ArchiveFile"/> на новый файл <paramref name="newEntry"/>
        /// </summary>
        /// <param name="oldEntry">Old <see cref="GameFile"/> in the <see cref="ArchiveFile"/> that needs to be replaced</param>
        /// <param name="oldEntry" xml:lang="ru">Старый файл <see cref="GameFile"/> в архиве <see cref="ArchiveFile"/>, который нужно заменить</param>
        /// <param name="newEntry">A replacement <see cref="GameFile"/> for the <paramref name="oldEntry"/></param>
        /// <param name="newEntry" xml:lang="ru">Новый файл <see cref="GameFile"/>, которым необходимо заменить <paramref name="oldEntry"/></param>
        /// <remarks>
        /// This function is not implemented yet.
        /// </remarks>
        /// <remarks xml:lang="ru">
        /// Эта функция ещё не реализована.
        /// </remarks>
        public void ReplaceFile(GameFile oldEntry, GameFile newEntry) { }

        /// <summary>
        /// Renames <paramref name="entry"/> to <paramref name="newName"/> in the current <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Переименовывает файл <paramref name="entry"/> в <paramref name="newName"/> в текущем архиве <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="entry"><see cref="GameFile"/> to be renamed</param>
        /// <param name="entry" xml:lang="ru">Файл <see cref="GameFile"/>, который необходимо переименовать</param>
        /// <param name="newName">New file name</param>
        /// <param name="newName" xml:lang="ru">Новое имя файла</param>
        /// <remarks>
        /// This function is not implemented yet.
        /// </remarks>
        /// <remarks xml:lang="ru">
        /// Эта функция ещё не реализована.
        /// </remarks>
        public void RenameFile(GameFile entry, string newName) { }

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
        public abstract void AddNewFileEntry(int offset, GameFile file);

        /// <summary>
        /// Deletes the table of contents entry for the <see cref="GameFile"/> from the current <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет запись о файле <see cref="GameFile"/> из текущего архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="file"><see cref="GameFile"/> which entry needs to be deleted</param>
        /// <param name="file" xml:lang="ru">Файл <see cref="GameFile"/>, запись которого необходимо удалить</param>
        public abstract void DeleteFileEntry(GameFile file);
    }
}
