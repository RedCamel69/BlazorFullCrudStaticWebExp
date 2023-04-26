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

        //[FunctionName("CreateStudent")]
        //public async Task<IActionResult> CreateStudent(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "student")] 
        //    HttpRequest req,
        //    //Tutor tutor,
        //    ILogger log)
        //{
            

        //    try
        //    {
        //        string result = await req.ReadAsStringAsync();
        //        var student = JsonConvert.DeserializeObject<Student>(result);

        //        log.LogInformation("C# HTTP POST trigger function processed api/student request.");
        //        return new OkObjectResult(_studentService.CreateStudent(student));
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError($"C# HTTP POST trigger function api/student exception:{ex.Message}");
        //        return new OkObjectResult(new ServiceResponse<Student>()
        //        {
        //            Data = null,
        //            Message = "Failed to create student",
        //            Success = false
        //        });
        //    }
        //}


        //[FunctionName("UpdateStudent")]
        //public async Task<IActionResult> UpdateStudent(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "student")] HttpRequest req,
        //    //Tutor tutor,
        //    ILogger log)
        //{
           

        //    try
        //    {
        //        string result = await req.ReadAsStringAsync();
        //        var student = JsonConvert.DeserializeObject<Student>(result);

        //        log.LogInformation("C# HTTP PUT trigger function processed api/student request.");
        //        return new OkObjectResult(_studentService.UpdateStudent(student));
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError($"C# HTTP PUT trigger function api/student exception:{ex.Message}");
        //        return new OkObjectResult(new ServiceResponse<Student>()
        //        {
        //            Data = null,
        //            Message = "Failed to uodate student",
        //            Success = false
        //        });
        //    }
        //}


        //[FunctionName("DeleteStudent")]
        //public async Task<IActionResult> DeleteStudent(
        //  [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "student/{studentId:int}")] HttpRequest req,
        //  //Tutor tutor,
        //  int studentId,
        //  ILogger log)
        //{
           
        //    try
        //    {
        //        log.LogInformation("C# HTTP DELETE trigger function processed api/student request.");
        //        return new OkObjectResult(_studentService.DeleteStudent(studentId));

        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError($"C# HTTP DELETE trigger function api/student/{studentId} exception:{ex.Message}");
        //        return new OkObjectResult(new ServiceResponse<Student>()
        //        {
        //            Data = null,
        //            Message = "Failed to delete student id : {studentId}",
        //            Success = false
        //        });
        //    }
        //}


    }
}
