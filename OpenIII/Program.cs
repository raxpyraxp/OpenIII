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

            ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\Grand Theft Auto Vice City\models\gta3.img");
            //ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\SteamLibrary\steamapps\common\Grand Theft Auto San Andreas\models\gta3.img");

            Application.Run(new MainWindow(img));
        }
    }
}
