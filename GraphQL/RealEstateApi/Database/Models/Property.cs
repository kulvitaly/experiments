namespace Database.Models;

public class Property
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public decimal Value { get; set; }

    public string Family { get; set; } = null!;

    public string? Street { get; set; }

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
