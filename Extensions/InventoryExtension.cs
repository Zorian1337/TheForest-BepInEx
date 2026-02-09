using Pathfinding;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using TheForest.Items.Inventory;
using TheForest.Items.Special;
using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;

namespace Forest_Mod.Extensions
{
    public static class InventoryExtension
    {
        public static void EquipItem(this PlayerInventory Inventory, int ItemId, Item.EquipmentSlot Slot)
        {

        }

        public static void EquipItem(this PlayerInventory Inventory, string Item, Item.EquipmentSlot Slot)
        {

        }

        public static bool EquipItem(this PlayerInventory Inventory, InventoryItemView itemView, Item.EquipmentSlot Slot)
        {
            Inventory.LockEquipmentSlot(Slot);
            Inventory.StartCoroutine(Inventory.EquipSequencePublic(Slot, itemView));
            return false;
        }

        public static void LightItem(this BurnableItem burn)
        {
            if (LocalPlayer.Inventory.HasInSlot(Item.EquipmentSlot.LeftHand, LocalPlayer.Inventory.DefaultLight._itemId))
            {
                LighterControler.HasLightableItem = true;
            }
        }

        public static IEnumerator EquipSequencePublic(this PlayerInventory Inventory, Item.EquipmentSlot slot, InventoryItemView itemView)
        {
            bool specialItemCheck = true;

            InventoryItemView[] Equipment = ReflectionHelper.GetField<InventoryItemView[]>(Inventory, "_equipmentSlots");

            int slotNum = 0;
            
            foreach(var Item in Equipment)
            {
                Main._Logger.LogInfo($"Equipped Name: {Item.name} - Id: {Item._itemId} - Slot: {slotNum}");
                slotNum++;
            }
            return null;


            //if (Inventory._equipmentSlots[(int)slot] != null && Inventory._equipmentSlots[(int)slot] != this._noEquipedItem)
            //{
            //    this._pendingEquip = true;
            //    this._equipmentSlotsNext[(int)slot] = itemView;
            //    bool canStash = this._equipmentSlots[(int)slot].ItemCache._maxAmount >= 0;
            //    if (canStash)
            //    {
            //        this.MemorizeItem(slot);
            //    }
            //    this._itemAnimHash.ApplyAnimVars(this._equipmentSlots[(int)slot].ItemCache, false);
            //    int currentItemId = this._equipmentSlots[(int)slot]._itemId;
            //    if (Time.timeScale > 0f)
            //    {
            //        float durationCountdown = this._equipmentSlots[(int)slot].ItemCache._unequipDelay;
            //        while (this._pendingEquip && durationCountdown > 0f)
            //        {
            //            durationCountdown -= Time.deltaTime;
            //            yield return null;
            //        }
            //    }
            //    if (!this.HasInSlot(slot, currentItemId) || !this.HasInNextSlot(slot, itemView._itemId))
            //    {
            //        if (canStash)
            //        {
            //            this.AddItem(itemView._itemId, 1, true, true, null);
            //        }
            //        else
            //        {
            //            this.FakeDrop(itemView._itemId, null);
            //        }
            //        if (this._equipmentSlotsNext[(int)slot] == itemView)
            //        {
            //            this._equipmentSlotsNext[(int)slot] = this._noEquipedItem;
            //        }
            //        this._pendingEquip = false;
            //        yield break;
            //    }
            //    this.UnlockEquipmentSlot(slot);
            //    if (itemView.ItemCache.MatchType(Item.Types.Special))
            //    {
            //        specialItemCheck = this.SpecialItemsControlers[itemView._itemId].ToggleSpecial(true);
            //    }
            //    if (specialItemCheck)
            //    {
            //        this.UnequipItemAtSlot(slot, !canStash, canStash, false);
            //    }
            //    this._equipmentSlotsNext[(int)slot] = this._noEquipedItem;
            //}
            //else if (itemView.ItemCache.MatchType(Item.Types.Special))
            //{
            //    specialItemCheck = this.SpecialItemsControlers[itemView._itemId].ToggleSpecial(true);
            //}
            //if (specialItemCheck)
            //{
            //    if (itemView._held)
            //    {
            //        this._equipmentSlots[(int)slot] = itemView;
            //        itemView.OnItemEquipped();
            //        itemView._held.SetActive(true);
            //        HeldItemIdentifier heldItem = itemView._held.GetComponent<HeldItemIdentifier>();
            //        if (heldItem != null)
            //        {
            //            heldItem.Properties.Copy(itemView.Properties);
            //        }
            //        itemView.ApplyEquipmentEffect(true);
            //        this._itemAnimHash.ApplyAnimVars(itemView.ItemCache, true);
            //        if (itemView.ItemCache._equipedSFX != Item.SFXCommands.None)
            //        {
            //            LocalPlayer.Sfx.SendMessage(itemView.ItemCache._equipedSFX.ToString(), SendMessageOptions.DontRequireReceiver);
            //        }
            //        if (itemView.ItemCache._maxAmount >= 0)
            //        {
            //            this.ToggleAmmo(itemView, true);
            //            this.ToggleInventoryItemView(itemView._itemId, false, null);
            //        }
            //        yield return (!itemView.ItemCache.MatchType(Item.Types.Projectile)) ? null : YieldPresets.WaitPointSevenSeconds;
            //    }
            //    else
            //    {
            //        Debug.LogError("Trying to equip item '" + itemView.ItemCache._name + "' which doesn't have a held reference in " + itemView.name);
            //    }
            //    this.UnlockEquipmentSlot(slot);
            //}
            //this._pendingEquip = false;
            //yield break;
        }
    }
}
