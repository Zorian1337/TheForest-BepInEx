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

        private static void Postfix(LighterControler __instance)
        {

            if (!__instance._lighterFlame || !__instance._lighterFlame.activeSelf) return;

            Light componentInChildren = __instance._lighterFlame.GetComponentInChildren<Light>();
            if (!componentInChildren) return;
            else
            {
                componentInChildren.intensity = 4f; // was fine at 4f //20 is great
                componentInChildren.range = 20f; // was fine at 20f //100 is great
                componentInChildren.type = LightType.Point;
                componentInChildren.color = new Color(1f, 0.95f, 0.85f);
            }

        }
    }
}
