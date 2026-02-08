using System;
using BepInEx.Configuration;

namespace Forest_Mod.Configs
{
    // Token: 0x0200000F RID: 15
    public static class WireAllConfigs
    {
        // Token: 0x06000020 RID: 32 RVA: 0x00002C75 File Offset: 0x00000E75
        public static void Wire(ConfigFile config, bool IsWire)
        {
            ItemInventoryConfig.Init(config, IsWire);
        }
    }
}
