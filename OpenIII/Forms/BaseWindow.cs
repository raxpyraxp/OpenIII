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
        /// Form constructor
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор формы
        /// </summary>
        public BaseWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes this window
        /// </summary>
        /// <summary xml:lang="ru">
        /// Закрывает текущее окно
        /// </summary>
        public void CloseWindow()
        {
            // TODO: Do we really need that? We're reimplementing the
            // "Form.Close" method here and nothing else
            Close();
        }
    }
}
