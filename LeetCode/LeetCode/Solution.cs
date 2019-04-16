using System;
using System.Collections.Generic;
using System.Text;

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


        public int RomanToInt(string romanValue)
        {
            var mapping = new Dictionary<char, int>
            {
                ['I'] = 1,
                ['V'] = 5,
                ['X'] = 10,
                ['L'] = 50,
                ['C'] = 100,
                ['D'] = 500,
                ['M'] = 1000
            };

            int result = 0;
            int prevValue = 0;
            for (int i = romanValue.Length - 1; i >= 0; --i)
            {
                var tmpValue = mapping[romanValue[i]];

                result += (prevValue > tmpValue) ? -tmpValue : tmpValue;

                prevValue = tmpValue;
            }

            return result;
        }

        public string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length == 0)
                return string.Empty;

            var builder = new StringBuilder();

            for (int i = 0; i < strs[0].Length; ++i)
            {
                char commonChar = strs[0][i];
                for (int j = 0; j < strs.Length; ++j)
                {
                    if (strs[j].Length <= i)
                        return builder.ToString();

                    if (strs[j][i] != commonChar)
                        return builder.ToString();
                }

                builder.Append(commonChar);
            }

            return builder.ToString();
        }

        public static void Main()
        {
        }
    }
}
