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
            var isnt = __instance;
            Renderer component = __instance.GetComponent<Renderer>();
            Material sharedMaterial = component.sharedMaterial;

            ReflectionHelper.SetPrivateField(__instance, "_normalMat", sharedMaterial);

            component.enabled = false;
            __instance.enabled = false;


            if (__instance._weaponFireSpawn != null) __instance._weaponFireSpawn.transform.parent = __instance.transform.parent;

            __instance._lightingDuration = 1.4f;
            __instance._burnDuration = 120f;
            __instance._firelightIntensityRatio = 3.2f;

            Light WeaponFireLight = ReflectionHelper.GetField<Light>(typeof(BurnableCloth), "_firelight");
            if(WeaponFireLight != null)
            {
                WeaponFireLight.intensity = 20f; // was fine at 4f //20 is great
                WeaponFireLight.range = 70f; // was fine at 20f //100 is great
                WeaponFireLight.type = LightType.Point;
                WeaponFireLight.color = new Color(1f, 0.95f, 0.85f);
            }

            return false;
        }
    }
}
