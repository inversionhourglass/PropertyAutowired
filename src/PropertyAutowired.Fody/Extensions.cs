using Mono.Collections.Generic;

namespace PropertyAutowired.Fody
{
    internal static class Extensions
    {
        public static void InsertRange<T>(this Collection<T> collection, int index, Collection<T> items)
        {
            for (var i = items.Count - 1; i >= 0; i--)
            {
                collection.Insert(index, items[i]);
            }
        }
    }
}
