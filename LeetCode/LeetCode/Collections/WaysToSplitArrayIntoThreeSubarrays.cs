namespace LeetCode.Collections.WaysToSplitArrayIntoThreeSubarrays;

public class Solution
{
    public int WaysToSplitDummy(int[] nums)
    {
        int leftEnd = 0;
        long leftSum = nums[0];

        long totalSum = nums.Sum();

        int splitCount = 0;

        while ((leftSum <= totalSum / 3) && leftEnd < nums.Length - 2)
        {
            if (FindMinSplit(leftEnd, out var middleEnd, out var middleSum))
            {
                splitCount++;

                while (ShiftRight(ref middleEnd, ref middleSum))
                {
                    splitCount++;
                }
            }

            leftEnd++;
            leftSum += nums[leftEnd];
        }

        return splitCount;

        bool FindMinSplit(int left, out int middle, out long middleSum)
        {
            middle = left + 1;
            middleSum = nums[middle];

            while (middle < nums.Length - 2 && middleSum < leftSum)
            {
                middle++;
                middleSum += nums[middle];
            }

            return IsValidSplit(leftSum, middleSum);
        }

        bool ShiftRight(ref int right, ref long sum)
        {
            right++;

            if (right >= nums.Length - 1)
            {
                return false;
            }

            sum += nums[right];
            return IsValidSplit(leftSum, sum);
        }

        bool IsValidSplit(long leftSum, long middleSum)
            => (leftSum <= middleSum) && (middleSum <= (totalSum - leftSum - middleSum));
    }

    public int WaysToSplit(int[] nums)
    {
        var total = nums.Sum();

        (var right, var rightSum) = FindInitialRight();
        if (right == -1)
            return 0;

        var sums = CalcPrefixSums();

        long splitCount = 0;

        while (right > 1)
        {
            var minMiddle = FindCandidate(1, right - 1, i => MiddleSum(i) > rightSum);
            if (IsValidSplit(minMiddle))
            {
                var maxMiddle = FindCandidate(minMiddle, right - 1, i => sums[right - 1] - sums[i] >= sums[i]);
                var splits = maxMiddle - minMiddle + 1;
                splitCount += maxMiddle - minMiddle + 1;
            }

            right--;
            rightSum += nums[right];
        }

        return (int)(splitCount % 1_000_000_007);

        (int, long) FindInitialRight()
        {
            var rightSum = nums[^1];
            var right = nums.Length - 1;

            while (right > 1 && rightSum < total / 3)
            {
                right--;
                rightSum += nums[right];
            }

            return right > 1
                ? (right, rightSum)
                : (-1, 0);
        }

        long[] CalcPrefixSums()
        {
            var sums = new long[right];
            sums[0] = nums[0];
            for (var i = 1; i < right; i++)
            {
                sums[i] = nums[i] + sums[i - 1];
            }

            return sums;
        }

        int FindCandidate(int left, int right, Func<int, bool> predicate)
        {
            if (left >= right)
                return left;

            var mid = (left + right) / 2;
            if (predicate(mid))
                return FindCandidate(Math.Max(mid, left + 1), right, predicate);
            else
                return FindCandidate(left, Math.Min(mid, right - 1), predicate);
        }

        bool IsValidSplit(int middle)
        {
            var middleSum = MiddleSum(middle);
            return middle > 0 && middle < right && middleSum >= sums[middle - 1] && middleSum <= rightSum;
        }

        long MiddleSum(int middle) => total - rightSum - sums[middle - 1];
    }
}