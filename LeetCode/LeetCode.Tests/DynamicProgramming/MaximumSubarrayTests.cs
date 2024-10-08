using FluentAssertions;
using LeetCode.DynamicProgramming.MaximumSubarray;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Tests.DynamicProgramming;

public class MaximumSubarrayTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    void GenerateParenthesis_ShouldSucceed(int[] nums, int expected)
    {
        new Solution().MaxSubArray(nums)
            .Should()
            .Be(expected);
    }


    public static IEnumerable<object[]> TestData =
        [
            [new int[] { 1 }, 1],
            [new int[] { -1 }, -1],
            [new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6 ],
            [new int[] { 5, 4, -1, 7, 8 }, 23]
        ];
}
