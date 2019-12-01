using System;
using System.Windows.Forms;
using OpenIII.GameFiles;

namespace OpenIII
{
    public class OpenIIIApplicationContext : ApplicationContext
    {
        public FileBrowserWindow fileBrowserWindow;
        public SetGamePathWindow setGamePathWindow;

        public OpenIIIApplicationContext()
        {
            if (Properties.Settings.Default.GTAPath != "")
            {
                showFileBrowserWindow();
            }
            else
            {
                showGamePathWindow();
            }
        }

        public void showFileBrowserWindow()
        {
            // TODO: We must obtain a path of the game and pass it to the form instead of archive
            //ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\Grand Theft Auto Vice City\models\gta3.img");
            //ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\SteamLibrary\steamapps\common\Grand Theft Auto San Andreas\models\gta3.img");

            GameDirectory dir = new GameDirectory(Properties.Settings.Default.GTAPath);

            fileBrowserWindow = new FileBrowserWindow(dir);
            fileBrowserWindow.FormClosed += onClosed;
            fileBrowserWindow.Show();
        }

        public void showGamePathWindow()
        {
            setGamePathWindow = new SetGamePathWindow();
            setGamePathWindow.FormClosed += onClosed;
            setGamePathWindow.OnCancelled += onClosed;
            setGamePathWindow.OnGtaPathSet += onGtaPathSet;
            setGamePathWindow.Show();
        }

        public void onClosed(object s, EventArgs e)
        {
            Application.Exit();
        }

        public void onGtaPathSet(object s, GtaPathEventArgs e)
        {
            Properties.Settings.Default.GTAPath = e.Path;
            Properties.Settings.Default.Save();
            setGamePathWindow.FormClosed -= onClosed;
            setGamePathWindow.Close();
            showFileBrowserWindow();
        }
    }
}
