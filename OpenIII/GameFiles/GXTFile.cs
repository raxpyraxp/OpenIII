using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenIII.GameFiles
{
    public class GXTFile : GameFile
    {
        public const int TABL_BLOCK_NAME_SIZE = 4;
        public const int TABL_BLOCK_SIZE_SIZE = 4;

        public const int TABL_ENTRY_NAME_SIZE = 4;
        public const int TABL_ENTRY_OFFSET_SIZE = 4;

        public List<GXTFileBlock> Blocks = new List<GXTFileBlock>();

        public GXTFile(string filePath) : base(filePath) { }

        public void ParseData()
        {
            Stream stream = this.GetStream(FileMode.Open, FileAccess.Read);

            byte[] buf;
            string blockName;
            int tablSize;
            int offset;

            // Создаём блок TABL

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            blockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tablSize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock block = new GXTFileBlock(blockName, tablSize, new List<GXTFileBlockEntry>());

            // Находим элементы блока TABL

            for (int i = 0; i < tablSize; i += (TABL_BLOCK_NAME_SIZE + TABL_BLOCK_SIZE_SIZE + TABL_ENTRY_OFFSET_SIZE))
            {
                buf = new byte[8];
                stream.Read(buf, 0, buf.Length);
                blockName = Encoding.ASCII.GetString(buf);

                buf = new byte[4];
                stream.Read(buf, 0, buf.Length);
                offset = BitConverter.ToInt32(buf, 0);

                block.Entries.Add(new GXTFileBlockEntry(offset, blockName));
            }

            Blocks.Add(block);

            string blockEntryName;
            int tkeySize;

            /*
            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            blockName = Encoding.ASCII.GetString(buf);
            */

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            blockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tkeySize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock tkeyBlock = new GXTFileBlock(blockName, blockName, tkeySize, new List<GXTFileBlockEntry>());

            for (int i = 0; i < tkeySize; i += (TABL_BLOCK_NAME_SIZE + TABL_BLOCK_SIZE_SIZE + TABL_ENTRY_OFFSET_SIZE))
            {
                buf = new byte[4];
                stream.Read(buf, 0, buf.Length);
                offset = BitConverter.ToInt32(buf, 0);

                buf = new byte[8];
                stream.Read(buf, 0, buf.Length);
                blockName = Encoding.ASCII.GetString(buf);

                tkeyBlock.Entries.Add(new GXTFileBlockEntry(offset, blockName));
            }

            Blocks.Add(tkeyBlock);
        }
    }

    public class GXTFileBlock
    {
        // public string EntryName;
        public string Name { get; set; }
        public string EntryName { get; set; }
        public int Size { get; set; }
        public List<GXTFileBlockEntry> Entries { get; set; }

        /// <summary>
        /// Инициализирует блок типа TABL
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="entries"></param>
        public GXTFileBlock(string name, int size, List<GXTFileBlockEntry> entries)
        {
            this.Name = name;
            this.Size = size;
            this.Entries = entries;
        }

        /// <summary>
        /// Инициализирует блок типа TKEY
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="entries"></param>
        public GXTFileBlock(string entryName, string name, int size, List<GXTFileBlockEntry> entries)
        {
            this.EntryName = entryName;
            this.Name = name;
            this.Size = size;
            this.Entries = entries;
        }
    }

    public class GXTFileBlockEntry
    {
        public int Offset { get; set; }
        public string Name { get; set; }

        public GXTFileBlockEntry(int offset, string name)
        {
            this.Offset = offset;
            this.Name = name;
        }
    }
}
