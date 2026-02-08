using System;
using System.Reflection;

namespace Forest_Mod
{
    // Token: 0x02000003 RID: 3
    public static class ReflectionHelper
    {
        // Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
        public static void SetPrivateField(object instance, string fieldName, object value)
        {
            bool flag = instance == null;
            if (flag)
            {
                throw new ArgumentNullException("instance");
            }
            FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            bool flag2 = field == null;
            if (flag2)
            {
                throw new MissingFieldException(instance.GetType().Name, fieldName);
            }
            field.SetValue(instance, value);
        }
    }
}
