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

using System.Windows.Forms;

namespace OpenIII.Forms
{
    /// <summary>
    /// "About" form
    /// </summary>
    /// <summary xml:lang="ru">
    /// Форма окна "О программе"
    /// </summary>
    public partial class AboutWindow : Form
    {
        /// <summary>
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
            versionLabel.Text = string.Format("Version {0}", Application.ProductVersion);
        }

        /// <summary>
        /// Email link click event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия на ссылку электронной почты
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void emailLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = "mailto:" + ((LinkLabel)sender).Text;
            System.Diagnostics.Process.Start(link);
        }

        /// <summary>
        /// Warranty "See details" link click event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия на ссылку "Подробнее" рядом с условиями гарантии
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void warrantyLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-3.0.html#section15");
        }

        /// <summary>
        /// Copyright "See details" link click event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия на ссылку "Подробнее" рядом с условиями использования
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void licenseLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-3.0.html");
        }

        private void githubLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/worm202/OpenIII");
        }
    }
}
