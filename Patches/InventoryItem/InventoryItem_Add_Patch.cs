using System;
using Forest_Mod.Configs;
using HarmonyLib;
using TheForest.Items;
using TheForest.Items.Inventory;

namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(InventoryItem), "Add")]
    public class InventoryItem_Add_Patch
    {

        private static bool Prefix(InventoryItem __instance, int amount, bool isEquiped, ref int __result)
        {
            // Checks if custom stash limit is enabled
            if (!ItemInventoryConfig.IsEnabled.Value) return true; // if disabled runs the original function
            else
            {
                int num = ItemDatabase.ItemIndexById(__instance._itemId);
                Item item = ItemDatabase.Items[num];

                Main._Logger.LogInfo($"Item: {item._name} Amount: {__instance._amount} Type: {item._type.ToString()}");
                int MaxLimit = ItemInventoryConfig.CustomStackLimit.Value;
                int SupposedAmount = __instance._amount + amount;

                // Check if item is valid for custom stack limit
                if (IsLimitedStackItem(item)) return true;

                __instance._maxAmount = MaxLimit;

                if (SupposedAmount > MaxLimit)
                {
                    int num3 = SupposedAmount - MaxLimit;
                    __instance._amount = MaxLimit;
                    __instance._amount += __result;
                }
                else
                {
                    Main._Logger.LogInfo($"[Item: {item._name} Added: {amount} Max: {item._maxAmount} Type: {item._type.ToString()}]");
                    __instance._amount += amount;
                }

                return false; // allows this custom function to run
            }

        }

        public static bool IsLimitedStackItem(Item item)
        {
            // Filter via Id


            // Filter via name
            string name = item._name.ToLower();
            switch (name)
            {
                case "pot": return true;
                case "pouch": return true;
            }

            // Filter via type
            if ((item._type & Item.Types.Weapon) > 0) return true; // runs original handler for stack limits

            // Lazy Filter 
            if (!(item is null) && item._maxAmount == 1) return true; // runs original handler for item if its meant to be capped at 1

            return false; // No Items sent here were detected as invalid for custom item stack
        }
    }
}
