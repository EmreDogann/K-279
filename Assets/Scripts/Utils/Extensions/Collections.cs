using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Utils.Extensions
{
    public static class CollectionExtensions
    {
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key,
            out TValue value)
        {
            key = kvp.Key;
            value = kvp.Value;
        }

        public static bool Remove<TKey, TValue>(this SortedDictionary<TKey, TValue> dict, TKey key, out TValue value)
        {
            if (dict.TryGetValue(key, out value))
            {
                return dict.Remove(key);
            }

            return false;
        }

        public static bool Remove<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, out TValue value)
        {
            if (dict.TryGetValue(key, out value))
            {
                return dict.Remove(key);
            }

            return false;
        }

#if !NET6_0_OR_GREATER
        public static bool TryGetNonEnumeratedCount<T>(this IEnumerable<T> source, out int count)
        {
            if (source is ICollection<T> collection)
            {
                count = collection.Count;
                return true;
            }

            if (source is IReadOnlyCollection<T> rCollection)
            {
                count = rCollection.Count;
                return true;
            }

            count = 0;
            return false;
        }
#endif


        public static string ListToString<T>(this List<T> list, string delimiter = "\n")
        {
            if (list == null)
            {
                return "null";
            }

            int lastIndex = list.Count - 1;
            if (lastIndex == -1)
            {
                return "{}";
            }

            StringBuilder builder = new StringBuilder(500);
            builder.Append('{');
            for (int n = 0; n < lastIndex; n++)
            {
                Append(list[n], builder);
                builder.Append(delimiter);
            }

            Append(list[lastIndex], builder);
            builder.Append('}');

            return builder.ToString();
        }

        private static void Append<T>(this T target, StringBuilder toBuilder)
        {
            if (target == null)
            {
                toBuilder.Append("null");
            }
            else
            {
                switch (target)
                {
                    case Object objTarget:
                        toBuilder.Append("\"");
                        toBuilder.Append(objTarget.name);
                        toBuilder.Append("\" (");
                        toBuilder.Append(target.GetType().Name);
                        toBuilder.Append(")");
                        break;
                    case string stringTarget:
                        toBuilder.Append("\"");
                        toBuilder.Append(stringTarget);
                        toBuilder.Append("\" (");
                        toBuilder.Append(target.GetType().Name);
                        toBuilder.Append(")");
                        break;
                }
            }
        }
    }

#if !NET5_0_OR_GREATER

    internal interface IReadOnlySet<T> : IEnumerable<T>, IReadOnlyCollection<T> {}

#endif
}