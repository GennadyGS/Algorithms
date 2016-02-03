using System;
using System.Collections;
using System.Collections.Generic;

namespace SelectKMax
{
    public class Shuffler<T> : IEnumerable<T>
    {
        private readonly T[] _sourceItems;

        public Shuffler(T[] sourceItems)
        {
            _sourceItems = sourceItems;
        }

        /// <summary> Returns an enumerator that iterates through the collection. </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            var random = new Random();
            T[] itemsArray = new T[_sourceItems.Length];
            _sourceItems.CopyTo(itemsArray, 0);
            for (int i = itemsArray.Length - 1; i >= 0; i--)
            {
                int j = random.Next(i + 1);
                yield return itemsArray[j];
                itemsArray[j] = itemsArray[i];
            }
        }

        /// <summary> Returns an enumerator that iterates through a collection. </summary>
        /// <returns> An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}