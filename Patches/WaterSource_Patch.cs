using Ceto;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Buildings.World;
using TheForest.Utils;

namespace Forest_Mod.Patches
{
    // none modified yet, but might need to do more than override CanDrink, look into billboardDrink

    [HarmonyPatch(typeof(WaterSource), "get_CanDrink")]
    internal class WaterSource_Patch
    {
        //private bool CustomCanDrink
        //{
        //    get
        //    {
        //        if (this.AtLake)
        //        {
        //            return (!LocalPlayer.FpCharacter.swimming || LocalPlayer.Transform.position.y - LocalPlayer.WaterViz.WaterLevel > 1.3f) && !LocalPlayer.FpCharacter.jumping && (double)LocalPlayer.AnimControl.normCamX > 0.3 && LocalPlayer.Rigidbody.velocity.sqrMagnitude <= 0.1f && !this._terrainBlockDrink;
        //        }
        //        return base.enabled && (!LocalPlayer.FpCharacter.swimming || LocalPlayer.Transform.position.y - LocalPlayer.WaterViz.WaterLevel > 1.2f) && (this.AmountReal > this._minAmount || this._maxAmount == 0f) && (this._iconMode == WaterSource.IconModes.FixedPosition || (double)LocalPlayer.AnimControl.normCamX > 0.3) && !LocalPlayer.FpCharacter.jumping && !this._terrainBlockDrink && (double)LocalPlayer.Rigidbody.velocity.sqrMagnitude < 0.01;
        //    }
        //}

        //public static bool Prefix(WaterSource __instance, ref int __result)
        //{
        //    // Overwrite CanDrink to allow more water sources to be drinkable

        //    // Get data for AtLake and terrainBlockDrink


        //    if (__instance.AtLake)
        //    {
        //        return (!LocalPlayer.FpCharacter.swimming || LocalPlayer.Transform.position.y - LocalPlayer.WaterViz.WaterLevel > 1.3f) && !LocalPlayer.FpCharacter.jumping && (double)LocalPlayer.AnimControl.normCamX > 0.3 && LocalPlayer.Rigidbody.velocity.sqrMagnitude <= 0.1f && !this._terrainBlockDrink;
        //    }
        //    return __instance.enabled && (!LocalPlayer.FpCharacter.swimming || LocalPlayer.Transform.position.y - LocalPlayer.WaterViz.WaterLevel > 1.2f) && (__instance.AmountReal > __instance._minAmount || __instance._maxAmount == 0f) && (__instance._iconMode == WaterSource.IconModes.FixedPosition || (double)LocalPlayer.AnimControl.normCamX > 0.3) && !LocalPlayer.FpCharacter.jumping && !this._terrainBlockDrink && (double)LocalPlayer.Rigidbody.velocity.sqrMagnitude < 0.01;
        //}


    }
}
