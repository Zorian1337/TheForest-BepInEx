using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bolt;


namespace Forest_Mod.Custom
{
    //CoopPeerStarter (where we can properly register a new event)
    //CoopUtils.AttachLocalPlayer  public static BoltEntity AttachLocalPlayer(string name)

    [HarmonyPatch(typeof(CoopPeerStarter), "BoltSetup")]
    internal class NetworkAttachmentFilter
    {
        //static void Postfix(BoltEntity entity)
        //{

        //    if (entity.gameObject.CompareTag("Player"))
        //    {
        //        UnityEngine.Debug.Log("Player connected: " + entity.name);
        //    }
        //}
        static void PostFix(CoopPeerStarter __instance)
        {
            // Attempt to register a custom event for later 
            BoltNetwork.RegisterTokenClass<CoopForestModConfigSync>();
        }
    }
}
