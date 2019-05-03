using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Collections
{
    public class ArraySolution
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
                return 0;

            int position = 0;

            for (int i = 1; i < nums.Length; ++i)
            {
                if (nums[position] < nums[i])
                {
                    nums[++position] = nums[i];
                }
            }

            return position + 1;
        }
    }
}
