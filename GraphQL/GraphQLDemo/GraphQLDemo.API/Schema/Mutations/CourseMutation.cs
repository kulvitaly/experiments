using AppAny.HotChocolate.FluentValidation;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Middlewares;
using GraphQLDemo.API.Models;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Validators;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class CourseMutation
{
    private readonly CoursesRepository _courseRepo;

    public CourseMutation(CoursesRepository courseRepo)
    {
        _courseRepo = courseRepo;
    }

    [Authorize]
    [UseUser]
    public async Task<CourseResult> CreateCourse(
        [UseFluentValidation, UseValidator<CourseTypeInputValidator>] CourseInputType input,
        [Service] ITopicEventSender eventSender,
        [User] User user)
    {
        var courseDto = new CourseDto
        {
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId,
            CreatorId = user.Id
        };

        courseDto = await _courseRepo.Create(courseDto);

        CourseResult course = new CourseResult
        {
            Id = courseDto.Id,
            Name = courseDto.Name,
            Subject = courseDto.Subject,
            InstructorId = courseDto.InstructorId
        };

        await eventSender.SendAsync(nameof(Subscriptions.Subscription.CourseCreated), course);

        return course;
    }

    //[Authorize]
    public async Task<CourseResult> UpdateCourse(Guid id,
        [UseFluentValidation, UseValidator<CourseTypeInputValidator>] CourseInputType input,
        [Service] ITopicEventSender eventSender,
        [User] User user)
    {

        var course = await _courseRepo.GetById(id);

        if (course == null)
        {
            throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
        }

        if (course.CreatorId != user.Id)
        {
            throw new GraphQLException(new Error("You do not have permission to update this course.", "INVALID_PERMISSION"));
        }

        course.Name = input.Name;
        course.Subject = input.Subject;
        course.InstructorId = input.InstructorId;

        course = await _courseRepo.Update(course);

        string updateCourseTopic = $"{course.Id}_{nameof(Subscriptions.Subscription.CourseUpdated)}";
        await eventSender.SendAsync(updateCourseTopic, course);

        return new CourseResult
        {
            Id = course.Id,
            Name = course.Name,
            Subject = course.Subject,
            InstructorId = course.InstructorId
        };
    }


    [Authorize(Policy = "IsAdmin")]
    public async Task<bool> DeleteCource(Guid id)
    {

        try
        {
            return await _courseRepo.Delete(id);
        }
        catch
        {
            return false;
        }
    }
}
