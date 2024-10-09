using Microsoft.AspNetCore.Mvc;
using ServerWebAPI.Models;
using System.IO;

namespace ServerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly Database _db = Database.Instance;


        //Get Customer by LastName
        [HttpGet("customerLN/{LN}")]
        public IActionResult GetCustomerByLN(string LN)
        {
            Console.WriteLine($"Last name: {LN}"); //, AcctNo: {customer.AcctNo}
            //GetLastNameByIndex

            // Normalize the search string (optional, depending on your use case)
            var normalizedLN = LN.Trim().ToLower();

            // Loop through all records in the database and look for matching last names
            for (int i = 0; i < _db.GetNumRecords(); i++)
            {
                // Retrieve the last name from the current record and normalize it
                var lastNameInDb = _db.GetLastNameByIndex(i).ToLower();

                // Check if the last name matches the input last name (case-insensitive)
                if (lastNameInDb == normalizedLN)
                {
                    var entry = new
                    {
                        AcctNo = _db.GetAcctNoByIndex(i),
                        Pin = _db.GetPINByIndex(i),
                        Balance = _db.GetBalanceByIndex(i),
                        FirstName = _db.GetFirstNameByIndex(i),
                        LastName = _db.GetLastNameByIndex(i),
                        Icon = _db.GetIconByIndex(i)  // Convert Bitmap to base64
                                                                                                     // Icon = _db.GetIconByIndex(id) // You may need to return this as a base64 string or other format

                        // Icon = _db.GetIconByIndex(i) // Optional: If you want to include an icon, handle it accordingly
                    };

                    // Log the customer data to verify it's populated
                    Console.WriteLine($"Returning Customer from search by LN: {entry.FirstName} {entry.LastName}"); //, AcctNo: {customer.AcctNo}");
                    return Ok(entry); //returns the first customer with the matching last name and ignore other duplicates

                }
            }
            return NotFound(new { Message = "Customer not found" });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
