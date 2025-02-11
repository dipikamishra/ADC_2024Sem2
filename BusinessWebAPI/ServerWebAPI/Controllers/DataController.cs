﻿using Microsoft.AspNetCore.Mvc;
using ServerWebAPI.Models;

namespace ServerWebAPI.Controllers
{
    public class DataController : ControllerBase
    {

        private readonly Database _db = Database.Instance;

        [HttpGet]
        public ActionResult<int> GetNumEntries()
        {
            return Ok(_db.GetNumRecords());
        }

        [HttpGet("entry/{index}")]
        public ActionResult GetValuesForEntry(int index)
        {
            if (index < 0 || index >= _db.GetNumRecords())
            {
                return BadRequest(new { Issue = "Index was not in range..." });
            }

            var entry = new
            {
                AcctNo = _db.GetAcctNoByIndex(index),
                Pin = _db.GetPINByIndex(index),
                Balance = _db.GetBalanceByIndex(index),
                FirstName = _db.GetFirstNameByIndex(index),
                LastName = _db.GetLastNameByIndex(index),
               // Icon = _db.GetIconByIndex(index) // You may need to return this as a base64 string or other format
            };

            return Ok(entry);


            /*public ActionResult<int> GetNumEntries()
            {
                return Ok(_db.GetNumRecords());
            }*/
        }

    }
}
