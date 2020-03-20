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
using OpenIII.Utils;

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
        /// Archive version
        /// </summary>
        /// <summary xml:lang="ru">
        /// Версия архива
        /// </summary>
        public abstract ArchiveFileVersion ImgVersion { get; }

        /// <summary>
        /// Total files count in the archive
        /// </summary>
        /// <summary xml:lang="ru">
        /// Количество файлов в архиве
        /// </summary>
        public abstract long TotalFiles { get; }

        /// <summary>
        /// Archive constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор архива
        /// </summary>
        /// <param name="filePath">Archive file path</param>
        /// <param name="filePath" xml:lang="ru">Путь к архиву</param>
        public ArchiveFile(string filePath) : base(filePath) { }

        /// <summary>
        /// Gets file handles list to access all archived files
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение списка указателей на все архивированные файлы
        /// </summary>
        public abstract List<FileSystemElement> GetFileList();

        /// <summary>
        /// Gets the new offset in file that can be used to write new file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение нового смещения в архиве для записи нового файла
        /// </summary>
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
        
        public void ReplaceFile(GameFile oldEntry, GameFile newEntry) { }

        public void RenameFile(GameFile entry, string newName) { }

        public abstract void AddNewFileEntry(int offset, GameFile file);

        public abstract void DeleteFileEntry(GameFile file);
    }
}
