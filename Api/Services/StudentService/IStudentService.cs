using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.StudentService
{
    public interface IStudentService
    {
        ServiceResponse<List<Student>> GetStudents();

        Task<ServiceResponse<List<Student>>> GetStudentsAsync();

        ServiceResponse<Student> GetStudent(int Id);

        Task<ServiceResponse<Student>> GetStudentAsync(int Id);

        Task<ServiceResponse<Student>> UpdateStudent(Student tutor);

        Task<ServiceResponse<Student>> CreateStudent(Student tutor);


        Task<ServiceResponse<bool>> DeleteStudentAsync(int Id);

        ServiceResponse<bool> DeleteStudent(int Id);
    }
}
