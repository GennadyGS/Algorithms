using System.Collections.Generic;

namespace SelectKMax
{
    public class DataSource
    {
        private readonly IEnumerator<int> _enumerator;

        public DataSource(IEnumerable<int> source)
        {
            _enumerator = source.GetEnumerator();
        }

        public int? GetNumber()
        {
            return _enumerator.MoveNext() ? (int?)_enumerator.Current : null;
        }
    }
}