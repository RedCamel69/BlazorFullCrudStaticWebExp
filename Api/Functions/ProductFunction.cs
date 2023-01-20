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

namespace Api.Functions
{
    public class ProductFunction
    {

        private readonly IProductService _productService;

        public ProductFunction(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("Products")]
        public IActionResult GetProducts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/products request.");
            return new OkObjectResult(_productService.GetProducts());
        }

        [FunctionName("ProductsAsync")]        
        public async Task<IActionResult> GetProductsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/productsasync request.");

            //var res = _productService.GetProductsAsync().Result;
            var res = await _productService.GetProductsAsync();

            return new OkObjectResult(res);
        }

        [FunctionName("GetAdminProducts")]
        public IActionResult GetAdminProducts(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAdminProducts")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/GetAdminProducts request.");
            var res = _productService.GetAdminProducts();
             return new OkObjectResult(res);
        }

        [FunctionName("GetAdminProductsAsync")]
        public IActionResult GetAdminProductsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAdminProductsAsync")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/GetAdminProducts request.");
            var res = _productService.GetAdminProductsAsync();
            return new OkObjectResult(res);
        }

        [FunctionName("GetFeatureProducts")]
        public IActionResult GetFeaturedProducts(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetFeaturedProducts")] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/GetFeaturedProducts request.");
            var res = _productService.GetFeaturedProducts();
            return new OkObjectResult(res);
        }

        [FunctionName("GetFeatureProductsAsync")]
        public IActionResult GetFeatureProductsAsync(
     [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetFeatureProductsAsync")] HttpRequest req,
         ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/GetFeatureProductsAsync request.");
            var res = _productService.GetFeaturedProductsAsync().Result;
            return new OkObjectResult(res);
        }

        [FunctionName("SearchProducts")]
        public IActionResult SearchProducts(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SearchProducts")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed api/SearchProducts request.");
            var res = _productService.SearchProducts(null,1);
            return new OkObjectResult(res);
        }

        [FunctionName("SearchProductsAsync")]
        public IActionResult SearchProductsAsync(
   [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SearchProductsAsync/{searchtext}/{page}")] HttpRequest req,
    string searchtext,
    int page,
       ILogger log)
        {
            var x = req;
            var s = searchtext;
            var p = page;

            log.LogInformation("C# HTTP GET trigger function processed api/SearchProductsAsync request.");
            var res = _productService.SearchProductsAsync(searchtext,page).Result;
            return new OkObjectResult(res);
        }
    }
}
