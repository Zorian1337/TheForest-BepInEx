using System;
using HarmonyLib;
using TheForest.Items.World;
using UnityEngine;

namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(BurnableCloth), "Awake")]
    internal class BurnableCloth_Patch
    {

        private static bool Prefix(BurnableCloth __instance)
        {
            Renderer component = __instance.GetComponent<Renderer>();
            Material sharedMaterial = component.sharedMaterial;

            ReflectionHelper.SetPrivateField(__instance, "_normalMat", sharedMaterial);

            component.enabled = false;
            __instance.enabled = false;


            if (__instance._weaponFireSpawn != null) __instance._weaponFireSpawn.transform.parent = __instance.transform.parent;

            __instance._lightingDuration = 1.4f;
            __instance._burnDuration = 120f;
            __instance._firelightIntensityRatio = 3.2f;
            return false;
        }
    }
}
