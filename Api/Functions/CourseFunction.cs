using Api.Services.CourseService;
using Api.Services.StudentService;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Api.Functions
{
    public class CourseFunction
    {
        private readonly ICourseService _courseService;

        public CourseFunction(ICourseService courseService)
        {
            _courseService = courseService;
        }




        [FunctionName("Courses")]
        public async Task<IActionResult> GetCourses(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "courses")] 
            HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP GET trigger function processed api/courses request.");
                return new OkObjectResult(await _courseService.GetCoursesAsync());
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP GET trigger function api/courses request exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Student>()
                {
                    Data = null,
                    Message = "Failed to retrieve courses",
                    Success = false
                });
            }
        }

        [FunctionName("Course")]
        public async Task<IActionResult> GetCourse(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "course/{courseId}")] HttpRequest req,
            int courseId,
            ILogger log)
        {
            try
            {
                log.LogInformation($"C# HTTP GET trigger function processed api/courses/{courseId} request.");
                return new OkObjectResult(await _courseService.GetCourseAsync(courseId));
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP GET trigger function api/course request exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Student>()
                {
                    Data = null,
                    Message = "Failed to retrieve course",
                    Success = false
                });
            }
        }

        [FunctionName("CreateCourse")]
        public async Task<IActionResult> CreateCourse(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "course")]
            HttpRequest req,
            //Tutor tutor,
            ILogger log)
        {


            try
            {
                string result = await req.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<Course>(result);

                log.LogInformation("C# HTTP POST trigger function processed api/student request.");
                return new OkObjectResult(await _courseService.CreateCourseAsync(course));
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP POST trigger function api/course exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Course>()
                {
                    Data = null,
                    Message = "Failed to create course",
                    Success = false
                });
            }
        }


        [FunctionName("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "course")] HttpRequest req,
            //Tutor tutor,
            ILogger log)
        {


            try
            {
                string result = await req.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<Course>(result);

                log.LogInformation("C# HTTP PUT trigger function processed api/courserequest.");
                return new OkObjectResult(await _courseService.UpdateCourseAsync(course));
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP PUT trigger function api/course exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Course>()
                {
                    Data = null,
                    Message = "Failed to uodate course",
                    Success = false
                });
            }
        }


        [FunctionName("DeleteCourse")] 
        public async Task<IActionResult> DeleteCourse(
          [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "course/{courseId:int}")] HttpRequest req,
          //Tutor tutor,
          int courseId,
          ILogger log)
        {

            try
            {
                log.LogInformation("C# HTTP DELETE trigger function processed api/course request.");
                return new OkObjectResult(await _courseService.DeleteCourseAsync(courseId));

            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP DELETE trigger function api/course/{courseId} exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Course>()
                {
                    Data = null,
                    Message = $"Failed to delete course id : {courseId}",
                    Success = false
                });
            }
        }


    }
}
