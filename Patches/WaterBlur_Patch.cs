using Ceto;
using Forest_Mod.Configs;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TheForest.Graphics;
using TheForest.Items;
using TheForest.Items.Inventory;
using UnityEngine;

namespace Forest_Mod.Patches
{

    internal class WaterBlur_Patch
    {

        [HarmonyPatch(typeof(UnderWaterPostEffect), "Start")]
        class UnderWaterPostEffect_Awake_Patch
        {
            static void Postfix(UnderWaterPostEffect __instance)
            {
                // Disables underwater blur filter
                __instance.blurMode = ImageBlur.BLUR_MODE.OFF;
            }
        }

    }
}
