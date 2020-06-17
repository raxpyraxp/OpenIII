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

using OpenIII.GameFiles;
using System;
using System.Collections.Generic;
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

            CurrentFile.isFileEdited = true;
        }

        /// <summary>
        /// Removes lines with the list of <paramref name="indexes"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаление строк с индексами из списка <paramref name="indexes"/>
        /// </summary>
        /// <param name="indexes">List of indexes of the lines that needs to be deleted</param>
        /// <param name="indexes" xml:lang="ru">Список индексов строк, которые необходимо удалить</param>
        private void DeleteRow(List<int> indexes)
        {
            if (DataGridView.SelectedCells.Count > 0)
            {
                CurrentFile.Items.RemoveAt(DataGridView.SelectedCells[0].RowIndex);
            }

            CurrentFile.isFileEdited = true;
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
            try
            {
                CurrentFile.SaveFile();
            } catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

            if (e.FormattedValue.ToString().Length > FXTFileItem.MAX_KEY_LENGTH && e.ColumnIndex == 0) {
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
            CurrentFile.isFileEdited = true;
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
            List<int> indexes = new List<int>();

            for (var i = 0; i < DataGridView.SelectedCells.Count; i++)
            {
                indexes.Add(DataGridView.SelectedCells[i].RowIndex);
            }

            DeleteRow(indexes);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FXTEditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentFile.isFileEdited)
            {
                DialogResult dialogResult = MessageBox.Show("You have unsaved changes. Do you want to save them?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        CurrentFile.SaveFile();
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
