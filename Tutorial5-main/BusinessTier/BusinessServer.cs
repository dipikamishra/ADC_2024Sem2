using System;
using System.ServiceModel;
using System.Drawing;
using DBInterface;
using System.Runtime.CompilerServices;

namespace BusinessTier
{
    public class BusinessServer : BusinessServerInterface
    {
        private uint LogNumber = 0;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Log(string logString)
        {
            LogNumber++;
            string logEntry = $"Log #{LogNumber}: {logString} - Timestamp: {DateTime.Now}";
            Console.WriteLine(logEntry); 
        }

        private DataServerInterface CreateDataTierChannel()
        {
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost:8100/DataService";
            var channelFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            return channelFactory.CreateChannel();
        }

        public int GetNumEntries()
        {
            Log("GetNumEntries called.");
            var dataTier = CreateDataTierChannel();
            return dataTier.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon)
        {
            Log($"GetValuesForEntry called with index: {index}.");
            var dataTier = CreateDataTierChannel();
            dataTier.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out icon);
            Log($"Data retrieved for index {index}: {fName} {lName}, AccountNo: {acctNo}, Balance: ${bal}, Pin: {pin}");
        }

        public bool SearchLastName(string lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon)
        {
            Log($"SearchByLastName called with lastName: {lastName}.");
            var dataTier = CreateDataTierChannel();
            int numEntries = dataTier.GetNumEntries();

            for (int i = 0; i < numEntries; i++)
            {
                // Loop through all entries
                dataTier.GetValuesForEntry(i, out acctNo, out pin, out bal, out fName, out lName, out icon);
                // If entry matches last name, return true
                if (lName.ToLower() == lastName.ToLower())
                {
                    Log($"Match found for {lastName}: {fName} {lName}, AccountNo: {acctNo}, Balance: ${bal}, Pin: {pin}");
                    return true;
                }
            }

            Log($"No match found for last name: {lastName}");
            // If no match is found, return defaults and false
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            icon = null;
            return false;
        }
    }
}
