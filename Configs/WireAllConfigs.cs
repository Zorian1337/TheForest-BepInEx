using System;
using BepInEx.Configuration;

namespace Forest_Mod.Configs
{

    public static class WireAllConfigs
    {
        public static void Wire(ConfigFile config, bool IsWire)
        {
            ItemInventoryConfig.Init(config, IsWire);
        }
    }
}
