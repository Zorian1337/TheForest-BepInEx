using System;
using HarmonyLib;
using TheForest.Items.World;
using UnityEngine;

namespace Forest_Mod.Patches
{
    // Token: 0x02000008 RID: 8
    [HarmonyPatch(typeof(BurnableCloth), "Awake")]
    internal class BurnableCloth_Patch
    {
        // Token: 0x0600000D RID: 13 RVA: 0x000023A0 File Offset: 0x000005A0
        private static bool Prefix(BurnableCloth __instance)
        {
            Renderer component = __instance.GetComponent<Renderer>();
            Material sharedMaterial = component.sharedMaterial;
            ReflectionHelper.SetPrivateField(__instance, "_normalMat", sharedMaterial);
            component.enabled = false;
            __instance.enabled = false;
            bool flag = __instance._weaponFireSpawn != null;
            if (flag)
            {
                __instance._weaponFireSpawn.transform.parent = __instance.transform.parent;
            }
            __instance._lightingDuration = 1.4f;
            __instance._burnDuration = 120f;
            __instance._firelightIntensityRatio = 3.2f;
            return false;
        }
    }
}
