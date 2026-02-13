using Forest_Mod.Custom.Item;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using TheForest.Items.Inventory;

namespace Forest_Mod.Patches
{
    [HarmonyPatch(typeof(PlayerInventory), "InitItemCache")]
    internal class PlayerInventory_Patch
    {
        private static void Postfix(PlayerInventory __instance)
        {
            // After ItemCache init we inject our custom items 
            var inst = __instance;

            var db = inst._itemDatabase;

            List<TheForest.Items.Item> Items = new List<Item>(db._items);


            Main._Logger.LogInfo($"BEFORE");
            foreach (var tm in Items)
            {
                Main._Logger.LogInfo($"Item: {tm._id} - Name: {tm._name} - Cache: {ItemDatabase.ItemIndexById(tm._id)}");
            }

            //// Add custom items to items
            var NewItemList = CustomItemManagerTEMP.LoadAllCustomItemsV2(Items).OrderBy(x => x._id).ToArray();

            db._items = NewItemList;

            // recache immediately
            var _itemsCache = ReflectionHelper.GetField<Dictionary<int, Item>>(db, "_itemsCache");
            _itemsCache = db._items.ToDictionary((Item item) => item._id);

            Main._Logger.LogInfo($"DICTIONARY");
            foreach (var item in _itemsCache)
            {
                Main._Logger.LogInfo($"Key: {item.Key} - Name: {item.Value._name} - Cache: {ItemDatabase.ItemIndexById(item.Value._id)}");
            }

            Main._Logger.LogInfo($"AFTER");

            foreach (var tm in NewItemList)
            {
                Main._Logger.LogInfo($"Item: {tm._id} - Name: {tm._name} - Cache: {ItemDatabase.ItemIndexById(tm._id)}");
            }
            Main._Logger.LogInfo($"ItemDatabase has been modified");
        }
    }
}
