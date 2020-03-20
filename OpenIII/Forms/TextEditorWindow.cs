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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenIII
{
    /// <summary>
    /// Text editor window for editing text files
    /// </summary>
    /// <summary xml:lang="ru">
    /// Форма для редактирования текстовых файлов
    /// </summary>
    public partial class TextEditorWindow : Form
    {
        /// <summary>
        /// Text editor window singleton
        /// </summary>
        /// <summary xml:lang="ru">
        /// Синглтон для формы текстового редактора
        /// </summary>
        private static TextEditorWindow instance;

        /// <summary>
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public TextEditorWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets <see cref="FileContent"/> text area to the specified <paramref name="text"/>
        /// </summary>
        /// <summary xml:lang="ru">
        /// Установить текст <paramref name="text"/> в текстовое поле <see cref="FileContent"/>
        /// </summary>
        /// <param name="text">Text that needs to be set</param>
        /// <param name="text" xml:lang="ru">Текст, который будет установлен</param>
        public void SetTextArea(string text)
        {
            FileContent.Text = text;
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
        public static TextEditorWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new TextEditorWindow();
            }

            return instance;
        }
    }
}
