using System.Collections.Generic;

namespace LeetCode.Design;

public class LRUCache
{
    private readonly int _capacity;
    private readonly Dictionary<int, LinkedListNode<CacheValue>> _cacheDict;
    private readonly LinkedList<CacheValue> _cacheList = new();

    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _cacheDict = new(capacity);
    }

    public int Get(int key)
    {
        if (_cacheDict.TryGetValue(key, out var node))
        {
            MoveToHead(node);
            return node.Value.Value;
        }

        return -1;
    }

    public void Put(int key, int value)
    {
        if (_cacheDict.TryGetValue(key, out var node))
        {
            node.Value.Value = value;
            MoveToHead(node);
        }
        else
        {
            LinkedListNode<CacheValue>? item = null;
            if (_cacheDict.Count == _capacity)
            {
                item = _cacheList.Last;

                _cacheList.RemoveLast();
                _cacheDict.Remove(item!.Value.Key);

                item.Value.Key = key;
                item.Value.Value = value;
            }

            item ??= new LinkedListNode<CacheValue>(new CacheValue { Key = key, Value = value });

            _cacheList.AddFirst(item);
            _cacheDict.Add(key, item);
        }
    }

    private void MoveToHead(LinkedListNode<CacheValue> node)
    {
        _cacheList.Remove(node);
        _cacheList.AddFirst(node);
    }

    private class CacheValue
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }
}

