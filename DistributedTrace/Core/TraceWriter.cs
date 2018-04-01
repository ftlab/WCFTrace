using System;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Устройство записи события трассировки
    /// </summary>
    public class TraceWriter
    {
        /// <summary>
        /// По-умолчанию
        /// </summary>
        public static TraceWriter Default = new TraceWriter();

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

            var builder = new StringBuilder();
            builder.AppendLine(id.ToString());
            @event.WriteTo(builder);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(builder.ToString());
            Console.ResetColor();
        }
    }
}
