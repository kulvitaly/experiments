using LeetCode.Mathematics.FindTheLengthOfTheLongestCommonPrefix;

namespace LeetCode.Tests.Mathematics;

public class FindTheLengthOfTheLongestCommonPrefixTests
{
    [Theory]
    [InlineData(new int[] { 1, 10, 100 }, new int[] { 100 }, 3)]
    [InlineData(new int[] { 1, 2, 3 }, new int[] { 4, 4, 4 }, 0)]
    public void LongestCommonPrefix_ShouldSucceed(int[] arr1, int[] arr2, int expected)
        => new Solution()
            .LongestCommonPrefix(arr1, arr2)
            .Should()
            .Be(expected);
}
