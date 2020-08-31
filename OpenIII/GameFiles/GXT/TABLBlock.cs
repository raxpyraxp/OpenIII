using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenIII.GameFiles.GXT
{
    public class TABLBlock : GXTFileBlock
    {
        public TABLEntry[] Entries { get; set; }

        public TABLBlock(Stream stream, GXTFileVersion version) : base(stream, version)
        {
            Parse(stream);
            ParseEntries(stream);
            FindAssociatedBlocks(stream);
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

        private void ParseEntries(Stream stream)
        {
            int entriesCount = Size / TABLEntry.ENTRY_SIZE;
            Entries = new TABLEntry[entriesCount];

            for (int i = 0; i < entriesCount; i++)
            {
                Entries[i] = new TABLEntry(stream, Version);
            }
        }

        private void FindAssociatedBlocks(Stream stream)
        {
            foreach (TABLEntry entry in Entries)
            {
                entry.FindAssociatedBlock(stream);
            }
        }
    }
}
