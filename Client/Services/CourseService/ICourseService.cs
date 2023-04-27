using BlazorEcommerceStaticWebApp.Shared;

namespace BlazorEcommerceStaticWebApp.Client.Services.CourseService
{
    public interface ICourseService
    {
        string Message { get; set; }

        event Action CoursesChanged;

        List<Course> Courses { get; set; }

        Task GetCourses();

        Task<Course?> GetCourseById(int id);

        Task CreateCourse(Course course);

        Task UpdateCourse(Course course);

        Task DeleteCourse(Course course);
    }
}
