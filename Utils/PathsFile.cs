using System.IO;

namespace QuickThrow.Utils
{
    public static class PathsFile
    {
        public static readonly string LogFilePath = Path.Combine(
            BepInEx.Paths.PluginPath, "QuickThrow", "QuickThrow_log.txt");

        public static readonly string DebugPath = Path.Combine(
            BepInEx.Paths.PluginPath, "QuickThrow", "debug.cfg");
    }
}