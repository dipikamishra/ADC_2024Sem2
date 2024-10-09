// Alex Starling - Distributed Computing - 2021
using System.Runtime.Serialization;
using System;

namespace BusinessWebAPI.Models
{
    
   
    public class IndexOutOfRangeFault : Exception
    {
    
        public string Issue { get; set; }
    }
}
