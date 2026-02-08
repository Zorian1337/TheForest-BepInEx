using System;
using Forest_Mod.Configs;
using HarmonyLib;
using TheForest.Items;
using TheForest.Items.Inventory;

namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(InventoryItem), "get_MaxAmount")]
    internal class InventoryItem_MaxAmount_Patch
    {

        private static bool Prefix(InventoryItem __instance, ref int __result)
        {
            if (!ItemInventoryConfig.IsEnabled.Value) return true;
            else
            {
                int num = ItemDatabase.ItemIndexById(__instance._itemId);
                Item item = ItemDatabase.Items[num];

                // Checks if item is limited or unlimited via our settings
                if (InventoryItem_Add_Patch.IsLimitedStackItem(item)) return true; // uses original stack limits

                __result = 1000;
                return false;
            }
        }
    }
}
