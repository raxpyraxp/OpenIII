using OpenIII.Forms;
using System.Windows.Forms;

namespace OpenIII.GameFiles
{
    /// <summary>
    /// Editor window for files in GXT format
    /// </summary>
    /// <summary xml:lang="ru">
    /// Форма для редактирования файлов в формате GXT
    /// </summary>
    public partial class GXTEditorWindow : BaseWindow
    {
        /// <summary>
        /// Current opened file which user is editing
        /// </summary>
        /// <summary xml:lang="ru">
        /// Текущий открытый файл с которым работает пользователь
        /// </summary>
        private GXTFile CurrentFile;

        /// <summary>
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public GXTEditorWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open the specified <see cref="FXTFile"/>, parse data from it and pass the
        /// parsed data to the <see cref="DataGridView"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Открыть указанный файл <see cref="FXTFile"/>, получить данные из него и
        /// показать полученные данные в <see cref="DataGridView"/>
        /// </summary>
        /// <param name="file">File to be opened</param>
        /// <param name="file" xml:lang="ru">Файл который необходимо открыть</param>
        public void OpenFile(GXTFile file)
        {
            file.ParseData();

            CurrentFile = file;

            foreach (GXTFileBlockEntry item in CurrentFile.Blocks[0].Entries)
            {
                listBox1.Items.Add(item.Name);
            }

            dataGridView1.ColumnCount = 1;

            foreach (GXTFileBlockEntry item in CurrentFile.Blocks[1].Entries)
            {
                dataGridView1.Rows.Add(item.Name);
            }
        }
    }
}
