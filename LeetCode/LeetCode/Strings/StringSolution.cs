using System.Text;

namespace LeetCode.Strings
{
    public class StringSolution
    {
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
    }
}
