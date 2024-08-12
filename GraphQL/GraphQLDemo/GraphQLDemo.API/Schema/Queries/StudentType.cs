namespace GraphQLDemo.API.Schema.Queries;

public class StudentType
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public double Gpa { get; set; }
}
