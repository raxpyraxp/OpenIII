using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenIII.Utils
{
    public enum IconSize
    {
        Small,
        Large
    }

    class IconsFetcher
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

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
            ref SHFILEINFO psfi, uint cbFileInfo, SHGFI uFlags);


        [DllImport("User32.dll")]
        private static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("comctl32.dll", SetLastError = true)]
        private static extern IntPtr ImageList_GetIcon(IntPtr html, long i, int flags);

        public static Bitmap GetIcon(string filePath, IconSize size)
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

            IntPtr PtrIconList = SHGetFileInfo(filePath, 0, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
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
    }
}
