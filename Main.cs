using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Forest_Mod.Configs;
using HarmonyLib;

namespace Forest_Mod
{
    // Token: 0x02000002 RID: 2
    [BepInPlugin("forest.multipatch.zorian", "Forest Example", "1.0")]
    public class Main : BaseUnityPlugin
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private void Awake()
        {
            Main.Instance = this;
            Main._Logger = base.Logger;
            base.Logger.LogInfo("ForestExample loaded");
            Main.ConfigFile = base.Config;
            WireAllConfigs.Wire(base.Config, true);
            base.Logger.LogInfo("ForestExample checking config");
            foreach (KeyValuePair<ConfigDefinition, ConfigEntryBase> keyValuePair in base.Config)
            {
                base.Logger.LogInfo(string.Format("Key: {0}, Value: {1}", keyValuePair.Key, keyValuePair.Value.BoxedValue));
            }
            Main.harmony = new Harmony("forest.multipatch.zor");
            Main.harmony.PatchAll();
        }

        // Token: 0x06000002 RID: 2 RVA: 0x0000212C File Offset: 0x0000032C
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

        // Token: 0x04000001 RID: 1
        public static ConfigFile ConfigFile;

        // Token: 0x04000002 RID: 2
        public static Main Instance;

        // Token: 0x04000003 RID: 3
        public static ManualLogSource _Logger;

        // Token: 0x04000004 RID: 4
        public static Harmony harmony;
    }
}
