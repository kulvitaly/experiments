using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Mathematics
{
    public class MathSolution
    {
        public int Reverse(int x)
        {
            unchecked
            {
                long result = 0;

                long tmpX = System.Math.Abs((long)x);

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
    }
}
