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
using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;
using static TheForest.Items.Item;

namespace Forest_Mod.Custom
{
    internal class OffhandManager : MonoBehaviour
    {
        InventoryItemView[] equipmentSlots;

        AssetBundle bundle = AssetBundle.LoadFromFile(@"BepInEx\plugins\samplebundle");
        GameObject itemPrefab;
        void Update()
        {
            if (!LocalPlayer.GameObject) { return; } //Main._Logger.LogInfo("Player is null");



            if (LocalPlayer.Inventory == null) return; // player not ready
            if (equipmentSlots == null)
            {
                equipmentSlots = ReflectionHelper.GetField<InventoryItemView[]>(LocalPlayer.Inventory, "_equipmentSlots");
                if (equipmentSlots == null)
                {
                    Main._Logger.LogInfo("slots are still null");
                    return; // still not initialized
                }
                else { Main._Logger.LogInfo("slots arent null"); }
            }

            if (itemPrefab is null)
            {
                Main._Logger.LogInfo("attempting to load pouch");
                itemPrefab = bundle.LoadAsset<GameObject>("ItemPouch");
                if (itemPrefab != null) Main._Logger.LogInfo("bundle item loaded sucessfully!");
            }

            if (TheForest.Utils.Input.GetKeyDown(KeyCode.F))
            {
                Main._Logger.LogInfo("F pressed");
                SpawnItem(); 
                //SpawnDebugCube();
            }


            

            //// Now you can safely use equipmentSlots
            //if (TheForest.Utils.Input.GetKeyDown(KeyCode.F))
            //{
            //    Main._Logger.LogInfo($"LighterId: {LocalPlayer.AnimControl._lighterId}");

            //    var Inventory = References.GetPlayerInventory();
            //    var LeftHand = PInventory.GetHeldItem(Inventory, TheForest.Items.Item.EquipmentSlot.LeftHand);
            //    var RightHand = PInventory.GetHeldItem(Inventory, TheForest.Items.Item.EquipmentSlot.RightHand);

            //    if (LocalPlayer.Inventory.HasInSlot(Item.EquipmentSlot.LeftHand, LocalPlayer.Inventory.DefaultLight._itemId))
            //    {
            //        LighterControler.HasLightableItem = true;
            //    }

            //    //Inventory.EquipItem(RightHand, Item.EquipmentSlot.LeftHand);
            //    //LocalPlayer.
            //    //LocalPlayer.Inventory.Equip(, false);
            //    //LocalPlayer.Inventory.StashLeftHand();

            //    Debug.Log("Offhand toggle pressed");
            //}



            //foreach (var Item in equipmentSlots)
            //{
            //    Main._Logger.LogInfo($"Name: {Item.name} Id: {Item._itemId}");
            //}
        }


        void SpawnItem()
        {
            var cam = LocalPlayer.MainCam;

            var go = Instantiate(itemPrefab);
            go.transform.position = cam.transform.position + cam.transform.forward * 2f;
            go.transform.rotation = Quaternion.identity;
            go.layer = 0;

            var rb = go.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            Main._Logger.LogInfo("Item spawned safely");
        }

        void EnablePhysics()
        {
            foreach (var rb in FindObjectsOfType<Rigidbody>())
                rb.isKinematic = false;
        }

        void SpawnDebugCube()
        {
            var cam = TheForest.Utils.LocalPlayer.MainCam;

            Main._Logger.LogInfo("Spawning cube at camera");

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
            cube.transform.rotation = Quaternion.identity;
            cube.transform.localScale = Vector3.one * 0.3f;

            cube.transform.SetParent(cam.transform); // <--- KEY PART

            var renderer = cube.GetComponent<Renderer>();
            renderer.material = new Material(Shader.Find("Standard"));
            renderer.material.color = Color.red;

            cube.layer = 0; // Default

            Destroy(cube.GetComponent<Collider>());
        }

        //void Update()
        //{
        //    // Detect what item is in the Left and Right Hand

        //    if (!LocalPlayer.GameObject) {  return; } //Main._Logger.LogInfo("Player is null");


        //    var Inventory = References.GetPlayerInventory();
        //    var LeftHand = PInventory.GetHeldItem(Inventory, TheForest.Items.Item.EquipmentSlot.LeftHand);
        //    var RightHand = PInventory.GetHeldItem(Inventory, TheForest.Items.Item.EquipmentSlot.RightHand);

        //    Main._Logger.LogInfo($"LeftHand: {LeftHand?.name} - RightHand: {RightHand?.name}");


        //    // This runs every frame
        //    if (TheForest.Utils.Input.GetKeyDown(KeyCode.F))
        //    {
        //        Main._Logger.LogInfo($"LighterId: {LocalPlayer.AnimControl._lighterId}");

        //        Inventory.EquipItem(RightHand, Item.EquipmentSlot.LeftHand);
        //        //LocalPlayer.
        //        //LocalPlayer.Inventory.Equip(, false);
        //        //LocalPlayer.Inventory.StashLeftHand();

        //        Debug.Log("Offhand toggle pressed");
        //    }
        //}
    }
}
