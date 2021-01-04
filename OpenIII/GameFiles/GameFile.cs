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
using System.IO;
using System.Drawing;
using OpenIII.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenIII.Forms;
using System.Threading;

namespace OpenIII.GameFiles
{
    /// <summary>
    /// Source where the file comes from
    /// </summary>
    /// <summary xml:lang="ru">
    /// Источник файла
    /// </summary>
    public enum FileSource
    {
        /// <summary>
        /// File comes from the filesystem
        /// </summary>
        /// <summary xml:lang="ru">
        /// Файл находится в файловой системе
        /// </summary>
        FILESYSTEM,

        /// <summary>
        /// File comes from the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Файл находится в архиве <see cref="ArchiveFile"/>
        /// </summary>
        ARCHIVE
    }

    /// <summary>
    /// Basic class implementation for managing game files
    /// </summary>
    /// <summary xml:lang="ru">
    /// Базовый класс для работы с игровыми файлами
    /// </summary>
    public class GameFile : FileSystemElement
    {
        /// <summary>
        /// File edited flag
        /// </summary>
        /// <summary xml:lang="ru">
        /// Флаг, указывающий на то, что файл был изменён после сохранения
        /// </summary>
        public bool isFileEdited = false;

        /// <summary>
        /// <see cref="GameFile"/> size in bytes in the <see cref="ArchiveFile"/>
        /// If file is not in <see cref="ArchiveFile"/> equals 0
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер файла <see cref="GameFile"/> в архиве <see cref="ArchiveFile"/> в байтах
        /// Если файл не находится в архиве, размер равен 0
        /// </summary>
        /// <remarks>
        /// It is important to use <see cref="Length"/> because this property holds the size for archived files only
        /// </remarks>
        /// <remarks xml:lang="ru">
        /// Для определения размера настоятельно рекомендуется использовать <see cref="Length"/> так как данное свойство содержит размер архивированного файла
        /// </remarks>
        private int Size { get; }

        /// <summary>
        /// An offset of the <see cref="GameFile"/> in the <see cref="ArchiveFile"/>.
        /// If file is not in <see cref="ArchiveFile"/> equals 0
        /// </summary>
        /// <summary xml:lang="ru">
        /// Смещение файла <see cref="GameFile"/> в архиве <see cref="ArchiveFile"/>
        /// Если файл не находится в архиве, смещение равно 0
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// A <see cref="FileInfo"/> element associated with current file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Информация о текущем файле <see cref="FileInfo"/>
        /// </summary>
        private FileInfo FileInfo;

        /// <summary>
        /// A source of the current <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Источник текущего <see cref="GameFile"/>
        /// </summary>
        public FileSource Source { get; }

        /// <summary>
        /// A parent <see cref="ArchiveFile"/> of the current <see cref="GameFile"/>
        /// If <see cref="GameFile"/> is not archived, equals <see cref="null"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Родительский архив <see cref="ArchiveFile"/> текущего файла <see cref="GameFile"/>
        /// Если файл <see cref="GameFile"/> не архивирован, равен null
        /// </summary>
        public ArchiveFile ParentArchive { get; }

        /// <summary>
        /// <see cref="GameFile"/> size in bytes
        /// </summary>
        /// <summary xml:lang="ru">
        /// Размер файла <see cref="GameFile"/> в байтах
        /// </summary>
        public long Length { get => Source == FileSource.FILESYSTEM ? FileInfo.Length : Size; }

        /// <summary>
        /// The name of the <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Имя файла <see cref="GameFile"/>
        /// </summary>
        public override string Name { get => FileInfo.Name; }

        /// <summary>
        /// The extension of the <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Расширение файла <see cref="GameFile"/>
        /// </summary>
        public override string Extension { get => FileInfo.Extension; }

        /// <summary>
        /// Type name of the <see cref="GameFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Наименование типа элемента файловой системы <see cref="GameFile"/>
        /// </summary>
        public override string Type { get => Extension.ToUpper() + " file"; }

        public delegate void UpdateProgressDelegate(int percent, string description = "");

        /// <summary>
        /// Default <see cref="GameFile"/> constructor for the file in the file system
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="GameFile"/> по умолчанию для неархивированного файла
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="path" xml:lang="ru">Путь к файлу</param>
        public GameFile(string path) : base(path)
        {
            this.FileInfo = new FileInfo(FullPath);
            Source = FileSource.FILESYSTEM;
        }

        /// <summary>
        /// Default <see cref="GameFile"/> constructor for the file in the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="GameFile"/> по умолчанию для архивированного файла в <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="offset">File offset in the <see cref="ArchiveFile"/></param>
        /// <param name="offset" xml:lang="ru">Смещение файла в архиве <see cref="ArchiveFile"/></param>
        /// <param name="size">File size in bytes</param>
        /// <param name="size" xml:lang="ru">Размер файла в байтах</param>
        /// <param name="filename">File name</param>
        /// <param name="filename" xml:lang="ru">Имя файла</param>
        /// <param name="parentFile">Parent <see cref="ArchiveFile"/> where the <see cref="GameFile"/> is stored</param>
        /// <param name="parentFile" xml:lang="ru">Родительский архив <see cref="ArchiveFile"/> в котором находится файл <see cref="GameFile"/></param>
        public GameFile(int offset, int size, string filename, ArchiveFile parentFile) : base()
        {
            this.Offset = offset;
            this.Size = size;
            this.FullPath = filename;
            this.ParentArchive = parentFile;
            this.FileInfo = new FileInfo(FullPath);
            Source = FileSource.ARCHIVE;
        }

        /// <summary>
        /// Creates the handle <see cref="GameFile"/> to the file under <paramref name="path"/> in the file system
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создаёт указатель на файл <see cref="GameFile"/> по пути <paramref name="path"/>
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="path" xml:lang="ru">Путь к файлу</param>
        /// <returns>
        /// <see cref="GameFile"/> handle
        /// </returns>
        /// <returns xml:lang="ru">
        /// Указатель на файл <see cref="GameFile"/>
        /// </returns>
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

        /// <summary>
        /// Creates the handle <see cref="GameFile"/> to the file in the <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создаёт указатель на файл <see cref="GameFile"/> в архиве <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="offset">File offset in the <see cref="ArchiveFile"/></param>
        /// <param name="offset" xml:lang="ru">Смещение файла в архиве <see cref="ArchiveFile"/></param>
        /// <param name="size">File size in bytes</param>
        /// <param name="size" xml:lang="ru">Размер файла в байтах</param>
        /// <param name="filename">File name</param>
        /// <param name="filename" xml:lang="ru">Имя файла</param>
        /// <param name="parentFile">Parent <see cref="ArchiveFile"/> where the <see cref="GameFile"/> is stored</param>
        /// <param name="parentFile" xml:lang="ru">Родительский архив <see cref="ArchiveFile"/> в котором находится файл <see cref="GameFile"/></param>
        /// <returns>
        /// <see cref="GameFile"/> handle
        /// </returns>
        /// <returns xml:lang="ru">
        /// Указатель на файл <see cref="GameFile"/>
        /// </returns>
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

        /// <summary>
        /// Fetches the icon of the <see cref="GameFile"/> that is predefined for the file type in it's class
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение предопределённой в классе иконки файла <see cref="GameDirectory"/>
        /// </summary>
        /// <param name="size">Icon size</param>
        /// <param name="size" xml:lang="ru">Размер иконки</param>
        /// <returns>
        /// <see cref="Bitmap"/> icon
        /// </returns>
        /// <returns xml:lang="ru">
        /// Иконка <see cref="Bitmap"/>
        /// </returns>
        public override Bitmap GetPredefinedIcon(IconSize size)
        {
            return Properties.Resources.File;
        }

        /// <summary>
        /// Fetches the file extension by it's path without handle
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение расширения файла по пути без указателя
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="path" xml:lang="ru">Путь к файлу</param>
        /// <returns>
        /// File extension
        /// </returns>
        /// <returns xml:lang="ru">
        /// Расширение файла
        /// </returns>
        public static string GetExtension(string path)
        {
            return new FileInfo(path).Extension;
        }

        /// <summary>
        /// Gets stream to access file in filesystem or <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение потока для доступа к файлу в файловой системе или архиве <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="mode">File access mode</param>
        /// <param name="mode" xml:lang="ru">Метод доступа к файлу</param>
        /// <param name="access">File access permissions</param>
        /// <param name="access" xml:lang="ru">Разрешения доступа к файлу</param>
        /// <returns>
        /// File <see cref="Stream"/> to access the file data
        /// </returns>
        /// <returns xml:lang="ru">
        /// Поток <see cref="Stream"/> для доступа к данным из файла
        /// </returns>
        public Stream GetStream(FileMode mode, FileAccess access)
        {
            return Source == FileSource.FILESYSTEM ?
                new FileStream(FullPath, mode, access) :
                new ArchiveStream(this, mode, access);
        }

        /// <summary>
        /// Extracts <see cref="GameFile"/> from the parent <see cref="ArchiveFile"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Извлекает файл <see cref="GameFile"/> из родительского архива <see cref="ArchiveFile"/>
        /// </summary>
        /// <param name="destinationPath">File path where the file should be extracted to</param>
        /// <param name="destinationPath" xml:lang="ru">Путь к файлу, куда необходимо распаковать файл</param>
        public void Extract(String destinationPath)
        {
            ParentArchive.ExtractFile(this, destinationPath);
        }

        public void ExtractAsync(String destinationPath, CancellationToken ct, UpdateProgressDelegate callback)
        {
            ParentArchive.ExtractFileAsync(this, destinationPath, ct, callback);
        }

        /// <summary>
        /// Deletes the <see cref="GameFile"/> from the parent <see cref="ArchiveFile"/> or filesystem
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет файл <see cref="GameFile"/> из родительского архива <see cref="ArchiveFile"/> или из файловой системы
        /// </summary>
        public void Delete()
        {
            if (Source == FileSource.FILESYSTEM)
            {
                File.Delete(FullPath);
            }
            else
            {
                ParentArchive.DeleteFile(this);
            }
        }

        /// <summary>
        /// Prepares <see cref="GameFile"/> for insertion in the <see cref="ArchiveFile"/> and sets the offset for it inside the archive
        /// </summary>
        /// <summary xml:lang="ru">
        /// Подготавливает файл <see cref="GameFile"/> к добавлению в архив <see cref="ArchiveFile"/> и назначает смещение для файла в архиве
        /// </summary>
        /// <param name="offset">New file offset</param>
        /// <param name="offset" xml:lang="ru">Новое смещение для файла</param>
        public void PrepareForArchiving(int offset)
        {
            // We probably will do something there to prevent file editing
            Offset = offset;
        }

        /// <summary>
        /// Compares this <see cref="GameFile"/> to the other
        /// </summary>
        /// <summary xml:lang="ru">
        /// Сравнивает файл <see cref="GameFile"/> с другим
        /// </summary>
        /// <param name="obj">Another file</param>
        /// <param name="obj" xml:lang="ru">Другой файл</param>
        public override bool Equals(Object obj)
        {
            if (obj == null && !(obj is GameFile))
            {
                return false;
            }

            GameFile file = (GameFile)obj;

            if (file.Source != this.Source)
            {
                return false;
            }

            if (file.Source == FileSource.FILESYSTEM)
            {
                return file.FullPath == this.FullPath;
            }
            else
            {
                // Length can be different for newly added files and file in archive, but offset + name + parent archive are always equal
                // This is because we have a length in bytes for filesystem entities and a length in sectors for archived files
                return file.Name == this.Name &&
                    file.Offset == this.Offset &&
                    //file.Length == this.Length &&
                    file.ParentArchive.Equals(this.ParentArchive);
            }
        }

        /// <summary>
        /// Generated function to calculating hash code for object comparison
        /// </summary>
        /// <summary xml:lang="ru">
        /// Сгенерированная функция для вычисления хэша для сравнения объектов
        /// </summary>
        /// <returns>
        /// Hash code of the object
        /// </returns>
        /// <returns xml:lang="ru">
        /// Хэш объекта
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 1062659230;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + Offset.GetHashCode();
            hashCode = hashCode * -1521134295 + Source.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ArchiveFile>.Default.GetHashCode(ParentArchive);
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}