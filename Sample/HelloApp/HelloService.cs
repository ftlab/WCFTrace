using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloApp
{
    public class HelloService : IHelloService
    {
        public void Hello()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello");
            Console.ResetColor();
        }
    }
}
