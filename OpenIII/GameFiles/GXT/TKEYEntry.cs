using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenIII.GameFiles.GXT
{
    public class TKEYEntry : GXTFileBlockEntry
    {
        public static readonly int NAME_SIZE = 8;
        public static readonly int HASH_SIZE = 4;
        public static readonly int OFFSET_SIZE = 4;

        // TODO: make it to represent entry size less sucky way
        // It is used by TKEYBlock to count keys
        // Maybe transform it to a function or a property?
        public static readonly int ENTRY_SIZE = NAME_SIZE + OFFSET_SIZE;
        public static readonly int ENTRY_SA_SIZE = HASH_SIZE + OFFSET_SIZE;

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

        public int Hash { get; set; }

        public TDATEntry AssociatedEntry { get; set; }

        public TKEYEntry(Stream stream, GXTFileVersion version) : base(version)
        {
            Parse(stream);
        }

        public void Parse(Stream stream)
        {
            byte[] buf;

            // Associated value offset
            buf = new byte[OFFSET_SIZE];
            stream.Read(buf, 0, buf.Length);
            Offset = BitConverter.ToUInt32(buf, 0);

            if (Version == GXTFileVersion.SA)
            {
                // Key name
                buf = new byte[HASH_SIZE];
                stream.Read(buf, 0, buf.Length);
                Hash = BitConverter.ToInt32(buf, 0);
            }
            else
            {
                // Key name
                buf = new byte[NAME_SIZE];
                stream.Read(buf, 0, buf.Length);
                Name = Encoding.ASCII.GetString(buf).Replace("\0", "");
            }
        }

        public void FindAssociatedValue(Stream stream, TDATBlock block)
        {
            AssociatedEntry = block.GetEntryByOffset(stream, Offset);
        }
    }
}
