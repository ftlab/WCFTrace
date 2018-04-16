using Contracts;
using DistributedTrace.Context;
using DistributedTrace.Core;
using System;
using System.Threading;

namespace HelloApp
{
    public class HelloService : IHelloService
    {
        public void Hello()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello");
            Console.ResetColor();

            using (var dbscope = new TraceContextScope("бд"))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                TraceContext.SetProperty("cs", "connection string");
            }

            using (var echo = new EchoClient())
                echo.Echo("вызов echo");
        }
    }
}
