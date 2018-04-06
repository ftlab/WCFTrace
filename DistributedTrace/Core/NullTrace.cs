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
        /// Null устройство
        /// </summary>
        private NullTrace()
            : base(TraceId.Create("NULL"), null)
        {
        }

        /// <summary>
        /// Добавить событие
        /// </summary>
        /// <param name="event">событие трассировки</param>
        internal override void AppendEvent(TraceEvent @event)
        {

        }
    }
}
