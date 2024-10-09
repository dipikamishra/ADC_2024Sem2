using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using BusinessWebAPI.Models;
using System;
using RestSharp;
using Newtonsoft.Json;

namespace BusinessWebAPI.Controllers
{
   // [Route("api/[controller]")]
   // [ApiController]
    public class BusinessController : ControllerBase
    {
        private uint LogNumber = 0;

       /* private readonly HttpClient _httpClient;

        public BusinessController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            RestClient restClient = new RestClient("http://localhost:5141");
        }

        private async Task<int> GetNumEntriesFromDataServerAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5141/WebAPIDataServer/data/num-entries"); // Replace with correct DataServer API URL
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<int>();
        }*/

        // Logging method
        private void Log(string logString)
        {
            LogNumber++;
            string logEntry = $"Log #{LogNumber}: {logString} - Timestamp: {DateTime.Now}";
            Console.WriteLine(logEntry);
        }



        // Replace WCF channel creation with HTTP calls to Data API
        private DataServerInterface CreateDataTierChannel()
        {
            // You would replace this with HTTP client logic to call DataServer Web API
           throw new NotImplementedException("Replace this with HTTP client logic");

        }

        

       /* [HttpGet("num-entries")]
        public ActionResult<int> GetNumEntries()
        {
            Log("GetNumEntries called.");
            var dataTier = CreateDataTierChannel(); // Replace with actual HTTP client call
            return Ok(dataTier.GetNumEntries());
        }*/

      /*  [HttpGet("entry/{index}")]
        public ActionResult GetValuesForEntry(int index)
        {
            Log($"GetValuesForEntry called with index: {index}.");
            var dataTier = CreateDataTierChannel(); // Replace with actual HTTP client call
            dataTier.GetValuesForEntry(index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out byte[] icon);

            var entry = new
            {
                AcctNo = acctNo,
                Pin = pin,
                Balance = bal,
                FirstName = fName,
                LastName = lName,
                Icon = Convert.ToBase64String(icon) // Convert to base64 string if using byte[] for images
            };

            Log($"Data retrieved for index {index}: {fName} {lName}, AccountNo: {acctNo}, Balance: ${bal}, Pin: {pin}");
            return Ok(entry);
        }

        [HttpGet("search/{lastName}")]
        public ActionResult SearchLastName(string lastName)
        {
            Log($"SearchByLastName called with lastName: {lastName}.");
            var dataTier = CreateDataTierChannel(); // Replace with actual HTTP client call
            bool found = dataTier.SearchLastName(lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out byte[] icon);

            if (found)
            {
                var entry = new
                {
                    AcctNo = acctNo,
                    Pin = pin,
                    Balance = bal,
                    FirstName = fName,
                    LastName = lName,
                    Icon = Convert.ToBase64String(icon)
                };
                return Ok(entry);
            }

            Log($"No match found for last name: {lastName}");
            return NotFound(new { Message = "No match found" });
        }*/
    }
}









       /* public IActionResult Index()
        {
            return View();
        }*/
/*   }
}
*/