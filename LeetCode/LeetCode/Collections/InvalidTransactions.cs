namespace LeetCode.Collections.InvalidTransactions;

public class Solution
{
    public IList<string> InvalidTransactions(string[] transactions)
    {
        HashSet<Transaction> invalidTransactions = [];

        var suspiciousTransactions = new Dictionary<string, LinkedList<Transaction>>();

        var invalid = new List<Transaction>();
        for (int i = 0; i < transactions.Length; i++)
        {
            invalid.Clear();
            var transaction = Transaction.Parse(transactions[i], i);

            if (transaction.IsInvalidAmount())
            {
                invalidTransactions.Add(transaction);
            }

            LinkedList<Transaction>? clientTransactions;
            if (!suspiciousTransactions.TryGetValue(transaction.Name, out clientTransactions))
            {
                clientTransactions = new LinkedList<Transaction>();
                suspiciousTransactions.Add(transaction.Name, clientTransactions);
            }

            var differentCity = clientTransactions.Where(transaction.IsInvalidPair);
            if (differentCity.Any())
            {
                if (!invalid.Any())
                {
                    invalid.Add(transaction);
                }

                invalid.AddRange(differentCity);
            }

            invalid.ForEach(t => invalidTransactions.Add(t));
            
            clientTransactions.AddLast(transaction);
        }

        return invalidTransactions.OrderBy(t => t.Order).Select(t => t.RawString).ToList();
    }

    private class Transaction : IEquatable<Transaction>
    {
        public required string RawString { get; set; }
        public required string Name { get; set; }
        public int Time { get; set; }
        public int Amount { get; set; }
        public required string City { get; set; }
        public int Order { get; set; }

        public static Transaction Parse(string transaction, int order)
        {
            var parts = transaction.Split(',');

            return new()
            {
                RawString = transaction,
                Name = parts[0],
                Time = int.Parse(parts[1]),
                Amount = int.Parse(parts[2]),
                City = parts[3],
                Order = order
            };
        }

        public bool IsInvalidAmount() => Amount > 1000;

        public bool IsInvalidPair(Transaction other)
            => Name.Equals(other.Name, StringComparison.Ordinal)
            && !City.Equals(other.City, StringComparison.Ordinal)
            && Math.Abs(Time - other.Time) <= 60;

        public override int GetHashCode() => Order.GetHashCode();

        public bool Equals(Transaction? other) 
            => other is not null
                && Order == other.Order
                //&& RawString.Equals(other.RawString, StringComparison.Ordinal)
            ;

        public override bool Equals(object? obj) => Equals(obj as Transaction);
    }
}
