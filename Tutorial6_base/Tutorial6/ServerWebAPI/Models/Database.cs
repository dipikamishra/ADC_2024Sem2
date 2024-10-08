using System.Collections.Generic;
using System.Net.NetworkInformation;
using APICoreClasses;
namespace ServerWebAPI.Models
{
    public class Database
    {



        private readonly List<APICoreClasses.Customer> _database;

        public static Database Instance { get; } = new Database();
        //private Database() {}


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

                    generator.GetNextAccount(out Pin, out AcctNo, out FirstName, out LastName, out Balance); // out temp.icon);

                    // Assign the local variables to the properties
                    temp.Pin = Pin;
                    temp.AcctNo = AcctNo;
                    temp.FirstName = FirstName;
                    temp.LastName = LastName;
                    temp.Balance = Balance;

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


            /*
            private Database() // Private constructor
            {
                _database = new List<Customer>();
            }

            // GenerateData method to populate the fake database
            public void GenerateData()
            {
                var generator = new DataGen();
                for (var i = 0; i < generator.NumOAccts(); i++)
                {
                    var temp = new Customer();
                    generator.GetNextAccount(out temp.pin, out temp.acctNo, out temp.firstName, out temp.lastName, out temp.balance);
                    _database.Add(temp);
                }
            }

            */

           
              

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

        public int GetNumRecords()
        {
            return _database.Count;
        }


    }
}
