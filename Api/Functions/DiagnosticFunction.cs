using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;

namespace Api.Functions
{
    public class DiagnosticFunction
    {
        [FunctionName("Diagnostics")]
        public IActionResult GetDiagnostics(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "diagnostics")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/diagnostics request.");

            var res = new {
                USE_AZURESQL = Environment.GetEnvironmentVariable("USE_AZURESQL"),
                ConnectionStringAzureSQL = Environment.GetEnvironmentVariable("ConnectionStringAzureSQL"),
                USE_SQLITE = Environment.GetEnvironmentVariable("USE_SQLITE"),
                USE_AZURESQL_ConvertToBool = Convert.ToBoolean(Environment.GetEnvironmentVariable("USE_AZURESQL"))
            };

            return new OkObjectResult(res);
        }
    }
}
