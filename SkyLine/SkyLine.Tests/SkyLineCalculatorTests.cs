namespace SkyLine.Tests;

public sealed class SkyLineCalculatorTests
{
    public static object[][] GetTestCases() =>
        new object[][]
        {
            [
                Array.Empty<HouseInfo>(),
                Array.Empty<SkyLinePoint>(),
            ],
            [
                new HouseInfo[]
                {
                    new(2, 9, 10),
                    new(3, 7, 15),
                    new(5, 12, 12),
                    new(15, 20, 10),
                    new(19, 24, 8),
                },
                new SkyLinePoint[]
                {
                    new(2, 10),
                    new(3, 15),
                    new(7, 12),
                    new(12, 0),
                    new(15, 10),
                    new(20, 8),
                    new(24, 0),
                },
            ],
            [
                new HouseInfo[]
                {
                    new(2, 9, 10),
                    new(3, 7, 15),
                    new(5, 12, 12),
                    new(15, 20, 10),
                    new(19, 24, 8),
                },
                new SkyLinePoint[]
                {
                    new(2, 10),
                    new(3, 15),
                    new(7, 12),
                    new(12, 0),
                    new(15, 10),
                    new(20, 8),
                    new(24, 0),
                },
            ],
            [
                new HouseInfo[]
                {
                    new(3, 6, 15),
                    new(2, 3, 10),
                    new(1, 3, 15),
                },
                new SkyLinePoint[]
                {
                    new(1, 15),
                    new(6, 0),
                },
            ],
        };

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void Calculate_ShouldCalculateCorrectSkyLine(
        IReadOnlyList<HouseInfo> houses, IReadOnlyCollection<SkyLinePoint> expectedResult)
    {
        var result = SkyLineCalculator.CalculateSkyLine(houses);

        Assert.Equal(expectedResult, result);
    }
}