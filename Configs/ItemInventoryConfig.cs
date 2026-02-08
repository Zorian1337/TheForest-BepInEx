using System;
using BepInEx.Configuration;

namespace Forest_Mod.Configs
{
    // Token: 0x0200000E RID: 14
    internal static class ItemInventoryConfig
    {
        public static ConfigEntry<bool> IsEnabled;

        public static ConfigEntry<int> CustomStackLimit;

        public static ConfigEntry<string> LimitStackById;

        public static ConfigEntry<string> LimitStackByName;

        public static ConfigEntry<string> LimitStackByType;

        public static ConfigEntry<string> UseDefaultStackLimitByType;

        private static EventHandler<SettingChangedEventArgs> _configHandler;

        public static void Init(ConfigFile config, bool IsWire)
        {
            if (ItemInventoryConfig._configHandler is null)
            {
                ItemInventoryConfig._configHandler = delegate (object _, SettingChangedEventArgs e)
                {
                    if (e.ChangedSetting.Definition.Section == "ItemInventory.StackLimits") ItemInventoryConfig.Parse();
                };
            }

            if (IsWire && !(ItemInventoryConfig._configHandler is null)) config.SettingChanged += ItemInventoryConfig._configHandler;
            else config.SettingChanged -= ItemInventoryConfig._configHandler;
            ItemInventoryConfig.IsEnabled = config.Bind<bool>("ItemInventory.StackLimits", "IsEnabled", true, "Enables or disables the custom stack limit (True: On, False: Off)");
            ItemInventoryConfig.CustomStackLimit = config.Bind<int>("ItemInventory.StackLimits", "CustomStackLimit", 999, "Sets the default item maximum to this number (if item isn't manually set to another)");
            ItemInventoryConfig.LimitStackById = config.Bind<string>("ItemInventory.StackLimits", "LimitStackById", "", "Limits the amount of max items by ItemId");
            ItemInventoryConfig.LimitStackByName = config.Bind<string>("ItemInventory.StackLimits", "LimitStackByName", "pot=1", "Limits the amount of max items by Name");
            ItemInventoryConfig.LimitStackByType = config.Bind<string>("ItemInventory.StackLimits", "LimitStackByType", "", "Limits the amount of max items by Type");
            ItemInventoryConfig.UseDefaultStackLimitByType = config.Bind<string>("ItemInventory.StackLimits", "UseDefaultStackLimitByType", "Weapon", "Disables CustomStackLimit based on ItemTypeFlag (Equipment, CraftingTool, CraftingMaterial, Craftable, Editable, Droppable, Ammo, Projectile, Special, Plant, RangedWeapon, Story, Weapon, Extension, Armor)");
            ItemInventoryConfig.Parse();
        }


        private static void Parse()
        {
        }



    }
}
