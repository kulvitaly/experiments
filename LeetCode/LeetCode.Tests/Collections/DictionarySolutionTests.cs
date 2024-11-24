using LeetCode.Collections;

namespace LeetCode.Tests.Collections;

public class DictionarySolutions
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
    [InlineData(new[] { 2, 7, 11, 15 }, 18, new[] { 1, 2 })]
    [InlineData(new[] { 2, 7, 11, 15 }, 10, null)]
    [InlineData(new[] { 2, 7, 11, 15 }, 14, null)]
    [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
    public void TwoSum_InputData_ShouldSucceed(int[] array, int target, int[] expected)
    {
        Assert.Equal(expected, new DictionarySolution().TwoSum(array, target));
    }
}
