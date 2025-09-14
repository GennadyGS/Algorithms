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
            .GroupBy(border => border.X)
            .ToList();
        var activeHeights = new PriorityQueue<int, int>(new ReverseComparer<int>());
        var result = new List<SkyLinePoint>();
        int? lastHeight = null;
        foreach (var borderGroup in borderGroups)
        {
            var currentX = borderGroup.Key;
            foreach (var border in borderGroup)
            {
                if (border.BorderType == BorderType.Left)
                {
                    activeHeights.Enqueue(border.House.Right, border.House.Height);
                }
            }

            while (activeHeights.TryPeek(out var right, out _) && right <= currentX)
            {
                activeHeights.Dequeue();
            };

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

