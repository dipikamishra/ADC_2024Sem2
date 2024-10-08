using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BusinessWebAPI.Controllers
{
    public class SearchData
    {
        public string LastName { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {


        private readonly RestClient _restClient;

        public SearchController()
        {
            _restClient = new RestClient("http://localhost:5141"); // DataServer API URL
        }

        [HttpGet]
        [Route("search/lastName")]
        public async Task<ActionResult<DataIntermed>> SearchByLastName(string name)
        {
            // Create RestRequest for DataServer
            RestRequest request = new RestRequest("/api/dataserver/search", Method.Get);
            /*request.AddJsonBody(searchData); // Add the search data as JSON

            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Deserialize the result into a DataIntermed object
                DataIntermed data = JsonConvert.DeserializeObject<DataIntermed>(response.Content);
                return Ok(data);
            }
            */
            return StatusCode(200, "Failed to get data from DataServer");
        }



    }
}
