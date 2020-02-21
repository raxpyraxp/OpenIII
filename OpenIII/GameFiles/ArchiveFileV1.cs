using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    class ArchiveFileV1 : ArchiveFile
    {
        public const string DirSuffix = "dir";

        public const int OFFSET_ENTRY_BYTE_SIZE = 4;
        public const int SIZE_ENTRY_BYTE_SIZE = 4;
        public const int FILENAME_ENTRY_BYTE_SIZE = 24;

        /// <summary>
        /// Размер элемента .dir-файла в байтах
        /// </summary>
        public const int DIR_ENTRY_SIZE = OFFSET_ENTRY_BYTE_SIZE +
                                          FILENAME_ENTRY_BYTE_SIZE +
                                          SIZE_ENTRY_BYTE_SIZE;

        public override int FILE_SECTION_START { get => 0; }

        /// <summary>
        /// Версия архива
        /// </summary>
        public override ArchiveFileVersion ImgVersion { get => ArchiveFileVersion.V1; }

        /// <summary>
        /// Количество файлов в архиве
        /// </summary>
        public override long TotalFiles { get => DirFile.Length / DIR_ENTRY_SIZE; }
        
        public GameFile DirFile { get => new GameFile(GetDirFilePath(FullPath)); }

        public ArchiveFileV1(string filePath) : base(filePath)
        {

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

        public override void AddNewFileEntry(int offset, GameFile file)
        {
            Stream stream = DirFile.GetStream(FileMode.Append, FileAccess.Write);
            byte[] buf;

            buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
            buf = BitConverter.GetBytes((int)Math.Ceiling((double)offset / SECTOR_SIZE));
            stream.Write(buf, 0, buf.Length);

            buf = new byte[SIZE_ENTRY_BYTE_SIZE];
            buf = BitConverter.GetBytes((int)Math.Ceiling((double)file.Length / SECTOR_SIZE));
            stream.Write(buf, 0, buf.Length);

            buf = new byte[FILENAME_ENTRY_BYTE_SIZE];
            Encoding.ASCII.GetBytes(file.Name + "\0").CopyTo(buf, 0);
            stream.Write(buf, 0, FILENAME_ENTRY_BYTE_SIZE);

            stream.Flush();
            stream.Close();
        }

        public override void DeleteFileEntry(GameFile entry)
        {
            List<FileSystemElement> entries = GetFileList();

            Stream stream = DirFile.GetStream(FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[4];

            foreach (GameFile dirEntry in entries)
            {
                if (!dirEntry.Equals(entry))
                {
                    buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
                    buf = BitConverter.GetBytes((int)Math.Ceiling((double)dirEntry.Offset / SECTOR_SIZE));
                    stream.Write(buf, 0, buf.Length);

                    buf = new byte[SIZE_ENTRY_BYTE_SIZE];
                    buf = BitConverter.GetBytes((int)Math.Ceiling((double)dirEntry.Length / SECTOR_SIZE));
                    stream.Write(buf, 0, buf.Length);

                    buf = new byte[FILENAME_ENTRY_BYTE_SIZE];
                    Encoding.ASCII.GetBytes(dirEntry.Name + "\0").CopyTo(buf, 0);
                    stream.Write(buf, 0, FILENAME_ENTRY_BYTE_SIZE);
                }
            }

            stream.Flush();
            stream.Close();
        }
    }
}
