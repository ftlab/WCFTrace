using System;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Контекст трассировки
    /// </summary>
    public class TraceContext
    {
        /// <summary>
        /// корневое событие трассировки
        /// </summary>
        private readonly TraceEvent _root;

        /// <summary>
        /// идентикатор трассировки
        /// </summary>
        private readonly TraceId _id;

        /// <summary>
        /// событие добавления события в контекст трассировки
        /// </summary>
        public EventHandler<OnAppendEventArgs> OnEvent;

        /// <summary>
        /// Контекст трассировки
        /// </summary>
        /// <param name="id">идентификатор трассировки</param>
        /// <param name="root">корневое событие трассировки</param>
        internal TraceContext(TraceId id
            , TraceEvent root)
        {
            if (id == null) throw new ArgumentNullException("id");
            _id = id;
            _root = root;
        }

        /// <summary>
        /// Идентификатор трассировки
        /// </summary>
        public TraceId Id { get { return _id; } }

        /// <summary>
        /// Корневое событие трассировки
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
        /// Добавить событие
        /// </summary>
        /// <param name="event">событие</param>
        internal virtual void AppendEvent(TraceEvent @event)
        {
            Root.AppendEvent(@event);

            var h = OnEvent;
            if (h != null)
                h(this, new OnAppendEventArgs(@event));
        }

        /// <summary>
        /// Добавить событие
        /// </summary>
        /// <param name="message">сообщение</param>
        /// <param name="type">тип</param>
        /// <param name="source">источник</param>
        public static void AppendEvent(string message
            , string type = null
            , string source = null)
        {
            var current = Current;

            current.AppendEvent(TraceEvent.Create(
                current.Id
                , message
                , source
                , type));
        }

        /// <summary>
        /// Добавить информационное событие
        /// </summary>
        /// <param name="info">информация</param>
        /// <param name="source">источник</param>
        public static void AppendInfo(string info, string source = null)
        {
            AppendEvent(message: info, type: "I", source: source);
        }

        /// <summary>
        /// Добавить событие о предупреждении
        /// </summary>
        /// <param name="warn">предупреждение</param>
        /// <param name="source">источник</param>
        public static void AppendWarn(string warn, string source = null)
        {
            AppendEvent(message: warn, type: "W", source: source);
        }

        /// <summary>
        /// Добавить событие об ошибке
        /// </summary>
        /// <param name="error">ошибка</param>
        /// <param name="source">источник</param>
        public static void AppendError(string error, string source = null)
        {
            AppendEvent(message: error, type: "E", source: source);
        }

        /// <summary>
        /// Добавить событие об ошибке
        /// </summary>
        /// <param name="exception">исключение</param>
        /// <param name="source">источник</param>
        public static void AppendError(Exception exception, string source = null)
        {
            AppendError(error: exception.GetType().Name, source: source);
        }
    }
}
