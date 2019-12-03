using System;
using System.Collections.Generic;
using System.IO;

namespace OpenIII.GameFiles
{
    public enum ArchiveFileVersion
    {
        Unknown,
        V1,
        V2
    }

    public abstract class ArchiveFile : GameFile
    {
        public static int SECTOR_SIZE = 2048;

        public abstract ArchiveFileVersion ImgVersion { get; }
        public abstract long TotalFiles { get; }

        public ArchiveFile(string filePath) : base(filePath) { }

        public abstract List<ArchiveEntry> GetFileList();

        public static new ArchiveFile CreateInstance(string path)
        {
            ArchiveFileVersion version = ArchiveFileV2.ReadVersionFromArchive(path);

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

        public void ExtractFile(ArchiveEntry entry, string destination)
        {
            FileStream imgFile = new FileStream(FullPath, FileMode.Open, FileAccess.Read);
            FileStream destinationFile = new FileStream(destination, FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[SECTOR_SIZE];
            int bytesLeft = entry.Size;

            imgFile.Seek(entry.Offset, SeekOrigin.Begin);
            
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

        public void DeleteFile(ArchiveEntry entry) { }
        
        public void ReplaceFile(ArchiveEntry oldEntry, ArchiveEntry newEntry) { }

        public void RenameFile(ArchiveEntry entry, string newName) { }

    }
}
