using System;
using System.Windows.Forms;
using OpenIII.GameFiles;

namespace OpenIII
{
    public class OpenIIIApplicationContext : ApplicationContext
    {
        public MainWindow mainWindow;
        //public SetGamePathWindow setGamePathWindow;

        public OpenIIIApplicationContext()
        {
            // TODO: This is temporary, because form for setting game path is not ready.
            // When GTAPath changing will be implemented, uncomment this and delete last showFileBrowserWindow call

            /*if (Properties.Settings.Default.GTAPath != "")
            {
                showFileBrowserWindow();
            }
            else
            {
                showGamePathWindow();
            }*/

            showFileBrowserWindow();
        }

        public void showFileBrowserWindow()
        {
            // TODO: We must obtain a path of the game and pass it to the form instead of archive
            ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\Grand Theft Auto Vice City\models\gta3.img");
            //ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\SteamLibrary\steamapps\common\Grand Theft Auto San Andreas\models\gta3.img");

            mainWindow = new MainWindow(img);
            mainWindow.FormClosed += onFileBrowserWindowClosed;
            mainWindow.Show();
        }

        public void showGamePathWindow()
        {
            // TODO: Implement game path window setting
            /*
            setGamePathWindow = new SetGamePathWindow();
            setGamePathWindow.FormClosed += onGamePathWindowClosed;
            setGamePathWindow.Show();
            */
        }

        public void onFileBrowserWindowClosed(object s, EventArgs e)
        {
            Application.Exit();
        }

        public void onGamePathWindowClosed(object s, EventArgs e)
        {
            showFileBrowserWindow();
        }
    }
}
