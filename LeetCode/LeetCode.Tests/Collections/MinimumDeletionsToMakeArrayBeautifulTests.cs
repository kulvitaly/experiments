using LeetCode.Collections.MinimumDeletionsToMakeArrayBeautiful;

namespace LeetCode.Tests.Collections;

public class MinimumDeletionsToMakeArrayBeautifulTests
{
    [Theory]
    [InlineData(new int[] { 1, 1, 2, 3, 5 }, 1)]
    [InlineData(new int[] { 1, 1, 2, 2, 3, 3 }, 2)]
    [InlineData(new int[] { 8 }, 1)]
    public void MinDeletion_ShouldSucceed(int[] nums, int expected)
        => new Solution()
            .MinDeletion(nums)
            .Should()
            .Be(expected);
}
