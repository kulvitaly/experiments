using LeetCode.Collections.InvalidTransactions;

namespace LeetCode.Tests.Collections;

public class InvalidTransactionTests
{
    [Theory]
    [InlineData(new string[] { "alice,20,800,mtv", "alice,50,100,beijing" }, new string[] { "alice,20,800,mtv", "alice,50,100,beijing" })]
    [InlineData(new string[] { "alice,20,800,mtv", "alice,50,1200,mtv" }, new string[] { "alice,50,1200,mtv" })]
    [InlineData(new string[] { "alice,20,800,mtv", "bob,50,1200,mtv" }, new string[] { "bob,50,1200,mtv" })]
    [InlineData(new string[] { "alice,20,1220,mtv", "alice,20,1220,mtv" }, new string[] { "alice,20,1220,mtv", "alice,20,1220,mtv" })]
    public void InvalidTransactions_ShouldSucceed(string[] transactions, string[] expected) 
        => new Solution()
            .InvalidTransactions(transactions)
            .Should()
            .BeEquivalentTo(expected, opt => opt.WithStrictOrdering());
}
