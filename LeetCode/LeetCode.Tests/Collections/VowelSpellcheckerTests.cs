using LeetCode.Collections.VowelSpellchecker;

namespace LeetCode.Tests.Collections;

public class VowelSpellcheckerTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void Spellchecker_ShouldReturnExpected(string[] wordlist, string[] queries, string[] expected)
    {
        // Arrange
        var spellchecker = new Solution();

        // Act
        var result = spellchecker.Spellchecker(wordlist, queries);

        // Assert
        result.Should().BeEquivalentTo(expected, opt => opt.WithStrictOrdering());
    }


    public static IEnumerable<object[]> TestData =
    [
        [
            new string[] { "yellow" },
            new string[] { "YellOw" },
            new string[] { "yellow" }
        ],
        [
            new string[] { "KiTe", "kite", "hare", "Hare" },
            new string[] { "kite", "Kite", "KiTe", "Hare", "HARE", "Hear", "hear", "keti", "keet", "keto" },
            new string[] { "kite", "KiTe", "KiTe", "Hare", "hare", "", "", "KiTe", "", "KiTe" }
        ]
    ];
}
