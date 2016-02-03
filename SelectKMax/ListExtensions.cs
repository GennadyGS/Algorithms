using System.Collections.Generic;

namespace SelectKMax
{
    internal static class ListExtensions
    {
        public static void SwapItems<T>(this List<T> list, int index1, int index2)
        {
            if (index1 == index2)
            {
                return;
            }
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}