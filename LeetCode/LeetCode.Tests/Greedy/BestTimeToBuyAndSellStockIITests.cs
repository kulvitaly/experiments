using LeetCode.Greedy.BestTimeToBuyAndSellStockII;

namespace LeetCode.Tests.Greedy;

public class BestTimeToBuyAndSellStockIITests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void ContainerWithMostWater_ShouldSucceed(int[] prices, int expected)
    {
        Assert.Equal(expected, new Solution().MaxProfit(prices));
    }

    public static IEnumerable<object[]> TestData =
        [
            [new int[] { 7, 1, 5, 3, 6, 4 }, 7],
            [new int[] { 1, 2, 3, 4, 5 }, 4],
            [new int[] { 7, 6, 4, 3, 1 }, 0]
        ];
}
