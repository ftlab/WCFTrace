using Contracts;
using DistributedTrace.Core;
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
        static List<Process> Childs;

        private class MyClass { public string Id; }

        static void Main(string[] args)
        {
            Console.WriteLine("starting sample runner");

            var traceId = TraceId.Create("Пример");
            var @event = new TraceEvent() { Start = DateTime.Now, Message = "Hello" };

            using (var scope = new TraceContextScope(traceId, @event, TraceContextMode.New))
            {
                Childs = new List<Process>() {
                Process.Start(nameof(EchoApp))
                , Process.Start(nameof(HelloApp))};

                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Console.WriteLine("wait...");
                Thread.Sleep(TimeSpan.FromSeconds(2));

                TraceContext.Current.AppendInfo("World");

                using (var echo = new EchoClient())
                    echo.Echo("Hello");

                using (var hello = new HelloClient())
                    hello.Hello();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Childs?.ForEach(p => p.Kill());
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Childs?.ForEach(p => p.Kill());
        }
    }
}
