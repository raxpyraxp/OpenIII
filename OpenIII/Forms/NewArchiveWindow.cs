using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenIII.Forms
{
    public partial class NewArchiveWindow : Form
    {
        /// <summary>
        /// Event that is emitted when file path is set
        /// </summary>
        /// <summary xml:lang="ru">
        /// Событие, вызываемое в случае когда пользователь подтвердил название файла
        /// </summary>
        public event EventHandler<PathEventArgs> OnPathSet;

        public NewArchiveWindow()
        {
            InitializeComponent();

            // Dummy events to prevent NullPointerException
            OnPathSet += (s, e) => { };
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!nameTextBox.Text.ToLower().EndsWith(".img"))
            {
                nameTextBox.Text += ".img";
            }

            OnPathSet(this, new PathEventArgs(nameTextBox.Text));
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = nameTextBox.Text != "";
        }
    }
}
