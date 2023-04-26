using BlazorEcommerceStaticWebApp.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.CourseService
{
    public interface ICourseService
    {


        ServiceResponse<List<Course>> GetCourses();

        Task<ServiceResponse<List<Course>>> GetCoursesAsync();

        ServiceResponse<Course> GetCourse(int Id);

        Task<ServiceResponse<Course>> GetCourseAsync(int Id);

        Task<ServiceResponse<Course>> UpdateCourseAsync(Course course);

        ServiceResponse<Course> UpdateCourse(Course course);

        Task<ServiceResponse<Course>> CreateCourse(Course course);


        Task<ServiceResponse<bool>> DeleteCourseAsync(int Id);

        ServiceResponse<bool> DeleteCourse(int Id);
    }
}
