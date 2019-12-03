using System;
using System.Collections.Generic;
using System.IO;
using OpenIII.Utils;

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

        public abstract List<GameResource> GetFileList();

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

        public static new ArchiveFile CreateInstance(int offset, int size, string filename, ArchiveFile parentFile)
        {
            throw new NotImplementedException();
        }

        public void ExtractFile(GameFile entry, string destination)
        {
            ArchiveStream entryStream = new ArchiveStream(entry, FileMode.Open, FileAccess.Read);
            FileStream destinationFile = new FileStream(destination, FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[SECTOR_SIZE];

            while (entryStream.Position < entryStream.Length)
            {
                int read = entryStream.Read(buf, 0, SECTOR_SIZE);
                destinationFile.Write(buf, 0, read);
            }

            destinationFile.Flush();
            destinationFile.Close();
            entryStream.Close();
        }

        public void DeleteFile(GameFile entry) { }
        
        public void ReplaceFile(GameFile oldEntry, GameFile newEntry) { }

        public void RenameFile(GameFile entry, string newName) { }

    }
}
