using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenIII.GameFiles.GXT
{
    /// <summary>
    /// An implementation for managing GXT entries
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для работы с элементами GXT
    /// </summary>
    public abstract class GXTFileBlockEntry
    {
        public GXTFileVersion Version { get; }

        public GXTFileBlockEntry(GXTFileVersion version)
        {
            Version = version;
        }

        /// <summary>
        /// Constructior for GXT element
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор для создания GXT элемента
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <param name="offset">Entry offset in block</param>
        /// <param name="name" xml:lang="ru">Название элемента</param>
        /// <param name="offset" xml:lang="ru">Смещение элемента в блоке</param>
        //public GXTFileBlockEntry(string name, int offset)
        //{
        //this.Name = name;
        //this.Offset = offset;
        //}
    }

}
