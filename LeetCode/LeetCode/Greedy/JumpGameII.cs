namespace LeetCode.Greedy.JumpGameII;

public class Solution
{
    public int Jump(int[] nums)
    {
        int jumpCount = 0;
        int start = 0;
        var end = nums.Length - 1;

        while (start < end)
        {
            ++jumpCount;
            start = Jump(nums, start, end);
        }

        return jumpCount;
    }

    private int Jump(int[] nums, int start, int end)
    {
        if (start == end)
        {
            return 0;
        }

        var maxCurrentJump = nums[start];
        if (start + maxCurrentJump >= end)
        {
            return start + (end - start);
        }

        var maxTwoJumps = maxCurrentJump + nums[start + maxCurrentJump];
        var jump = maxCurrentJump;

        for (int i = 1; i <= maxCurrentJump; ++i)
        {
            if (i + nums[start + i] > maxTwoJumps)
            {
                maxTwoJumps = i + nums[start + i];
                jump = i;
            }
        }

        return start + jump;
    }
}
