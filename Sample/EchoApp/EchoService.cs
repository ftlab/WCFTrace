using Contracts;
using DistributedTrace.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoApp
{
    public class EchoService : IEchoService
    {
        public void Echo(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now}> {msg}");
            Console.ResetColor();
        }
    }
}
