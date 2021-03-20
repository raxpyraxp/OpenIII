using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenIII.GameFiles
{
    public class ConfigFile : GameFile
    {
        public static List<string> SectionNames = new List<string> {
            "objs", "tobj", "hier", "cars", "peds", "path", "2dfx", "weap", "anim", "txdp"
        };

        public static List<string> ExcludedSynbols = new List<string>
        {
            "end", "#", ";"
        };

        /// <summary>
        /// Список секций разных видов объектов
        /// </summary>
        public List<ConfigSection> ConfigSections = new List<ConfigSection>();

        public ConfigFile(string filePath) : base(filePath) { }

        /// <summary>
        /// Очищает строку с параметрами от ненужных символов и возвращает список параметров
        /// </summary>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static List<string> CleanParams(List<string> listParams)
        {
            List<string> result = new List<string>();

            foreach (string param in listParams)
            {
                result.Add(param.Replace("\"", "").Trim());
            }

            return result;
        }

        /// <summary>
        /// Формирует читаемое для игры содержмое файла из секций и их элементов
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string buf = null;

            foreach (ConfigSection configSection in this.ConfigSections)
            {
                buf += configSection.ToString() + "\r\n";
            }

            return buf;
        }
    }

    /// <summary>
    /// Секция файла настроек
    /// </summary>
    public class ConfigSection
    {
        /// <summary>
        /// Название секции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Элементы секции
        /// </summary>
        public List<ConfigRow> ConfigRows = new List<ConfigRow>();

        public ConfigSection(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Формирует строку из всех эелментов секции
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var rows = string.Join<ConfigRow>("\r\n", this.ConfigRows.ToArray());

            return this.Name + "\r\n" + rows + "\r\n" + "end";
        }
    }


    /// <summary>
    /// Элемент секции файла настроек
    /// </summary>
    public class ConfigRow
    {
        /// <summary>
        /// Создает объект типа, соответсвующего контексту из списка переданных ему параметров
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ConfigRow Parse(List<string> parameters)
        {
            var props = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var type = prop.PropertyType;
                var val = Convert.ChangeType(parameters[i], type);
                prop.SetValue(this, val, null);
            }

            return this;
        }

        /// <summary>
        /// Создаёт строку из всех параметров, разделённых запятыми
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Object[] result = this.GetType()
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Select(field => field.GetValue(this))
                             .ToArray();

            // TODO: Заменить после переобъявления get-методов у полей Corona и Shadow
            for (var i = 0; i < result.Length; i++)
            {
                switch (result[i])
                {
                    case "coronastar":
                        result[i] = "\"coronastar\"";
                        break;
                    case "shad_exp":
                        result[i] = "\"shad_exp\"";
                        break;
                }
            }
            return string.Join(", ", result);
        }
    }
}
