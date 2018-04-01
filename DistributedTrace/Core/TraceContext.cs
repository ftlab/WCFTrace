using System;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Контекст трассировки
    /// </summary>
    public class TraceContext
    {
        /// <summary>
        /// событие трассировки
        /// </summary>
        private readonly TraceEvent _event;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="event"></param>
        internal TraceContext(TraceEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");
            _event = @event;
        }

        /// <summary>
        /// Корневая запись трассировки
        /// </summary>
        public TraceEvent Event { get { return _event; } }

        /// <summary>
        /// Текущий контекст
        /// </summary>
        public static TraceContext Current
        {
            get
            {
                var scope = TraceContextScope.Current;
                if (scope == null) return NullTrace.Instance;

                return scope.Context ?? NullTrace.Instance;
            }
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        /// <param name="event"></param>
        public virtual void AppendEvent(TraceEvent @event)
        {
            Event.AppendEvent(@event);
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AppendEvent(string message
            , string type = null
            , string source = null
            , DateTime? start = null, DateTime? end = null)
        {
            AppendEvent(new TraceEvent()
            {
                Message = message,
                Type = type,
                Source = source,
                Start = start ?? DateTime.Now,
                End = end,
            });
        }

        /// <summary>
        /// Добавить информацию
        /// </summary>
        /// <param name="info"></param>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AppendInfo(string info, string source = null, DateTime? start = null, DateTime? end = null)
        {
            AppendEvent(message: info, type: "I", source: source
                , start: start, end: end);
        }

        /// <summary>
        /// Добавить предупреждение
        /// </summary>
        /// <param name="warn"></param>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AppendWarn(string warn, string source = null, DateTime? start = null, DateTime? end = null)
        {
            AppendEvent(message: warn, type: "W", source: source
                , start: start, end: end);
        }

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        /// <param name="error"></param>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AppendError(string error, string source = null, DateTime? start = null, DateTime? end = null)
        {
            AppendEvent(message: error, type: "E", source: source
                , start: start, end: end);
        }

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AppendError(Exception exception, string source = null, DateTime? start = null, DateTime? end = null)
        {
            AppendError(error: exception.GetType().Name, source: source
                , start: start, end: end);
        }
    }
}
