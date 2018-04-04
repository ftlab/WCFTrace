using System;
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
                Console.WriteLine(id.ToString());

                @event.Visit((e, level, index) =>
                {
                    string pref = new string(' ', level * 2);
                    Console.Write(pref);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[{0:HH:mm:ss}] ", e.GetBeginDateTime(id).ToLocalTime());

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

                        if (dt.TotalDays > 1)
                            Console.Write("[{0} d, {1:d2}]", dt.Days, dt.Hours);
                        else if (dt.TotalHours > 1)
                            Console.Write("[{0:d2} h, {1:d2} m]", dt.Hours, dt.Minutes);
                        else if (dt.TotalMinutes > 1)
                            Console.Write("[{0:d2} m, {1:d2} s]", dt.Minutes, dt.Seconds);
                        else if (dt.TotalSeconds > 1)
                            Console.Write("[{0:d2} s, {1:d3} ms]", dt.Seconds, dt.Milliseconds);
                        else
                            Console.Write("[{0:d3} ms]", dt.Milliseconds);

                        Console.ResetColor();
                    }

                    Console.WriteLine();
                });
            }
        }
    }
}
