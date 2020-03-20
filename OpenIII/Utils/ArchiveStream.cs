/*
 *  This file is a part of OpenIII project, the GTA modding tool.
 *  
 *  Copyright (C) 2019-2020 Savelii Morozov (Prographer)
 *  Email: morozov.salevii@gmail.com
 *  
 *  Copyright (C) 2019-2020 Sergey Filatov (raxp)
 *  Email: raxp.worm202@gmail.com
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

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
