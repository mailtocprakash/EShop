namespace EShopWeb.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json; // May require the Newtonsoft.Json NuGet package

    public class AzureFunctionService
    {
        private readonly HttpClient _httpClient;        

        public AzureFunctionService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> CallFunctionAsync(string _functionname, string _functionUrl, string _functionKey)
        {
            // Construct the full URL with query parameters or prepare a JSON body for a POST request
            var requestUrl = $"{_functionUrl}/api/{_functionname}";

            
            // Add the x-functions-key header if the function requires a key
            if (!string.IsNullOrEmpty(_functionKey))
            {
                _httpClient.DefaultRequestHeaders.Add("x-functions-key", _functionKey);
            }

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                // Handle error cases
                throw new HttpRequestException($"Error calling function: {response.StatusCode}");
            }
        }
    }

}
