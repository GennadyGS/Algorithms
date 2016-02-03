using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SelectKMax
{
    internal class Heap<T> : IEnumerable<T>
        where T : IComparable
    {
        public enum HeapOrder
        {
            Min,
            Max
        }

        private readonly List<T> _items = new List<T>();
        private readonly HeapOrder _order;

        public Heap(HeapOrder order)
        {
            _order = order;
        }

        public int Count => _items.Count;

        public T Root
        {
            get
            {
                if (!_items.Any())
                {
                    throw new Exception("Heap is empty");
                }
                return _items[0];
            }
        }

        /// <summary> Returns an enumerator that iterates through the collection. </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary> Returns an enumerator that iterates through a collection. </summary>
        /// <returns> An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void ReplaceRoot(T item)
        {
            _items[0] = item;
            Heapify(0);
        }

        public void MinHeapInsertItem(T item)
        {
            _items.Add(item);
            int index = _items.Count - 1;
            while (index > 0)
            {
                var heapParentIndex = GetHeapParentIndex(index);
                if (CheckOrder(_items[heapParentIndex], _items[index]))
                {
                    break;
                }
                _items.SwapItems(index, heapParentIndex);
                index = heapParentIndex;
            }
        }

        private void Heapify(int index)
        {
            int minItemIndex = GetUpperItemIndexFromAllChildren(index,
                new[] { GetHeapLeftChildIndex(index), GetHeapRightChildIndex(index) });
            if (minItemIndex != index)
            {
                _items.SwapItems(index, minItemIndex);
                Heapify(minItemIndex);
            }
        }

        private int GetUpperItemIndexFromAllChildren(int index, int[] childIndexes)
        {
            return childIndexes.Aggregate(index, GetUpperItemIndexFromChild);
        }

        private int GetUpperItemIndexFromChild(int index, int childIndex)
        {
            if (childIndex < _items.Count && !CheckOrder(_items[index], _items[childIndex]))
            {
                return childIndex;
            }
            return index;
        }

        private bool CheckOrder(T parentItem, T item)
        {
            var compareResult = parentItem.CompareTo(item);
            return (_order == HeapOrder.Min && compareResult < 0) || (_order == HeapOrder.Max && compareResult > 0);
        }

        private static int GetHeapParentIndex(int index)
        {
            if (index <= 0)
            {
                throw new Exception($"No parent index for {index}");
            }
            return index - 1;
        }

        private static int GetHeapRightChildIndex(int index)
        {
            return (index + 1) << 1 - 1;
        }

        private static int GetHeapLeftChildIndex(int index)
        {
            return (index + 1) << 1;
        }
    }
}