using System;
using System.Collections.Generic;
using HarmonyLib;
using TheForest.Items.Inventory;
using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;

namespace Forest_Mod.Patches
{
    // Token: 0x02000006 RID: 6
    [HarmonyPatch(typeof(BowController), "get_PrevioustArrowItemView")]
    internal class Bow_PreviousArrowItemView_Patch
    {
        // Token: 0x06000009 RID: 9 RVA: 0x000022F0 File Offset: 0x000004F0
        private static bool Prefix(BowController __instance, ref InventoryItemView __result)
        {
            PlayerInventory inventory = LocalPlayer.Inventory;
            bool flag = inventory == null;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                int ammoItemId = __instance._ammoItemId;
                List<InventoryItemView> list;
                bool flag2 = !inventory.InventoryItemViewsCache.TryGetValue(ammoItemId, out list);
                if (flag2)
                {
                    __result = null;
                    result = false;
                }
                else
                {
                    bool flag3 = list == null || list.Count == 0;
                    if (flag3)
                    {
                        __result = null;
                        result = false;
                    }
                    else
                    {
                        int num = inventory.AmountOf(ammoItemId, false);
                        int index = Mathf.Clamp(num - 2, 0, list.Count - 1);
                        __result = list[index];
                        result = false;
                    }
                }
            }
            return result;
        }
    }

    [HarmonyPatch(typeof(BowController), "get_CurrentArrowItemView")]
    internal class Bow_CurrentArrowItemView_Patch
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000224C File Offset: 0x0000044C
        private static bool Prefix(BowController __instance, ref InventoryItemView __result)
        {
            PlayerInventory inventory = LocalPlayer.Inventory;
            bool flag = inventory == null;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                int ammoItemId = __instance._ammoItemId;
                List<InventoryItemView> list;
                bool flag2 = !inventory.InventoryItemViewsCache.TryGetValue(ammoItemId, out list);
                if (flag2)
                {
                    __result = null;
                    result = false;
                }
                else
                {
                    bool flag3 = list == null || list.Count == 0;
                    if (flag3)
                    {
                        __result = null;
                        result = false;
                    }
                    else
                    {
                        int num = inventory.AmountOf(ammoItemId, false);
                        int index = Mathf.Clamp(num - 1, 0, list.Count - 1);
                        __result = list[index];
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
