namespace GraphQLDemo.API.DTOs
{
    public class InstructorDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
