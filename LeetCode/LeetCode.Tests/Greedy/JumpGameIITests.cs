using LeetCode.Greedy.JumpGameII;

namespace LeetCode.Tests.Greedy;

public class JumpGameIITests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void ContainerWithMostWater_ShouldSucceed(int[] jumps, int expected)
    {
        Assert.Equal(expected, new Solution().Jump(jumps));
    }

    public static IEnumerable<object[]> TestData =
        [
            [new int[] { 2, 3, 1, 1, 4 }, 2],
            [new int[] { 2, 3, 0, 1, 4 }, 2]
        ];
}
