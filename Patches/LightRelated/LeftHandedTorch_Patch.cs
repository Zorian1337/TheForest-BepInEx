using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items.Special;

namespace Forest_Mod.Patches
{
    //LightingHeldFireRoutine
    [HarmonyPatch(typeof(LighterControler), "LightingHeldFireRoutine")]
    internal class LeftHandedTorch_Patch
    {
    }
}
