using LeetCode.DynamicProgramming.LongestPalindromicSubstring;

namespace LeetCode.Tests.DynamicProgramming;

public class LongestPalindromeSubstringTests
{
    [Theory]
    [InlineData("babad", "aba")]
    [InlineData("cbbd", "bb")]
    [InlineData("abcba", "abcba")]
    void LongestPalindrome_ShouldSucceed(string s, string expected)
    {
        new Solution().LongestPalindrome(s)
            .Should()
            .Be(expected);
    }
}
