using BlazorEcommerceStaticWebApp.Shared;

namespace BlazorEcommerceStaticWebApp.Client.Services.StudentService
{
    public interface IStudentService
    {
        string Message { get; set; }

        event Action StudentsChanged;

        List<Student> Students { get; set; }

        Task GetStudents();

        Task<Student?> GetStudentById(int id);

        Task CreateStudent(Student student);

        Task UpdateStudent(Student student);

        Task DeleteStudent(Student student);
    }
}
