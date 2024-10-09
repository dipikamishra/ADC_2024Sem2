using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ServerWebAPI.Models;

namespace ServerWebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AddController : Controller
    {
        private readonly Database _db = Database.Instance;
        
        [HttpPost("/customerA/data")]
     
        public IActionResult AddCustomer(APICoreClasses.Customer customer)
        {
            try
            {
                Console.WriteLine("AddCustomer endpoint from ServerAPI");
                // Add the customer to the database
                _db.AddCustomer(customer);

                // Print customer details to the console
                Console.WriteLine($"Adding customer: FirstName = {customer.FirstName}, LastName = {customer.LastName}");

                // Return the newly added customer object
                return new JsonResult(customer);
                //return Ok(customer);
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
using ServerWebAPI.Models;

namespace ServerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly Database _db = Database.Instance;

        [HttpPost("customerA/data")]
        public IActionResult AddCustomer([FromBody] APICoreClasses.Customer customer)
        {
            try
            {
                // Add the customer to the database
                _db.AddCustomer(customer);

                // Log customer details
                Console.WriteLine($"Added customer: {customer.FirstName} {customer.LastName}");

                // Return the customer as the response
                return Ok(customer);
            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
*/

