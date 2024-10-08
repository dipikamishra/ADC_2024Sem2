using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleClient
{
    public class Customer
    {
        public uint AcctNo;
         public uint Pin;
         public int Balance;
         public string FirstName;
         public string LastName;

        /*
        [JsonProperty("acctNo")]
        public int acctNo { get; set; }

        [JsonProperty("pin")]
        public int pin { get; set; }

        [JsonProperty("balance")]
        public int balance { get; set; }

        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; } */



      /* public Customer()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
           // icon = null;
        }
      */
        
    }
}
