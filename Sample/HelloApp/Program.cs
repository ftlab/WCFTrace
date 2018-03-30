using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HelloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting hello app");

            ServiceHost host = new ServiceHost(typeof(HelloService));
            host.Open();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
