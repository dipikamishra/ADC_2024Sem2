using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using APICoreClasses;
namespace ServerWebAPI.Models
{
    public class Database
    {



        private readonly List<APICoreClasses.Customer> _database;

        public static Database Instance { get; } = new Database();
   
        public Database()
        {
           try
            {
                 // Create the fake values for our fake database
                _database = new List<APICoreClasses.Customer>();
                var generator = new DataGen();
                for (var i = 0; i < generator.NumOAccts(); i++)
                {
                    var temp = new APICoreClasses.Customer();

                    // Declare local variables to hold the out parameters
                    uint Pin, AcctNo;
                    string FirstName, LastName;
                    int Balance;
                    string Icon;

                    generator.GetNextAccount(out Pin, out AcctNo, out FirstName, out LastName, out Balance, out Icon);

                    // Assign the local variables to the properties
                    temp.Pin = Pin;
                    temp.AcctNo = AcctNo;
                    temp.FirstName = FirstName;
                    temp.LastName = LastName;
                    temp.Balance = Balance;
                    temp.Icon = Icon;

                    _database.Add(temp);

                    // Log the customer being added to the database
                    Console.WriteLine($"Generated Customer: {temp.FirstName} {temp.LastName}, AcctNo: {temp.AcctNo}, Balance: {temp.Balance}, Pin: { temp.Pin}");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error during data generation: {ex.Message}");
            }
        }

        public void AddCustomer(APICoreClasses.Customer customer)
        {
            // Simulate adding customer to a database (or actual DB logic)
            _database.Add(customer);
           
        }





        public uint GetAcctNoByIndex(int index)
        {
            return _database[index].AcctNo;
        }

        public uint GetPINByIndex(int index)
        {
            return _database[index].Pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            return _database[index].FirstName;
        }

        public string GetLastNameByIndex(int index)
        {
            return _database[index].LastName;
        }

        public int GetBalanceByIndex(int index)
        {
            return _database[index].Balance;
        }

        public string GetIconByIndex(int index) => _database[index].Icon; // Return the base64 icon string

        public int GetNumRecords()
        {
            return _database.Count;
        }


    }
}
