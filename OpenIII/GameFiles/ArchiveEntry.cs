using System;

namespace OpenIII.GameFiles
{
    public class ArchiveEntry
    {
        public int Offset { get; }
        public int Size { get; }
        public string Filename { get; }
        public ArchiveFile ParentFile { get; }

        public ArchiveEntry(int offset, int size, string filename, ArchiveFile parentFile)
        {
            this.Offset = offset;
            this.Size = size;
            this.Filename = filename;
            this.ParentFile = parentFile;
        }

        public void extract(String destinationPath)
        {
            ParentFile.ExtractFile(this, destinationPath);
        }
    }
}
