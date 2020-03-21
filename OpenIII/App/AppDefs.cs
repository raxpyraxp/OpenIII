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

namespace OpenIII
{
    /// <summary>
    /// Common application functions that doesn't fit in any other classes
    /// </summary>
    /// <summary xml:lang="ru">
    /// Общие функции приложения которые не могут являться частью другого класса
    /// </summary>
    public static class AppDefs
    {
        /// <summary>
        /// Terminates the program
        /// </summary>
        /// <summary xml:lang="ru">
        /// Закрывает приложение
        /// </summary>
        public static void ExitFromApp()
        {
            // TODO: Do we really need that? We're reimplementing the
            // "Application.Exit" method here and nothing else
            Application.Exit();
        }
    }
}