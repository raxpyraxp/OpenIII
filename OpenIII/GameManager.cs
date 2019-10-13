using System.IO;

namespace OpenIII
{
    public enum Game
    {
        Unknown,
        III,
        VC,
        SA
    }

    class GameManager
    {
        public static Game getGameFromPath(string path)
        {
            if (isGta3(path))
                return Game.III;

            if (isGtaVC(path))
                return Game.VC;

            if (isGtaSA(path))
                return Game.SA;

            return Game.Unknown;
        }

        public static bool isGta3(string path)
        {
            return File.Exists(path + @"\gta3.exe");
        }

        public static bool isGtaVC(string path)
        {
            return File.Exists(path + @"\gta-vc.exe");
        }

        public static bool isGtaSA(string path)
        {
            return File.Exists(path + @"\gta-sa.exe");
        }
    }
}
