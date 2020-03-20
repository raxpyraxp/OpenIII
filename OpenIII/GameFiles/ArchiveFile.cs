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
    /// Версия архива
    /// </summary>
    public enum ArchiveFileVersion
    {
        Unknown,
        V1,
        V2
    }

    public abstract class ArchiveFile : GameFile
    {
        public const int SECTOR_SIZE = 2048;

        public abstract int FILE_SECTION_START { get; }

        /// <summary>
        /// Версия архива
        /// </summary>
        public abstract ArchiveFileVersion ImgVersion { get; }

        /// <summary>
        /// Количество файлов в архиве
        /// </summary>
        public abstract long TotalFiles { get; }

        public ArchiveFile(string filePath) : base(filePath) { }

        public abstract List<FileSystemElement> GetFileList();

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
        /// Создаёт экземпляр
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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
        /// Создаёт экземпляр
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="filename"></param>
        /// <param name="parentFile"></param>
        /// <returns></returns>
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
