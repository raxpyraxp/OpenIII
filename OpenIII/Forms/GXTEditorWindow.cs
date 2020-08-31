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

using OpenIII.Forms;
using OpenIII.GameFiles.GXT;
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

            /*foreach (GXTFileBlockEntry item in CurrentFile.Blocks[0].Entries)
            {
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.Tag = item;
                listView1.Items.Add(listItem);
            }

            dataGridView1.ColumnCount = 2;

            ListViewItem selected = listView1.Items[0];

            SelectDictionary((GXTFileBlockEntry)selected.Tag);*/

            /*for (int i = 0; i < CurrentFile.Blocks[1].Entries.Count; i++)
            {
                dataGridView1.Rows.Add(CurrentFile.Blocks[1].Entries[i].Name, null);
            }*/

            dataGridView1.ColumnCount = 2;

            switch (CurrentFile.FileVersion)
            {
                case GXTFileVersion.III:
                    SelectDictionary((TKEYBlock)CurrentFile.MainBlock);
                    break;
                case GXTFileVersion.VC:
                case GXTFileVersion.SA:
                    foreach (TABLEntry entry in ((TABLBlock)CurrentFile.MainBlock).Entries)
                    {
                        ListViewItem listItem = new ListViewItem(entry.Name);
                        listItem.Tag = entry;
                        listView1.Items.Add(listItem);
                    }

                    ListViewItem selected = listView1.Items[0];
                    TABLEntry selectedEntry = (TABLEntry)selected.Tag;
                    SelectDictionary(selectedEntry.ChildBlock);
                    break;
                default:
                    break;
            }
        }

        public void SelectDictionary(TKEYBlock tkey)
        {
            dataGridView1.Rows.Clear();

            foreach (TKEYEntry entry in tkey.Entries)
            {
                dataGridView1.Rows.Add(
                    CurrentFile.FileVersion == GXTFileVersion.SA ? "0x" + entry.Hash.ToString("X8") : entry.Name,
                    entry.AssociatedEntry.TranslatedText);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                int selectedIndex = listView1.SelectedItems[0].Index;
                ListViewItem selected = listView1.Items[selectedIndex];
                TABLEntry selectedEntry = (TABLEntry)selected.Tag;
                SelectDictionary(selectedEntry.ChildBlock);
            }
        }
    }
}
