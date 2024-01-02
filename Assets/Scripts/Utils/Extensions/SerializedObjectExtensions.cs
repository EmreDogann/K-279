using System;
using UnityEditor;

namespace Utils.Extensions
{
    public static class SerializedObjectExtensions
    {
        public static SerializedProperty FindAutoProperty(this SerializedObject @this, string name)
        {
            return @this.FindProperty(GetBackingFieldName(name));
        }

        public static SerializedProperty FindAutoPropertyRelative(this SerializedProperty @this, string name)
        {
            return @this.FindPropertyRelative(GetBackingFieldName(name));
        }

        private static string GetBackingFieldName(string name)
        {
#if NET_STANDARD || NET_STANDARD_2_1
            return string.Create(1 /*<*/ + name.Length + 16 /*>k__BackingField*/, name, static (span, name) =>
            {
                span[0] = '<';
                name.AsSpan().CopyTo(span[1..]);
                ">k__BackingField".AsSpan().CopyTo(span[(name.Length + 1)..]);
            });
#else
            return '<' + name + ">k__BackingField";
#endif
        }
    }
}