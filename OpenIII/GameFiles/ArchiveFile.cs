using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles
{
    public class ArchiveFile : GameFile
    {
        public static int SECTOR_SIZE = 2048;

        public ArchiveFile(string filePath) : base(filePath) {}

        private string getDirFile()
        {
            // Just replace extension to dir in the original file path
            return this.filePath.Remove(this.filePath.Length - 3) + "dir";
        }

        public List<ArchiveEntry> parseFileNames()
        {
            FileStream fileDir = new FileStream(getDirFile(), FileMode.Open, FileAccess.Read);
            List<ArchiveEntry> fileList = new List<ArchiveEntry>();
            int read = 1;

            while (read > 0)
            {
                byte[] offsetBuf = new byte[4];
                read = fileDir.Read(offsetBuf, 0, offsetBuf.Length);
                int offset = BitConverter.ToInt32(offsetBuf, 0) * SECTOR_SIZE;

                byte[] sizeBuf = new byte[4];
                read = fileDir.Read(sizeBuf, 0, sizeBuf.Length);
                int size = BitConverter.ToInt32(sizeBuf, 0) * SECTOR_SIZE;

                byte[] nameBuf = new byte[24];
                read = fileDir.Read(nameBuf, 0, nameBuf.Length);
                string filename = Encoding.ASCII.GetString(nameBuf);

                // Remove null-terminate char
                filename = filename.Remove(filename.IndexOf("\0"));
                
                fileList.Add(new ArchiveEntry(offset, size, filename, this));
            }

            fileDir.Close();

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
