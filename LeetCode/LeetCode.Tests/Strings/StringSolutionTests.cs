using LeetCode.Strings;
using Xunit;

namespace LeetCode.Tests.Strings
{
    public class StringSolutionTests
    {
        [Theory]
        [InlineData(new[] { "flower", "flow", "flight" }, "fl")]
        [InlineData(new[] { "dog", "racecar", "car" }, "")]
        [InlineData(new string[0], "")]
        [InlineData(new[] { "aa", "a" }, "a")]
        public void LongestCommonPrefix_ShouldSucceed(string[] words, string expected)
        {
            Assert.Equal(expected, new StringSolution().LongestCommonPrefix(words));
        }
    }
}
