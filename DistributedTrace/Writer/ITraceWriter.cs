using DistributedTrace.Core;

namespace DistributedTrace.Writer
{
    /// <summary>
    /// Механизм записи событий трассировки
    /// </summary>
    public interface ITraceWriter
    {
        /// <summary>
        /// Запись события трассировки
        /// </summary>
        /// <param name="id">идентификатор трассировки</param>
        /// <param name="event">событие трассировки</param>
        void Write(TraceId id, TraceEvent @event);
    }
}
