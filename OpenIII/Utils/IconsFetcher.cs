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
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;

namespace OpenIII.Utils
{
    /// <summary>
    /// Enum that defines icon sizes
    /// </summary>
    /// <summary xml:lang="ru">
    /// Перечисление, определяющее допустимые размеры иконок
    /// </summary>
    public enum IconSize
    {
        Small,
        Large
    }

    /// <summary>
    /// A class that is used to obtain system associated icons
    /// This class is WinAPI-dependent, it will not work in onther OS
    /// </summary>
    /// <summary xml:lang="ru">
    /// Класс для получения ассоциированной иконки из системы
    /// Класс завязан на WinAPI и не может быть использован на другой ОС
    /// </summary>
    class IconsFetcher
    {
        // Constants, structure and enum used for SHGetFileInfo call
        // Example from http://www.pinvoke.net/default.aspx/shell32.SHGetFileInfo

        /// <summary>
        /// File path length limit
        /// </summary>
        /// <summary xml:lang="ru">
        /// Максимальное количество символов в пути файла
        /// </summary>
        private const int MAX_PATH = 260;

        /// <summary>
        /// File extension length limit
        /// </summary>
        /// <summary xml:lang="ru">
        /// Максимальное количество символов в расширении файла
        /// </summary>
        private const int MAX_TYPE = 80;

        /// <summary>
        /// A flag that determines if transparent icon should be fetched
        /// </summary>
        /// <summary xml:lang="ru">
        /// Флаг, указывающий на то, что получаемая иконка должна быть прозрачной
        /// </summary>
        private const int ILD_TRANSPARENT = 0x1;

        /// <summary>
        /// A directory type number
        /// </summary>
        /// <summary xml:lang="ru">
        /// Номер типа, указывающий на то, что элемент файловой системы является директорией
        /// </summary>
        private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;

        /// <summary>
        /// Contains information about a file object
        /// </summary>
        /// <summary xml:lang="ru">
        /// Содержит информацию о файле
        /// </summary>
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

        /// <summary>
        /// Enum that defines different file attributes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Перечисление атрибутов файла
        /// </summary>
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

        /// <summary>
        /// Retrieves information about an object in the file system, such as a file, folder, directory, or drive root
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получить информацию об объекте файловой системы такого как файл, директория или диск
        /// </summary>
        /// <param name="pszPath">A pointer to a null-terminated string of maximum length <see cref="MAX_PATH"/> that contains the path and file name. Both absolute and relative paths are valid</param>
        /// <param name="dwFileAttributes">A combination of one or more file attribute flags (FILE_ATTRIBUTE_ values as defined in Winnt.h). If <paramref name="uFlags"/> does not include the SHGFI_USEFILEATTRIBUTES flag, this parameter is ignored</param>
        /// <param name="psfi">Pointer to a <see cref="SHFILEINFO"/> structure to receive the file information.</param>
        /// <param name="cbFileInfo">The size, in bytes, of the <see cref="SHFILEINFO"/> structure pointed to by the <paramref name="psfi"/> parameter.</param>
        /// <param name="uFlags">The flags that specify the file information to retrieve</param>
        /// <param name="pszPath" xml:lang="ru">Указатель на строку с максимальной длиной <see cref="MAX_PATH"/> содержащий путь и имя файла. Могут быть использованы как абсолютный, так и относительный путь</param>
        /// <param name="dwFileAttributes" xml:lang="ru">Один или несколько флагов FILE_ATTRIBUTE_ из Winnt.h. Если <paramref name="uFlags"/> не включает в себя SHGFI_USEFILEATTRIBUTES, данный параметр игнорируется</param>
        /// <param name="psfi" xml:lang="ru">Указатель на структуру <see cref="SHFILEINFO"/> для получения информации о файле</param>
        /// <param name="cbFileInfo" xml:lang="ru">Размер структуры <see cref="SHFILEINFO"/> в байтах указатель на которую передан в параметре <paramref name="psfi"/></param>
        /// <param name="uFlags" xml:lang="ru">Флаги, указывающие на информацию, которую нужно получить</param>
        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
            ref SHFILEINFO psfi, uint cbFileInfo, SHGFI uFlags);

        /// <summary>
        /// Destroys an icon and frees any memory the icon occupied
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет иконку и освобождает занятую память
        /// </summary>
        /// <param name="hIcon">A handle to the icon to be destroyed. The icon must not be in use</param>
        /// <param name="hIcon" xml:lang="ru">Указатель на иконку, которую необходимо удалить. Иконка не должна использоваться перед удалением</param>
        [DllImport("User32.dll")]
        private static extern int DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// Creates an icon from an image and mask in an image list
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создать иконку из картинки и маски в списке изображений
        /// </summary>
        /// <param name="html">A handle to the image list</param>
        /// <param name="html" xml:lang="ru">Указатель на список изображений</param>
        /// <param name="i">An index of the image</param>
        /// <param name="i" xml:lang="ru">Индекс изображения</param>
        /// <param name="flags">A combination of flags that specify the drawing style. For a list of values, see the description of the fStyle parameter of the ImageList_Draw function</param>
        /// <param name="flags" xml:lang="ru">Флаги, определяющие стиль рисования. Подходящие значения можно найти в описании параметра fStyle функции ImageList_Draw</param>
        [DllImport("comctl32.dll", SetLastError = true)]
        private static extern IntPtr ImageList_GetIcon(IntPtr html, long i, int flags);

        /// <summary>
        /// Gets icon of the desired <paramref name="size"/> for file under <paramref name="filePath"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получить иконку размера <paramref name="size"/> для файла по пути <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <param name="filePath" xml:lang="ru">Путь к файлу</param>
        /// <param name="size">Icon size</param>
        /// <param name="size" xml:lang="ru">Размер иконки</param>
        public static Bitmap GetIcon(string filePath, IconSize size)
        {
            SHFILEINFO shfi = new SHFILEINFO();
            SHGFI flags = SHGFI.Icon | SHGFI.SysIconIndex | SHGFI.UseFileAttributes;
            uint fileAttributes = 0x0;

            if (size == IconSize.Small)
            {
                flags = flags | SHGFI.SmallIcon;
            }
            else
            {
                flags = flags | SHGFI.LargeIcon;
            }

            if (Directory.Exists(filePath) &&
                (File.GetAttributes(filePath) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                fileAttributes = fileAttributes | FILE_ATTRIBUTE_DIRECTORY;
            }

            IntPtr PtrIconList = SHGetFileInfo(filePath, fileAttributes, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
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
