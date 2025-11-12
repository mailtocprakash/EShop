using EShopDataAccessModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EShopFunctionApp
{
    public class EShopFunctions
    {
        private readonly ILogger<EShopFunctions> _logger;
        private readonly IConfiguration _configuration;

        public EShopFunctions(ILogger<EShopFunctions> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Function("GetProducts")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            List<Product> products = new List<Product>();
            //products = ProductService.GetMultipleProducts(Environment.GetEnvironmentVariable("DBEnvironment"));
            products = ProductService.GetMultipleProducts(_configuration["DBEnvironment"]);
            //Product itemToUpdate = products.FirstOrDefault(p => p.Id == 2);
            //itemToUpdate.Name = itemToUpdate.Name + Environment.GetEnvironmentVariable("Environment");
            //itemToUpdate.Name = itemToUpdate.Name + _configuration["Environment"];

            return new OkObjectResult(products);
        }
    }
}
