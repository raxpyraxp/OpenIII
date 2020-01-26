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

        public void DeleteFile(GameFile entry) { }
        
        public void ReplaceFile(GameFile oldEntry, GameFile newEntry) { }

        public void RenameFile(GameFile entry, string newName) { }

    }
}
