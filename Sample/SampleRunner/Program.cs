using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting sample runner");

            var echoApp = Process.Start("EchoApp");
            var helloApp = Process.Start("HelloApp");

            Console.WriteLine("wait...");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
            echoApp.Kill();
            helloApp.Kill();

        }
    }
}
