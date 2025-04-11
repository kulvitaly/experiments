using LeetCode.BinarySearch.FindInMountainArray;

namespace LeetCode.Tests.BinarySearch;

public class FindInMountainArrayTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 3, 1 }, 3, 2)]
    [InlineData(new[] { 0, 1, 2, 4, 2, 1 }, 3, -1)]
    [InlineData(new[] { 1, 5, 2 }, 2, 2)]
    public void FindInMountainArray_ReturnsExpected(int[] mountainArr, int target, int expected)
        => new Solution().FindInMountainArray(target, new(mountainArr))
            .Should()
            .Be(expected);
}
