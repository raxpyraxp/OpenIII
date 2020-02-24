using OpenIII.GameFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenIII.Forms
{
    /// <summary>
    /// Editor window for files in FXT format (CLEO text dictionaries)
    /// </summary>
    /// <summary xml:lang="ru">
    /// Форма для редактирования файлов в формате FXT (словари текстовых строк для CLEO)
    /// </summary>
    public partial class FXTEditorWindow : BaseWindow
    {
        /// <summary>
        /// File edited flag
        /// </summary>
        /// <summary xml:lang="ru">
        /// Флаг, указывающий на то, что файл был изменён после сохранения
        /// </summary>
        private bool isFileEdited = false;

        /// <summary>
        /// Standard header for each FXT file
        /// </summary>
        /// <summary xml:lang="ru">
        /// Стандартная строка заголовка с которой начинается каждый FXT файл
        /// </summary>
        private const string Headers = "# Hello!";

        /// <summary>
        /// Current opened file which user is editing
        /// </summary>
        /// <summary xml:lang="ru">
        /// Текущий открытый файл с которым работает пользователь
        /// </summary>
        private FXTFile CurrentFile;

        /// <summary>
        /// FXT Editor window singleton
        /// </summary>
        /// <summary xml:lang="ru">
        /// Синглтон для формы FXT редактора
        /// </summary>
        private static FXTEditorWindow instance;

        /// <summary>
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public FXTEditorWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create the instance of this form if no other instances created and return it
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создать инстанс формы если он ещё не создан и вернуть его
        /// </summary>
        /// <returns>
        /// Current form instance
        /// </returns>
        /// <returns xml:lang="ru">
        /// Текущий инстанс формы
        /// </returns>
        public static FXTEditorWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new FXTEditorWindow();
            }

            return instance;
        }

        /// <summary>
        /// Adds new string line
        /// </summary>
        /// <summary xml:lang="ru">
        /// Добавление новой строки
        /// </summary>
        /// <param name="key">Key for the line. Max: 8 chars, capital latin letters or numbers or underscore</param>
        /// <param name="key" xml:lang="ru">
        /// Ключ для строки. Максимальная длина: 8 символов. Допускаются только заглавные латинские символы, цифры
        /// и нижнее подчёркивание
        /// </param>
        /// <param name="value">
        /// The string itself. Can use symbols and formatting in compliance with https://gtamods.com/wiki/GXT#Character_maps
        /// </param>
        /// <param name="value" xml:lang="ru">
        /// Строка, которая будет отображаться в игре. В строке могут содержаться символы и форматирование, которые
        /// описаны здесь: https://gtamods.com/wiki/GXT#Character_maps
        /// </param>
        private void AddRow(string key, string value)
        {
            CurrentFile.Items.Add(new FXTFileItem(key, value));
        }

        /// <summary>
        /// Removes selected string line
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаление выделенной строки
        /// </summary>
        private void DeleteRow()
        {
            if (DataGridView.SelectedCells.Count > 0)
            {
                CurrentFile.Items.RemoveAt(DataGridView.SelectedCells[0].RowIndex);
            }
        }

        /// <summary>
        /// Removes the line under the <paramref name="index"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаление строки с индексом <paramref name="index"/>
        /// </summary>
        /// <param name="index">Index of the line that needs to be deleted</param>
        /// <param name="index" xml:lang="ru">Индекс строки, которую необходимо удалить</param>
        private void DeleteRow(uint index)
        {
            CurrentFile.Items.RemoveAt((int)index);
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
        public void OpenFile(FXTFile file)
        {
            file.ParseData();
            CurrentFile = file;
            SetWindowTitle($"{(CurrentFile.Name != null ? CurrentFile.Name : "")} — {Text}");
            DataGridView.DataSource = CurrentFile.Items;
        }

        /// <summary>
        /// Save menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Сохранить..."
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string data = CurrentFile.DataToString();

            try
            {
                if (isFileEdited == true)
                {
                    StreamWriter streamWriter = new StreamWriter(CurrentFile.GetStream(FileMode.Create, FileAccess.Write));
                    streamWriter.WriteLine(Headers);
                    streamWriter.Write(data);
                    streamWriter.Close();
                } else
                {
                    MessageBox.Show("File wasn't changed!");
                    return;
                }
            } catch (Exception exception)
            {
                MessageBox.Show("Can't save the file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("File saved successsfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            isFileEdited = false;
        }

        /// <summary>
        /// Exit menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Выйти"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (isFileEdited == true)
            {
                DialogResult dialogResult = MessageBox.Show("Some changes wasn't saved. Do you really want to close window?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        CloseWindow();
                    break;
                    
                    case DialogResult.No:
                        return;
                    break;
                }
            }
            else
            {
                CloseWindow();
            }
        }

        /// <summary>
        /// <see cref="DataGridView"/> validation event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события запроса проверки введённых данных в <see cref="DataGridView"/>
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView.Rows[e.RowIndex].ErrorText = "";

            if (e.FormattedValue.ToString().Length > 7 && e.ColumnIndex == 0) {
                e.Cancel = true;
                DataGridView.Rows[e.RowIndex].ErrorText = "Key's name length can't be greater than 7!";
            }
        }

        /// <summary>
        /// <see cref="DataGridView"/> cell value changed event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события изменения ячейки <see cref="DataGridView"/>
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isFileEdited = true;
        }

        /// <summary>
        /// Add row menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Добавить строку"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRow(null, null);
            isFileEdited = true;
        }

        /// <summary>
        /// Delete row menu item event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия пункта меню "Удалить строку"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRow();
            isFileEdited = true;
        }
    }
}
