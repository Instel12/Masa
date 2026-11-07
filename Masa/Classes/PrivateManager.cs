using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Classes
{
    class PrivateManager
    {
        public static T GetField<T>(object obj, string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            return field != null ? (T)field.GetValue(obj) : default;
        }

        public static void SetField(object obj, string fieldName, object value)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            field?.SetValue(obj, value);
        }

        public static void ClearField(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (field?.GetValue(obj) is System.Collections.IList list) list.Clear();
        }
        public static Type GetClass(object obj, string className)
        {
            return obj.GetType().GetNestedType(
                className,
                BindingFlags.Public | BindingFlags.NonPublic
            );
        }

    }
}
