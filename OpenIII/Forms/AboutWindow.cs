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
        }

        private void emailLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = "mailto:" + ((LinkLabel)sender).Text;
            System.Diagnostics.Process.Start(link);
        }

        private void warrantyLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-3.0.html#section15");
        }

        private void licenseLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-3.0.html");
        }
    }
}
