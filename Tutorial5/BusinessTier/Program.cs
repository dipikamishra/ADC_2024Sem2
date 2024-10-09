using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace BusinessTier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Business Tier Starting...");
            var tcp = new NetTcpBinding();
            var host = new ServiceHost(typeof(BusinessServer));
            host.AddServiceEndpoint(typeof(BusinessServerInterface), tcp, "net.tcp://localhost:8200/BusinessService");
            host.Open();
            Console.WriteLine("Business Tier Online");
            Console.ReadLine();
            host.Close();
        }
    }
}
