using LeetCode.Mathematics;

namespace LeetCode.Tests.Mathematics;

public class MathSolutionTests
{
    [Theory]
    [InlineData(123, 321)]
    [InlineData(-123, -321)]
    [InlineData(120, 21)]
    [InlineData(1534236469, 0)]
    public void Reverse_InputData_ShouldSucceed(int value, int expected)
    {
        Assert.Equal(expected, new MathSolution().Reverse(value));
    }

    [Theory]
    [InlineData(123, false)]
    [InlineData(1, true)]
    [InlineData(121, true)]
    [InlineData(12, false)]
    [InlineData(-121, false)]
    public void IsPalindrome_ShouldSucceed(int x, bool expected)
    {
        Assert.Equal(expected, new MathSolution().IsPalindrome(x));
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("V", 5)]
    [InlineData("IV", 4)]
    [InlineData("IX", 9)]
    [InlineData("LVIII", 58)]
    [InlineData("MCMXCIV", 1994)]
    public void RomanToInt_ShouldSucceed(string romanValue, int expected)
    {
        Assert.Equal(expected, new MathSolution().RomanToInt(romanValue));
    }
}
