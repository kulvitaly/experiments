namespace LeetCode.Collections.MinimumDeletionsToMakeArrayBeautiful;

public class Solution
{
    public int MinDeletion(int[] nums)
    {
        var firstIdx = 0;
        var secondIdx = 1;
        var deletedCount = 0;

        while (true)
        {
            if (firstIdx >= nums.Length)
            {
                break;
            }

            if (firstIdx < nums.Length && secondIdx >= nums.Length)
            {
                deletedCount++;
                break;
            }

            if (nums[firstIdx] == nums[secondIdx])
            {
                deletedCount++;
                secondIdx++;
                continue;
            }

            firstIdx = secondIdx + 1;
            secondIdx = secondIdx + 2;
        }

        return deletedCount;
    }
}
