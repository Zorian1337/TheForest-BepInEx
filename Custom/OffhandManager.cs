using Forest_Mod.Extensions;
using Forest_Mod.Util;
using Forest_Mod.Util.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using TheForest.Items.Inventory;
using TheForest.Items.Special;
using TheForest.Items.Utils;
using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;
using static TheForest.Items.Item;
using static UIPopupList;

namespace Forest_Mod.Custom
{
    //Official spawn item command found in (suitCaseSpawn)
    //Vector3 position = new Vector3(UnityEngine.Random.Range(1f, 740f), (float)0, UnityEngine.Random.Range(1800f, 1280f));
    //GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.WorldItem[UnityEngine.Random.Range(0, Extensions.get_length(this.WorldItem))], position, this.transform.rotation);

    internal class OffhandManager : MonoBehaviour
    {
        InventoryItemView[] equipmentSlots;


        void Update()
        {
            // Detect what item is in the Left and Right Hand

            if (!LocalPlayer.GameObject) { return; } //Main._Logger.LogInfo("Player is null");

            var Inventory = LocalPlayer.Inventory;


            //Inventory.has

            //var LeftHand = PInventory.GetHeldItem(Inventory, TheForest.Items.Item.EquipmentSlot.LeftHand);
            //var RightHand = PInventory.GetHeldItem(Inventory, TheForest.Items.Item.EquipmentSlot.RightHand);

            //Main._Logger.LogInfo($"LeftHand: {LeftHand?.name} - RightHand: {RightHand?.name}");


            //// This runs every frame
            if (TheForest.Utils.Input.GetKeyDown(KeyCode.F))
            {
                var LeftHand = Inventory.GetInSlot(EquipmentSlot.LeftHand);
                var RightHand = Inventory.GetInSlot(EquipmentSlot.RightHand);

                Main._Logger.LogInfo($"LeftHand: {LeftHand?._name} | RightHand {RightHand?._name}");

                //Inventory.GetEquipmentSlots()[0] 

                //Main._Logger.LogInfo($"LighterId: {LocalPlayer.AnimControl._lighterId}");

                //Inventory.EquipItem(RightHand, Item.EquipmentSlot.LeftHand);
                //LocalPlayer.
                //LocalPlayer.Inventory.Equip(, false);
                //LocalPlayer.Inventory.StashLeftHand();

                //Debug.Log("Offhand toggle pressed");
            }
        }
    }
}
