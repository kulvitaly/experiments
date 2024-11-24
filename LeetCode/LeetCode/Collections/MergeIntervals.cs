namespace LeetCode.Collections.MergeIntervals;

public class Solution
{
    /// <summary>
    /// It is an alternative idea: we create dictionary of numbers.
    /// When interval starts we add 1 to the number, when it ends we subtract 1.
    /// Then we go through the dictionary and generate intervals. Interval ends when sum of values is 0.
    /// </summary>
    public int[][] Merge2(int[][] intervals)
    {
        Dictionary<int, int> points = new(intervals.Length);

        foreach (var interval in intervals)
        {
            AddPoint(interval[0], 1);
            AddPoint(interval[1], -1);
        }

        return GenerateIntervals(points).ToArray();

        void AddPoint(int number, int value)
        {
            if (!points.TryAdd(number, value))
            {
                points[number] += value;
            }
        }
    }

    private static IEnumerable<int[]> GenerateIntervals(IDictionary<int, int> points)
    {
        int currentStart = 0;
        int currentSum = 0;
        foreach (var pair in points.OrderBy(p => p.Key))
        {
            if (currentSum == 0)
            {
                currentStart = pair.Key;
                currentSum = pair.Value;

                if (currentSum == 0)
                {
                    yield return [pair.Key, pair.Key];
                }

                continue;
            }

            currentSum += pair.Value;

            if (currentSum == 0)
            {
                yield return [currentStart, pair.Key];
            }
        }
    }

    /// <summary>
    /// Sort and merge intervals.
    /// This method has side effects: it modifies input array.
    /// </summary>
    public int[][] Merge(int[][] intervals)
    {
        Array.Sort(intervals, (a, b) => a[0] - b[0]);

        return MergeIntervals().ToArray();

        IEnumerable<int[]> MergeIntervals()
        {
            int[] currentInterval = intervals[0];
            for (int i = 1; i < intervals.Length; i++)
            {
                if (currentInterval[1] >= intervals[i][0])
                {
                    currentInterval[1] = Math.Max(currentInterval[1], intervals[i][1]);
                }
                else
                {
                    yield return currentInterval;
                    currentInterval = intervals[i];
                }
            }

            yield return currentInterval;
        }
    }
}
