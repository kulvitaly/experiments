using LeetCode.Collections.WaysToSplitArrayIntoThreeSubarrays;

namespace LeetCode.Tests.Collections;

public class WaysToSplitArrayIntoThreeSubarraysTests
{
    [Theory]
    [InlineData(new[] { 0, 0, 0 }, 1)]
    [InlineData(new[] { 1, 1, 1 }, 1)]
    [InlineData(new[] { 1, 2, 2, 2, 5, 0 }, 3)]
    [InlineData(new[] { 3, 2, 1 }, 0)]
    [InlineData(new[] { 8892, 2631, 7212, 1188, 6580, 1690, 5950, 7425, 8787, 4361, 9849, 4063, 9496, 9140, 9986, 1058, 2734, 6961, 8855, 2567, 7683, 4770, 40, 850, 72, 2285, 9328, 6794, 8632, 9163, 3928, 6962, 6545, 6920, 926, 8885, 1570, 4454, 6876, 7447, 8264, 3123, 2980, 7276, 470, 8736, 3153, 3924, 3129, 7136, 1739, 1354, 661, 1309, 6231, 9890, 58, 4623, 3555, 3100, 3437 }, 227)]
    public void WaysToSplit_ValidInput_ReturnsExpected(int[] nums, int expected)
    {
        // Act
        var actual = new Solution().WaysToSplit(nums);

        // Assert
        actual.Should().Be(expected);
    }
}
