using System;
using HarmonyLib;
using TheForest.Items.World;

namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(BatteryBasedLight), "Awake")]
    internal class BatteryBasedLight_Patch
    {
        private static bool Prefix(BatteryBasedLight __instance)
        {
            __instance._mainLight.range = 260f;
            __instance._mainLight.spotAngle = 135f;
            __instance._mainLight.intensity = 30f; //default 22f
            __instance._batterieCostPerSecond = 0.000833333354f;
            __instance.SetColor(__instance._torchBaseColor);

            netPlayerVis component = __instance.transform.root.GetComponent<netPlayerVis>();
            ReflectionHelper.SetPrivateField(__instance, "_vis", component);
            return false;
        }
    }
}
