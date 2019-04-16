using System;
using System.Collections.Generic;

namespace LeetCode
{
    public class Solution
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

        public int Reverse(int x)
        {
            unchecked
            {
                long result = 0;

                long tmpX = Math.Abs((long)x);

                while (tmpX > 0)
                {
                    result = 10 * result + tmpX % 10;
                    tmpX = tmpX / 10;
                }

                if (x > 0)
                {
                    if (result > int.MaxValue)
                        return 0;

                    return (int)result;
                }

                result = -result;

                if (result < int.MinValue)
                    return 0;

                return (int)result;
            }
        }

        public bool IsPalindrome(int x)
        {
            var str = x.ToString();

            for (int i = 0; i < str.Length - i - 1; ++i)
            {
                if (str[i] != str[str.Length - i - 1])
                    return false;
            }

            return true;
        }

        public static void Main()
        {
        }
    }
}
