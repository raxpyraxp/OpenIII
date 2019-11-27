using System;
using System.Windows.Forms;
using OpenIII.GameFiles;

namespace OpenIII
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OpenIIIApplicationContext context = new OpenIIIApplicationContext();
            Application.Run(context);
        }
    }
}
