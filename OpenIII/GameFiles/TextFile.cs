using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenIII.GameFiles
{
    public enum SectionName
    {
        /// <summary>
        /// Секция статических объектов
        /// </summary>
        objs,
        
        /// <summary>
        /// Секция объектов, зависящих от игрового времени
        /// </summary>
        tobj,
        
        /// <summary>
        /// Секция путей транспорта
        /// </summary>
        path,
        
        /// <summary>
        /// Секция двумерных эффектов
        /// </summary>
        twoDfx,

        /// <summary>
        /// Секция цветов автомобилей
        /// </summary>
        col,

        /// <summary>
        /// Секция статических объектов
        /// </summary>
        inst,
        
        /// <summary>
        /// Секция пикапов
        /// </summary>
        pick,
        
        /// <summary>
        /// Секция специальных зон
        /// </summary>
        cull,

        /// <summary>
        /// Секция анимированных объектов
        /// </summary>
        anim,

        /// <summary>
        /// Секция объектов оружия
        /// </summary>
        weap,

        /// <summary>
        /// Секция педов
        /// </summary>
        peds,

        /// <summary>
        /// Секция транспортных средств
        /// </summary>
        cars,

        /// <summary>
        /// Секция объектов для катсцен
        /// </summary>
        heir,

        /// <summary>
        /// Секция текстур
        /// </summary>
        txdp,

        /// <summary>
        /// Секция звуков
        /// </summary>
        auzo,

        /// <summary>
        /// Секция зон для модификации параметров погоды
        /// </summary>
        tcyc,

        /// <summary>
        /// Секция уникальных прыжков
        /// </summary>
        jump,

        /// <summary>
        /// Секция НЕ ИСПОЛЬЗУЕТСЯ
        /// </summary>
        enex,

        /// <summary>
        /// Секция гаражей
        /// </summary>
        grge,

        /// <summary>
        /// Секция НЕ ИСПОЛЬЗУЕТСЯ
        /// </summary>
        mult,

        /// <summary>
        /// Секция объектов окклюзии
        /// </summary>
        occl,

        /// <summary>
        /// Секция зон
        /// </summary>
        zone
    }

    /// <summary>
    /// Секция текстового файла
    /// </summary>
    public class TextFileSection
    {
        private static string Name;

        /// <summary>
        /// Список элементов секции текстового файла
        /// </summary>
        private static List<TextFileSectionItem> Items;

        public TextFileSection(string sectionName)
        {
            Name = sectionName;
        }

        public static void AddItem(TextFileSectionItem parameter) { }

        public static void RemoveItem(TextFileSectionItem parameter) { }
    }


    /// <summary>
    /// Элемент секции текстового файла
    /// </summary>
    public class TextFileSectionItem
    {
        public TextFileSectionItem(string param1, string param2, string param3) { }

        public TextFileSectionItem(string param1, string param2, string param3, string param4) { }

        public TextFileSectionItem(string param1, string param2, string param3, string param4, string param5) { }
    }


    public class TextFile : GameFile
    {
        /// <summary>
        /// Список секций текстового файла
        /// </summary>
        private static List<TextFileSection> Sections;

        public TextFile(string filePath) : base(filePath) { }

        /// <summary>
        /// Создаёт экземпляр
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static new TextFile CreateInstance(string path)
        {
            return new TextFile(path);
        }

        /// <summary>
        /// Возвращает содержимое текстового файла
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public List<String[]> ParseData(string path)
        {
            string iteratableLine;
            List<string> listOfRows = new List<string>();
            List<string[]> listOfArrayParams = new List<string[]>();
            StreamReader Reader = null;

            if (File.Exists(path))
            {
                Reader = new StreamReader(path);
            }
            else
            {
                throw new Exception("File does not exist!");
            }

            // определяем тип обрабатываемого файла
            while ((iteratableLine = Reader.ReadLine()) != null)
            {
                String[] namesOfSectionItem = { "ENDWEAPONDATA", "end", "objs", "tobj", "path", "2dfx", "col", "inst", "pick", "cull", "anim", "weap", "path", "peds", "cars", "hier" };
                
                if (iteratableLine != "" && Char.IsLetterOrDigit(iteratableLine[0]) && Array.IndexOf(namesOfSectionItem, iteratableLine) < 0)
                {
                    iteratableLine = Regex.Replace(iteratableLine, @",", " ");
                    listOfRows.Add(Regex.Replace(iteratableLine, @"\s+", ","));
                }
            }

            listOfRows.ForEach(delegate (String row)
            {
                listOfArrayParams.Add(row.Split(','));
            });

            return listOfArrayParams;
        }
    }
}