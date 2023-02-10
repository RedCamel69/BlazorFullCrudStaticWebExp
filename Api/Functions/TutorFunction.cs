using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Api.Services.ProductService;
using Api.Services.TutorService;
using BlazorEcommerceStaticWebApp.Shared;

namespace Api.Functions
{
    public class TutorFunction
    {

        private readonly ITutorService _tutorService;

        public TutorFunction(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        [FunctionName("Tutors")]
        public IActionResult GetTutors(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tutors")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/tutors request.");
            return new OkObjectResult(_tutorService.GetTutors());
        }

        [FunctionName("Tutor")]
        public IActionResult GetTutor(
[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tutor/{tutorId}")] HttpRequest req,
int tutorId,
ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/tutors request.");
            return new OkObjectResult(_tutorService.GetTutor(tutorId));
        }

        [FunctionName("CreateTutor")]
        public async Task<IActionResult> CreateTutor(
[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tutor")] HttpRequest req,
//Tutor tutor,
ILogger log)
        {
            string result = await req.ReadAsStringAsync();

            var j=  JsonConvert.DeserializeObject<Tutor>(result);
    
            log.LogInformation("C# HTTP POST trigger function processed api/tutor request.");

            return new OkObjectResult(_tutorService.CreateTutor(j));
        }


        [FunctionName("UpdateTutor")]
        public async Task<IActionResult> UpdateTutor(
[HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "tutor")] HttpRequest req,
//Tutor tutor,
ILogger log)
        {
            string result = await req.ReadAsStringAsync();

            var j = JsonConvert.DeserializeObject<Tutor>(result);


            log.LogInformation("C# HTTP POST trigger function processed api/tutor request.");


            // var pinkleton = await _tutorService.CreateTutor(j);


            return new OkObjectResult(_tutorService.UpdateTutor(j));
        }

       
        [FunctionName("DeleteTutor")]
        public async Task<IActionResult> DeleteTutor(
          [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "tutor/{tutorId:int}")] HttpRequest req,
          //Tutor tutor,
          int tutorId,
          ILogger log)
        {
            log.LogInformation("C# HTTP DELETE trigger function processed api/tutor request.");
            return new OkObjectResult(await _tutorService.DeleteTutorAsync(tutorId));
        }
           

    }
}
