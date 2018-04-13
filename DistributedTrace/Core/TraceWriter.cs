﻿using System;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Устройство записи события трассировки
    /// </summary>
    public class TraceWriter : ITraceWriter
    {
        /// <summary>
        /// По-умолчанию
        /// </summary>
        public static ITraceWriter Default = new TraceWriter();

        /// <summary>
        /// Запись события
        /// </summary>
        /// <param name="id">идентификатор трассировки</param>
        /// <param name="event"></param>
        public virtual void Write(TraceId id, TraceEvent @event)
        {
            if (Environment.UserInteractive == false) return;

            if (id == null) throw new ArgumentNullException("id");
            if (@event == null) throw new ArgumentNullException("event");

            lock (this)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(id.ToString());
                Console.ResetColor();

                DateTime prev = default(DateTime);

                @event.Visit((node) =>
                {
                    var e = node.Node;
                    string pref = new string(' ', node.Level * 2);
                    Console.Write(pref);
                    Console.ForegroundColor = ConsoleColor.Green;

                    var bdt = e.GetBeginDateTime(id);
                    if ((bdt - prev).TotalSeconds > 1)
                    {
                        Console.Write("[{0:HH:mm:ss}] ", bdt);
                        prev = bdt;
                    }

                    Console.ResetColor();
                    if (string.IsNullOrEmpty(e.Type) == false)
                        Console.Write("{0}> ", e.Type);
                    if (string.IsNullOrEmpty(e.Source) == false)
                        Console.Write("{0}: ", e.Source);
                    if (string.IsNullOrEmpty(e.Message) == false)
                        Console.Write("{0}", e.Message);

                    if (e.Duration.HasValue)
                    {
                        var dt = e.Duration.Value;
                        if (dt > TimeSpan.FromSeconds(2))
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[{0}]", dt.GetDisplayText());
                        Console.ResetColor();
                    }

                    if (e.Different != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("[{0}]", e.Different.Value.GetDisplayText());
                        Console.ResetColor();
                    }

                    Console.WriteLine();
                });
            }
        }
    }
}
