using System.Collections.Generic;
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
    }
}
