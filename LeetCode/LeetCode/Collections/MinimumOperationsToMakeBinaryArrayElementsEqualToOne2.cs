namespace LeetCode.Collections.MinimumOperationsToMakeBinaryArrayElementsEqualToOne2;

public class Solution
{
    public int MinOperations(int[] nums)
    {
        int flipCount = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if ((nums[i] + flipCount) % 2 == 1)
                continue;

            flipCount++;
        }

        return flipCount;
    }
}
