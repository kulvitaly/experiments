namespace GraphQLDemo.API.Models;

public class User
{
    public required string Id { get; set; }

    public required string Email { get; set; }

    public string? Name { get; set; } 

    public bool EmailVerified { get; set; }
}
