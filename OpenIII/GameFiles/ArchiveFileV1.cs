using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    class ArchiveFileV1 : ArchiveFile
    {
        public const string DirSuffix = "dir";

        /// <summary>
        /// 
        /// </summary>
        public const int OFFSET_ENTRY_BYTE_SIZE = 4;
        public const int SIZE_ENTRY_BYTE_SIZE = 4;
        public const int FILENAME_ENTRY_BYTE_SIZE = 24;

        /// <summary>
        /// Размер элемента .dir-файла в байтах
        /// </summary>
        public const int DIR_ENTRY_SIZE = 
            OFFSET_ENTRY_BYTE_SIZE +
            FILENAME_ENTRY_BYTE_SIZE +
            SIZE_ENTRY_BYTE_SIZE;

        /// <summary>
        /// Версия архива
        /// </summary>
        public override ArchiveFileVersion ImgVersion { get => ArchiveFileVersion.V1; }

        /// <summary>
        /// Количество файлов в архиве
        /// </summary>
        public override long TotalFiles { get => CalculateTotalFilesFromDir(); }
        
        public GameFile DirFile { get; }

        public ArchiveFileV1(string filePath) : base(filePath)
        {
            DirFile = new GameFile(GetDirFilePath(filePath));
        }

        /// <summary>
        /// Получить количество файлов из .dir-файла
        /// </summary>
        /// <returns></returns>
        private long CalculateTotalFilesFromDir()
        {
            return DirFile.Length / DIR_ENTRY_SIZE;
        }

        /// <summary>
        /// Получить путь до .dir-файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirFilePath(string path)
        {
            // Just replace extension to dir in the original file path
            return path.Remove(path.Length - DirSuffix.Length) + DirSuffix;
        }

        /// <summary>
        /// Получить список файлов из архива
        /// </summary>
        /// <returns></returns>
        public override List<FileSystemElement> GetFileList()
        {
            long filesCount = CalculateTotalFilesFromDir();

            Stream stream = DirFile.GetStream(FileMode.Open, FileAccess.Read);
            List<FileSystemElement> fileList = new List<FileSystemElement>();
            int read = 1;
            byte[] buf;

            while (read > 0 && filesCount > fileList.Count)
            {
                buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                int offset = BitConverter.ToInt32(buf, 0) * SECTOR_SIZE;

                buf = new byte[SIZE_ENTRY_BYTE_SIZE];
                read = stream.Read(buf, 0, buf.Length);
                int size = BitConverter.ToInt32(buf, 0) * SECTOR_SIZE;

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

    }
}
