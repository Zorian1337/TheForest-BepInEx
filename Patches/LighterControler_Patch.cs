using System;
using HarmonyLib;
using TheForest.Items.Special;
using UnityEngine;

namespace Forest_Mod.Patches
{
    // Token: 0x0200000B RID: 11
    [HarmonyPatch(typeof(LighterControler), "SparkLighter")]
    internal class LighterControler_Patch
    {
        // Token: 0x06000013 RID: 19 RVA: 0x00002644 File Offset: 0x00000844
        private static bool Postfix(LighterControler __instance, ref int __result)
        {
            bool flag = !__instance._lighterFlame || !__instance._lighterFlame.activeSelf;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                Light componentInChildren = __instance._lighterFlame.GetComponentInChildren<Light>();
                bool flag2 = !componentInChildren;
                if (flag2)
                {
                    result = true;
                }
                else
                {
                    componentInChildren.intensity = 4f;
                    componentInChildren.range = 20f;
                    result = false;
                }
            }
            return result;
        }
    }
}
