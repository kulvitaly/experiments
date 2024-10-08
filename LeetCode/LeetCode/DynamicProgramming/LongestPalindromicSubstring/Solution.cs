namespace LeetCode.DynamicProgramming.LongestPalindromicSubstring;

public class Solution
{
    public string LongestPalindrome(string s)
    {
        var palindromeMatrix = new bool[s.Length, s.Length];
        for (int i = 0; i < s.Length; ++i)
        {
            palindromeMatrix[i, i] = true;
        }

        (int Start, int Length) = (s.Length - 1, 1);

        int lastFoundLength = 1;
        for (int i = 0; i < s.Length - 1; ++i)
        {
            if (s[i] == s[i + 1])
            {
                palindromeMatrix[i, i + 1] = true;

                (Start, Length) = (i, 2);
                lastFoundLength = 2;
            }
        }

        for (int length = 3; length <= s.Length; ++length)
        {
            for (int i = 0; i < s.Length - length + 1; ++i)
            {
                int j = i + length - 1;

                if (s[i] == s[j] && palindromeMatrix[i + 1, j - 1])
                {
                    palindromeMatrix[i, j] = true;
                    (Start, Length) = (i, length);
                    lastFoundLength = length;
                }
            }

            if (lastFoundLength < length - 1)
            {
                break;
            }
        }

        return s.Substring(Start, Length);
    }
}
