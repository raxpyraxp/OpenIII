using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    class ArchiveFileV2 : ArchiveFile
    {
        // Directory entry constants
        public const int OFFSET_ENTRY_BYTE_SIZE = 4;
        public const int STREAMING_ENTRY_BYTE_SIZE = 2;
        public const int SIZE_ENTRY_BYTE_SIZE = 2;
        public const int FILENAME_ENTRY_BYTE_SIZE = 24;

        public const int DIR_ENTRY_SIZE =
            OFFSET_ENTRY_BYTE_SIZE +
            FILENAME_ENTRY_BYTE_SIZE +
            SIZE_ENTRY_BYTE_SIZE +
            STREAMING_ENTRY_BYTE_SIZE;

        // Header constants
        public static int VERSION_SIZE = 4;
        public static int NUMBER_OF_ENTRIES_SIZE = 4;

        public static int HEADER_SIZE = VERSION_SIZE + NUMBER_OF_ENTRIES_SIZE;

        // This is the starting point of the first file in the default IMG file
        public override int FILE_SECTION_START { get => 0x7F800; }

        public override ArchiveFileVersion ImgVersion { get => ArchiveFileVersion.V2; }
        public override long TotalFiles { get => ReadTotalFilesFromArchive(); }

        public ArchiveFileV2(string filePath) : base(filePath)
        {
        }

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

        public int GetFirstFileOffset()
        {
            GameFile firstFile = null;

            foreach (GameFile file in GetFileList())
            {
                if (firstFile == null || file.Offset < firstFile.Offset)
                {
                    firstFile = file;
                }
            }

            return firstFile.Offset;
        }

        public override void AddNewFileEntry(int offset, GameFile file)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFileEntry(GameFile file)
        {
            throw new NotImplementedException();
        }
    }
}
