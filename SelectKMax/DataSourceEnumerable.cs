using System.Collections;
using System.Collections.Generic;

namespace SelectKMax
{
    internal class DataSourceEnumerable : IEnumerable<int>
    {
        private readonly DataSource _dataSource;

        public DataSourceEnumerable(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        /// <summary> Returns an enumerator that iterates through the collection. </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<int> GetEnumerator()
        {
            while (true)
            {
                int? nextValue = _dataSource.GetNumber();
                if (!nextValue.HasValue)
                {
                    yield break;
                }
                yield return nextValue.Value;
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