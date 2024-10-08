namespace LeetCode.Greedy.BestTimeToBuyAndSellStockII;

public class Solution
{
    public int MaxProfit(int[] prices)
    {
        int profit = 0;

        for (int i = 0; i < prices.Length - 1; ++i)
        {
            var diff = prices[i + 1] - prices[i];
            profit += diff > 0 ? diff : 0;
        }

        return profit;
    }
}
