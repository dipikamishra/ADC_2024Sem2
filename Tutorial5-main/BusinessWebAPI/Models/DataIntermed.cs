using System.Drawing;

namespace BusinessWebAPI.Models
{
    public class DataIntermed
    {

        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;
        public Bitmap icon;

        public DataIntermed()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
            icon = null;
        }

        /* public uint AcctNo { get; set; }
         public uint Pin { get; set; }
         public int Balance { get; set; }
         public string FirstName { get; set; }
         public string LastName { get; set; }
         public Bitmap Icon { get; set; } // Base64 string for the icon */

    }
}
