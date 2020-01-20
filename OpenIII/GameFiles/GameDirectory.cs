using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using OpenIII.Utils;

namespace OpenIII.GameFiles
{
    public class GameDirectory : FileSystemElement
    {
        /// <summary>
        /// Имя директории
        /// </summary>
        public override string Name { get => directoryInfo.Name; }

        /// <summary>
        /// Расширение директории
        /// </summary>
        public override string Extension { get => directoryInfo.Extension; }

        /// <summary>
        /// Информация о директории
        /// </summary>
        private DirectoryInfo directoryInfo;

        /// <summary>
        /// Создаёт новую директорию
        /// </summary>
        /// <param name="path"></param>
        public GameDirectory(string path) : base(path)
        {
            this.directoryInfo = new DirectoryInfo(FullPath);
        }

        public static new GameDirectory CreateInstance(string path)
        {
            return new GameDirectory(path);
        }

        public override Bitmap GetPredefinedIcon(IconSize size)
        {
            return Properties.Resources.Folder;
        }

        /// <summary>
        /// Возвращает все файлы из директории
        /// </summary>
        /// <returns></returns>
        public List<GameFile> GetFiles()
        {
            List<GameFile> gameFiles = new List<GameFile>();

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                gameFiles.Add(GameFile.CreateInstance(Path.Combine(file.DirectoryName, file.Name)));
            }

            return gameFiles;
        }

        /// <summary>
        /// Возвращает все директории из директории
        /// </summary>
        /// <returns></returns>
        public List<GameDirectory> GetDirectories()
        {
            List<GameDirectory> gameDirectories = new List<GameDirectory>();

            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                gameDirectories.Add(GameDirectory.CreateInstance(dir.FullName));
            }

            return gameDirectories;
        }

        /// <summary>
        /// Возвращает все элементы, находящиеся в директории
        /// </summary>
        /// <returns></returns>
        public List<FileSystemElement> GetContent()
        {
            List<FileSystemElement> resources = new List<FileSystemElement>();
            resources.AddRange(GetDirectories());
            resources.AddRange(GetFiles());

            return resources;
        }
    }
}
