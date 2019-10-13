using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    class ArchiveFileV1 : ArchiveFile
    {
        // Directory entry constants
        public static int OFFSET_ENTRY_BYTE_SIZE = 4;
        public static int SIZE_ENTRY_BYTE_SIZE = 4;
        public static int FILENAME_ENTRY_BYTE_SIZE = 24;

        public static int DIR_ENTRY_SIZE = 
            OFFSET_ENTRY_BYTE_SIZE +
            FILENAME_ENTRY_BYTE_SIZE +
            SIZE_ENTRY_BYTE_SIZE;

        public override ArchiveFileVersion ImgVersion { get; }
        public override long TotalFiles { get => calculateTotalFilesFromDir(); }
        public string DirPath { get => getDirFilePath(FilePath); }

        public ArchiveFileV1(string filePath) : base(filePath)
        {
            ImgVersion = ArchiveFileVersion.V1;
        }

        private long calculateTotalFilesFromDir()
        {
            FileInfo info = new FileInfo(DirPath);
            return info.Length / DIR_ENTRY_SIZE;
        }

        public static string getDirFilePath(string path)
        {
            // Just replace extension to dir in the original file path
            return path.Remove(path.Length - 3) + "dir";
        }

        public override List<ArchiveEntry> readImgFileList()
        {
            FileStream dirFile = new FileStream(DirPath, FileMode.Open, FileAccess.Read);
            List<ArchiveEntry> fileList = new List<ArchiveEntry>();
            int read = 1;

            while (read > 0)
            {
                byte[] offsetBuf = new byte[OFFSET_ENTRY_BYTE_SIZE];
                read = dirFile.Read(offsetBuf, 0, offsetBuf.Length);
                int offset = BitConverter.ToInt32(offsetBuf, 0) * SECTOR_SIZE;

                byte[] sizeBuf = new byte[SIZE_ENTRY_BYTE_SIZE];
                read = dirFile.Read(sizeBuf, 0, sizeBuf.Length);
                int size = BitConverter.ToInt32(sizeBuf, 0) * SECTOR_SIZE;

                byte[] nameBuf = new byte[FILENAME_ENTRY_BYTE_SIZE];
                read = dirFile.Read(nameBuf, 0, nameBuf.Length);
                string filename = Encoding.ASCII.GetString(nameBuf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));

                fileList.Add(new ArchiveEntry(offset, size, filename, this));
            }

            dirFile.Close();

            return fileList;
        }

    }
}
