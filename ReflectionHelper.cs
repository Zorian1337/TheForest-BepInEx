using System;
using System.Reflection;

namespace Forest_Mod
{
    public static class ReflectionHelper
    {
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
