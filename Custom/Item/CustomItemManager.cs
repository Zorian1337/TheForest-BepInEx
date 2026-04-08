using Bolt;
using Forest_Mod.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using TheForest.Utils;
using UnityEngine;
using static TheForest.Items.World.CookStew;

namespace Forest_Mod.Custom.Item
{
    internal class CustomItemManagerREAL
    {
        // Load our bundles

        // Find an existing item 

        // Get all item ids that are already populated

        // Modify our item base into the item we want withotherassets 

        
    }

    public static class CustomItemManagerTEMP
    {
        public static int GenerateCustomItemId()
        {
            bool IdExists = true;
            int RandomNumber = 0;

            // Fetech a number that isnt used then return it
            do
            {
                RandomNumber = new System.Random().Next(0, 99999);

                if (!(ItemDatabase.Items.Any(x => x._id == RandomNumber))) IdExists = false;
            } while (IdExists);

            return RandomNumber;
        }

        public static TheForest.Items.Item GetBaseItem(List<TheForest.Items.Item> OriginalItems, int ItemId)
        {
            ItemDatabase.ItemById(ItemId);
            TheForest.Items.Item Item = OriginalItems.Find(x => x._id == ItemId);
            if ((Item is null)) return default;
            else return Item;
        }
        public static TheForest.Items.Item GetBaseItem(List<TheForest.Items.Item> OriginalItems, string ItemName)
        {
            //ItemDatabase.ItemByName(ItemName);
            TheForest.Items.Item Item = OriginalItems.Find(x => x._name.ToLower() == ItemName.ToLower());
            if ((Item is null)) return default;
            else return Item;
        }


        //public static TheForest.Items.Item[] LoadAllCustomItems(List<TheForest.Items.Item> OriginalItems)
        //{
        //    Main._Logger.LogInfo($"Attempting to load custom items({OriginalItems.Count()} original items)...");

        //    // Create new list for our items and custom items to live together without creating issues
        //    List<TheForest.Items.Item> AllItems = new List<TheForest.Items.Item>(OriginalItems);

        //    CreateItem(OriginalItems, "Stick", CustomItem =>
        //    {
        //        try
        //        {
        //            Main._Logger.LogInfo($"func ran first");

        //            // SOTF Stun Batton Asset
        //            GameObject Stun = AssetManager.LoadAsset<GameObject>("TaserStick");
        //            Main._Logger.LogInfo($"Loaded taser");

        //            // This is where we modify our item properties (IE prefabs)

        //            CustomItem._name = "Stun Baton";
        //            CustomItem._maxAmount = 1;
        //            CustomItem._pickupPrefab = Stun.transform;
        //            CustomItem._bareItemPrefab = Stun.transform;
        //            CustomItem._type = TheForest.Items.Item.Types.Weapon;

        //            Main._Logger.LogInfo($"Created custom item {CustomItem._name}");
        //        }
        //        catch (Exception Ex) { Main._Logger.LogInfo(Ex.ToString()); return default; }



        //        return CustomItem;
        //    }).RegisterCustomItemV2(AllItems); // Add extention to easily register this custom item (.RegisterCustomItem())

        //    // Return our total list of original and modded items...
        //    return AllItems.ToArray();
        //}


        public static TheForest.Items.Item[] LoadAllCustomItemsV2(List<TheForest.Items.Item> OriginalItems)
        {
            Main._Logger.LogInfo($"Attempting to load custom items({OriginalItems.Count()} original items)...");

            // Create new list for our items and custom items to live together without creating issues
            List<TheForest.Items.Item> AllItems = new List<TheForest.Items.Item>(OriginalItems);


            CreateItem(OriginalItems, 57).RegisterCustomItemV2(ref AllItems);

            // Return our total list of original and modded items...
            return AllItems.ToArray();
        }
        
        public static TheForest.Items.Item CreateItem(List<TheForest.Items.Item> OriginalItems, int BaseItemId)
        {
            TheForest.Items.Item CustomItem;

            try
            {
                // Gets the base item class and then sends it into the function to create a template item using it.
                TheForest.Items.Item BaseItem = GetBaseItem(OriginalItems, BaseItemId);

                if (BaseItem is null) Main._Logger.LogInfo($"BaseItem is null");
                else Main._Logger.LogInfo($"BaseItem is not null");

                CustomItem = JsonUtility.FromJson<TheForest.Items.Item>(JsonUtility.ToJson(BaseItem));

                if (CustomItem is null) Main._Logger.LogInfo($"CustomItem is null");
                else Main._Logger.LogInfo($"CustomItem is not null");

                if (CustomItem is null) Main._Logger.LogInfo($"CustomItem is null");
                else Main._Logger.LogInfo($"CustomItem is not null");

                // Update our custom item
                //Main._Logger.LogInfo($"func ran first");

                // SOTF Stun Batton Asset
                GameObject Stun = AssetManager.DirectLoadAsset<GameObject>(@"BepInEx\plugins\samplebundle", "TaserStick");
                if (!(Stun is null)) { Main._Logger.LogInfo($"Loaded taser"); }
                else return default;

                    // This is where we modify our item properties (IE prefabs)

                CustomItem._name = "Stun Baton";
                CustomItem._maxAmount = 1;
                CustomItem._pickupPrefab = Stun.transform;
                CustomItem._bareItemPrefab = Stun.transform;
                CustomItem._type = TheForest.Items.Item.Types.Weapon;

                Main._Logger.LogInfo($"Created custom item {CustomItem._name}");

                // Pick out a valid ID to set this item under
                int MaxId = OriginalItems.Select(x => x._id).Max();
                CustomItem._id = MaxId + 1; // Sets new Id to the highest available +1 
            }
            catch (Exception Ex) { Main._Logger.LogInfo(Ex.ToString()); return default; }

            return CustomItem;
        }





        //public static TheForest.Items.Item CreateItem(List<TheForest.Items.Item> OriginalItems, int BaseItemId, Func<TheForest.Items.Item, TheForest.Items.Item> Func)
        //{
        //    // Gets the base item class and then sends it into the function to create a template item using it.
        //    TheForest.Items.Item BaseItem = GetBaseItem(OriginalItems, BaseItemId);

        //    if (BaseItem is null) Main._Logger.LogInfo($"BaseItem is null");
        //    else Main._Logger.LogInfo($"BaseItem is not null");

        //    TheForest.Items.Item CustomItem = JsonUtility.FromJson<TheForest.Items.Item>(JsonUtility.ToJson(BaseItem));

        //    if(CustomItem is null) Main._Logger.LogInfo($"CustomItem is null");
        //    else Main._Logger.LogInfo($"CustomItem is not null");

        //    // Update our custom item with the settings that our API easily allows us to change (using the base item class)
        //    CustomItem = Func(CustomItem);

        //    if (!(CustomItem is null)) Main._Logger.LogInfo($"CustomItem-UPDATE is null");
        //    else Main._Logger.LogInfo($"CustomItem-UPDATE is not null");

        //    // Pick out a valid ID to set this item under
        //    int MaxId = OriginalItems.Select(x => x._id).Max();
        //    CustomItem._id = MaxId+1; // Sets new Id to the highest available +1 

        //    // Returns the data that we inputed from our func to simply item creation
        //    return CustomItem;
        //}

        //public static TheForest.Items.Item CreateItem(List<TheForest.Items.Item> OriginalItems, string BaseItemName, Func<TheForest.Items.Item, TheForest.Items.Item> Func)
        //{
        //    Main._Logger.LogInfo($"Create ran first");
        //    // Gets the base item class and then sends it into the function to create a template item using it.
        //    TheForest.Items.Item BaseItem = GetBaseItem(OriginalItems, BaseItemName);

        //    if (BaseItem is null) Main._Logger.LogInfo($"BaseItem is null");
        //    else Main._Logger.LogInfo($"BaseItem is not null");

        //    TheForest.Items.Item CustomItem = JsonUtility.FromJson<TheForest.Items.Item>(JsonUtility.ToJson(BaseItem));

        //    if (CustomItem is null) Main._Logger.LogInfo($"CustomItem is null");
        //    else Main._Logger.LogInfo($"CustomItem is not null");

        //    // Update our custom item with the settings that our API easily allows us to change (using the base item class)
        //    CustomItem = Func(CustomItem);

        //    // Pick out a valid ID to set this item under
        //    int MaxId = OriginalItems.Select(x => x._id).Max();
        //    CustomItem._id = MaxId + 1; // Sets new Id to the highest available +1 

        //    // Returns the data that we inputed from our func to simply item creation
        //    return CustomItem;
        //}
    }
}
