using System;

namespace LeetCode.DynamicProgramming.MaximumSubarray;

public class Solution
{
    public int MaxSubArray(int[] nums)
    {
        int maxSum = nums[0], currentSum = nums[0];

        for (int i = 1; i < nums.Length; ++i)
        {
            if (currentSum < 0)
            {
                currentSum = 0;
            }

            currentSum += nums[i];
            
            maxSum = Math.Max(maxSum, currentSum);
        }

        return maxSum;
    }
}
