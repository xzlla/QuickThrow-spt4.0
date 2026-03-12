using Comfort.Common;
using HarmonyLib;
using EFT;
using QuickThrow.Utils;
using UnityEngine;

namespace QuickThrow.Patch
{
    [HarmonyPatch(typeof(Player))]
    internal class QuickThrowGrenade
    {
        [HarmonyPrefix]
        [HarmonyPatch("Proceed", typeof(ThrowWeapItemClass), typeof(Callback<IHandsThrowController>), typeof(bool))]
        public static bool Prefix(Player __instance, ThrowWeapItemClass throwWeap, Callback<IHandsThrowController> callback)
        {
            if (Plugin.DisableFastGrenade.Value)
                return true;
            if (!__instance.IsYourPlayer)
                return true;
            if (Input.GetKey(Plugin.KeyboardBindingOrigine.Value.MainKey))
                return true;
            
            __instance.Proceed(throwWeap,
                new Callback<GInterface206>(result =>
                {
                    QuickThrowLogger.Log($"[QuickThrowGrenade] Proceed (QuickUse) result = {result}");
                }), false);
            return false;
        }
        
        [HarmonyPatch(typeof(Player.BaseGrenadeHandsController), "vmethod_2")]
        public class ForceLowThrow
        {
            [HarmonyPrefix]
            public static void Prefix(
                ref float timeSinceSafetyLevelRemoved,
                ref bool lowThrow,
                Player.BaseGrenadeHandsController __instance)
            {
                if (Plugin.DisableFastGrenade.Value)
                    return;
                    
                var playerField = AccessTools.Field(typeof(Player.BaseGrenadeHandsController), "_player");
                if (playerField?.GetValue(__instance) is not Player player)
                    return;

                if (__instance is Player.QuickGrenadeThrowHandsController && player.IsYourPlayer)
                {
                    if (!Input.GetKey(Plugin.KeyboardBindingShort.Value.MainKey))
                        return;

                    lowThrow = true;
                    QuickThrowLogger.Log($"[QuickThrowGrenade] Forced low throw (QuickThrow) | timeSinceSafetyLevelRemoved={timeSinceSafetyLevelRemoved}, lowThrow={lowThrow}");
                }
                else
                {
                    QuickThrowLogger.Log($"[QuickThrowGrenade] Normal throw | timeSinceSafetyLevelRemoved={timeSinceSafetyLevelRemoved}, lowThrow={lowThrow}");
                }
            }
        }
    }
}
