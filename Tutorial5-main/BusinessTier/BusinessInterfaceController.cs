using Microsoft.AspNetCore.Mvc;

namespace BusinessWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessInterfaceController : Controller
    {
        // Placeholder for interface-related functionality
        // This could be extended for API documentation or for dynamic interface discovery, if needed

        [HttpGet("get-interface-info")]
        public ActionResult GetInterfaceInfo()
        {
            // Return a description of the interface, methods, etc.
            var interfaceInfo = new
            {
                Methods = new string[] { "GetNumEntries", "GetValuesForEntry", "SearchLastName" },
                Description = "Interface methods for Business Server"
            };
            return Ok(interfaceInfo);

            /*    public IActionResult Index()
            {
                return View();
            }*/

        }
    }
}
