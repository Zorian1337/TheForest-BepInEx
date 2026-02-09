using System;
using System.Collections.Generic;
using System.Linq;
using Forest_Mod.Configs;
using HarmonyLib;
using TheForest.Items;
using TheForest.Items.Inventory;
using static Forest_Mod.Configs.ItemInventoryConfig;


namespace Forest_Mod.Patches
{

    [HarmonyPatch(typeof(InventoryItem), "Add")]
    public class InventoryItem_Add_Patch
    {
        public static List<StackLimitTracker> LimitedById() => ItemInventoryConfig.CustomStackLimitList.Where(x => x.TypeLimit == ItemInventoryConfig.StackLimitTracker.LimitedBy.ById).ToList();
        public static List<StackLimitTracker> LimitedByName() => ItemInventoryConfig.CustomStackLimitList.Where(x => x.TypeLimit == ItemInventoryConfig.StackLimitTracker.LimitedBy.ByName).ToList();
        public static List<StackLimitTracker> LimitedByType() => ItemInventoryConfig.CustomStackLimitList.Where(x => x.TypeLimit == ItemInventoryConfig.StackLimitTracker.LimitedBy.ByType).ToList();
        public static List<StackLimitTracker> LimitedByDefaultType() => ItemInventoryConfig.CustomStackLimitList.Where(x => x.TypeLimit == ItemInventoryConfig.StackLimitTracker.LimitedBy.ByDefaultType).ToList();

        //ItemInventoryConfig.CustomStackLimitList.Select(x => x.TypeLimit == ItemInventoryConfig.StackLimitTracker.LimitedBy.ById);
        private static bool Prefix(InventoryItem __instance, int amount, bool isEquiped, ref int __result)
        {
            // Checks if custom stash limit is enabled
            if (!ItemInventoryConfig.IsEnabled.Value) return true; // if disabled runs the original function
            else
            {
                int num = ItemDatabase.ItemIndexById(__instance._itemId);
                Item item = ItemDatabase.Items[num];

                // Check if item is valid for custom stack limit
                if (IsLimitedStackItemV2(item, out StackLimitTracker.LimitedBy filterType, out int MaxLimit)) return true;

                Main._Logger.LogInfo($"Item: {item._name} Amount: {__instance._amount} Type: {item._type.ToString()}");
                //int MaxLimit = ItemInventoryConfig.CustomStackLimit.Value;
                int SupposedAmount = __instance._amount + amount;

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

        public static bool IsLimitedStackItemV2(Item item, out StackLimitTracker.LimitedBy FilterType, out int MaxAmount)
        {
            MaxAmount = -1;

            // Filter via Id
            var IdFilter = LimitedById();

            if(IdFilter != null && IdFilter.Count() > 0)
            {
                StackLimitTracker Item = IdFilter.Find(x => x.ItemId == item._id);
                if (!(Item is null)) { FilterType = StackLimitTracker.LimitedBy.ById;  MaxAmount = Item.MaxAmount; return false; }
            }

            // Filter via name
            var NameFilter = LimitedByName();

            if (NameFilter != null && NameFilter.Count() > 0)
            {
                StackLimitTracker Item = NameFilter.Find(x => x.ItemName == item._name);
                if (!(Item is null)) { FilterType = StackLimitTracker.LimitedBy.ByName; MaxAmount = Item.MaxAmount; return false; }
            }

            // Filter via Type
            var TypeFilter = LimitedByType();

            if (TypeFilter != null && TypeFilter.Count() > 0)
            {
                StackLimitTracker Item = TypeFilter.Find(x => x.ItemType == item._type);
                if (!(Item is null)) { FilterType = StackLimitTracker.LimitedBy.ByType; MaxAmount = Item.MaxAmount; return false; }
            }

            // Filter via Default Type
            var DefaultTypeFilter = LimitedByDefaultType();

            if (DefaultTypeFilter != null && DefaultTypeFilter.Count() > 0)
            {
                StackLimitTracker Item = DefaultTypeFilter.Find(x => x.ItemType == item._type);
                if (!(Item is null)) { FilterType = StackLimitTracker.LimitedBy.ByDefaultType; return true; } // automatically skips this one 
            }

            // Lazy Filter - if its not in the filter we handle
            if (!(item is null) && item._maxAmount == 1) { FilterType = StackLimitTracker.LimitedBy.ByDefaultType; return true; } // runs original handler for item if its meant to be capped at 1

            // No Items sent here were detected as invalid for custom item stack
            MaxAmount = ItemInventoryConfig.CustomStackLimit.Value;
            FilterType = StackLimitTracker.LimitedBy.NONE;
            return false;
        }
    }
}
