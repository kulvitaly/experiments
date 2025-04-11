namespace LeetCode.BinarySearch.FindInMountainArray;

public class Solution
{
    public int FindInMountainArray(int target, MountainArray mountainArr)
    {
        var maxPosition = FindMax(mountainArr, 0, mountainArr.Length());

        var first = Search(target, mountainArr, 0, maxPosition + 1, direction: 1);

        return first == -1
            ? Search(target, mountainArr, maxPosition, mountainArr.Length(), direction: -1)
            : first;
    }

    int FindMax(MountainArray arr, int start, int end)
    {
        if (start + 2 >= end)
        {
            return GetElement(arr, start) > GetElement(arr, start + 1)
                ? start
                : start + 1;
        }

        var pos1 = (end + start) / 2;
        var pos2 = pos1 + 1;

        var v1 = GetElement(arr, pos1);
        var v2 = GetElement(arr, pos2);

        if (v1 < v2)
            return FindMax(arr, pos2, end);
        else
            return FindMax(arr, start, pos2);
    }

    int Search(int target, MountainArray arr, int start, int end, int direction)
    {
        if (start + 1 >= end)
        {
            return target == GetElement(arr, start)
                ? start
                : -1;
        }

        var pos = (end + start) / 2;

        var curValue = GetElement(arr, pos);
        if (target == curValue)
            return pos;

        if (direction * target > direction * curValue)
            return Search(target, arr, pos, end, direction);
        else
            return Search(target, arr, start, pos, direction);
    }

    int GetElement(MountainArray arr, int position) => arr.Get(position);
}

public class MountainArray(int[] elements)
{
    private readonly int[] _elements = elements;

    public int Get(int index) => _elements[index];

    public int Length() => _elements.Length;
}
