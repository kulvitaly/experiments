namespace LeetCode.Collections.VowelSpellchecker;

public class Solution
{
    private static readonly HashSet<char> Vowels = [ 'a', 'e', 'i', 'o', 'u' ];

    public string[] Spellchecker(string[] wordlist, string[] queries)
    {
        var result = new List<string>(queries.Length);
        var wordListSet = new HashSet<string>(wordlist);
        var cache = new Dictionary<string, string>();

        foreach (var query in queries)
        {
            if (cache.TryGetValue(query, out var cachedValue))
            {
                result.Add(cachedValue);
                continue;
            }

            if (wordListSet.Contains(query))
            {
                result.Add(query);
                continue;
            }

            string foundWord = string.Empty;
            foreach (var word in wordlist)
            {
                if (query.Length != word.Length)
                    break;

                if (query.Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    foundWord = word;
                    break;
                }

                if (IsMatchIgnoreVowels(query, word) && foundWord == string.Empty)
                {
                    foundWord = word;
                }
            }

            result.Add(foundWord);
            cache.Add(query, foundWord);
        }

        return [.. result];
    }

    private static bool IsMatchIgnoreVowels(ReadOnlySpan<char> query, ReadOnlySpan<char> word)
    {
        for (int i = 0; i < query.Length; ++i)
        {
            var lowerChar1 = char.ToLowerInvariant(query[i]);
            var lowerChar2 = char.ToLowerInvariant(word[i]);

            if (lowerChar1 == lowerChar2)
                continue;

            if (Vowels.Contains(lowerChar1) && Vowels.Contains(lowerChar2))
                continue;

            return false;
        }

        return true;
    }
}
