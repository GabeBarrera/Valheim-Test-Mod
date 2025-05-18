using BepInEx;              // Base classes & attributes (BaseUnityPlugin, [BepInPlugin]) that let your DLL register as a mod and hook into Unity’s life-cycle.
using BepInEx.Logging;      // A simple wrapper (ManualLogSource) around Unity’s console so you can spit text into the BepInEx window and log file.
using HarmonyLib;           // The hot-swappable IL patcher used by almost every Unity mod. Lets you intercept, prefix, postfix, or replace any method in the game.
using System.Reflection;    // Run-time metadata access—needed here to grab Assembly.GetExecutingAssembly() so Harmony can scan all classes for [HarmonyPatch] attributes.

namespace TestMod
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)] // Declares the DLL as a BepInEx plugin with an immutable GUID (com.example.GUID), a friendly name, and a version.
    public class Main : BaseUnityPlugin // Inheriting BaseUnityPlugin gives you Unity callbacks like Awake(), Update(), etc.
    {
        const string pluginGUID = "com.example.GUID";
        const string pluginName = "MyPlugin";
        const string pluginVersion = "1.0.0";

        private readonly Harmony HarmonyInstance = new Harmony(pluginGUID); // Creates a Harmony engine tagged with your GUID so it doesn’t collide with other mods.

        public static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(pluginName); // Custom logger so you can write logger.LogInfo("text") and have it appear in the BepInEx console/log.

        public void Awake() // Awake() runs once when the DLL loads.
        {
            Main.logger.LogInfo("Thank you for using my mod!");

            Assembly assembly = Assembly.GetExecutingAssembly();
            HarmonyInstance.PatchAll(assembly); // Calls PatchAll: Harmony scans every class in this assembly for [HarmonyPatch] attributes and applies them—no manual registration needed.
        }

        // More Code Here!
        [HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
        public static class Patch_Player_UseStamina
        {
            private static bool Prefix()
            {
                return false;
            }
        }

    }
}

