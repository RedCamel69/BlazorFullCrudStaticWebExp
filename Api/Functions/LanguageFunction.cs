using Api.Services.LanguageService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

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
