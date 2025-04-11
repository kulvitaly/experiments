namespace LeetCode.DynamicProgramming.ConstructStringWithMinimumCost;

public interface IConstructStringWithMinCost
{
    int MinimumCost(string target, string[] words, int[] costs);
}

public class DynamicWithCachingSolution : IConstructStringWithMinCost
{
    private readonly KeyValueComparer _comparer = new();
    private Dictionary<char, (string, int)[]> _hashTable = null!;
    private Dictionary<string, int> _wordsCost = null!;
    private Dictionary<int, int> _minCosts = [];

    public int MinimumCost(string target, string[] words, int[] costs)
    {
        _hashTable = GetWordsHashtable(words, costs);
        return GetCostRecursively(target, currentPos: 0, currentMinCost: int.MaxValue);
    }

    private int GetCostRecursively(ReadOnlySpan<char> target, int currentPos, int currentMinCost)
    {
        var minCost = currentMinCost;
        if (currentPos == target.Length)
            return 0;

        if (_minCosts.TryGetValue(currentPos, out var min))
            return min;

        if (!_hashTable.TryGetValue(target[currentPos], out var wordCosts))
            return -1;

        string str = "";

        foreach ((var word, var cost) in wordCosts)
        {
            var nextPos = currentPos + word.Length;
            if (target.Length >= nextPos && target[currentPos..nextPos].StartsWith(word, StringComparison.Ordinal))
            {
                var currentMax = minCost - cost;
                if (currentMax > 0)
                {
                    var subCost = GetCostRecursively(target, nextPos, currentMax);

                    if (subCost >= 0)
                    {
                        if (subCost + cost < minCost)
                        {
                            minCost = subCost + cost;
                            str = word;
                        }
                    }
                }
            }
        }

        if (str.Length == 0)
        {
            minCost = -1;
        }

        if (minCost > 0)
        {
            _minCosts[currentPos] = minCost;
        }

        return minCost;
    }

    private Dictionary<char, (string Word, int Cost)[]> GetWordsHashtable(string[] words, int[] costs)
    {
        Dictionary<char, Dictionary<string, int>> hashtable = [];
        for (int i = 0; i < words.Length; i++)
        {
            var letter = words[i][0];
            hashtable.TryGetValue(letter, out var wordCosts);
            hashtable[letter] = AddWord(wordCosts ?? [], words[i], costs[i]);
        }

        return hashtable.ToDictionary(x => x.Key, x => x.Value.OrderDescending(_comparer).Select(p => (p.Key, p.Value)).ToArray());

        static Dictionary<string, int> AddWord(Dictionary<string, int> wordCosts, string word, int cost)
        {
            if (wordCosts.TryGetValue(word, out var savedCost))
            {
                if (savedCost > cost)
                {
                    wordCosts[word] = cost;
                }
            }
            else
            {
                wordCosts.Add(word, cost);
            }

            return wordCosts;
        }
    }

    class KeyValueComparer : IComparer<KeyValuePair<string, int>>
    {
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            var diff = x.Key.Length.CompareTo(y.Key.Length);
            return diff != 0 ? diff : y.Value.CompareTo(x.Value);
        }
    }
}

public class DynamicSolution : IConstructStringWithMinCost
{
    private readonly KeyValueComparer _comparer = new();
    private Dictionary<char, (string, int)[]> _hashTable = null!;
    private Dictionary<string, int> _wordsCost = null!;

    public int MinimumCost(string target, string[] words, int[] costs)
    {
        _hashTable = GetWordsHashtable(words, costs);
        return GetCostRecursively(target, int.MaxValue);
    }

    private int GetCostRecursively(ReadOnlySpan<char> target, int minCost)
    {
        if (target.Length == 0)
            return 0;

        if (!_hashTable.TryGetValue(target[0], out var wordCosts))
            return -1;

        foreach ((var word, var cost) in wordCosts)
        {
            if (target.StartsWith(word, StringComparison.Ordinal))
            {
                var currentMax = minCost - cost;
                if (currentMax > 0)
                {
                    var subCost = GetCostRecursively(target[word.Length..], currentMax);

                    if (subCost >= 0)
                    {
                        minCost = Math.Min(minCost, subCost + cost);
                    }
                }
            }
        }

        return minCost;
    }

    private Dictionary<char, (string Word, int Cost)[]> GetWordsHashtable(string[] words, int[] costs)
    {
        Dictionary<char, Dictionary<string, int>> hashtable = [];
        for (int i = 0; i < words.Length; i++)
        {
            var letter = words[i][0];
            hashtable.TryGetValue(letter, out var wordCosts);
            hashtable[letter] = AddWord(wordCosts ?? [], words[i], costs[i]);
        }

        return hashtable.ToDictionary(x => x.Key, x => x.Value.OrderDescending(_comparer).Select(p => (p.Key, p.Value)).ToArray());

        static Dictionary<string, int> AddWord(Dictionary<string, int> wordCosts, string word, int cost)
        {
            if (wordCosts.TryGetValue(word, out var savedCost))
            {
                if (savedCost > cost)
                {
                    wordCosts[word] = cost;
                }
            }
            else
            {
                wordCosts.Add(word, cost);
            }

            return wordCosts;
        }
    }

    class KeyValueComparer : IComparer<KeyValuePair<string, int>>
    {
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            var diff = x.Key.Length.CompareTo(y.Key.Length);
            return diff != 0 ? diff : y.Value.CompareTo(x.Value);
        }
    }
}

public class NonRecursiveSolution : IConstructStringWithMinCost
{
    public int MinimumCost(string target, string[] words, int[] costs)
    {
        var hashtable = GetWordsHashtable(words, costs);

        return FindMinCost(target, hashtable);
    }

    private static int FindMinCost(ReadOnlySpan<char> target, Dictionary<char, (string Word, int Cost)[]> hashtable)
    {
        var minCosts = new int[target.Length + 1];
        
        for (int current = target.Length; current > 0; current--)
        {
            var currentCost = minCosts[current];
            if (current != target.Length && currentCost == 0)
                continue;

            if (!hashtable.TryGetValue(target[current - 1], out var costs))
                continue;

            foreach ((var word, var cost) in costs)
            {
                var previous = current - word.Length;
                if (previous < 0)
                    continue;

                if (target.Slice(previous, word.Length).SequenceEqual(word))
                {
                    var previousCost = minCosts[previous];
                    if (previousCost == 0 || previousCost > cost + currentCost)
                        minCosts[previous] = cost + currentCost;
                }
            }
        }

        return minCosts[0] == 0 ? -1 : minCosts[0];
    }

    private static Dictionary<char, (string Word, int Cost)[]> GetWordsHashtable(string[] words, int[] costs)
    {
        Dictionary<char, Dictionary<string, int>> hashtable = [];
        for (int i = 0; i < words.Length; i++)
        {
            var letter = words[i][^1];
            hashtable.TryGetValue(letter, out var wordCosts);
            hashtable[letter] = AddWord(wordCosts ?? [], words[i], costs[i]);
        }

        return hashtable.ToDictionary(x => x.Key, x => x.Value.Select(p => (p.Key, p.Value)).ToArray());

        static Dictionary<string, int> AddWord(Dictionary<string, int> wordCosts, string word, int cost)
        {
            if (wordCosts.TryGetValue(word, out var savedCost))
            {
                if (savedCost > cost)
                {
                    wordCosts[word] = cost;
                }
            }
            else
            {
                wordCosts.Add(word, cost);
            }

            return wordCosts;
        }
    }
}