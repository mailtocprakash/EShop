using EShopDataAccessModel;
using EShopWeb.Models;
using EShopWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;


namespace EShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AzureFunctionService _azureFunctionService;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AzureFunctionService azureFunctionService)
        {
            _logger = logger;
            _configuration = configuration;
            _azureFunctionService = azureFunctionService;
        }

        public async Task<IActionResult> Index()
        {
            string FunctionAppUrl = _configuration["FunctionAppUrl"];
            string FunctionAppKey = _configuration["FunctionAppKey"];
            List<Product> products = new List<Product>();
            //products = ProductService.GetMultipleProducts();

            try
            {
                string functionResponse = await _azureFunctionService.CallFunctionAsync("GetProducts", FunctionAppUrl, FunctionAppKey);
                if (!string.IsNullOrEmpty(functionResponse))
                {

                    products = JsonConvert.DeserializeObject<List<Product>>(functionResponse);
                    //Product itemToUpdate = products.FirstOrDefault(p => p.Id == 1);
                    //itemToUpdate.Name = itemToUpdate.Name + _configuration["Environment"];
                }
            }
            catch (HttpRequestException ex)
            {
                products = new List<Product>();
            }

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
