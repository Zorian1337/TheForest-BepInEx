using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Forest_Mod
{
    public static class AssetManager
    {
        public static List<AssetBundle> LoadedBundles;
        //public static List<GameObject> GameObjectAssets = new List<GameObject>();


        public static void LoadAllBundles()
        {
            LoadBundle(@"BepInEx\plugins\samplebundle");
        }

        public static void LoadBundle(string bundleName)
        {
            if (LoadedBundles is null) LoadedBundles = new List<AssetBundle>();

            AssetBundle bundle = AssetBundle.LoadFromFile(bundleName);
            LoadedBundles.Add(bundle);

            //// Only load unloaded bundles - add override for this later to reset 
            //if (!(LoadedBundles.Any(x => x.name == bundleName)))
            //{
            //    AssetBundle bundle = AssetBundle.LoadFromFile(bundleName);
            //    LoadedBundles.Add(bundle);
            //}
        }

        [Obsolete]
        public static AssetType LoadAssetNOTWORKING<AssetType>(string AssetName) where AssetType : UnityEngine.Object
        {
            // Init everything from this call if its not already
            LoadAllBundles();

            //bundle.LoadAsset<GameObject>("TaserStick");
            AssetBundle bundle = LoadedBundles.Find(x => x.name == AssetName);
            if (!(bundle is null)) return bundle.LoadAsset<AssetType>(AssetName);
            else return default;
        }

        public static AssetType DirectLoadAsset<AssetType>(string bundlePath, string AssetName) where AssetType : UnityEngine.Object
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            return bundle.LoadAsset<AssetType>(AssetName);
        }

        //public static AssetType LoadAsset<AssetType>(string bundleName, string AssetName)
        //{

        //}

        public static void ReloadBundles()
        {

        }
    }
}
