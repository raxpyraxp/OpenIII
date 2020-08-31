using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenIII.GameFiles.GXT
{
    public class TABLEntry : GXTFileBlockEntry
    {
        public static readonly int NAME_SIZE = 8;
        public static readonly int OFFSET_SIZE = 4;
        public static readonly int ENTRY_SIZE = NAME_SIZE + OFFSET_SIZE;

        /// <summary>
        /// Offset of the entry in block
        /// </summary>
        /// <summary xml:lang="ru">
        /// Смещение элемента в блоке
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Name of the entry
        /// </summary>
        /// <summary xml:lang="ru">
        /// Имя элемента
        /// </summary>
        public string Name { get; set; }

        public TKEYBlock ChildBlock { get; set; }

        public TABLEntry(Stream stream, GXTFileVersion version) : base(version)
        {
            Parse(stream);
        }

        public void Parse(Stream stream)
        {
            byte[] buf;

            // Key name
            buf = new byte[NAME_SIZE];
            stream.Read(buf, 0, buf.Length);
            Name = Encoding.ASCII.GetString(buf).Replace("\0", "");

            // Associated TABL block offset
            buf = new byte[OFFSET_SIZE];
            stream.Read(buf, 0, buf.Length);
            Offset = BitConverter.ToUInt32(buf, 0);
        }

        public void FindAssociatedBlock(Stream stream)
        {
            stream.Seek(Offset, SeekOrigin.Begin);

            // Last parameter is set if TKEY name is MAIN. This is important for TKEY parsing
            // since the block is slightly different than others
            ChildBlock = new TKEYBlock(stream, Name == "MAIN", Version);
        }
    }
}
