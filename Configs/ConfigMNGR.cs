using System;
using BepInEx.Configuration;

namespace Forest_Mod.Configs
{
    public static class ManageConfigs
    {

        /// <summary>
        /// Area for us to wire all our configs together
        /// </summary>
        /// <param name="config"></param>
        /// <param name="IsWire"></param>
        public static void Wire(ConfigFile config, bool IsWire)
        {
            ItemInventoryConfig.Init(config, IsWire);
        }

        // Handler for updating configs per section
        public static void HandleConfigSettingsUpdate(SettingChangedEventArgs arg)
        {
            // needs to be dynamically registered for each new config so I dont need to add it manually each time

            switch (arg.ChangedSetting.Definition.Section)
            {
                case ItemInventoryConfig.SECTION: break;
            }
        }

    }

}
