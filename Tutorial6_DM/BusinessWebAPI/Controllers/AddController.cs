using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using APICoreClasses;
using System.Text;
using System;
using RestSharp;
using System.Net.Http;

namespace BusinessWebAPI.Controllers
{
    public class AddController : Controller
    {
        private readonly HttpClient _httpClient;

        public AddController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("/customerA/details/data")]
        public async Task<IActionResult> AddCustomer([FromBody] APICoreClasses.Customer customer)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(customer);
                // Print jsonContent to check the serialized JSON string
                Console.WriteLine($"JSON Content: {jsonContent}");

                // var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");


                // Print the created content(it won't print the actual content directly, but you can check the length or existence)
                // Console.WriteLine($"StringContent Length: {content.Headers.ContentLength}");
                // Console.WriteLine($"Content-Type: {content.Headers.ContentType}");
                // Send the request to ServerAPI to add the customer
               
                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5197/api/Add/customerA/data", new StringContent(jsonContent, Encoding.UTF8, "application/json"));
                Console.WriteLine($"Response from business Add controller: {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var addedCustomer = JsonConvert.DeserializeObject<Customer>(responseBody);

                    return Ok(addedCustomer);
                }

                return StatusCode((int)response.StatusCode, "Failed to add customer.");
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}


 /*

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using APICoreClasses;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AddController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("customerA/details/data")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            try
            {
                // Serialize customer object to JSON
                var jsonContent = JsonConvert.SerializeObject(customer);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Post the data to ServerWebAPI
                var response = await _httpClient.PostAsync("http://localhost:5197/api/add/customerA/data", content);

                // Log the status code for debugging
                Console.WriteLine($"Response from Server API: {response.StatusCode}");

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var addedCustomer = JsonConvert.DeserializeObject<Customer>(responseBody);
                    return Ok(addedCustomer); // Return the added customer to the client
                }

                // If not successful, return the status code and error message
                return StatusCode((int)response.StatusCode, "Failed to add customer to Server API.");
            }
            catch (Exception ex)
            {
                // Return server error status code in case of any exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}


*/