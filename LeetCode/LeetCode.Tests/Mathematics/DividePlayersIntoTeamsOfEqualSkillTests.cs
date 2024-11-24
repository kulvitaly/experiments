using LeetCode.Collections.DividePlayersIntoTeamsofEqualSkill;

namespace LeetCode.Tests.Mathematics;

public class DividePlayersIntoTeamsOfEqualSkillTests
{
    private readonly Solution _solution = new();

    [Theory]
    [MemberData(nameof(TestData))]
    public void DividePlayers_ShouldSucceed(int[] skills, int expected)
    {
        Assert.Equal(expected, _solution.DividePlayers(skills));
    }

    public static IEnumerable<object[]> TestData =
        [
            [new int[] { 3, 2, 5, 1, 3, 4 }, 22],
            [new int[] { 3, 4 }, 12],
            [new int[] { 1, 1, 2, 3 }, -1]
        ];
}
