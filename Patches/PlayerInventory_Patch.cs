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
            var instance = ReflectionHelper.GetStaticField<ItemDatabase>(typeof(ItemDatabase), "_instance");
            Main._Logger.LogInfo("Injecting into DB instance: " + instance.GetHashCode());
           
            // After ItemCache init we inject our custom items 
            var inst = __instance;

            var db = inst._itemDatabase;
            
            if(instance is null) Main._Logger.LogInfo("Instance is null");

            List<TheForest.Items.Item> Items = new List<Item>(instance._items);//(db._items);

            // Add custom items to items
            var NewItemList = CustomItemManagerTEMP.LoadAllCustomItemsV2(Items).OrderBy(x => x._id).ToArray();

            instance._items = NewItemList;

            // recache immediately
            var _itemsCache = ReflectionHelper.GetField<Dictionary<int, Item>>(instance, "_itemsCache");
            _itemsCache = instance._items.ToDictionary((Item item) => item._id);
            ReflectionHelper.SetPrivateField(instance, "_itemsCache", _itemsCache);
            
            string Itemname = "Stun Baton";
            //Main._Logger.LogInfo($"ValidCacheKey: {ItemDatabase.IsItemidValid(instance._items.Where(x => x._name == Itemname).First()._id)}");

        }
    }
}
