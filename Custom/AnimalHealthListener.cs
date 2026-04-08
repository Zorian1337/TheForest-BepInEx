using Bolt;
using BoltInternal;
using TheForest ; // assuming this is where animalHealth is
using TheForest.Tools;
using UnityEngine;
using static Forest_Mod.Custom.MyBoltEvents;

//public class AnimalHealthListener : EventListener
//{
//    // Called when the entity is attached to the network
//    public override void Attached()
//    {
//        //AnimalDamageEvent evnt = new AnimalDamageEvent();

//        base.Attached();
//        Debug.Log($"[Bolt] animalHealth entity attached: {entity.networkId}");
//    }

//    // Example: Listen for a custom event
//    public override void OnEvent(AnimalDamageEvent ev)
//    {
//        Debug.Log($"[Bolt] animal {entity.networkId} took {ev.Damage} damage!");
//        //entity.ApplyDamage(ev.Damage);
//    }
//}