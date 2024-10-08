using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebAPI.Models
{
    public class Customer
    {
        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;


        public Customer()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
           // icon = null;
        }


    }
}
