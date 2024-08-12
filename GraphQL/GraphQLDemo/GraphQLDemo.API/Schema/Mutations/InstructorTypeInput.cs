namespace GraphQLDemo.API.Schema.Mutations;

public class InstructorTypeInput
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public decimal Salary { get; set; }
}