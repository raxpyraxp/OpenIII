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
            string tablBlockName, tkeyBlockName, tdatBlockName;
            int tablBlockSize, tkeyBlockSize, tdatBlockSize;

            // Создаём блок TABL
            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tablBlockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tablBlockSize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock tablBlock = new GXTFileBlock(tablBlockName, tablBlockSize, new List<GXTFileBlockEntry>());

            // Находим элементы блока TABL
            for (int i = 0; i < tablBlockSize; i += (TABL_BLOCK_NAME_SIZE + TABL_BLOCK_SIZE_SIZE + TABL_ENTRY_OFFSET_SIZE))
            {
                tablBlock.Entries.Add(tablBlock.GetEntry(stream));
            }

            Blocks.Add(tablBlock);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tkeyBlockName = Encoding.ASCII.GetString(buf);


            // Создаём блок TKEY
            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tkeyBlockSize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock tkeyBlock = new GXTFileBlock(tkeyBlockName, tkeyBlockName, tkeyBlockSize, new List<GXTFileBlockEntry>());

            stream.Read(buf, 0, buf.Length);

            // Находим элементы блока TKEY
            for (int i = 0; i < tkeyBlockSize; i += (TABL_BLOCK_NAME_SIZE + TABL_BLOCK_SIZE_SIZE + TABL_ENTRY_OFFSET_SIZE))
            {
                tkeyBlock.Entries.Add(tkeyBlock.GetEntry(stream));
            }

            Blocks.Add(tkeyBlock);


            // Создаём блок TDAT
            //buf = new byte[4];
            //stream.Read(buf, 0, buf.Length);
            //tdatBlockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            tdatBlockSize = BitConverter.ToInt32(buf, 0);

            GXTFileBlock tdatBlock = new GXTFileBlock("", tdatBlockSize, new List<GXTFileBlockEntry>());

            string tmp = "";

            for (int i = 0; i < tkeyBlock.Entries.Count; i++)
            {
                while (Encoding.ASCII.GetString(buf) != "\0\0")
                {
                    buf = new byte[2];
                    stream.Read(buf, 0, buf.Length);
                    tmp += Encoding.ASCII.GetString(buf);
                }

                tmp = tmp.Replace("\0", "");

                tdatBlock.Entries.Add(new GXTFileBlockEntry(tmp, 0));

                tmp = "";

                buf = new byte[0];
                stream.Read(buf, 0, buf.Length);
            }

            Blocks.Add(tdatBlock);
        }
    }

    public class GXTFileBlock
    {
        // public string EntryName;
        public int Size { get; set; }
        public string Name { get; set; }
        public string EntryName { get; set; }
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

        public GXTFileBlockEntry GetEntry(Stream stream)
        {
            string blockName;
            int entryOffset;
            byte[] buf;

            buf = new byte[8];
            stream.Read(buf, 0, buf.Length);
            blockName = Encoding.ASCII.GetString(buf);

            buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            entryOffset = BitConverter.ToInt32(buf, 0);

            return new GXTFileBlockEntry(blockName, entryOffset);
        }
    }

    public class GXTFileBlockEntry
    {
        public int Offset { get; set; }
        public string Name { get; set; }

        public GXTFileBlockEntry(string name, int offset)
        {
            this.Name = name;
            this.Offset = offset;
        }
    }
}
