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
        var houseProcessed = new bool[houses.Count];
        var activeBorders = new PriorityQueue<HouseBorder, int>(new ReverseComparer<int>());
        var result = new List<SkyLinePoint>();
        int? lastHeight = null;
        foreach (var borderGroup in borderGroups)
        {
            foreach (var border in borderGroup)
            {
                if (border.BorderType == BorderType.Left)
                {
                    activeBorders.Enqueue(border, border.Height);
                }
                else
                {
                    houseProcessed[border.HouseIndex] = true;
                }
            }

            while (activeBorders.TryPeek(out var border, out _) && houseProcessed[border.HouseIndex])
            {
                activeBorders.Dequeue();
            };

            var currentHeight = activeBorders.TryPeek(out _, out var height) ? height : 0;
            if (lastHeight != currentHeight)
            {
                result.Add(new SkyLinePoint(borderGroup.Key, currentHeight));
                lastHeight = currentHeight;
            }
        }
        return result;

    }

    private static IReadOnlyCollection<HouseBorder> GetHouseBorders(
        HouseInfo house, int houseIndex) =>
    [
        new(BorderType.Left, houseIndex, house.Left, house.Height),
        new(BorderType.Right, houseIndex, house.Right, house.Height),
    ];

    private enum BorderType
    {
        Left,
        Right,
    }

    private sealed record HouseBorder(
        BorderType BorderType, int HouseIndex, int X, int Height);

    private sealed class ReverseComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T? x, T? y) => y?.CompareTo(x) ?? 0;
    }
}

