using System;
using System.Collections.Generic;
using HarmonyLib;
using TheForest.Items.Inventory;
using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;

namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(BowController), "get_PrevioustArrowItemView")]
    internal class Bow_PreviousArrowItemView_Patch
    {

        private static bool Prefix(BowController __instance, ref InventoryItemView __result)
        {
            PlayerInventory inventory = LocalPlayer.Inventory;
            if (inventory is null) return true;

            int ammoItemId = __instance._ammoItemId;

            if (!inventory.InventoryItemViewsCache.TryGetValue(ammoItemId, out List<InventoryItemView> list)) return true;
            else
            {
                if(list is null ||  list.Count == 0) return true;
                else
                {
                    int num = inventory.AmountOf(ammoItemId, false);
                    int index = Mathf.Clamp(num - 2, 0, list.Count - 1);
                    __result = list[index];
                    return false;
                }
            }

            return true; // uses the default control for handling arrow amounts
        }
    }

    [HarmonyPatch(typeof(BowController), "get_CurrentArrowItemView")]
    internal class Bow_CurrentArrowItemView_Patch
    {
        private static bool Prefix(BowController __instance, ref InventoryItemView __result)
        {
            PlayerInventory inventory = LocalPlayer.Inventory;

            if (inventory is null) return true;
            else
            {
                int ammoItemId = __instance._ammoItemId;

                if (!inventory.InventoryItemViewsCache.TryGetValue(ammoItemId, out List<InventoryItemView> list)) return true;
                else
                {
                    if (list == null || list.Count == 0) return true;
                    else
                    {
                        int num = inventory.AmountOf(ammoItemId, false);
                        int index = Mathf.Clamp(num - 1, 0, list.Count - 1);
                        __result = list[index];
                        return false;
                    }
                }
            }

            return true; // uses the default control for handling arrow amounts
        }
    }
}
