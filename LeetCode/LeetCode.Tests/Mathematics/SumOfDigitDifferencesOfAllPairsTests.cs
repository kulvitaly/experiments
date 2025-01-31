using LeetCode.Mathematics.SumOfDigitDifferencesOfAllPairs;

namespace LeetCode.Tests.Mathematics;



public class SumOfDigitDifferencesOfAllPairsTests
{
    [Theory]
    [InlineData(new int[] { 13, 23, 12 }, 4)]
    [InlineData(new int[] { 10, 10, 10, 10 }, 0)]
    public void SumDigitDifferences_ShouldReturnSumOfDigitDifferences(int[] nums, long expected)
        => new Solution().SumDigitDifferences(nums)
            .Should()
            .Be(expected);
}
