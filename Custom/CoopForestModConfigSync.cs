using BepInEx.Configuration;
using Bolt;
using Forest_Mod.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using UdpKit;
using static System.Collections.Specialized.BitVector32;

namespace Forest_Mod.Custom
{

    //public class CoopForestModConfigSerealizable
    //{

    //}
    // use lb_Bird.DieFire() for examples of how to attach this packet
    /// <summary>
    /// Packet meant to keep our hosts mods synced with the client if the host has any available
    /// </summary>
    public class CoopForestModConfigSync : IProtocolToken
    {
        public CoopForestModConfigSync() { }
        public CoopForestModConfigSync(ConfigFile HostConfig) => HostConfig = Config;

        public void Read(UdpPacket packet)
        {
            throw new NotImplementedException();
        }

        public void Write(UdpPacket packet)
        {
            //packet.Write()
        }

        public ConfigFile Config { get; set; }

        //public enum ConfigType
        //{
        //    Inventory,
        //    Visibilty
        //}

        //public static string GetSectionBasedOnConfigType(ConfigType type)
        //{
        //    switch(type)
        //    {
        //        case ConfigType.Inventory: return ItemInventoryConfig.SECTION;
        //        case ConfigType.Visibilty: return VisiblityConfig.SECTION;
        //    }

        //    return String.Empty;
        //}
    }
}
