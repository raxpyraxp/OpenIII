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

        public GameFile GetFirstFile()
        {
            GameFile firstFile = null;

            foreach (GameFile file in GetFileList())
            {
                if (firstFile == null || file.Offset < firstFile.Offset)
                {
                    firstFile = file;
                }
            }

            return firstFile;
        }

        public int GetFirstFileOffset()
        {
            return GetFirstFile().Offset;
        }

        public byte[] CreateNewEntry(int offset, long length, string filename)
        {
            MemoryStream stream = new MemoryStream();
            byte[] buf;
            byte[] entry = new byte[DIR_ENTRY_SIZE];

            buf = new byte[OFFSET_ENTRY_BYTE_SIZE];
            buf = BitConverter.GetBytes((int)Math.Ceiling((double)offset / SECTOR_SIZE));
            stream.Write(buf, 0, buf.Length);

            buf = new byte[STREAMING_ENTRY_BYTE_SIZE];
            buf = BitConverter.GetBytes((ushort)Math.Ceiling((double)length / SECTOR_SIZE));
            stream.Write(buf, 0, buf.Length);

            // It was never used in production game release, so we just write it with zeroes
            buf = new byte[SIZE_ENTRY_BYTE_SIZE];
            stream.Write(buf, 0, buf.Length);

            buf = new byte[FILENAME_ENTRY_BYTE_SIZE];
            Encoding.ASCII.GetBytes(filename + "\0").CopyTo(buf, 0);
            stream.Write(buf, 0, FILENAME_ENTRY_BYTE_SIZE);

            stream.Flush();
            stream.ToArray().CopyTo(entry, 0);
            stream.Close();
            return entry;
        }

        public byte[] CreateHeader(int numberOfFiles)
        {
            MemoryStream stream = new MemoryStream();
            byte[] buf;
            byte[] header = new byte[HEADER_SIZE];

            // Writing version
            buf = new byte[VERSION_SIZE];
            Encoding.ASCII.GetBytes("VER2").CopyTo(buf, 0);
            stream.Write(buf, 0, buf.Length);

            // Copy and modify total files
            buf = new byte[NUMBER_OF_ENTRIES_SIZE];
            BitConverter.GetBytes(numberOfFiles).CopyTo(buf, 0);
            stream.Write(buf, 0, NUMBER_OF_ENTRIES_SIZE);

            stream.Flush();
            stream.ToArray().CopyTo(header, 0);
            stream.Close();
            return header;
        }

        public byte[] CreateFileTableSection(List<GameFile> entries)
        {
            MemoryStream tempStream = new MemoryStream();
            byte[] fileTableSection;
            byte[] header;
            byte[] newEntry;

            // Create archive version header
            header = CreateHeader(entries.Count);
            tempStream.Write(header, 0, header.Length);

            // Copy file entries
            foreach (GameFile entry in entries)
            {
                newEntry = CreateNewEntry(entry.Offset, entry.Length, entry.Name);
                tempStream.Write(newEntry, 0, newEntry.Length);
            }

            tempStream.Flush();
            fileTableSection = tempStream.ToArray();
            tempStream.Close();
            return fileTableSection;
        }

        public override void AddNewFileEntry(int offset, GameFile file)
        {
            List<GameFile> entries = new List<GameFile>();
            int firstFileOffset = GetFirstFileOffset();
            byte[] buf;
            byte[] fileTableSection;
            int read = 1;

            // TODO: We should find better solution for this mess
            foreach (GameFile entry in GetFileList())
            {
                entries.Add(entry);
            }

            file.PrepareForArchiving(offset);
            entries.Add(file);

            fileTableSection = CreateFileTableSection(entries);

            // Check if new IMG header doesn't overlap the first file
            if (fileTableSection.Length > firstFileOffset)
            {
                // We should correctly remove it and write it to the end
                GameFile firstFile = GetFirstFile();
                GameFile tmpFile = new GameFile(Path.GetTempFileName());
                firstFile.Extract(tmpFile.FullPath);
                firstFile.Delete();
                InsertFile(tmpFile);
                tmpFile.Delete();

                // Try to add file entry again
                AddNewFileEntry(offset, file);
            }
            else
            {
                MemoryStream headerStream = new MemoryStream(fileTableSection);
                Stream archiveStream = GetStream(FileMode.Open, FileAccess.ReadWrite);

                while (headerStream.Position < headerStream.Length)
                {
                    buf = new byte[SECTOR_SIZE];
                    read = headerStream.Read(buf, 0, SECTOR_SIZE);
                    archiveStream.Write(buf, 0, read);
                }

                headerStream.Close();
                archiveStream.Close();
            }
        }

        public override void DeleteFileEntry(GameFile file)
        {
            List<GameFile> entries = new List<GameFile>();
            byte[] buf;
            byte[] fileTableSection;
            int read = 1;

            // TODO: We should find better solution for this mess
            foreach (GameFile entry in GetFileList())
            {
                if (!file.Equals(entry))
                {
                    entries.Add(entry);
                }
            }

            fileTableSection = CreateFileTableSection(entries);
            MemoryStream headerStream = new MemoryStream(fileTableSection);
            Stream archiveStream = GetStream(FileMode.Open, FileAccess.ReadWrite);

            while (headerStream.Position < headerStream.Length)
            {
                buf = new byte[SECTOR_SIZE];
                read = headerStream.Read(buf, 0, SECTOR_SIZE);
                archiveStream.Write(buf, 0, read);
            }

            headerStream.Close();
            archiveStream.Close();
        }
    }
}
