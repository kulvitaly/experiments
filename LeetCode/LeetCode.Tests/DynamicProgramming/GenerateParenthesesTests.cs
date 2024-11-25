using LeetCode.Tests.DynamicProgramming.GenerateParentheses;

namespace LeetCode.Tests.DynamicProgramming;

public class GenerateParenthesesTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    void GenerateParenthesis_ShouldSucceed(int n, string[] expected)
    {
        new Solution().GenerateParenthesis(n)
            .Should()
            .BeEquivalentTo(expected);
    }


    public static IEnumerable<object[]> TestData =
        [
            [1, new string[] { "()" }],
            [3, new string[] { "((()))", "(()())", "(())()", "()(())", "()()()" }]
        ];
}
