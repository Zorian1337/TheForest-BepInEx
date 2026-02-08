using System;
using BepInEx.Configuration;

namespace Forest_Mod.Configs
{
    // Token: 0x0200000E RID: 14
    internal static class ItemInventoryConfig
    {
        // Token: 0x0600001E RID: 30 RVA: 0x00002B48 File Offset: 0x00000D48
        public static void Init(ConfigFile config, bool IsWire)
        {
            bool flag = ItemInventoryConfig._configHandler == null;
            if (flag)
            {
                ItemInventoryConfig._configHandler = delegate (object _, SettingChangedEventArgs e)
                {
                    bool flag3 = e.ChangedSetting.Definition.Section == "ItemInventory.StackLimits";
                    if (flag3)
                    {
                        ItemInventoryConfig.Parse();
                    }
                };
            }
            bool flag2 = IsWire && ItemInventoryConfig._configHandler != null;
            if (flag2)
            {
                config.SettingChanged += ItemInventoryConfig._configHandler;
            }
            else
            {
                config.SettingChanged -= ItemInventoryConfig._configHandler;
            }
            ItemInventoryConfig.IsEnabled = config.Bind<bool>("ItemInventory.StackLimits", "IsEnabled", true, "Enables or disables the custom stack limit (True: On, False: Off)");
            ItemInventoryConfig.CustomStackLimit = config.Bind<int>("ItemInventory.StackLimits", "CustomStackLimit", 999, "Sets the default item maximum to this number (if item isn't manually set to another)");
            ItemInventoryConfig.LimitStackById = config.Bind<string>("ItemInventory.StackLimits", "LimitStackById", "", "Limits the amount of max items by ItemId");
            ItemInventoryConfig.LimitStackByName = config.Bind<string>("ItemInventory.StackLimits", "LimitStackByName", "pot=1", "Limits the amount of max items by Name");
            ItemInventoryConfig.LimitStackByType = config.Bind<string>("ItemInventory.StackLimits", "LimitStackByType", "", "Limits the amount of max items by Type");
            ItemInventoryConfig.UseDefaultStackLimitByType = config.Bind<string>("ItemInventory.StackLimits", "UseDefaultStackLimitByType", "Weapon", "Disables CustomStackLimit based on ItemTypeFlag (Equipment, CraftingTool, CraftingMaterial, Craftable, Editable, Droppable, Ammo, Projectile, Special, Plant, RangedWeapon, Story, Weapon, Extension, Armor)");
            ItemInventoryConfig.Parse();
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00002C72 File Offset: 0x00000E72
        private static void Parse()
        {
        }

        // Token: 0x0400000A RID: 10
        public static ConfigEntry<bool> IsEnabled;

        // Token: 0x0400000B RID: 11
        public static ConfigEntry<int> CustomStackLimit;

        // Token: 0x0400000C RID: 12
        public static ConfigEntry<string> LimitStackById;

        // Token: 0x0400000D RID: 13
        public static ConfigEntry<string> LimitStackByName;

        // Token: 0x0400000E RID: 14
        public static ConfigEntry<string> LimitStackByType;

        // Token: 0x0400000F RID: 15
        public static ConfigEntry<string> UseDefaultStackLimitByType;

        // Token: 0x04000010 RID: 16
        private static EventHandler<SettingChangedEventArgs> _configHandler;
    }
}
