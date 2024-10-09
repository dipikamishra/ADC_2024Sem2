using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ServerWebAPI.Models;
using System;
using System.Net.NetworkInformation;

namespace ServerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllController : Controller
    {
        
        private readonly Database _db = Database.Instance;


        [HttpGet("customers")]
        public ActionResult<int> GetNumEntries()
        {
            return Ok(_db.GetNumRecords());
        }

        //Get Customer by index
        [HttpGet("customer/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if (id < 0 || id >= _db.GetNumRecords())
            {
                return BadRequest(new { Issue = "Index was not in range..." });
            }

           

            var entry = new
            {
                AcctNo = _db.GetAcctNoByIndex(id),
                Pin = _db.GetPINByIndex(id),
                Balance = _db.GetBalanceByIndex(id),
                FirstName = _db.GetFirstNameByIndex(id),
                LastName = _db.GetLastNameByIndex(id),
                Icon = _db.GetIconByIndex(id)  // Convert Bitmap to base64

            };
            // Log the customer data to verify it's populated
            Console.WriteLine($"Returning Customer for search by index: {entry.FirstName} {entry.LastName}"); //, AcctNo: {customer.AcctNo}");
            return Ok(entry);

        }


       

    }
}
