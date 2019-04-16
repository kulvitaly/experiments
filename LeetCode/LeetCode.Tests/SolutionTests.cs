using System;
using Xunit;

namespace LeetCode.Tests
{
    public class SolutionTests
    {
        [Theory]
        [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
        [InlineData(new[] { 2, 7, 11, 15 }, 18, new[] { 1, 2 })]
        [InlineData(new[] { 2, 7, 11, 15 }, 10, null)]
        [InlineData(new[] { 2, 7, 11, 15 }, 14, null)]
        [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
        public void TwoSum_InputData_ShouldSuceed(int[] array, int target, int[] expected)
        {
            Assert.Equal(expected, new Solution().TwoSum(array, target));
        }

        [Theory]
        [InlineData(123, 321)]
        [InlineData(-123, -321)]
        [InlineData(120, 21)]
        [InlineData(1534236469, 0)]
        public void Reverse_InputData_ShouldSuceed(int value, int expected)
        {
            Assert.Equal(expected, new Solution().Reverse(value));
        }

        [Theory]
        [InlineData(123, false)]
        [InlineData(1, true)]
        [InlineData(121, true)]
        [InlineData(12, false)]
        [InlineData(-121, false)]
        public void IsPalindrome_ShouldSucceed(int x, bool expected)
        {
            Assert.Equal(expected, new Solution().IsPalindrome(x));
        }
    }
}
