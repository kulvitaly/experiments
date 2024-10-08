using LeetCode.Greedy.ContainerWithMostWater;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Tests.Greedy;

public class ContainerWithMostWaterTests
{
    private readonly Solution _solution = new();

    [Theory]
    [MemberData(nameof(TestData))]
    public void ContainerWithMostWater_ShouldSucceed(int[] heights, int expected)
    {
        Assert.Equal(expected, _solution.MaxArea(heights));
    }

    public static IEnumerable<object[]> TestData =
        [
            [new int[] { 1, 1 }, 1],
            [new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }, 49],
            [new int[] { 1, 8, 8, 1, 2, 1 }, 8],
            [new int[] { 8, 7, 2, 1 }, 7],
            [new int[] { 2, 3, 4, 5, 18, 17, 6 }, 17]
        ];
}
