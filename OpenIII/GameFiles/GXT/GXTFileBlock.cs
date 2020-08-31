using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenIII.GameFiles.GXT
{
    /// <summary>
    /// An implementation for managing GXT blocks
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с блоками GXT
    /// </summary>
    public abstract class GXTFileBlock
    {
        public static readonly int HEADER_SIZE = 4;
        public static readonly int BLOCK_SIZE_SIZE = 4;

        /// <summary>
        /// Block header
        /// </summary>
        /// <summary xml:lang="ru">
        /// Заголовок блока
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Size of the block
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер блока
        /// </summary>
        public int Size { get; set; }

        public long BlockOffset { get; protected set; }

        public GXTFileVersion Version { get; }

        /// <summary>
        /// List of child <see cref="GXTFileBlockEntry"/> entries
        /// </summary>
        /// <summary xml:lang="ru">
        /// Список дочерних элементов <see cref="GXTFileBlockEntry"/>
        /// </summary>
        //public List<GXTFileBlockEntry> Entries { get; set; }

        public GXTFileBlock(Stream stream, GXTFileVersion version)
        {
            BlockOffset = stream.Position;
            Version = version;
        }
    }
}
