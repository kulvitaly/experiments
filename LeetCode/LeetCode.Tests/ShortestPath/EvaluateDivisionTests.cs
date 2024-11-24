using LeetCode.ShortestPath.EvaluateDivision;
using System.Collections.Generic;

namespace LeetCode.Tests.ShortestPath;

public class EvaluateDivisionTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void CalcEquation_ShouldSucceed(IList<IList<string>> equations, double[] values, IList<IList<string>> queries, double[] expected)
    {
        new Solution().CalcEquation(equations, values, queries)
            .Should()
            .BeEquivalentTo(expected,
                opt => opt.Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 0.00001))
                    .WhenTypeIs<double>());
    }

    public static IEnumerable<object[]> TestData =
        [
            [
                new List<IList<string>>() { new List<string>() { "a", "b" }, new List<string>() { "b", "c" } },
                new double[] { 2.0, 3.0 },
                new List<IList<string>>() { new List<string>() { "a", "c" }, new List<string>() { "b", "a" }, new List<string>() { "a", "e" }, new List<string>() { "a", "a" }, new List<string>() { "x", "x" } },
                new double[] { 6.0, 0.5, -1.0, 1.0, -1.0 }
            ],
            [
                new List<IList<string>>() { new List<string>() { "a", "b" }, new List<string>() { "b", "c" }, new List<string>() { "bc", "cd" }  },
                new double[] { 1.5, 2.5, 5.0 },
                new List<IList<string>>() { new List<string>() { "a", "c" }, new List<string>() { "c", "b" }, new List<string>() { "bc", "cd" }, new List<string>() { "cd", "bc" } },
                new double[] { 3.75, 0.4, 5.0, 0.2 }
            ],
            [
                new List<IList<string>>() { new List<string>() { "a", "b" } },
                new double[] { 0.5 },
                new List<IList<string>>() { new List<string>() { "a", "b" }, new List<string>() { "b", "a" }, new List<string>() { "a", "c" }, new List<string>() { "x", "y" } },
                new double[] { 0.5, 2.0, -1.0, -1.0 }
            ]
        ];
}
