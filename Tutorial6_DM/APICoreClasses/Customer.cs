﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APICoreClasses
{
    public class Customer
    {
       /* public uint AcctNo;
        public uint Pin;
        public int Balance;
        public string FirstName;
        public string LastName;*/


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint AcctNo { get; set; }
        public uint Pin { get; set; }
        public int Balance { get; set; }

        // Change the Icon property to a string to store the base64 image
        public string Icon { get; set; }

        /*// Add a Bitmap property for the customer icon
        public Bitmap Icon { get; set; }*/



        /*
        //[JsonProperty("acctNo")]
        public uint AcctNo { get; set; }

       // [JsonProperty("pin")]
        public uint Pin { get; set; }

       // [JsonProperty("balance")]
        public int Balance { get; set; }

       // [JsonProperty("firstName")]
        public string FirstName { get; set; }

      //  [JsonProperty("lastName")]
        public string LastName { get; set; }*/

    }
}
