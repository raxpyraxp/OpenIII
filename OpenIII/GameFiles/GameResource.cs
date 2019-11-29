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
        public string FullPath { get; }
        public string Name { get => getName(); }
        public string Extension { get => getExtension(); }
        public Bitmap SmallIcon { get => getIcon(FullPath, IconSize.Small); }
        public Bitmap LargeIcon { get => getIcon(FullPath, IconSize.Large); }

        public GameResource(string path)
        {
            FullPath = path;
        }

        public GameResource createInstance(string path)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return GameDirectory.createInstance(path);
            }
            else
            {
                return GameFile.createInstance(path);
            }

        }

        public abstract string getName();

        public abstract string getExtension();

        public static Bitmap getIcon(string path, IconSize size)
        {
            // Uncomment this to use predefined png icons
            /*if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                return Properties.Resources.Folder;
            else
                return Properties.Resources.File;*/

            if (Environment.OSVersion.Platform == PlatformID.Win32NT
                && Environment.OSVersion.Version.Major > 5)
            {
                // Obtain system icons from WinAPI on Vista+
                return IconsFetcher.GetIcon(path, size);
            }
            else
            {
                // Use predefined icons from app resources on XP/Mono
                if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                    return Properties.Resources.Folder;
                else
                    return Properties.Resources.File;
            }
        }
    }
}
