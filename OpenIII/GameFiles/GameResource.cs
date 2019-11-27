using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenIII.GameFiles
{
    public enum IconSize
    {
        Small,
        Large
    }

    public abstract class GameResource
    {
        // Constants, structure and enum used for SHGetFileInfo call
        // Example from http://www.pinvoke.net/default.aspx/shell32.SHGetFileInfo
        private const int MAX_PATH = 260;
        private const int MAX_TYPE = 80;
        private const int ILD_TRANSPARENT = 0x1;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public SHFILEINFO(bool b)
            {
                hIcon = IntPtr.Zero;
                iIcon = IntPtr.Zero;
                dwAttributes = 0;
                szDisplayName = "";
                szTypeName = "";
            }
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_TYPE)]
            public string szTypeName;
        };

        [Flags]
        enum SHGFI : int
        {
            Icon = 0x000000100,
            DisplayName = 0x000000200,
            TypeName = 0x000000400,
            Attributes = 0x000000800,
            IconLocation = 0x000001000,
            ExeType = 0x000002000,
            SysIconIndex = 0x000004000,
            LinkOverlay = 0x000008000,
            Selected = 0x000010000,
            Attr_Specified = 0x000020000,
            LargeIcon = 0x000000000,
            SmallIcon = 0x000000001,
            OpenIcon = 0x000000002,
            ShellIconSize = 0x000000004,
            PIDL = 0x000000008,
            UseFileAttributes = 0x000000010,
            AddOverlays = 0x000000020,
            OverlayIndex = 0x000000040,
        }

        public string FullPath { get; }
        public string Name { get => getName(); }
        public string Extension { get => getExtension(); }
        public Bitmap SmallIcon { get => GetIcon(IconSize.Small); }
        public Bitmap LargeIcon { get => GetIcon(IconSize.Large); }

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

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
            ref SHFILEINFO psfi, uint cbFileInfo, SHGFI uFlags);


        [DllImport("User32.dll")]
        private static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("comctl32.dll", SetLastError = true)]
        private static extern IntPtr ImageList_GetIcon(IntPtr html, long i, int flags);


        public Bitmap GetIcon(IconSize size)
        {
            SHFILEINFO shfi = new SHFILEINFO();
            SHGFI flags = SHGFI.Icon | SHGFI.SysIconIndex;

            if (size == IconSize.Small)
            {
                flags = flags | SHGFI.SmallIcon;
            }
            else
            {
                flags = flags | SHGFI.LargeIcon;
            }

            IntPtr PtrIconList = SHGetFileInfo(FullPath, 0, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
            IntPtr PtrIcon = shfi.hIcon;
            long sysIconIndex = shfi.iIcon.ToInt64();

            if (PtrIconList != IntPtr.Zero && sysIconIndex != 0)
            {
                PtrIcon = ImageList_GetIcon(PtrIconList, sysIconIndex, ILD_TRANSPARENT);
            }

            Bitmap icon = Icon.FromHandle(PtrIcon).ToBitmap();
            DestroyIcon(shfi.hIcon);
            return icon;
        }

        public static BitmapSource icon_of_path_large(string FileName, bool jumbo, bool checkDisk)
        {
            SHFILEINFO shinfo = new SHFILEINFO();

            uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
            uint SHGFI_SYSICONINDEX = 0x4000;

            int FILE_ATTRIBUTE_NORMAL = 0x80;

            uint flags;
            flags = SHGFI_SYSICONINDEX;

            if (!checkDisk)  // This does not seem to work. If I try it, a folder icon is always returned.
                flags |= SHGFI_USEFILEATTRIBUTES;

            var res = SHGetFileInfo(FileName, (uint)FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);
            if (res == 0)
            {
                throw (new System.IO.FileNotFoundException());
            }
            var iconIndex = shinfo.iIcon;

            // Get the System IImageList object from the Shell:
            Guid iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

            IImageList iml;
            int size = jumbo ? SHIL_JUMBO : SHIL_EXTRALARGE;
            var hres = SHGetImageList(size, ref iidImageList, out iml); // writes iml
            //if (hres == 0)
            //{
            //    throw (new System.Exception("Error SHGetImageList"));
            //}

            IntPtr hIcon = IntPtr.Zero;
            int ILD_TRANSPARENT = 1;
            hres = iml.GetIcon(iconIndex, ILD_TRANSPARENT, ref hIcon);
            //if (hres == 0)
            //{
            //    throw (new System.Exception("Error iml.GetIcon"));
            //}

            var myIcon = System.Drawing.Icon.FromHandle(hIcon);
            var bs = bitmap_source_of_icon(myIcon);
            myIcon.Dispose();
            bs.Freeze(); // very important to avoid memory leak
            DestroyIcon(hIcon);
            CloseHandle(hIcon);

            return bs;

        }
    }
}
