using System.Collections;

namespace SkyLine;

public sealed record HouseInfo(int Left, int Right, int Height);

public sealed record SkyLinePoint(int X, int Height);

public static class SkyLineCalculator
{
    public static IReadOnlyCollection<SkyLinePoint> CalculateSkyLine(
        IReadOnlyList<HouseInfo> houses)
    {
        var borderGroups = houses
            .SelectMany(GetHouseBorders)
            .OrderBy(border => border.X)
            .GroupByAdjacent(border => border.X);
        var activeHeights = new PriorityQueue<int, int>(new ReverseComparer<int>());
        var result = new List<SkyLinePoint>();
        int? lastHeight = null;
        foreach (var borderGroup in borderGroups)
        {
            activeHeights.EnqueueRange(
                borderGroup
                    .Where(border => border.BorderType == BorderType.Left)
                    .Select(border => (border.House.Right, border.House.Height)));
            var currentX = borderGroup.Key;
            activeHeights.DequeueWhile((right, _) => right <= currentX);
            var currentHeight = activeHeights.TryPeek(out _, out var height) ? height : 0;
            if (lastHeight != currentHeight)
            {
                result.Add(new SkyLinePoint(currentX, currentHeight));
                lastHeight = currentHeight;
            }
        }
        return result;
    }

    private static IReadOnlyCollection<HouseBorder> GetHouseBorders(HouseInfo house) =>
    [
        new(BorderType.Left, house.Left, house),
        new(BorderType.Right, house.Right, house),
    ];

    private enum BorderType
    {
        Left,
        Right,
    }

    private sealed record HouseBorder(BorderType BorderType, int X, HouseInfo House);

    private sealed class ReverseComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T? x, T? y) => y?.CompareTo(x) ?? 0;
    }
}

internal static class PriorityQueueExtensions
{
    public static void DequeueWhile<TElement, TPriority>(
        this PriorityQueue<TElement, TPriority> source, Func<TElement, TPriority, bool> predicate)
    {
        while (source.TryPeek(out var element, out var priority) && predicate(element, priority))
        {
            source.Dequeue();
        }
    }
}

internal static class EnumerableExtensions
{
    public static IEnumerable<IGrouping<TKey, TSource>> GroupByAdjacent<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        var isFirst = true;
        var currentKey = default(TKey);
        var currentValues = new List<TSource>();
        foreach (var item in source)
        {
            var key = keySelector(item);
            if (EqualityComparer<TKey>.Default.Equals(currentKey, key) || isFirst)
            {
                isFirst = false;
            }
            else
            {
                yield return new Grouping<TKey, TSource>(currentKey!, currentValues);
                currentValues = new List<TSource>();
            }
            currentKey = key;
            currentValues.Add(item);
        }

        if (currentValues.Count > 0)
        {
            yield return new Grouping<TKey, TSource>(currentKey!, currentValues);
        }
    }

    private sealed record Grouping<TKey, TSource>(TKey Key, IEnumerable<TSource> Values)
        : IGrouping<TKey, TSource>
    {
        public IEnumerator<TSource> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
