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
    public class StudentFunction
    {
        private readonly IStudentService _studentService;

        public StudentFunction(IStudentService studentService)
        {
            _studentService = studentService;
        }




        [FunctionName("Students")]
        public async Task<IActionResult> GetStudents(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "students")] 
            HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP GET trigger function processed api/students request.");
                return new OkObjectResult(await _studentService.GetStudentsAsync());
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP GET trigger function api/students request exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Student>()
                {
                    Data = null,
                    Message = "Failed to retrieve students",
                    Success = false
                });
            }
        }

        [FunctionName("Student")]
        public async Task<IActionResult> GetStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "student/{studentId}")] HttpRequest req,
            int studentId,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP GET trigger function processed api/student request.");
                return new OkObjectResult(await _studentService.GetStudentAsync(studentId));
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP GET trigger function api/student request exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Student>()
                {
                    Data = null,
                    Message = "Failed to retrieve student",
                    Success = false
                });
            }
        }

        [FunctionName("CreateStudent")]
        public async Task<IActionResult> CreateStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "student")] 
            HttpRequest req,
            //Tutor tutor,
            ILogger log)
        {
            

            try
            {
                string result = await req.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<Student>(result);

                log.LogInformation("C# HTTP POST trigger function processed api/student request.");
                return new OkObjectResult(_studentService.CreateStudent(student));
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP POST trigger function api/student exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Student>()
                {
                    Data = null,
                    Message = "Failed to create student",
                    Success = false
                });
            }
        }


        [FunctionName("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "student")] HttpRequest req,
            //Tutor tutor,
            ILogger log)
        {
           

            try
            {
                string result = await req.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<Student>(result);

                log.LogInformation("C# HTTP PUT trigger function processed api/student request.");
                return new OkObjectResult(_studentService.UpdateStudent(student));
            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP PUT trigger function api/student exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Student>()
                {
                    Data = null,
                    Message = "Failed to uodate student",
                    Success = false
                });
            }
        }


        [FunctionName("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(
          [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "student/{studentId:int}")] HttpRequest req,
          //Tutor tutor,
          int studentId,
          ILogger log)
        {
           
            try
            {
                log.LogInformation("C# HTTP DELETE trigger function processed api/student request.");
                return new OkObjectResult(_studentService.DeleteStudent(studentId));

            }
            catch (Exception ex)
            {
                log.LogError($"C# HTTP DELETE trigger function api/student/{studentId} exception:{ex.Message}");
                return new OkObjectResult(new ServiceResponse<Student>()
                {
                    Data = null,
                    Message = "Failed to delete student id : {studentId}",
                    Success = false
                });
            }
        }


    }
}
