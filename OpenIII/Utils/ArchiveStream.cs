using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenIII.GameFiles;

namespace OpenIII.Utils
{
    class ArchiveStream : FileStream
    {
        public GameFile File { get; }
        public override long Length { get => File.Length; }

        public override long Position {
            get => base.Position - File.Offset;
            set => base.Position = value + File.Offset;
        }

        public ArchiveStream(GameFile gameFile, FileMode mode, FileAccess access)
            : base(gameFile.ParentArchive.FullPath, mode, access)
        {
            File = gameFile;
            base.Seek(File.Offset, SeekOrigin.Begin);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return base.Seek(offset + File.Offset, origin);
        }
    }
}
