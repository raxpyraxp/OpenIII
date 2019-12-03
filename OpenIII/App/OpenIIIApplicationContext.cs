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
                ShowFileBrowserWindow();
            }
            else
            {
                ShowGamePathWindow();
            }
        }

        public void ShowFileBrowserWindow()
        {
            // TODO: We must obtain a path of the game and pass it to the form instead of archive
            //ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\Grand Theft Auto Vice City\models\gta3.img");
            //ArchiveFile img = ArchiveFile.createInstance(@"D:\Games\SteamLibrary\steamapps\common\Grand Theft Auto San Andreas\models\gta3.img");

            GameDirectory dir = new GameDirectory(Properties.Settings.Default.GTAPath);

            fileBrowserWindow = FileBrowserWindow.GetInstance();
            fileBrowserWindow.OpenDir(dir);
            fileBrowserWindow.FormClosed += OnClosed;
            fileBrowserWindow.Show();
        }

        public void ShowGamePathWindow()
        {
            setGamePathWindow = new SetGamePathWindow();
            setGamePathWindow.FormClosed += OnClosed;
            setGamePathWindow.OnCancelled += OnClosed;
            setGamePathWindow.OnGtaPathSet += OnGtaPathSet;
            setGamePathWindow.Show();
        }

        public void OnClosed(object s, EventArgs e)
        {
            Application.Exit();
        }

        public void OnGtaPathSet(object s, PathEventArgs e)
        {
            Properties.Settings.Default.GTAPath = e.Path;
            Properties.Settings.Default.Save();
            setGamePathWindow.FormClosed -= OnClosed;
            setGamePathWindow.Close();
            ShowFileBrowserWindow();
        }
    }
}
