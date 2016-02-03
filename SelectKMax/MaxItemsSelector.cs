using System;
using System.Collections.Generic;
using System.Linq;

namespace SelectKMax
{
    internal static class MaxItemsSelector<T>
        where T : struct, IComparable
    {
        public static T[] SelectMaxItems(IEnumerable<T> sequence, int itemCount)
        {
            if (itemCount == 0)
            {
                return new T[] { };
            }
            Heap<T> result = new Heap<T>(Heap<T>.HeapOrder.Min);
            foreach (T item in sequence)
            {
                if (result.Count < itemCount)
                {
                    result.MinHeapInsertItem(item);
                }
                else
                {
                    if (result.Root.CompareTo(item) < 0)
                    {
                        result.ReplaceRoot(item);
                    }
                }
            }
            return result.ToArray();
        }
    }
}