using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using OpenIII.Utils;

namespace OpenIII.GameFiles
{
    public abstract class FileSystemElement
    {
        /// <summary>
        /// Полный путь к файлу
        /// </summary>
        public string FullPath { get; protected set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Расширение файла
        /// </summary>
        public abstract string Extension { get; }

        /// <summary>
        /// Маленькая иконка
        /// </summary>
        public Bitmap SmallIcon { get => GetIcon(IconSize.Small); }

        /// <summary>
        /// Большая иконка
        /// </summary>
        public Bitmap LargeIcon { get => GetIcon(IconSize.Large); }

        public FileSystemElement() { }

        public FileSystemElement(string path)
        {
            FullPath = path;
        }


        public FileSystemElement CreateInstance(string path)
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

        /// <summary>
        /// Получить иконку
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
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
