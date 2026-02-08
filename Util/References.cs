using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;

namespace Forest_Mod.Util
{
    public static class References
    {
        public static LocalPlayer Player { get; private set; }
        public static void SetLocalPlayer(ref LocalPlayer _Player) => Player = _Player;
    }
}
