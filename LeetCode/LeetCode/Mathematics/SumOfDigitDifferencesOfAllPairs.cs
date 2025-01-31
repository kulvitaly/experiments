namespace LeetCode.Mathematics.SumOfDigitDifferencesOfAllPairs;

public class Solution
{
    public long SumDigitDifferences(int[] nums)
    {
        var digitCounts = CountDigits(nums);

        long totalDifference = 0;

        foreach (var digitCount in digitCounts)
        {
            foreach (var count in digitCount.Values)
            {
                totalDifference += count * (nums.Length - count);
            }
        }

        return totalDifference / 2;
    }

    private static List<Dictionary<byte, uint>> CountDigits(int[] nums)
    {
        List<Dictionary<byte, uint>> digitCounts = new();

        foreach (var num in nums)
        {
            CountNumberDigits(num);
        }

        return digitCounts;

        void CountNumberDigits(int number)
        {
            int idx = 0;
            while (number > 0)
            {
                var digit = (byte)(number % 10);

                if (digitCounts.Count == idx)
                {
                    digitCounts.Add([]);
                }

                if (digitCounts[idx].TryGetValue(digit, out var count))
                {
                    digitCounts[idx][digit] = count + 1;
                }
                else
                {
                    digitCounts[idx].Add(digit, 1);
                }

                idx++;
                number /= 10;
            }
        }
    }
}
