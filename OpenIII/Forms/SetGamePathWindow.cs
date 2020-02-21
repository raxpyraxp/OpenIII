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
    /// <summary>
    /// Form for setting game path
    /// </summary>
    /// <summary xml:lang="ru">
    /// Форма для настройки пути к игре
    /// </summary>
    public partial class SetGamePathWindow : Form
    {
        /// <summary>
        /// Event that is emitted when game path is set
        /// </summary>
        /// <summary xml:lang="ru">
        /// Событие, вызываемое в случае когда пользователь выбрал каталог с игрой
        /// </summary>
        public event EventHandler<PathEventArgs> OnGtaPathSet;

        /// <summary>
        /// Event that is emitted when user cancelled game path setting
        /// </summary>
        /// <summary xml:lang="ru">
        /// Событие, вызываемое в случае отмены выбора каталога с игрой пользователем
        /// </summary>
        public event EventHandler OnCancelled;

        /// <summary>
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public SetGamePathWindow()
        {
            InitializeComponent();

            // Dummy events to prevent NullPointerException
            OnGtaPathSet += (s, e) => { };
            OnCancelled += (s, e) => { };
        }

        /// <summary>
        /// "..." button event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия на кнопку "..." для выбора каталога
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
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

        /// <summary>
        /// Check that the selected directory contains the supported game
        /// </summary>
        /// <summary xml:lang="ru">
        /// Проверка указанного пути на наличие поддерживаемой игры
        /// </summary>
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

        /// <summary>
        /// Text box changed event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события изменения текстового поля
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void OnGtaPathTextBoxChanged(object sender, EventArgs e)
        {
            Check();
        }

        /// <summary>
        /// Next button event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия на кнопку "Далее"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>

        private void OnNextButtonClick(object sender, EventArgs e)
        {
            OnGtaPathSet(this, new PathEventArgs(gtaPathTextBox.Text));
            Close();
        }

        /// <summary>
        /// Cancel button event handler
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обработчик события нажатия на кнопку "Отмена"
        /// </summary>
        /// <param name="sender">Component that emitted the event</param>
        /// <param name="e">Event arguments</param>
        /// <param name="sender" xml:lang="ru">Указатель на компонент, который отправил событие</param>
        /// <param name="e" xml:lang="ru">Аргументы события</param>
        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            OnCancelled(this, new EventArgs());
            Close();
        }
    }
}
