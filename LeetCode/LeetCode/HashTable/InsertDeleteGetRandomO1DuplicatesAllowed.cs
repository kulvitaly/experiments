namespace LeetCode.HashTable;

public class RandomizedCollection
{
    private int _totalCount = 0;
    private Dictionary<int, int> _values = new();

    public RandomizedCollection()
    {

    }

    public bool Insert(int val)
    {
        if (_values.ContainsKey(val))
        {
            _values[val]++;
            _totalCount++;
            return false;
        }

        _values.Add(val, 1);
        _totalCount++;
        return true;
    }

    public bool Remove(int val)
    {
        if (_values.TryGetValue(val, out var count))
        {
            if (count == 1)
            {
                _values.Remove(val);

                _totalCount--;
                return true;
            }

            _values[val]--;
            _totalCount--;
            return true;
        }

        return false;
    }

    public int GetRandom()
    {
        var index = Random.Shared.Next(0, _totalCount);

        foreach (var (key, value) in _values)
        {
            if (index < value)
            {
                return key;
            }

            index -= value;
        }

        return -1;
    }
}
