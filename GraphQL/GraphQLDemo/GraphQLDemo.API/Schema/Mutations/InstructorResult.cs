namespace GraphQLDemo.API.Schema.Mutations;

public class InstructorResult
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public decimal Salary { get; set; }
}
