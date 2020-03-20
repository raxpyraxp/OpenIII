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

using System.IO;

namespace OpenIII
{
    public enum Game
    {
        Unknown,
        III,
        VC,
        SA
    }

    class GameManager
    {
        public static Game GetGameFromPath(string path)
        {
            if (IsGta3(path))
                return Game.III;

            if (IsGtaVC(path))
                return Game.VC;

            if (IsGtaSA(path))
                return Game.SA;

            return Game.Unknown;
        }

        public static bool IsGta3(string path)
        {
            return File.Exists(Path.Combine(path, "gta3.exe"));
        }

        public static bool IsGtaVC(string path)
        {
            return File.Exists(Path.Combine(path, "gta-vc.exe"));
        }

        public static bool IsGtaSA(string path)
        {
            return File.Exists(Path.Combine(path, "gta-sa.exe"));
        }
    }
}
