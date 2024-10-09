using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BusinessWebAPI.Controllers
{

    public class DataIntermed
    {
        public uint AcctNo { get; set; }
        public uint Pin { get; set; }
        public int Balance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Icon { get; set; } // Base64 representation of the icon image
    }

    [Route("api/[controller]")]
    [ApiController]

    public class GetValuesController : ControllerBase
    {

        private readonly RestClient _restClient;

        public GetValuesController()
        {
            _restClient = new RestClient("http://localhost:5100"); // DataServer API URL
        }

        [HttpGet("{index}")]
        public async Task<ActionResult<DataIntermed>> GetValuesForEntry(int index)
        {
            // Create RestRequest for DataServer
            RestRequest request = new RestRequest($"/WebAPIDataServer/dataserver/entry/{index}", Method.Get);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Deserialize the result into a DataIntermed object
                DataIntermed data = JsonConvert.DeserializeObject<DataIntermed>(response.Content);
                return Ok(data);
            }

            return StatusCode((int)response.StatusCode, "Failed to get data from DataServer");
        }
    }

}

