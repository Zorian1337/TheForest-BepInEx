using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using TheForest.Items.Inventory;
using TheForest.Utils;
using static InvBaseItem;
using Forest_Mod.Util;

namespace Forest_Mod.Util.Player
{
    public static class PInventory
    {
        //LOOK INTO CONVERTING TO INVGAMEITEM LATER, ALSO LOOK INTO INVEQUIPMENT
        //public static InventoryItemView GetHeldItem(Item.EquipmentSlot slots = Item.EquipmentSlot.RightHand) => References.GetPlayerInventory().EquipmentSlots[(int)slots]; //Player._inventory
        //public static InventoryItemView GetHeldItem(PlayerInventory Inventory, Item.EquipmentSlot slots = Item.EquipmentSlot.RightHand) => Inventory.EquipmentSlots[(int)slots]; //Player._inventory
        //playerAnimatorControl - has lighterid(LocalPlayer.AnimControl._lighterId) and torchid (probably counts as flashlight)

        //PlayerStats.switchToLighter
        //private void switchToLighter()
        //{
        //    LocalPlayer.Inventory.Equip(LocalPlayer.AnimControl._lighterId, false);
        //    LocalPlayer.Inventory.StashLeftHand();
        //}

        //public void equipLighterOnly()
        //{
        //    base.StartCoroutine(this.equipLighterRoutine());
        //}
        //LocalPlayer.Inventory.Equip(this._itemId, false)
        //LocalPlayer.Inventory.UnequipItemAtSlot(Item.EquipmentSlot.LeftHand, false, true, false);
        //public static void SetHeldItem()
    }
}
