using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles.GXT
{
    public class TKEYBlock : GXTFileBlock
    {
        public static readonly int NAME_SIZE = 8;

        /// <summary>
        /// Name of the block
        /// </summary>
        /// <summary xml:lang="ru">
        /// Название блока
        /// </summary>
        public string Name { get; set; }

        public TKEYEntry[] Entries { get; set; }

        public bool IsMain { get; private set; }

        public TDATBlock DataBlock { get; private set; }

        public TKEYBlock(Stream stream, bool isMain, GXTFileVersion version) : base(stream, version)
        {
            this.IsMain = isMain;
            Parse(stream);
            ParseEntries(stream);
            ParseTDATBlock(stream);
            FindAssociatedValues(stream);
        }

        public void Parse(Stream stream)
        {
            byte[] buf;

            // Block name
            if (IsMain)
            {
                Name = "MAIN";
            }
            else
            {
                buf = new byte[NAME_SIZE];
                stream.Read(buf, 0, buf.Length);
                Name = Encoding.ASCII.GetString(buf).Replace("\0", "");
            }

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
            int entriesCount = Size / (Version == GXTFileVersion.SA ? TKEYEntry.ENTRY_SA_SIZE : TKEYEntry.ENTRY_SIZE);
            Entries = new TKEYEntry[entriesCount];

            for (int i = 0; i < entriesCount; i++)
            {
                Entries[i] = new TKEYEntry(stream, Version);
            }
        }

        public void ParseTDATBlock(Stream stream)
        {
            // Each TDAT block placed right after each TKEY block
            long TDATOffset = BlockOffset + Size + HEADER_SIZE + BLOCK_SIZE_SIZE;

            // Main TKEY block is smaller, so we'll take different size
            TDATOffset += IsMain ? 0 : NAME_SIZE;

            stream.Seek(TDATOffset, SeekOrigin.Begin);

            DataBlock = new TDATBlock(stream, Version);
        }

        public void FindAssociatedValues(Stream stream)
        {
            foreach (TKEYEntry entry in Entries)
            {
                entry.FindAssociatedValue(stream, DataBlock);
            }
        }
    }
}
