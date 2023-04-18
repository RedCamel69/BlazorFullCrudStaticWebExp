using Api.Services.HelperService;
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
    public class HelperFunction
    {
        private readonly IHelperService _helperService;

        public HelperFunction(IHelperService helperService)
        {
            _helperService = helperService;
        }




        [FunctionName("Helper")]
        public async Task<IActionResult> GetSettings(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "helper")] 
            HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP GET trigger function processed api/students request.");
                var val = _helperService.GetAppSetting("StudentService_GetStudents_Throw_Test_Exception");
                if (val != null)
                {
                    return new OkObjectResult(val);
                }
                
                return new OkObjectResult("Error!");
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex.Message);
            }
        }

       

    }
}
