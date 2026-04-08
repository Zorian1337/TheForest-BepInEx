using System;
using HarmonyLib;
using TheForest.Items.World;

namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(BatteryBasedLight), "Awake")]
    public class BatteryBasedLight_Patch
    {
        private static bool Prefix(BatteryBasedLight __instance)
        {
            __instance._mainLight.range = 260f;
            __instance._mainLight.spotAngle = 135f;
            __instance.SetIntensity(10f); //was 22f looked bright but not as deadly, 30f was bright asf
            __instance._batterieCostPerSecond = GetBatteryCostPerSecond(60);
            __instance.SetColor(__instance._torchBaseColor);

            netPlayerVis component = __instance.transform.root.GetComponent<netPlayerVis>();
            ReflectionHelper.SetPrivateField(__instance, "_vis", component);
            return false;
        }

        public static float GetBatteryCostPerSecond(int ActiveTimeMinutes)
        {
            int ActiveTimeSeconds = ActiveTimeMinutes * 60;
            return 100f / ActiveTimeSeconds;
        }
    }
}
