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
        private readonly TraceEvent _root;

        private readonly TraceId _id;

        public EventHandler<OnTraceEventArgs> OnEvent;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="event"></param>
        internal TraceContext(TraceId id
            , TraceEvent root)
        {
            if (id == null) throw new ArgumentNullException("id");
            _id = id;
            _root = root;
        }

        public TraceId Id { get { return _id; } }

        /// <summary>
        /// Корневая запись трассировки
        /// </summary>
        public TraceEvent Root { get { return _root; } }

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
        internal virtual void AppendEvent(TraceEvent @event)
        {
            Root.AppendEvent(@event);

            var h = OnEvent;
            if (h != null)
                h(this, new OnTraceEventArgs(@event));
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void AppendEvent(string message
            , string type = null
            , string source = null
            , DateTime? start = null, DateTime? end = null)
        {
            var current = Current;

            current.AppendEvent(TraceEvent.Create(
                current.Id
                , message
                , source
                , type));
        }

        /// <summary>
        /// Добавить информацию
        /// </summary>
        /// <param name="info"></param>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void AppendInfo(string info, string source = null, DateTime? start = null, DateTime? end = null)
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
        public static void AppendWarn(string warn, string source = null, DateTime? start = null, DateTime? end = null)
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
        public static void AppendError(string error, string source = null, DateTime? start = null, DateTime? end = null)
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
        public static void AppendError(Exception exception, string source = null, DateTime? start = null, DateTime? end = null)
        {
            AppendError(error: exception.GetType().Name, source: source
                , start: start, end: end);
        }
    }
}
