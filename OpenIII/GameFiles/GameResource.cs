using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using OpenIII.Utils;

namespace OpenIII.GameFiles
{
    public abstract class GameResource
    {
        public string FullPath { get; protected set; }
        public abstract string Name { get; }
        public abstract string Extension { get; }
        public Bitmap SmallIcon { get => GetIcon(IconSize.Small); }
        public Bitmap LargeIcon { get => GetIcon(IconSize.Large); }

        public GameResource()
        {

        }

        public GameResource(string path)
        {
            FullPath = path;
        }

        public GameResource CreateInstance(string path)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return GameDirectory.CreateInstance(path);
            }
            else
            {
                return GameFile.CreateInstance(path);
            }

        }

        public Bitmap GetIcon(IconSize size)
        {
            // Uncomment this to use predefined png icons
            //return GetIcon(size);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT
                && Environment.OSVersion.Version.Major > 5)
            {
                // Obtain system icons from WinAPI on Vista+
                return IconsFetcher.GetIcon(FullPath, size);
            }
            else
            {
                // Use predefined icons from app resources on XP/Mono
                return GetPredefinedIcon(size);
            }
        }

        public abstract Bitmap GetPredefinedIcon(IconSize size);
    }
}
