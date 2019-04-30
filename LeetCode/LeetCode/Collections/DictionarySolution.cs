using System.Collections.Generic;

namespace LeetCode.Collections
{
    public class DictionarySolution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            var map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; ++i)
            {
                if (map.TryGetValue(target - nums[i], out var j))
                {
                    return i < j ? new[] { i, j } : new[] { j, i };
                }

                map[nums[i]] = i;
            }
            return null;
        }

    }
}
