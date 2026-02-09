using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items.Special;
using TheForest.Items.World;
using UnityEngine;

namespace Forest_Mod.Patches.LightRelated
{
    //[HarmonyPatch(typeof(LightableFlareController), "SparkLighter")]
    //internal class LightableFlareController_Patch
    //{
    //    private static void Postfix(LightableFlareController __instance)
    //    {

    //        GameObject WeaponFireLight = ReflectionHelper.GetField<GameObject>(typeof(LightableFlareController), "fire");
    //        if (WeaponFireLight != null)
    //        {
    //            WeaponFireLight. = 20f; // was fine at 4f //20 is great
    //            WeaponFireLight.range = 70f; // was fine at 20f //100 is great
    //            WeaponFireLight.type = LightType.Point;
    //            WeaponFireLight.color = new Color(1f, 0.95f, 0.85f);
    //        }
    //    }
    //}


}
