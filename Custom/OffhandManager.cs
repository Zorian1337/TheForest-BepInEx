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

        //AssetBundle bundle = AssetBundle.LoadFromFile(@"BepInEx\plugins\samplebundle");
        GameObject itemPrefab;
        void Update()
        {
            if (!LocalPlayer.GameObject) { return; } //Main._Logger.LogInfo("Player is null");


            if (LocalPlayer.Inventory == null) return; // player not ready
            //if (equipmentSlots == null)
            //{
            //    equipmentSlots = ReflectionHelper.GetField<InventoryItemView[]>(LocalPlayer.Inventory, "_equipmentSlots");
            //    if (equipmentSlots == null)
            //    {
            //        Main._Logger.LogInfo("slots are still null");
            //        return; // still not initialized
            //    }
            //    else { Main._Logger.LogInfo("slots arent null"); }
            //}

            //if (itemPrefab is null)
            //{
            //    //Main._Logger.LogInfo("attempting to load pouch");
            //    //itemPrefab = bundle.LoadAsset<GameObject>("TaserStick");//ItemPouch
            //    if (itemPrefab != null) Main._Logger.LogInfo("bundle item loaded sucessfully!");
            //}

            if (TheForest.Utils.Input.GetKeyDown(KeyCode.F))
            {
                Main._Logger.LogInfo("F pressed");


                //.Where(x => x._type == TheForest.Items.Item.Types.Weapon)
                foreach (var item in ItemDatabase.Items.OrderByDescending(x => x._id))
                {
                    

                    Main._Logger.LogInfo($"Item: {item._id} - Name: {item._name} - Cache: {ItemDatabase.ItemIndexById(item._id)}");
                    if (item._name == "Stun Baton")
                    {
                        //ItemUtils.SpawnItem(ItemDatabase.ItemById(), LocalPlayer.Transform.position + Vector3.forward * 2f, Quaternion.identity);
                        item.SpawnItem();
                    }

                    //_pickupPrefab
                    //SpawnCustomItem(item._pickupPrefab.gameObject);
                    //Main._Logger.LogInfo($"{}");
                }

                //SpawnItem();
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

        //void SpawnCustomItem(GameObject Item)
        //{
        //    var cam = LocalPlayer.MainCam;

        //    GameObject instance = SpawnItem2(Item, LocalPlayer.Transform.position + Vector3.forward * 2f);
        //    instance.localScale = Vector3.one;
        //    Main._Logger.LogInfo($"Spawned at: {instance.transform.position}");
        //}


        void SpawnItem()
        {
            var cam = LocalPlayer.MainCam;



            if(itemPrefab is null) Main._Logger.LogInfo("prefab is null so wont spawn");
            Main._Logger.LogInfo("Prefab scale: " + itemPrefab.transform.localScale);

            foreach (var comp in itemPrefab.GetComponents<Component>())
                Main._Logger.LogInfo(comp.GetType().Name);



            var mesh = itemPrefab.GetComponentInChildren<MeshFilter>();
            var renderer = itemPrefab.GetComponentInChildren<MeshRenderer>();
            
            Main._Logger.LogInfo($"Mesh: {mesh?.sharedMesh?.name ?? "null"}, Materials: {(renderer != null ? renderer.sharedMaterials.Length : 0)}");
            Main._Logger.LogInfo($"Mesh: {mesh}, Renderer: {renderer}, Materials: {(renderer != null ? renderer.sharedMaterials.Length : 0)}");

            
            
            //GameObject instance = GameObject.Instantiate(itemPrefab);
            //instance.SetActive(true);
            
            //instance.transform.position = ;
            

            GameObject instance = SpawnItem2(itemPrefab, LocalPlayer.Transform.position + Vector3.forward * 2f);
            Main._Logger.LogInfo($"Spawned at: {instance.transform.position}");
            //below worked

            //var test = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //test.transform.position = LocalPlayer.Transform.position + Vector3.forward * 2f;
            //Main._Logger.LogInfo("test scale: " + test.transform.localScale);
            //Instantiate()
        }

        public static GameObject SpawnItem2(GameObject itemPrefab, Vector3 position, Transform parent = null)
        {
            Vector3 spawnPos = LocalPlayer.Transform.position + LocalPlayer.Transform.forward * 2f;

            GameObject instance = GameObject.Instantiate(itemPrefab, spawnPos, Quaternion.identity);
            instance.name = itemPrefab.name;  // prevent "(Clone)" confusion
            instance.SetActive(true);

            // Reset transform
            instance.transform.localScale = Vector3.one;

            // Force all renderers to be enabled and assign default material if missing
            foreach (var renderer in instance.GetComponentsInChildren<MeshRenderer>(true))
            {
                renderer.enabled = true;
                if (renderer.sharedMaterial == null || renderer.sharedMaterial.shader == null)
                    renderer.sharedMaterial = new Material(Shader.Find("Standard"));
            }

            // Optional: add BoxCollider if missing
            if (instance.GetComponent<Collider>() == null)
                instance.AddComponent<BoxCollider>();

            //// Optional: add Rigidbody if missing
            //if (instance.GetComponent<Rigidbody>() == null)
            //{
            //    var rb = instance.AddComponent<Rigidbody>();
            //    rb.isKinematic = false;
            //}

            // Make it easier to see: highlight with gizmo or temporarily move it up
            instance.transform.position += Vector3.up * 2f;

            Debug.Log($"Spawned {instance.name} at {instance.transform.position}");
            return instance;
        }

        public static GameObject ForceSpawn(GameObject prefab, Vector3 position)
        {
            // Instantiate prefab
            GameObject instance = Instantiate(prefab, position, Quaternion.identity);

            // Ensure active
            instance.SetActive(true);

            // Handle all MeshRenderers
            foreach (var renderer in instance.GetComponentsInChildren<MeshRenderer>(true))
            {
                renderer.enabled = true;
                // Assign a new standard material unconditionally
                renderer.material = new Material(Shader.Find("Standard"));
            }

            // Handle all SkinnedMeshRenderers (in case the prefab has bones)
            foreach (var skinned in instance.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                skinned.enabled = true;
                // Assign a new standard material unconditionally
                skinned.material = new Material(Shader.Find("Standard"));
            }

            // Make sure colliders and rigidbodies are active
            foreach (var col in instance.GetComponentsInChildren<Collider>(true))
                col.enabled = true;

            foreach (var rb in instance.GetComponentsInChildren<Rigidbody>(true))
                rb.isKinematic = false;

            return instance;
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
