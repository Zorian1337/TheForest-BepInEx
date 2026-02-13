using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using TheForest.Utils;
using UnityEngine;

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
        public static TheForest.Items.Item GetBaseItem(int ItemId) => ItemDatabase.ItemById(ItemId);
        public static TheForest.Items.Item GetBaseItem(string ItemName) => ItemDatabase.ItemByName(ItemName);


        public static void LoadAllCustomItems()
        {

            CreateItem("Stick", CustomItem =>
            {
                // This is where we modify our item properties (IE prefabs)

                CustomItem._name = "Stun Baton";
                //CustomItem._pickupMaterial
                CustomItem._type = TheForest.Items.Item.Types.Weapon;


                return CustomItem;
            }); // Add extention to easily register this custom item (.RegisterCustomItem())
        }


        public static TheForest.Items.Item CreateItem(int BaseItemId, Func<TheForest.Items.Item, TheForest.Items.Item> Func)
        {
            // Gets the base item class and then sends it into the function to create a template item using it.
            TheForest.Items.Item BaseItem = GetBaseItem(BaseItemId);

            TheForest.Items.Item CustomItem = JsonUtility.FromJson<TheForest.Items.Item>(JsonUtility.ToJson(BaseItem));

            // Update our custom item with the settings that our API easily allows us to change (using the base item class)
            CustomItem = Func(CustomItem);

            // Pick out a valid ID to set this item under
            int MaxId = ItemDatabase.Items.Select(x => x._id).Max();
            CustomItem._id = MaxId+1; // Sets new Id to the highest available +1 

            // Returns the data that we inputed from our func to simply item creation
            return CustomItem;
        }

        public static TheForest.Items.Item CreateItem(string BaseItemName, Func<TheForest.Items.Item, TheForest.Items.Item> Func)
        {
            // Gets the base item class and then sends it into the function to create a template item using it.
            TheForest.Items.Item BaseItem = GetBaseItem(BaseItemName);

            TheForest.Items.Item CustomItem = JsonUtility.FromJson<TheForest.Items.Item>(JsonUtility.ToJson(BaseItem));

            // Update our custom item with the settings that our API easily allows us to change (using the base item class)
            CustomItem = Func(CustomItem);

            // Pick out a valid ID to set this item under
            int MaxId = ItemDatabase.Items.Select(x => x._id).Max();
            CustomItem._id = MaxId + 1; // Sets new Id to the highest available +1 

            // Returns the data that we inputed from our func to simply item creation
            return CustomItem;
        }
    }
}
