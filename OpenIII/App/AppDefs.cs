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
        public static void ExitFromApp()
        {
            // TODO: Do we really need that? We're reimplementing the
            // "Application.Exit" method here and nothing else
            Application.Exit();
        }
    }
}