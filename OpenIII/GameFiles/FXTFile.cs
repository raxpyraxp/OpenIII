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
using System.IO;

namespace OpenIII.GameFiles
{
    public class FXTFile : GameFile
    {
        public BindingList<FXTFileItem> Items = new BindingList<FXTFileItem>();

        public FXTFile(string filePath) : base(filePath) { }

        public BindingList<FXTFileItem> ParseData()
        {
            string lineIterator = null;
            StreamReader Reader = new StreamReader(this.FullPath);
            BindingList<FXTFileItem> data = new BindingList<FXTFileItem>();

            while ((lineIterator = Reader.ReadLine()) != null)
            {

                if (lineIterator != "" && Char.IsLetterOrDigit(lineIterator[0]))
                {
                    data.Add(new FXTFileItem(
                        lineIterator.Substring(0, lineIterator.IndexOf(" ")), lineIterator.Substring(lineIterator.IndexOf(" ") + 1))
                    );
                }
            }

            Items = data;

            Reader.Close();

            return data;
        }

        public string DataToString()
        {
            string buf = null;

            foreach (FXTFileItem item in this.Items)
            {
                buf += item.Key + " " + item.Value + '\n';
            }

            return buf;
        }

        public void AddItem(string key, string value)
        {
            Items.Add(new FXTFileItem(key, value));
        }
    }

    public class FXTFileItem
    {
        private const int maxKeyLength = 8;

        public string Key { get; set; }
        public string Value { get; set; }

        public FXTFileItem(string key, string value) {
            this.Key = key;
            this.Value = value;
        }
    }
}
