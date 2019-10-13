using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    public enum ArchiveFileVersion
    {
        Unknown,
        V1,
        V2
    }

    public class ArchiveFile : GameFile
    {
        public static int SECTOR_SIZE = 2048;
        
        public ArchiveFileVersion ImgVersion { get; private set; }

        public int TotalFiles
        {
            get
            {
                switch (ImgVersion)
                {
                    case ArchiveFileVersion.V1:
                        return -1;
                    case ArchiveFileVersion.V2:
                        return readTotalFilesFromImg();
                    default:
                        return -1;
                }
            }
        }

        public ArchiveFile(string filePath) : base(filePath)
        {
            ImgVersion = readVersionFromImg();

            // We've tried to extract the version from the archive file itself in readVersionFromImg().
            // If we've failed, then we're checking if we have a .dir file nearby.
            // That indicates that we deal with V1 archive.
            if (ImgVersion == ArchiveFileVersion.Unknown)
            {
                ImgVersion = File.Exists(getDirFile()) ?
                    ArchiveFileVersion.V1 :
                    ArchiveFileVersion.Unknown;
            }
        }

        private string getDirFile()
        {
            // Just replace extension to dir in the original file path
            return this.filePath.Remove(this.filePath.Length - 3) + "dir";
        }

        private ArchiveFileVersion readVersionFromImg()
        {
            FileStream fileImg = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            int read = 1;
            string versionHeader = "";

            byte[] versionBuf = new byte[4];
            read = fileImg.Read(versionBuf, 0, versionBuf.Length);
            versionHeader = Encoding.ASCII.GetString(versionBuf);

            fileImg.Close();

            return versionHeader.IndexOf("VER2") != -1 ?
                ArchiveFileVersion.V2 :
                ArchiveFileVersion.Unknown;
        }

        private int readTotalFilesFromImg()
        {
            FileStream fileImg = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            int read = 1;
            int totalFiles = 0;

            fileImg.Seek(4, SeekOrigin.Begin);

            byte[] totalFilesBuf = new byte[4];
            read = fileImg.Read(totalFilesBuf, 0, totalFilesBuf.Length);
            totalFiles = BitConverter.ToInt32(totalFilesBuf, 0);

            fileImg.Close();

            return totalFiles;
        }

        public List<ArchiveEntry> readImgFileList()
        {
            switch (ImgVersion)
            {
                case ArchiveFileVersion.V1:
                    return readImgFileListV1();
                case ArchiveFileVersion.V2:
                    return readImgFileListV2();
                default:
                    return null;
            }
        }

        public List<ArchiveEntry> readImgFileListV1()
        {
            FileStream dirFile = new FileStream(getDirFile(), FileMode.Open, FileAccess.Read);
            List<ArchiveEntry> fileList = new List<ArchiveEntry>();
            int read = 1;

            while (read > 0)
            {
                byte[] offsetBuf = new byte[4];
                read = dirFile.Read(offsetBuf, 0, offsetBuf.Length);
                int offset = BitConverter.ToInt32(offsetBuf, 0) * SECTOR_SIZE;

                byte[] sizeBuf = new byte[4];
                read = dirFile.Read(sizeBuf, 0, sizeBuf.Length);
                int size = BitConverter.ToInt32(sizeBuf, 0) * SECTOR_SIZE;

                byte[] nameBuf = new byte[24];
                read = dirFile.Read(nameBuf, 0, nameBuf.Length);
                string filename = Encoding.ASCII.GetString(nameBuf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));

                fileList.Add(new ArchiveEntry(offset, size, filename, this));
            }

            dirFile.Close();

            return fileList;
        }

        public List<ArchiveEntry> readImgFileListV2()
        {
            int filesCount = readTotalFilesFromImg();

            FileStream imgFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            List<ArchiveEntry> fileList = new List<ArchiveEntry>();
            int read = 1;

            // Skipping header
            imgFile.Seek(8, SeekOrigin.Begin);

            while (read > 0 && filesCount > fileList.Count)
            {
                byte[] offsetBuf = new byte[4];
                read = imgFile.Read(offsetBuf, 0, offsetBuf.Length);
                int offset = BitConverter.ToInt32(offsetBuf, 0) * SECTOR_SIZE;

                byte[] streamingSize = new byte[2];
                read = imgFile.Read(streamingSize, 0, streamingSize.Length);
                int size = BitConverter.ToInt16(streamingSize, 0) * SECTOR_SIZE;

                byte[] sizeInArchiveBuf = new byte[2];
                read = imgFile.Read(sizeInArchiveBuf, 0, sizeInArchiveBuf.Length);
                // It was never used in production game release, so we just skip this for now
                //int size = BitConverter.ToInt16(sizeInArchiveBuf, 0) * SECTOR_SIZE;

                byte[] nameBuf = new byte[24];
                read = imgFile.Read(nameBuf, 0, nameBuf.Length);
                string filename = Encoding.ASCII.GetString(nameBuf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));

                fileList.Add(new ArchiveEntry(offset, size, filename, this));
            }

            imgFile.Close();

            return fileList;
        }

        public void extractFile(ArchiveEntry entry, string destination)
        {
            FileStream imgFile = new FileStream(this.filePath, FileMode.Open, FileAccess.Read);
            FileStream destinationFile = new FileStream(destination, FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[SECTOR_SIZE];
            int bytesLeft = entry.size;

            imgFile.Seek(entry.offset, SeekOrigin.Begin);
            
            while (bytesLeft > 0)
            {
                int bytesToRead = bytesLeft > SECTOR_SIZE ? SECTOR_SIZE : bytesLeft;
                imgFile.Read(buf, 0, bytesToRead);
                destinationFile.Write(buf, 0, bytesToRead);
                bytesLeft -= SECTOR_SIZE;
            }

            destinationFile.Flush();
            destinationFile.Close();
            imgFile.Close();
        }
    }
}
