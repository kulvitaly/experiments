using FluentValidation;
using GraphQLDemo.API.Schema.Mutations;

namespace GraphQLDemo.API.Validators;

public class CourseTypeInputValidator : AbstractValidator<CourseInputType>
{
    public CourseTypeInputValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Name must be between 3 and 50 characters")
            .WithErrorCode("COURSE_NAME_LENGTH");

    }
}
