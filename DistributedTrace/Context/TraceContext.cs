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
            if (_root == null)
                _root = TraceEvent.Create(_id, _id.Name);
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
        /// <param name="name">сообщение</param>
        /// <param name="type">тип</param>
        public static void AddEvent(string name)
        {
            var current = Current;

            var @event = TraceEvent.Create(current.Id, name);
            current.Root.AddEvent(@event);

            var h = current.OnEvent;
            if (h != null)
                h(current, new OnAppendEventArgs(@event));
        }

        /// <summary>
        /// Получить свойство
        /// </summary>
        /// <param name="name">имя свойства</param>
        /// <returns></returns>
        public static string GetProperty(string name)
        {
            var current = Current;
            return current.Root[name];
        }

        /// <summary>
        /// Установить свойство
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="value">значение</param>
        public static void SetProperty(string name, string value)
        {
            var current = Current;
            current.Root[name] = value;
        }
    }
}
