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
        public string DirPath { get => getDirFilePath(FullPath); }

        public ArchiveFileV1(string filePath) : base(filePath)
        {
            ImgVersion = ArchiveFileVersion.V1;
        }

        private long calculateTotalFilesFromDir()
        {
            return new FileInfo(DirPath).Length / DIR_ENTRY_SIZE;
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
            byte[] buf;

            while (read > 0)
            {
                buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
                read = dirFile.Read(buf, 0, buf.Length);
                int offset = BitConverter.ToInt32(buf, 0) * SECTOR_SIZE;

                buf = new byte[SIZE_ENTRY_BYTE_SIZE];
                read = dirFile.Read(buf, 0, buf.Length);
                int size = BitConverter.ToInt32(buf, 0) * SECTOR_SIZE;

                buf = new byte[FILENAME_ENTRY_BYTE_SIZE];
                read = dirFile.Read(buf, 0, buf.Length);
                string filename = Encoding.ASCII.GetString(buf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));

                fileList.Add(new ArchiveEntry(offset, size, filename, this));
            }

            dirFile.Close();

            return fileList;
        }

    }
}
