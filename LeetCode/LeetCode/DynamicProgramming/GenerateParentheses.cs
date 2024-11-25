namespace LeetCode.Tests.DynamicProgramming.GenerateParentheses;

public class Solution
{
    public IList<string> GenerateParenthesis(int n)
    {
        return Generate(string.Empty, 0, 0, n).ToList();
    }

    private IEnumerable<string> Generate(string s, int opened, int closed, int max)
    {
        if (closed == max)
            yield return s;

        if (opened < max)
        {
            foreach (var result in Generate(s + '(', opened + 1, closed, max))
                yield return result;
        }

        if (opened > closed)
        {
            foreach (var result in Generate(s + ')', opened, closed + 1, max))
                yield return result;
        }
    }
}
