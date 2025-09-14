using System.Diagnostics;

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
        var result = new List<SkyLinePoint>();
        var orderedBorders = houses
            .SelectMany(GetHouseBorders)
            .OrderBy(border => border, new HouseBorderComparer());
        var currentHouses = new PriorityQueue<HouseInfo, Height>(new ReverseComparer<Height>());
        var lastPosition = (Position?)null;
        var lastHeight = new Height(0);
        foreach (var border in orderedBorders)
        {
            var currentPosition = border.Position;
            if (border.BorderType == BorderType.Left)
            {
                currentHouses.Enqueue(border.House, border.House.Height);
            }

            currentHouses.DequeueWhile((house, _) => house.Right.Value <= currentPosition.Value);
            var currentHeight = currentHouses.TryPeek(out _, out var height) ? height : 0;
            if (currentPosition != lastPosition && currentHeight != lastHeight)
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
}

internal enum BorderType
{
    Left,
    Right,
}

internal sealed record HouseBorder(
    BorderType BorderType, Position Position, HouseInfo House);

internal sealed class ReverseComparer<T> : IComparer<T>
    where T : struct, IComparable<T>
{
    public int Compare(T x, T y) => y.CompareTo(x);
}

internal sealed class HouseBorderComparer : IComparer<HouseBorder>
{
    public int Compare(HouseBorder? x, HouseBorder? y)
    {
        if (x is null || y is null)
        {
            throw new UnreachableException();
        }

        var positionComparison = x.Position.CompareTo(y.Position);
        if (positionComparison != 0)
        {
            return positionComparison;
        }

        var borderTypeComparison = x.BorderType.CompareTo(y.BorderType);
        if (borderTypeComparison != 0)
        {
            return borderTypeComparison;
        }

        return x.BorderType == BorderType.Left
            ? y.House.Height.CompareTo(x.House.Height)
            : x.House.Height.CompareTo(y.House.Height);
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
