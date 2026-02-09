using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forest_Mod.Extensions
{
    public static class EnumExtensions
    {

        public static bool TryParseEnum<T>(this string TypeName, out T result) where T : struct, Enum
        {
            result = default(T);

            try { result = (T)Enum.Parse(typeof(T), TypeName, true); return true; } 
            catch (Exception Ex) { return false; }
        }
    }
}
