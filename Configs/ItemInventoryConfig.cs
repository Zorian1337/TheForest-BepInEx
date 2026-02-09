using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TheForest.Items;

using Item = TheForest.Items.Item;

using Forest_Mod.Extensions;

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

        public static string SECTION = "ItemInventory.StackLimits";

        public static void Init(ConfigFile config, bool IsWire)
        {
            //if (ItemInventoryConfig._configHandler is null)
            //{
            //    ItemInventoryConfig._configHandler = delegate (object _, SettingChangedEventArgs e)
            //    {
            //        if (e.ChangedSetting.Definition.Section == SECTION) ItemInventoryConfig.Parse();
            //    };
            //}

            if (IsWire && !(ItemInventoryConfig._configHandler is null)) config.SettingChanged += ItemInventoryConfig._configHandler;
            else config.SettingChanged -= ItemInventoryConfig._configHandler;

            ItemInventoryConfig.IsEnabled = config.Bind<bool>(SECTION, "IsEnabled", true, "Enables or disables the custom stack limit (True: On, False: Off)");
            ItemInventoryConfig.CustomStackLimit = config.Bind<int>(SECTION, "CustomStackLimit", 999, "Sets the default item maximum to this number (if item isn't manually set to another)");
            ItemInventoryConfig.LimitStackById = config.Bind<string>(SECTION, "LimitStackById", "[]", "Limits the amount of max items by ItemId");
            ItemInventoryConfig.LimitStackByName = config.Bind<string>(SECTION, "LimitStackByName", "[pot=1, TurtleShell=10, SpearUpgraded=5, Spear=5]", "Limits the amount of max items by Name");
            ItemInventoryConfig.LimitStackByType = config.Bind<string>(SECTION, "LimitStackByType", "[Weapon=1]", "Limits the amount of max items by Type");
            ItemInventoryConfig.UseDefaultStackLimitByType = config.Bind<string>(SECTION, "UseDefaultStackLimitByType", "[Weapon, Story, Armor]", "Disables CustomStackLimit based on ItemTypeFlag (Equipment, CraftingTool, CraftingMaterial, Craftable, Editable, Droppable, Ammo, Projectile, Special, Plant, RangedWeapon, Story, Weapon, Extension, Armor)");
            ItemInventoryConfig.Parse();
        }

        public static List<StackLimitTracker> CustomStackLimitList = new List<StackLimitTracker>();
        private static void Parse()
        {
            string rawStackLimitStackById = ItemInventoryConfig.LimitStackById.Value;
            string rawStackLimitByName = ItemInventoryConfig.LimitStackByName.Value;
            string rawStackLimitStackByType = ItemInventoryConfig.LimitStackByType.Value;
            string rawDefaultStackLimitByType = ItemInventoryConfig.UseDefaultStackLimitByType.Value;

            StackLimitTracker.handleCustomLimits(rawStackLimitStackById, StackLimitTracker.LimitedBy.ById);
            StackLimitTracker.handleCustomLimits(rawStackLimitByName, StackLimitTracker.LimitedBy.ByName);
            StackLimitTracker.handleCustomLimits(rawStackLimitStackByType, StackLimitTracker.LimitedBy.ByType);
            StackLimitTracker.handleCustomLimits(rawDefaultStackLimitByType, StackLimitTracker.LimitedBy.ByDefaultType);
        }

        public class StackLimitTracker
        {
            public enum LimitedBy { ById, ByName, ByType, ByDefaultType }
            public int MaxAmount { get; set; } = -1;
            public LimitedBy TypeLimit { get; set; }

            public int ItemId { get; set; }
            
            public StackLimitTracker(int ItemId, int MaxAmount, LimitedBy LimitedBy)
            {
                this.ItemId = ItemId;
                this.MaxAmount = MaxAmount;
                this.TypeLimit = LimitedBy;
            }

            public string ItemName { get; set; }
            public StackLimitTracker(string ItemName, int MaxAmount, LimitedBy LimitedBy)
            {
                this.ItemName = ItemName;
                this.MaxAmount = MaxAmount;
                this.TypeLimit = LimitedBy;
            }

            public Item.Types ItemType { get; set; }
            

            // This is shared with default by type)
            public StackLimitTracker(Item.Types Type, int MaxAmount, LimitedBy LimitedBy)
            {
                this.ItemType = Type;
                this.MaxAmount = MaxAmount;
                this.TypeLimit = LimitedBy;
            }


            public static void handleCustomLimits(string ConfigValue, LimitedBy LimitType)
            {
                // Parses our config data and outputs it into one list filtered via enum LimitedBy

                // Remove spaces for later
                string Modified = ConfigValue.Replace(" ", ""); // this isnt working for some reason

                // Detect brackets so we at least can assume they are following our standards
                if (!(Modified.StartsWith("[") && Modified.EndsWith("]"))) return;

                Modified = ConfigValue.Replace("[", "").Replace("]", "");

                // Split per comma so we can treat them as each individual items
                string[] ItemStrings = Modified.Split(',').Select(x => x.Replace(" ", "")).ToArray();

                //Main._Logger.LogInfo($"filtering items via {LimitType.ToString()}");

                foreach (string ItemString in ItemStrings.Where(x => !x.ToString().IsNullOrWhiteSpace()))
                {
                    //Main._Logger.LogInfo($"detected string: \"{ItemString}\"");
                    switch(LimitType)
                    {
                        case LimitedBy.ById:
                            // Require an Equal to detect if its parsed properly 

                            if (!ItemString.Contains("=")) break; // Item isnt setup properly so skip this one

                            string[] ItemData = ItemString.Split('=');

                            if (int.TryParse(ItemData[0], out int ItemId) && int.TryParse(ItemData[1], out int ItemMaxAmount))
                            {
                                CustomStackLimitList.Add(new StackLimitTracker(ItemId, ItemMaxAmount, LimitedBy.ById));
                            }
                            break;
                        case LimitedBy.ByName:
                            // Require an Equal to detect if its parsed properly 

                            if (!ItemString.Contains("=")) break; // Item isnt setup properly so skip this one

                            ItemData = ItemString.Split('=');

                            if (int.TryParse(ItemData[1], out ItemMaxAmount))
                            {
                                CustomStackLimitList.Add(new StackLimitTracker(ItemData[0], ItemMaxAmount, LimitedBy.ByName));
                            }

                            break;
                        case LimitedBy.ByType:
                            // Require an Equal to detect if its parsed properly 

                            if (!ItemString.Contains("=")) break; // Item isnt setup properly so skip this one

                            ItemData = ItemString.Split('=');
                            //Item.Types.
                            
                            if(ItemData[0].TryParseEnum<Item.Types>(out Item.Types ItemType) && int.TryParse(ItemData[1], out ItemMaxAmount))
                            {
                                CustomStackLimitList.Add(new StackLimitTracker(ItemType, ItemMaxAmount, LimitedBy.ByType));
                            }
                            break;
                        case LimitedBy.ByDefaultType:
                            if (ItemString.TryParseEnum<Item.Types>(out ItemType))
                            {
                                CustomStackLimitList.Add(new StackLimitTracker(ItemType, -1, LimitedBy.ByDefaultType));
                            }
                            break;
                    }
                }
                Main._Logger.LogInfo($"");


                foreach (var item in CustomStackLimitList)
                {
                    Main._Logger.LogInfo($"ItemId: {item?.ItemId} -ItemName: {item?.ItemName} - ItemType: {item?.ItemType.ToString()} - MaxAmount: {item.MaxAmount} - Filter: {item.TypeLimit.ToString()}");
                }

            }

            //public static bool IsValidItem(string Item)
        } 


    }
}
