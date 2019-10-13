using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    class ArchiveFileV2 : ArchiveFile
    {
        // Directory entry constants
        public static int OFFSET_ENTRY_BYTE_SIZE = 4;
        public static int STREAMING_ENTRY_BYTE_SIZE = 2;
        public static int SIZE_ENTRY_BYTE_SIZE = 2;
        public static int FILENAME_ENTRY_BYTE_SIZE = 24;

        public static int DIR_ENTRY_SIZE =
            OFFSET_ENTRY_BYTE_SIZE +
            FILENAME_ENTRY_BYTE_SIZE +
            SIZE_ENTRY_BYTE_SIZE +
            STREAMING_ENTRY_BYTE_SIZE;

        // Header constants
        public static int VERSION_SIZE = 4;
        public static int NUMBER_OF_ENTRIES_SIZE = 4;

        public static int HEADER_SIZE = VERSION_SIZE + NUMBER_OF_ENTRIES_SIZE;

        public override ArchiveFileVersion ImgVersion { get; }
        public override long TotalFiles { get => readTotalFilesFromImg(); }

        public ArchiveFileV2(string filePath) : base(filePath)
        {
            ImgVersion = ArchiveFileVersion.V2;
        }

        public static ArchiveFileVersion readVersionFromImg(string imgPath)
        {
            FileStream imgFile = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            int read = 1;
            string versionHeader = "";

            byte[] versionBuf = new byte[VERSION_SIZE];
            read = imgFile.Read(versionBuf, 0, versionBuf.Length);
            versionHeader = Encoding.ASCII.GetString(versionBuf);

            imgFile.Close();

            return versionHeader.IndexOf("VER2") != -1 ?
                ArchiveFileVersion.V2 :
                ArchiveFileVersion.Unknown;
        }

        private long readTotalFilesFromImg()
        {
            FileStream fileImg = new FileStream(Path, FileMode.Open, FileAccess.Read);
            int read = 1;
            int totalFiles = 0;

            fileImg.Seek(VERSION_SIZE, SeekOrigin.Begin);

            byte[] totalFilesBuf = new byte[NUMBER_OF_ENTRIES_SIZE];
            read = fileImg.Read(totalFilesBuf, 0, totalFilesBuf.Length);
            totalFiles = BitConverter.ToInt32(totalFilesBuf, 0);

            fileImg.Close();

            return totalFiles;
        }

        public override List<ArchiveEntry> readImgFileList()
        {
            long filesCount = readTotalFilesFromImg();

            FileStream imgFile = new FileStream(Path, FileMode.Open, FileAccess.Read);
            List<ArchiveEntry> fileList = new List<ArchiveEntry>();
            int read = 1;

            // Skipping header
            imgFile.Seek(HEADER_SIZE, SeekOrigin.Begin);

            while (read > 0 && filesCount > fileList.Count)
            {
                byte[] offsetBuf = new byte[OFFSET_ENTRY_BYTE_SIZE];
                read = imgFile.Read(offsetBuf, 0, offsetBuf.Length);
                int offset = BitConverter.ToInt32(offsetBuf, 0) * SECTOR_SIZE;

                byte[] streamingSize = new byte[STREAMING_ENTRY_BYTE_SIZE];
                read = imgFile.Read(streamingSize, 0, streamingSize.Length);
                int size = BitConverter.ToInt16(streamingSize, 0) * SECTOR_SIZE;

                byte[] sizeInArchiveBuf = new byte[SIZE_ENTRY_BYTE_SIZE];
                read = imgFile.Read(sizeInArchiveBuf, 0, sizeInArchiveBuf.Length);
                // It was never used in production game release, so we just skip this for now
                //int size = BitConverter.ToInt16(sizeInArchiveBuf, 0) * SECTOR_SIZE;

                byte[] nameBuf = new byte[FILENAME_ENTRY_BYTE_SIZE];
                read = imgFile.Read(nameBuf, 0, nameBuf.Length);
                string filename = Encoding.ASCII.GetString(nameBuf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));

                fileList.Add(new ArchiveEntry(offset, size, filename, this));
            }

            imgFile.Close();

            return fileList;
        }
    }
}
