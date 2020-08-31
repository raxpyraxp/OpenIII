using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles.GXT
{
    public class TDATBlock : GXTFileBlock
    {
        public TDATBlock(Stream stream, GXTFileVersion version) : base(stream, version)
        {
            Parse(stream);
        }

        public void Parse(Stream stream)
        {
            byte[] buf;

            // Block header
            buf = new byte[HEADER_SIZE];
            stream.Read(buf, 0, buf.Length);
            Header = Encoding.ASCII.GetString(buf);

            // Block size
            buf = new byte[BLOCK_SIZE_SIZE];
            stream.Read(buf, 0, buf.Length);
            Size = BitConverter.ToInt32(buf, 0);
        }

        public TDATEntry GetEntryByOffset(Stream stream, long stringOffset)
        {
            // Getting a TDAT entry containing strings
            long stringsArrayOffset = BlockOffset + HEADER_SIZE + BLOCK_SIZE_SIZE;

            // Getting a string from array
            stream.Seek(stringsArrayOffset + stringOffset, SeekOrigin.Begin);
            return new TDATEntry(stream, Version);
        }
    }
}
