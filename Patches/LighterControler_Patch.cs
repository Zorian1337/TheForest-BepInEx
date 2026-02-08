using System;
using HarmonyLib;
using Mono.Cecil;
using TheForest.Items.Special;
using UnityEngine;

namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(LighterControler), "SparkLighter")]
    internal class LighterControler_Patch
    {

        private static bool Postfix(LighterControler __instance, ref int __result)
        {

            if (!__instance._lighterFlame || !__instance._lighterFlame.activeSelf) return true;

            Light componentInChildren = __instance._lighterFlame.GetComponentInChildren<Light>();
            if (!componentInChildren) return true;
            else
            {
                componentInChildren.intensity = 10f; // was fine at 4f
                componentInChildren.range = 50f; // was fine at 20f
            }

            return false;
        }
    }
}
