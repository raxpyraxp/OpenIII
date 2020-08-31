using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenIII.GameFiles.GXT
{
    public class TDATEntry : GXTFileBlockEntry
    {
        /// <summary>
        /// Name of the entry
        /// </summary>
        /// <summary xml:lang="ru">
        /// Имя элемента
        /// </summary>
        public string TranslatedText { get; set; }

        public TDATEntry(Stream stream, GXTFileVersion version) : base(version)
        {
            Parse(stream);
        }

        public void Parse(Stream stream)
        {
            // III and VC both use wchar_t and UTF-16 while SA uses Windows-1252 single byte.
            int bufSize = Version == GXTFileVersion.SA ? 1 : 2;
            byte[] buf = new byte[bufSize];
            string chr = "";
            TranslatedText = "";

            while (stream.Read(buf, 0, buf.Length) != 0)
            {
                chr = Version == GXTFileVersion.SA ?
                    Encoding.ASCII.GetString(buf) :
                    Encoding.Unicode.GetString(buf);

                if (chr == "\0")
                    return;

                TranslatedText += chr;
                buf = new byte[bufSize];
            }
        }

    }
}
