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
    public class TutorFunction
    {

        private readonly ITutorService _tutorService;

        public TutorFunction(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        [FunctionName("Tutors")]
        public async Task<IActionResult> GetTutors(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tutors")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/tutors request.");
            return new OkObjectResult(await _tutorService.GetTutorsAsync());
        }

        [FunctionName("Tutor")]
        public async Task<IActionResult> GetTutor(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tutor/{tutorId}")] HttpRequest req,
            int tutorId,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/tutor/{tutorid} request.");
            return new OkObjectResult(await _tutorService.GetTutorAsync(tutorId));
        }

        [FunctionName("CreateTutor")]
        public async Task<IActionResult> CreateTutor(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tutor")] HttpRequest req,
            //Tutor tutor,
            ILogger log)
        {
            string result = await req.ReadAsStringAsync();
            var j = JsonConvert.DeserializeObject<Tutor>(result);

            log.LogInformation("C# HTTP POST trigger function processed api/tutor post.");
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
