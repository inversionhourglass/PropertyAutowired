using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace PropertyAutowired.Fody
{
    internal static class Extensions
    {
        public static void InsertRange<T>(this Collection<T> collection, int index, IList<T> items)
        {
            for (var i = items.Count - 1; i >= 0; i--)
            {
                collection.Insert(index, items[i]);
            }
        }

        public static void AddRange<T>(this Collection<T> collection, IList<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static bool AddRangeUnique<TKey, TValue, TItem>(this Dictionary<TKey, TValue> dictionary, IList<TItem> items, Func<TItem, KeyValuePair<TKey, TValue>> func, out TItem duplicateItem)
        {
            duplicateItem = default;
            if (items == null) return true;

            foreach (var item in items)
            {
                var kvp = func(item);
                if (dictionary.ContainsKey(kvp.Key))
                {
                    duplicateItem = item;
                    return false;
                }
                dictionary.Add(kvp.Key, kvp.Value);
            }
            return true;
        }

        public static Collection<Instruction> GetFromPosition(this Position position, Collection<Instruction> firstOfAll, Collection<Instruction> afterDefaultInit, Collection<Instruction> afterBaseConstructor, Collection<Instruction> endOfConstructor)
        {
            switch (position)
            {
                case Position.FirstOfAll:
                    return firstOfAll;
                case Position.AfterDefaultInit:
                    return afterDefaultInit;
                case Position.AfterBaseConstructor:
                    return afterBaseConstructor;
                case Position.EndOfConstructor:
                    return endOfConstructor;
                default:
                    throw new ArgumentException("unknow position: " + position);
            }
        }

        public static int FindIndex<T>(this Collection<T> collection, int startIndex, Predicate<T> match)
        {
            if (collection == null || collection.Count == 0 || startIndex < 0 || startIndex >= collection.Count) return -1;
            for (int i = startIndex; i < collection.Count; i++)
            {
                if (match(collection[i])) return i;
            }
            return -1;
        }
    }
}
