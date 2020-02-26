using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenIII.Forms
{
    /// <summary>
    /// Basic form that implements standard features for all other forms
    /// </summary>
    /// <summary xml:lang="ru">
    /// Базовая форма, которая содержит базовые функции для всех других форм
    /// </summary>
    public partial class BaseWindow : Form
    {
        /// <summary>
        /// File edited flag
        /// </summary>
        /// <summary xml:lang="ru">
        /// Флаг, указывающий на то, что файл был изменён после сохранения
        /// </summary>
        protected bool isFileEdited = false;

        /// <summary>
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public BaseWindow()
        {
            InitializeComponent();
        }

        public void SetWindowTitle(string title)
        {
            Text = title;
        }

        /// <summary>
        /// Closes this window
        /// </summary>
        /// <summary xml:lang="ru">
        /// Закрывает текущее окно
        /// </summary>
        public void CloseWindow(CancelEventArgs e)
        {
            if (isFileEdited == true)
            {
                DialogResult dialogResult = MessageBox.Show("Some changes wasn't saved. Do you really want to close window?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        Close();
                        break;

                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                    break;
                }
            }
        }

        public void CloseWindow()
        {
            if (isFileEdited == true)
            {
                DialogResult dialogResult = MessageBox.Show("Some changes wasn't saved. Do you really want to close window?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        Close();
                        break;

                    case DialogResult.No:
                        return;
                    break;
                }
            }
        }
    }
}
