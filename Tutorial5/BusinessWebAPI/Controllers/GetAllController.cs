using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BusinessWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllController : ControllerBase
    {


        private readonly RestClient _restClient;

        public GetAllController()
        {
            _restClient = new RestClient("http://localhost:5141"); // DataServer API URL
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetNumEntries()
        {
            // Create RestRequest for DataServer
            RestRequest request = new RestRequest("/api/dataserver/num-entries", Method.Get);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Deserialize response into an integer
                int numEntries = JsonConvert.DeserializeObject<int>(response.Content);
                return Ok(numEntries);
            }

            return StatusCode((int)response.StatusCode, "Failed to get data from DataServer");
        }
    }
}

       //     public IActionResult Index()
       // {
        //    return View();
        //}
    