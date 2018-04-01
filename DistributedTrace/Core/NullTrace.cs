using System;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Null устройство
    /// </summary>
    public class NullTrace : TraceContext
    {
        /// <summary>
        /// Экземпляр
        /// </summary>
        public static NullTrace Instance = new NullTrace();

        /// <summary>
        /// Конструктор
        /// </summary>
        private NullTrace() : base(new TraceEvent() { Start = DateTime.Now, Message = "NULL" })
        {
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        /// <param name="event"></param>
        public override void AppendEvent(TraceEvent @event)
        {

        }
    }
}
