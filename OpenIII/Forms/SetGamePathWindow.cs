using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace OpenIII
{
    public partial class SetGamePathWindow : Form
    {
        public event EventHandler<GtaPathEventArgs> OnGtaPathSet;
        public event EventHandler OnCancelled;

        public SetGamePathWindow()
        {
            InitializeComponent();
        }

        private void selectPathButtonClick(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            
            if (result == CommonFileDialogResult.Ok)
            {
                gtaPathTextBox.Text = dialog.FileName;
                check();
            }
        }

        private void check()
        {
            switch (GameManager.getGameFromPath(gtaPathTextBox.Text))
            {
                case Game.III:
                    statusLabel.ForeColor = Color.Green;
                    statusLabel.Text = "Detected GTA III";
                    nextButton.Enabled = true;
                    break;
                case Game.VC:
                    statusLabel.ForeColor = Color.Green;
                    statusLabel.Text = "Detected GTA: Vice City";
                    nextButton.Enabled = true;
                    break;
                case Game.SA:
                    statusLabel.ForeColor = Color.Green;
                    statusLabel.Text = "Detected GTA: San Andreas";
                    nextButton.Enabled = true;
                    break;
                default:
                    statusLabel.ForeColor = Color.Red;
                    statusLabel.Text = "Game not detected! Check your game directory.";
                    nextButton.Enabled = false;
                    break;
            }
        }

        private void gtaPathTextBoxTextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void nextButtonClick(object sender, EventArgs e)
        {
            OnGtaPathSet(this, new GtaPathEventArgs(gtaPathTextBox.Text));
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            OnCancelled(this, new EventArgs());
        }
    }
}
