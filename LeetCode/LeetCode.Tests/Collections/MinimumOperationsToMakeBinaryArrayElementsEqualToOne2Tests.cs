using LeetCode.Collections.MinimumOperationsToMakeBinaryArrayElementsEqualToOne2;

namespace LeetCode.Tests.Collections;

public class MinimumOperationsToMakeBinaryArrayElementsEqualToOne2Tests
{
    [Theory]
    [InlineData(new int[] { 0, 1, 1, 0, 1 }, 4)]
    [InlineData(new int[] { 1, 0, 0, 0 }, 1)]
    public void MinOperations_ShouldSucceed(int[] nums, int expected)
        => new Solution()
            .MinOperations(nums)
            .Should()
            .Be(expected);
}
