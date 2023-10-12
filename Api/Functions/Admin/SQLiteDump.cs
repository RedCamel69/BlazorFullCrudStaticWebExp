using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Services.Admin.DataDumpService;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Api.Functions.Admin
{
    //todo: secure function!
    public class SQLiteDump 
    {
        private readonly IDataDumpService _service;

        public SQLiteDump(IDataDumpService Service)
        {
            _service= Service;
        }


        [FunctionName("SQLiteDiagnostics")]
        public async Task<HttpResponseMessage> Diagnostics2(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Diagnostics2")] HttpRequest req, ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");


            var resp = _service.Diagnostics2();

            var myObj = new { name = "thomas", location = "Denver" };
            var jsonToReturn = JsonConvert.SerializeObject(resp.Data);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }


        [FunctionName("SQLiteDump")]
        public  async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Dump")] HttpRequest req, ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");


            var resp = _service.GetEverything();

            var myObj = new { name = "thomas", location = "Denver" };
            var jsonToReturn = JsonConvert.SerializeObject(resp.Data);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
        
    }
}

