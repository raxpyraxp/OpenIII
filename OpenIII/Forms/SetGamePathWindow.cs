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
        public event EventHandler<PathEventArgs> OnGtaPathSet;
        public event EventHandler OnCancelled;

        public SetGamePathWindow()
        {
            InitializeComponent();
        }

        private void SelectPathButtonClick(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            
            if (result == CommonFileDialogResult.Ok)
            {
                gtaPathTextBox.Text = dialog.FileName;
                Check();
            }
        }

        private void Check()
        {
            switch (GameManager.GetGameFromPath(gtaPathTextBox.Text))
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

        private void OnGtaPathTextBoxChanged(object sender, EventArgs e)
        {
            Check();
        }

        private void OnNextButtonClick(object sender, EventArgs e)
        {
            OnGtaPathSet(this, new PathEventArgs(gtaPathTextBox.Text));
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            OnCancelled(this, new EventArgs());
        }
    }
}
