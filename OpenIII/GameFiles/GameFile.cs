using System;
using System.IO;
using System.Drawing;
using OpenIII.Utils;

namespace OpenIII.GameFiles
{
    public enum FileSource
    {
        FILESYSTEM,
        ARCHIVE
    }

    public class GameFile : FileSystemElement
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public override string Name { get => fileInfo.Name; }

        /// <summary>
        /// Расширение файла
        /// </summary>
        public override string Extension { get => fileInfo.Extension; }

        /// <summary>
        /// Размер файла в байтах
        /// </summary>
        public long Length { get => fileInfo.Length; }

        /// <summary>
        /// Отступ от нулевого байта в родительском файле в байтах
        /// </summary>
        public int Offset { get; }
        public int Size { get; }

        /// <summary>
        /// Родительский архив
        /// </summary>
        public ArchiveFile ParentArchive { get; }

        public FileSource Source { get; }

        /// <summary>
        /// Информация о файле
        /// </summary>
        private FileInfo fileInfo;

        public GameFile(string path) : base(path)
        {
            this.fileInfo = new FileInfo(FullPath);
            Source = FileSource.FILESYSTEM;
        }

        public GameFile(int offset, int size, string filename, ArchiveFile parentFile) : base()
        {
            this.Offset = offset;
            this.Size = size;
            this.FullPath = filename;
            this.ParentArchive = parentFile;
            this.fileInfo = new FileInfo(FullPath);
            Source = FileSource.ARCHIVE;
        }

        public static new GameFile CreateInstance(string path)
        {
            switch (GetExtension(path))
            {
                case ".img":
                    return ArchiveFile.CreateInstance(path);
                default:
                    return new GameFile(path);
            }
        }

        public static GameFile CreateInstance(int offset, int size, string filename, ArchiveFile parentFile)
        {
            switch (GetExtension(filename))
            {
                case ".img":
                    return ArchiveFile.CreateInstance(offset, size, filename, parentFile);
                default:
                    return new GameFile(offset, size, filename, parentFile);
            }
        }

        public override Bitmap GetPredefinedIcon(IconSize size)
        {
            return Properties.Resources.File;
        }

        public static string GetExtension(string path)
        {
            return new FileInfo(path).Extension;
        }

        public Stream GetStream(FileMode mode, FileAccess access)
        {
            return Source == FileSource.FILESYSTEM ?
                new FileStream(FullPath, mode, access) :
                new ArchiveStream(this, mode, access);
        }

        /// <summary>
        /// Экспортирует файл из родительского архива
        /// </summary>
        /// <param name="destinationPath"></param>
        public void Extract(String destinationPath)
        {
            ParentArchive.ExtractFile(this, destinationPath);
        }
    }
}