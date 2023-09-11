namespace Models.Time;

public record struct Year(int Number)
{
    public Month GetMonth(int ordinal) 
        => ordinal >= 1 && ordinal <= 12 
            ? new(this, ordinal) 
            : throw new ArgumentOutOfRangeException(nameof(ordinal), ordinal, "Month ordinal must be between 1 and 12.");

    public IEnumerable<Month> TryGetMonth(int ordinal) 
    {
        if (ordinal >= 1 && ordinal <= 12)
            yield return new(this, ordinal);
    } 

    public IEnumerable<Month> Months => GetMonths(this);

    public Year DecadeBeginning => new(Number / 10 * 10 + 1);

    public IEnumerable<Year> Decade => Enumerable.Range(DecadeBeginning.Number, 10)
        .Select(year => new Year(year));

    private IEnumerable<Month> GetMonths(Year year) 
        => Enumerable.Range(1, 12).Select(month => new Month(year, month));
}
