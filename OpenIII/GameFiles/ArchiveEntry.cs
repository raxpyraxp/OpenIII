using System;

namespace OpenIII.GameFiles
{
    public class ArchiveEntry
    {
        public int offset { get; }
        public int size { get; }
        public string filename { get; }
        public ArchiveFile parentFile { get; }

        public ArchiveEntry(int offset, int size, string filename, ArchiveFile parentFile)
        {
            this.offset = offset;
            this.size = size;
            this.filename = filename;
            this.parentFile = parentFile;
        }

        public void extract(String destinationPath)
        {
            parentFile.extractFile(this, destinationPath);
        }
    }
}
