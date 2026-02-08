using System;
using Forest_Mod.Configs;
using HarmonyLib;
using TheForest.Items;
using TheForest.Items.Inventory;

namespace Forest_Mod.Patches
{
    // Token: 0x02000009 RID: 9
    [HarmonyPatch(typeof(InventoryItem), "Add")]
    public class InventoryItem_Add_Patch
    {
        // Token: 0x0600000F RID: 15 RVA: 0x00002438 File Offset: 0x00000638
        private static bool Prefix(InventoryItem __instance, int amount, bool isEquiped, ref int __result)
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
                Main._Logger.LogInfo(string.Format("Item: {0} Amount: {1} Type: {2}", item._name, __instance._amount, item._type.ToString()));
                int value = ItemInventoryConfig.CustomStackLimit.Value;
                int num2 = __instance._amount + amount;
                bool flag2 = (item._type & 4096) > 0;
                if (flag2)
                {
                    result = true;
                }
                else
                {
                    string text = item._name.ToLower();
                    string a = text;
                    if (!(a == "pot"))
                    {
                        if (!(a == "pouch"))
                        {
                            __instance._maxAmount = value;
                            bool flag3 = num2 > value;
                            if (flag3)
                            {
                                int num3 = num2 - value;
                                __instance._amount = value;
                                __instance._amount += __result;
                            }
                            else
                            {
                                Main._Logger.LogInfo(string.Format("[Item: {0} Added: {1} Max: {2} Type: {3}]", new object[]
                                {
                                    item._name,
                                    amount,
                                    item._maxAmount,
                                    item._type.ToString()
                                }));
                                __instance._amount += amount;
                            }
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
