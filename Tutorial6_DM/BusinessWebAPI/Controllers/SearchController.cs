using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace BusinessWebAPI.Controllers
{
    public class SearchController : Controller
    {

        private readonly HttpClient _httpClient;

        public SearchController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch a client by ID from DatabaseAPI


        [HttpGet("customerLN/detail/{LN}")]
        public async Task<IActionResult> GetCustomerByLN(string LN)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5197/api/Search/customerLN/{LN}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                APICoreClasses.Customer customer = JsonConvert.DeserializeObject<APICoreClasses.Customer>(content);
                Console.WriteLine($"Response from ServerWebAPI: {response.StatusCode} - {content}");
                //Console.WriteLine($"Customer data: {customer.FirstName} {customer.LastName}, AcctNo: {customer.AcctNo}");

                if (customer == null)
                {
                    Console.WriteLine("Deserialization failed, customer object is null.");
                    return StatusCode(500, new { Message = "Failed to deserialize customer data." });
                }

                Console.WriteLine($"From BusinessAPI Customer data: {customer.FirstName} {customer.LastName}, AcctNo: {customer.AcctNo}");

                return new JsonResult(customer);
                // return Ok(customer);

            }
            else
            {
                Console.WriteLine($"Failed to connect to ServerWebAPI: {response.StatusCode}");
                return NotFound(new { Message = "Client not found" });
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
