using LeetCode.Collections.FindThePowerOfKSizeSubarrays2;

namespace LeetCode.Tests.Collections.FindThePowerOfKSizeSubarrays2;

public class FindThePowerOfKSizeSubarrays2Tests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 4, 3, 2, 5 }, 3, new int[] { 3, 4, -1, -1, -1 })]
    [InlineData(new int[] { 2, 2, 2, 2, 2 }, 4, new int[] { -1, -1 })]
    [InlineData(new int[] { 3, 2, 3, 2, 3, 2 }, 2, new int[] { -1, 3, -1, 3, -1 })]
    [InlineData(new int[] { 1 }, 1, new int[] { 1 })]
    [InlineData(new int[] { 1, 1 }, 1, new int[] { 1, 1 })]
    public void FindPowerOfKSizeSubarrays_ShouldSucceed(int[] nums, int k, int[] expected)
        => new Solution()
            .ResultsArray(nums, k)
            .Should()
            .BeEquivalentTo(expected);
}
