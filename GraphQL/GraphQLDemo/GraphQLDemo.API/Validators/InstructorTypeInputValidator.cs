using FluentValidation;
using GraphQLDemo.API.Schema.Mutations;

namespace GraphQLDemo.API.Validators;

public class InstructorTypeInputValidator : AbstractValidator<InstructorTypeInput>
{
    public InstructorTypeInputValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Salary).GreaterThan(0);
    }
}