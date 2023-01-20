using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Api.Services.CategoryService;

namespace Api.Functions
{
    public class CategoryFunction
    {

        private readonly ICategoryService _categoryService;

        public CategoryFunction(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [FunctionName("Categories")]
        public IActionResult GetCategories(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categories")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/categories request.");
            return new OkObjectResult(_categoryService.GetCategories());
        }
    }
}
