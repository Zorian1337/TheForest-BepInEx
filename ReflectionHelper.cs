using System;
using System.Reflection;

namespace Forest_Mod
{
    public static class ReflectionHelper
    {
        [Obsolete]
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

        private const BindingFlags Flags =
    BindingFlags.Instance |
    BindingFlags.Static |
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.FlattenHierarchy;

        // ---------- FIELDS ----------
        public static T GetField<T>(object instance, string fieldName)
        {
            if (instance == null) return default;

            var field = instance.GetType().GetField(fieldName, Flags);
            return field != null ? (T)field.GetValue(instance) : default;
        }

        public static void SetField(object instance, string fieldName, object value)
        {
            if (instance == null) return;

            var field = instance.GetType().GetField(fieldName, Flags);
            field?.SetValue(instance, value);
        }

        // ---------- PROPERTIES ----------
        public static T GetProperty<T>(object instance, string propName)
        {
            if (instance == null) return default;

            var prop = instance.GetType().GetProperty(
                propName,
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy
            );

            if (prop == null)
                return default;

            var getter = prop.GetGetMethod(true); // TRUE = allow non-public
            if (getter == null)
                return default;

            return (T)getter.Invoke(instance, null);
        }

        public static void SetProperty(object instance, string propName, object value)
        {
            if (instance == null) return;

            var prop = instance.GetType().GetProperty(
                propName,
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy
            );

            if (prop == null)
                return;

            var setter = prop.GetSetMethod(true); // TRUE = allow non-public
            if (setter == null)
                return;

            setter.Invoke(instance, new object[] { value });
        }

        // ---------- METHODS ----------
        public static object Call(object instance, string methodName, params object[] args)
        {
            if (instance == null) return null;

            var method = instance.GetType().GetMethod(methodName, Flags);
            return method?.Invoke(instance, args);
        }

        public static T Call<T>(object instance, string methodName, params object[] args)
        {
            object result = Call(instance, methodName, args);
            return result is T t ? t : default;
        }
    }
}
