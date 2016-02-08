using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReverseList
{
    internal static class LinkedList
    {
        public static IEnumerable<T> ToEnumerable<T>(this LinkedList<T> list)
        {
            return list ?? Enumerable.Empty<T>();
        }

        public static LinkedList<T> FromEnumerable<T>(IEnumerable<T> source)
        {
            return FromEnumerator(source.GetEnumerator());
        }

        public static LinkedList<T> ReverseList<T>(this LinkedList<T> list)
        {
            return list != null ? ReverseList(list, null) : null;
        }

        private static LinkedList<T> FromEnumerator<T>(IEnumerator<T> source)
        {
            if (!source.MoveNext())
            {
                return null;
            }
            var data = source.Current;
            var next = FromEnumerator(source);
            return new LinkedList<T>(data, next);
        }

        private static LinkedList<T> ReverseList<T>(this LinkedList<T> list, LinkedList<T> reversedList)
        {
            var nextReversedList = new LinkedList<T>(list.Data, reversedList);
            return list.Next != null ? ReverseList(list.Next, nextReversedList) : nextReversedList;
        }
    }

    internal class LinkedList<T> : IEnumerable<T>
    {
        public LinkedList(T data, LinkedList<T> next)
        {
            Data = data;
            Next = next;
        }

        public T Data { get; }

        public LinkedList<T> Next { get; }

        /// <summary> Returns an enumerator that iterates through the collection. </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = new[] { Data };
            return (Next != null ? current.Concat(Next) : current).GetEnumerator();
        }

        /// <summary> Returns an enumerator that iterates through a collection. </summary>
        /// <returns> An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}