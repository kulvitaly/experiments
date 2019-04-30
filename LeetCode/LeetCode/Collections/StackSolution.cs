using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Collections
{
    public class StackSolution
    {
        public bool IsValid(string s)
        {
            var stack = new Stack<char>();

            foreach (char ch in s)
            {
                if (IsStart(ch))
                {
                    stack.Push(ch);
                    continue;
                }

                if (!stack.TryPop(out char start))
                {
                    return false;
                }
                
                if (!IsAppropriate(start, ch))
                    return false;
            }

            return stack.Count == 0;

            bool IsAppropriate(char chStart, char chEnd)
            {
                return chStart == '(' && chEnd == ')' ||
                       chStart == '{' && chEnd == '}' ||
                       chStart == '[' && chEnd == ']';
            }

            bool IsStart(char ch)
            {
                return ch == '(' || ch == '{' || ch == '[';
            }
        }
    }
}
