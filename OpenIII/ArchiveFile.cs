using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenIII
{
    public class ArchiveFile
    {
        public String pathToFile = @"D:\Games\Grand Theft Auto Vice City\models\gta3.img";
        public String pathToFileDir = @"D:\Games\Grand Theft Auto Vice City\models\gta3.dir";

        public List<ArchiveEntry> parseFileNames()
        {
            FileStream fileDir = new FileStream(pathToFileDir, FileMode.Open, FileAccess.Read);
            List<ArchiveEntry> fileList = new List<ArchiveEntry>();
            int read = 1;

            while (read > 0)
            {
                ArchiveEntry entry = new ArchiveEntry();

                byte[] offsetBuf = new byte[4];
                read = fileDir.Read(offsetBuf, 0, offsetBuf.Length);
                entry.offset = BitConverter.ToInt32(offsetBuf, 0);

                byte[] sizeBuf = new byte[4];
                read = fileDir.Read(sizeBuf, 0, sizeBuf.Length);
                entry.size = BitConverter.ToInt32(sizeBuf, 0);

                byte[] nameBuf = new byte[24];
                read = fileDir.Read(nameBuf, 0, nameBuf.Length);
                entry.filename = Encoding.ASCII.GetString(nameBuf);

                // Remove null-terminate char
                entry.filename = entry.filename.Remove(entry.filename.IndexOf("\0"));
                
                fileList.Add(entry);
            }

            return fileList;
        }
    }
}
