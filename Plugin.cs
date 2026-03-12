using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using QuickThrow.Utils;
using UnityEngine;

namespace QuickThrow
{
    [BepInPlugin("com.spt.QuickThrow", "QuickThrow", "2.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LOGSource;
        public static ConfigEntry<bool> DisableFastGrenade;
        public static ConfigEntry<KeyboardShortcut> KeyboardBindingOrigine;
        public static ConfigEntry<KeyboardShortcut> KeyboardBindingShort;

        
        private static Harmony HarmonyInstance { get; set; }
        
        private void Awake()
        {
            KeyboardBindingOrigine = Config.Bind(
                "Fast grenade",
                "Cancel fast grenade",
                new KeyboardShortcut(KeyCode.LeftShift),
                "Keyboard to cancel fast grenade"
            );
            KeyboardBindingShort = Config.Bind(
                "Fast grenade",
                "Short grenqde",
                new KeyboardShortcut(KeyCode.LeftControl),
                "Forces low-throw behavior for quick grenade throws when using the fast grenade feature"
            );
            DisableFastGrenade = Config.Bind(
                "Fast grenade",
                "disabled fast grenade", 
                false,
                "Enables or disables fast grenade"
            );


            InitializeFiles();
            InitializeLogger();
            InitializeActionLogger();
            SetupHarmonyPatches();
        }

        private void InitializeFiles()
        {
            if (!File.Exists(PathsFile.DebugPath))
            {
                File.WriteAllText(PathsFile.DebugPath, "false");
            }
        
            if (!File.Exists(PathsFile.LogFilePath))
            {
                File.WriteAllText(PathsFile.LogFilePath, "");
            }
        
            Logger.LogInfo("Log dans le fichier :" + PathsFile.LogFilePath);
        }

        private void InitializeLogger()
        {
            LOGSource = Logger;
        }

        private static void InitializeActionLogger()
        {
            QuickThrowLogger.Init(EnumLoggerMode.DirectWrite);
            Application.quitting += QuickThrowLogger.OnApplicationQuit;
        }
        
        private static void SetupHarmonyPatches()
        {
            HarmonyInstance = new Harmony("com.spt.SmartAction");
            HarmonyInstance.PatchAll();
        }
       
    }
}