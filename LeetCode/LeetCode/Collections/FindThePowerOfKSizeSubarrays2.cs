namespace LeetCode.Collections.FindThePowerOfKSizeSubarrays2;

public class Solution
{
    public int[] ResultsArray(int[] nums, int k)
    {
        int lastDesc = -1;

        if (k == 1)
            return nums;

        var results = new int[nums.Length - k + 1];

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[i - 1] + 1)
            {
                lastDesc = i - 1;
            }

            if (i < k - 1)
                continue;

            if (lastDesc >= i - k + 1)
            {
                results[i - k+1] = -1;
            }
            else
            {
                results[i - k+1] = nums[i];
            }
        }

        return results;
    }
}
