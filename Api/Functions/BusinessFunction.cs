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

using Api.Services.BusinessService;
using BlazorEcommerceStaticWebApp.Shared;

namespace Api.Functions
{
    public class BusinessFunction
    {

        private readonly IBusinessService _businessService;

        public BusinessFunction(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [FunctionName("Businesses")]
        public IActionResult GetBusinesses(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "businesses")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/businesses request.");
            return new OkObjectResult(_businessService.GetBusinesses());
        }

      

        [FunctionName("CreateBusiness")]
        public async Task<IActionResult> CreateBusiness(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "business")] HttpRequest req,
            ILogger log)
        {
            string result = await req.ReadAsStringAsync();

            var j=  JsonConvert.DeserializeObject<Business>(result);
    
            log.LogInformation("C# HTTP POST trigger function processed api/business request.");

            return new OkObjectResult(await _businessService.CreateBusiness(j));
        }


      
    }
}
