using Api.Migrations;
using Api.Services.LanguageService;
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
    public class LanguageFunction
    {

        private readonly ILanguageService _languageService;

        public LanguageFunction(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [FunctionName("Languages")]
        public IActionResult GetLanguages(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "languages")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/languages request.");
            return new OkObjectResult(_languageService.GetLanguages());
        }

        

    }
}
