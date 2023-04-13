using Api.Migrations;
using Api.Services.StudentService;
using Api.Services.TutorService;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "students")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/students request.");
            return new OkObjectResult(await _studentService.GetStudentsAsync());
        }

        [FunctionName("Student")]
        public async Task<IActionResult> GetStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "student/{studentId}")] HttpRequest req,
            int studentId,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/student request.");
            return new OkObjectResult(await _studentService.GetStudentAsync(studentId));
        }

        [FunctionName("CreateStudent")]
        public async Task<IActionResult> CreateStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "student")] HttpRequest req,
            //Tutor tutor,
            ILogger log)
        {
            string result = await req.ReadAsStringAsync();
            var student = JsonConvert.DeserializeObject<Student>(result);

            log.LogInformation("C# HTTP POST trigger function processed api/student request.");
            return new OkObjectResult(_studentService.CreateStudent(student));
        }


        [FunctionName("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(
[HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "student")] HttpRequest req,
//Tutor tutor,
ILogger log)
        {
            string result = await req.ReadAsStringAsync();

            var student = JsonConvert.DeserializeObject<Student>(result);


            log.LogInformation("C# HTTP POST trigger function processed api/student request.");

            return new OkObjectResult(_studentService.UpdateStudent(student));
        }


        [FunctionName("DeleteStudent")]
        public async Task<IActionResult> DeleteTutor(
          [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "student/{studentId:int}")] HttpRequest req,
          //Tutor tutor,
          int studentId,
          ILogger log)
        {
            log.LogInformation("C# HTTP DELETE trigger function processed api/student request.");
            return new OkObjectResult( _studentService.DeleteStudent(studentId));
        }


    }
}
