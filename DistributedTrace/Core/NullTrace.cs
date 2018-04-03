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
        private NullTrace() : base(TraceId.Create("NULL")
            , null)
        {
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        /// <param name="event"></param>
        internal override void AppendEvent(TraceEvent @event)
        {

        }
    }
}
