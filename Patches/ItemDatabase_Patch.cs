//using Forest_Mod.Custom.Item;
//using HarmonyLib;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using TheForest.Items;
//using TheForest.Items.Inventory;
//using UnityEngine;
//using static CoopPlayerUpgrades;
//using Item = TheForest.Items.Item;

//namespace Forest_Mod.Patches
//{
//    [HarmonyPatch(typeof(ItemDatabase), "OnEnable")]
//    internal class ItemDatabase_Patch
//    {
//        //private static void Postfix(ItemDatabase __instance)
//        //{
//        //    Main._Logger.LogInfo($"inside ItemDatabase");
//        //    __instance.hideFlags = HideFlags.None;

//        //    var _instance = ReflectionHelper.GetField<ItemDatabase>(typeof(ItemDatabase), "_instance");

//        //    if (_instance == null)
//        //    {
//        //        _instance = __instance;

//        //        // Inject our custom items into _items then allow it to cache

//        //        Main._Logger.LogInfo($"Patching ItemDatabase");
//        //        CustomItemManagerTEMP.LoadAllCustomItems();

//        //        ReflectionHelper.SetField(__instance, "_itemsCache", _instance._items.ToDictionary((Item item) => item._id));

//        //        //var _itemsCache = ReflectionHelper.GetField<Dictionary<int, Item>>(__instance, "_itemsCache");
//        //        //_itemsCache = _instance._items.ToDictionary((Item item) => item._id);
//        //    }
//        //    else
//        //    {
//        //        // Inject our custom items into _items then allow it to cache

//        //        Main._Logger.LogInfo($"Patching ItemDatabase");
//        //        CustomItemManagerTEMP.LoadAllCustomItems();

//        //        ReflectionHelper.SetField(__instance, "_itemsCache", _instance._items.ToDictionary((Item item) => item._id));

//        //        //var _itemsCache = ReflectionHelper.GetField<Dictionary<int, Item>>(__instance, "_itemsCache");
//        //        //_itemsCache = _instance._items.ToDictionary((Item item) => item._id);
//        //    }

//        //    //return false;
//        //}

//        //private static void Postfix(ItemDatabase __instance)
//        //{
//        //    //Main._Logger.LogInfo($"inside ItemDatabase");

//        //    List<TheForest.Items.Item> Items = new List<Item>(__instance._items);

//        //    Main._Logger.LogInfo($"BEFORE");
//        //    foreach (var tm in Items)
//        //    {
//        //        Main._Logger.LogInfo($"Item: {tm._id} - Name: {tm._name} - Cache: {ItemDatabase.ItemIndexById(tm._id)}");
//        //    }

//        //    //// Add custom items to items
//        //    var NewItemList = CustomItemManagerTEMP.LoadAllCustomItemsV2(Items).OrderBy(x => x._id).ToArray();

//        //    __instance._items = NewItemList;

//        //    // recache immediately
//        //    var _itemsCache = ReflectionHelper.GetField<Dictionary<int, Item>>(__instance, "_itemsCache");
//        //    _itemsCache = __instance._items.ToDictionary((Item item) => item._id);

//        //    Main._Logger.LogInfo($"DICTIONARY");
//        //    foreach (var item in _itemsCache)
//        //    {
//        //        Main._Logger.LogInfo($"Key: {item.Key} - Name: {item.Value._name} - Cache: {ItemDatabase.ItemIndexById(item.Value._id)}");
//        //    }


//        //    Main._Logger.LogInfo($"AFTER");

//        //    foreach (var tm in NewItemList)
//        //    {
//        //        Main._Logger.LogInfo($"Item: {tm._id} - Name: {tm._name} - Cache: {ItemDatabase.ItemIndexById(tm._id)}");
//        //    }
//        //    Main._Logger.LogInfo($"ItemDatabase has been modified");
//        //}
//    }
//}
