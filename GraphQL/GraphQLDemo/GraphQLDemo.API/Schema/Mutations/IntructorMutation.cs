using AppAny.HotChocolate.FluentValidation;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Instructors;
using GraphQLDemo.API.Validators;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class InstructorMutation
{
    [Authorize]
    public async Task<InstructorResult> CreateInstructor(
        [UseFluentValidation, UseValidator<InstructorTypeInputValidator>] InstructorTypeInput input,
        [Service] InstructorsRepository instructorsRepository,
        ITopicEventSender eventSender)
    {
        var instructorDto = new InstructorDto
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Salary = input.Salary
        };

        instructorDto = await instructorsRepository.Create(instructorDto);

        InstructorResult instructor = new InstructorResult
        {
            Id = instructorDto.Id,
            FirstName = instructorDto.FirstName,
            LastName = instructorDto.LastName,
            Salary = instructorDto.Salary
        };

        await eventSender.SendAsync(nameof(Subscription.InstructorCreated), instructor);

        return instructor;
    }

    [Authorize]
    public async Task<InstructorResult> UpdateInstructor(
        Guid id,
        [UseFluentValidation, UseValidator<InstructorTypeInputValidator>] InstructorTypeInput input,
        [Service] InstructorsRepository instructorsRepository)
    {
        var instructorDto = new InstructorDto
        {
            Id = id,
            FirstName = input.FirstName,
            LastName = input.LastName,
            Salary = input.Salary
        };

        instructorDto = await instructorsRepository.Update(instructorDto);

        InstructorResult instructor = new InstructorResult
        {
            Id = instructorDto.Id,
            FirstName = instructorDto.FirstName,
            LastName = instructorDto.LastName,
            Salary = instructorDto.Salary
        };

        return instructor;
    }

    [Authorize(Policy = "IsAdmin")]
    public async Task<bool> DeleteInstructor(
        Guid id,
        [Service] InstructorsRepository instructorsRepository)
    {
        return await instructorsRepository.Delete(id);
    }
}
