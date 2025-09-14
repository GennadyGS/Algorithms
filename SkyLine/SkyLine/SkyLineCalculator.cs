using System.Collections;

namespace SkyLine;

public sealed record HouseInfo(Position Left, Position Right, Height Height);

public sealed record SkyLinePoint(Position Position, Height Height);

public readonly record struct Position(int Value) : IComparable<Position>
{
    public static implicit operator Position(int value) => new(value);

    public int CompareTo(Position other) => Value.CompareTo(other.Value);
}

public readonly record struct Height(int Value) : IComparable<Height>
{
    public static implicit operator Height(int value) => new(value);

    public int CompareTo(Height other) => Value.CompareTo(other.Value);
}

public static class SkyLineCalculator
{
    public static IReadOnlyCollection<SkyLinePoint> CalculateSkyLine(
        IReadOnlyList<HouseInfo> houses)
    {
        var borderGroups = houses
            .SelectMany(GetHouseBorders)
            .OrderBy(border => border.Position)
            .GroupByAdjacent(border => border.Position);
        var currentHeightsByRight =
            new PriorityQueue<Position, Height>(new ReverseComparer<Height>());
        var result = new List<SkyLinePoint>();
        Height? lastHeight = null;
        foreach (var borderGroup in borderGroups)
        {
            var currentPosition = borderGroup.Key;
            currentHeightsByRight.EnqueueRange(
                borderGroup
                    .Where(border => border.BorderType == BorderType.Left)
                    .Select(border => (border.House.Right, border.House.Height)));
            currentHeightsByRight.DequeueWhile((right, _) => right.Value <= currentPosition.Value);
            var currentHeight = currentHeightsByRight.TryPeek(out _, out var height) ? height : 0;
            if (lastHeight != currentHeight)
            {
                result.Add(new SkyLinePoint(currentPosition, currentHeight));
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

    private sealed record HouseBorder(BorderType BorderType, Position Position, HouseInfo House);

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
        var lastKey = default(TKey);
        var lastValues = new List<TSource>();
        foreach (var item in source)
        {
            var key = keySelector(item);
            if (EqualityComparer<TKey>.Default.Equals(lastKey, key) || isFirst)
            {
                isFirst = false;
            }
            else
            {
                yield return new Grouping<TKey, TSource>(lastKey!, lastValues);
                lastValues = new List<TSource>();
            }
            lastKey = key;
            lastValues.Add(item);
        }

        if (lastValues.Count > 0)
        {
            yield return new Grouping<TKey, TSource>(lastKey!, lastValues);
        }
    }

    private sealed record Grouping<TKey, TSource>(TKey Key, IEnumerable<TSource> Values)
        : IGrouping<TKey, TSource>
    {
        public IEnumerator<TSource> GetEnumerator() => Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
