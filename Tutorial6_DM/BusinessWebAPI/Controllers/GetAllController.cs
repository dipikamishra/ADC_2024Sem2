using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BusinessWebAPI.Models;
using System;
using RestSharp;
using APICoreClasses;

namespace BusinessWebAPI.Controllers
{
    
    public class GetAllController : Controller
    {

        private readonly HttpClient _httpClient;

        public GetAllController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("customer/detail/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5197/api/GetAll/customer/{id}");

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

                Console.WriteLine($"Customer data: {customer.FirstName} {customer.LastName}, AcctNo: {customer.AcctNo}");

                return new JsonResult(customer);
               // return Ok(customer);
        
            }
            else
            {
                Console.WriteLine($"Failed to connect to ServerWebAPI: {response.StatusCode}");
                return NotFound(new { Message = "Client not found" });
            }
        }

        // Fetch all clients from DatabaseAPI
        [HttpGet("Customers")]
        public async Task<IEnumerable<APICoreClasses.Customer>> GetAllCustomers()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5197/api/GetAll/customers");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<APICoreClasses.Customer>>(content);
        }

        


    }
}
