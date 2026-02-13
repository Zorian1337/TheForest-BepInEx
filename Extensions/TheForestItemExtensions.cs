using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using TheForest.Items.Utils;
using TheForest.Utils;
using UnityEngine;
using static CoopPlayerUpgrades;
using static TheForest.Items.World.CookStew;

namespace Forest_Mod.Extensions
{
    public static class TheForestItemExtensions
    {
        public static bool IsItemIdRegistered(List<TheForest.Items.Item> AllItems, int Id)
        {
            if (AllItems.Any(x => x._id == Id)) return true;
            else return false;
        }

        //[Obsolete]
        //public static bool RegisterCustomItem(this TheForest.Items.Item Item)
        //{
        //    if (Item is null) return false;

        //    // Figure out if the item we want to register already exists in the database 
        //    if (IsItemIdRegistered(Item._id)) // Skipped the extreme determination if this item is unique for now.
        //    {
        //        Main._Logger.LogInfo($"{Item._name} Failed to register as a custom item due to none unique ItemId");
        //        return false;
        //    }

        //    var items = ItemDatabase.Items.ToList();
        //    items.Add(Item);

        //    // 
        //    ItemDatabase instance = ReflectionHelper.GetField<ItemDatabase>(typeof(ItemDatabase), "_instance");
        //    instance._items = items.ToArray();
        //    //ReflectionHelper.SetProperty(typeof(ItemDatabase), "Items", items.ToArray());

        //    if (IsItemIdRegistered(Item._id))
        //    {
        //        Main._Logger.LogInfo($"{Item._name} has been added as a custom item");
        //        return true;
        //    }
        //    else return false;

        //}

        public static bool RegisterCustomItemV2(this TheForest.Items.Item Item, ref List<TheForest.Items.Item> AllItems)
        {
            if (Item is null) return false;

            // Figure out if the item we want to register already exists in the database 
            if (IsItemIdRegistered(AllItems, Item._id)) // Skipped the extreme determination if this item is unique for now.
            {
                Main._Logger.LogInfo($"{Item._name} Failed to register as a custom item due to none unique ItemId");
                return false;
            }

            if (ItemDatabase.Items is null) { Main._Logger.LogInfo($"ItemDatabase items is null");  return false; }

            AllItems.Add(Item);

            //var items = ItemDatabase.Items.ToList();
            //items.Add(Item);

            //ItemDatabase instance = ReflectionHelper.GetField<ItemDatabase>(typeof(ItemDatabase), "_instance");
            //instance._items = items.ToArray();
            ////ReflectionHelper.SetProperty(typeof(ItemDatabase), "Items", items.ToArray());

            if (IsItemIdRegistered(AllItems, Item._id))
            {
                Main._Logger.LogInfo($"{Item._name} has been added as a custom item");
                return true;
            }
            else return false;

        }

        public static GameObject SpawnItem(this TheForest.Items.Item Item)
        {
            if (Item is null) return null;

            int ItemIndex = ItemDatabase.ItemIndexById(Item._id);

            Main._Logger.LogInfo($"Index of {Item._name} is {ItemIndex}\nId of it is {Item._id}");
            // Get the caches index of the item and use it to summon it
            return ItemUtils.SpawnItem(Item._id, LocalPlayer.Transform.position + Vector3.forward * 2f, Quaternion.identity);
        }
    }
}
