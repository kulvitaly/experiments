namespace LeetCode.Mathematics.FindTheLengthOfTheLongestCommonPrefix;

public class Solution
{
    public int LongestCommonPrefix(int[] arr1, int[] arr2)
    {
        var commonPrefix = 0;

        var allArr1Prefixes = new HashSet<int>(arr1.SelectMany(GetPrefixesDesc));

        foreach (var number in arr2)
        {
            foreach (var prefix in GetPrefixesDesc(number))
            {
                var digitCount = GetDigitCount(prefix);
                if (digitCount <= commonPrefix)
                {
                    break;
                }

                if (allArr1Prefixes.Contains(prefix))
                {
                    commonPrefix = Math.Max(commonPrefix, digitCount);
                }
            }
        }

        return commonPrefix;
    }

    private static IEnumerable<int> GetPrefixesDesc(int number)
    {
        while (number > 0)
        {
            yield return number;
            number /= 10;
        }
    }

    private static int GetDigitCount(int number)
    {
        if (number == 0)
            return 1;

        return (int)Math.Log10(number) + 1;
    }
}
