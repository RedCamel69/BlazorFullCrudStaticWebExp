using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorEcommerceStaticWebApp.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManger;
       
        public List<Student> Students { get; set; } = new List<Student>();

        public event Action StudentsChanged;

        public StudentService(HttpClient http, NavigationManager navigationManger)
        {
            _http = http;
            _navigationManger = navigationManger;
        }
        public async Task CreateStudent(Student student)
        {
            await _http.PostAsJsonAsync("api/student", student);
            _navigationManger.NavigateTo("students");
        }

        public async Task DeleteStudent(Student student)
        {            
            await _http.DeleteAsync($"api/student/{student.StudentId}");
            _navigationManger.NavigateTo("students");
        }

        public async Task<Student?> GetStudentById(int id)
        {
            var result = await _http.GetAsync($"api/student/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var serviceRespnse = await result.Content.ReadFromJsonAsync<ServiceResponse<Student>>();

                //todo: Inspect response for errors
                if(serviceRespnse != null)
                {
                    return serviceRespnse.Data;
                }
            }

            return null;
        }

        public async Task GetStudents()
        {
            var res =
               await _http.GetFromJsonAsync<ServiceResponse<List<Student>>>("api/Students");



            if (res != null && res.Data != null)
                Students = res.Data;

            //CurrentPage = 1;
            //PageCount = 0;

            //if (Tutors.Count == 0)
            //{
            //    Message = "No Products Found";
            //}

            //if (Products.Count > 0)
            //{
            //    Message = "Products Found";
            //}

            StudentsChanged.Invoke();
        }

        public async Task UpdateStudent(Student student)
        {
            await _http.PutAsJsonAsync("api/student", student);
            _navigationManger.NavigateTo("students");
        }
    }
}
