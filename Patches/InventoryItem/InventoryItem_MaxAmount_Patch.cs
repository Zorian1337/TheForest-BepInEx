using System;
using Forest_Mod.Configs;
using HarmonyLib;
using TheForest.Items;
using TheForest.Items.Inventory;

namespace Forest_Mod.Patches
{
    // Token: 0x0200000A RID: 10
    [HarmonyPatch(typeof(InventoryItem), "get_MaxAmount")]
    internal class InventoryItem_MaxAmount_Patch
    {
        // Token: 0x06000011 RID: 17 RVA: 0x000025C0 File Offset: 0x000007C0
        private static bool Prefix(InventoryItem __instance, ref int __result)
        {
            bool flag = !ItemInventoryConfig.IsEnabled.Value;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                int num = ItemDatabase.ItemIndexById(__instance._itemId);
                Item item = ItemDatabase.Items[num];
                bool flag2 = (item._type & 4096) > 0;
                if (flag2)
                {
                    result = true;
                }
                else
                {
                    bool flag3 = item._name.ToLower() == "pot";
                    if (flag3)
                    {
                        result = true;
                    }
                    else
                    {
                        __result = 1000;
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
