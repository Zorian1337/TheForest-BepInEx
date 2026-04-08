using Bolt;
using BoltInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Tools;
using UnityEngine;
namespace Forest_Mod.Custom
{
    [BoltGlobalBehaviour]
    public class MyBoltEvents : EventListener
    {
        // Example: Custom event class (you can generate this in Bolt if needed)
        public class AnimalDamageEvent
        {
            public int Damage { get; set; }
        }
    }
}



