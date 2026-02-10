using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Forest_Mod.Configs;
//using Forest_Mod.Custom;
using Forest_Mod.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using TheForest.Items;
using UnityEngine;

namespace Forest_Mod
{


    [BepInPlugin("forest.multipatch.zorian", "TheForest-MultiPatch", "1.0")]
    public class Main : BaseUnityPlugin
    {
        public const string ModName = "TheForest-MultiPatch";

        private void Awake()
        {
            Main.Instance = this;
            Main._Logger = base.Logger;
            base.Logger.LogInfo($"{ModName} loaded");
            Main.ConfigFile = base.Config;
            WireAllConfigs.Wire(base.Config, true);
            base.Logger.LogInfo("{ModName}} checking config");
            foreach (KeyValuePair<ConfigDefinition, ConfigEntryBase> keyValuePair in base.Config)
            {
                base.Logger.LogInfo(string.Format("Key: {0}, Value: {1}", keyValuePair.Key, keyValuePair.Value.BoxedValue));
            }


            // This is for testing
            //GameObject go = new GameObject("ForestMod_OffhandManager");
            //DontDestroyOnLoad(go);
            //go.AddComponent<OffhandManager>();

            //Logger.LogInfo("OffhandManager injected");

            Main.harmony = new Harmony("forest.multipatch.zorian");
            Main.harmony.PatchAll();
        }


        private void OnDestroy()
        {
            bool flag = Main.harmony != null;
            if (flag)
            {
                Main.harmony.UnpatchSelf();
                Main.harmony = null;
            }
            WireAllConfigs.Wire(base.Config, true);
        }


        public static ConfigFile ConfigFile;

        public static Main Instance;


        public static ManualLogSource _Logger;

        public static Harmony harmony;
    }
}
