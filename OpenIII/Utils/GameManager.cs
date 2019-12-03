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
        public static Game GetGameFromPath(string path)
        {
            if (IsGta3(path))
                return Game.III;

            if (IsGtaVC(path))
                return Game.VC;

            if (IsGtaSA(path))
                return Game.SA;

            return Game.Unknown;
        }

        public static bool IsGta3(string path)
        {
            return File.Exists(Path.Combine(path, "gta3.exe"));
        }

        public static bool IsGtaVC(string path)
        {
            return File.Exists(Path.Combine(path, "gta-vc.exe"));
        }

        public static bool IsGtaSA(string path)
        {
            return File.Exists(Path.Combine(path, "gta-sa.exe"));
        }
    }
}
