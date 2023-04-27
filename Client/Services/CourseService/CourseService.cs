using BlazorEcommerceStaticWebApp.Client.Pages;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace BlazorEcommerceStaticWebApp.Client.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManger;

        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Course> Courses { get; set ; }

        public event Action CoursesChanged;


        public CourseService(HttpClient http, NavigationManager navigationManger)
        {
            _http = http;
            _navigationManger = navigationManger;
        }

        public async Task CreateCourse(Course course)
        {
            await _http.PostAsJsonAsync("api/course", course);
            _navigationManger.NavigateTo("courses");
        }

        public async Task DeleteCourse(Course course)
        {
            await _http.DeleteAsync($"api/course/{course.CourseId}");
            _navigationManger.NavigateTo("courses");
        }

        public async Task<Course?> GetCourseById(int id)
        {
            var result = await _http.GetAsync($"api/course/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var serviceRespnse = await result.Content.ReadFromJsonAsync<ServiceResponse<Course>>();

                //todo: Inspect response for errors
                if (serviceRespnse != null)
                {
                    if (serviceRespnse.Data != null && serviceRespnse.Success == true)
                    {
                        return serviceRespnse.Data;
                    }
                    else
                    {
                        Message = $"Error retrieving student. Service indicates the action failed : {serviceRespnse.Message}";
                    }

                }
            }

            return null;
        }

        public async Task GetCourses()
        {
            var res =
              await _http.GetFromJsonAsync<ServiceResponse<List<Course>>>("api/courses");



            if (res != null && res.Data != null)
                Courses = res.Data;
        }

        public async Task UpdateCourse(Course course)
        {
            await _http.PutAsJsonAsync("api/course", course);
            _navigationManger.NavigateTo("courses");
        }
    }
}
