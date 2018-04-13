using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Событие трассировки
    /// </summary>
    [DataContract(Name = "ev", Namespace = Namespace.Main)]
    public partial class TraceEvent
    {
        private static string CurrentSource;

        static TraceEvent()
        {
            CurrentSource = Dns.GetHostName();
        }

        /// <summary>
        /// Событие трассировки
        /// </summary>
        private TraceEvent() { }

        /// <summary>
        /// Время начала события в мс
        /// </summary>
        [DataMember(Name = "b", Order = 0)]
        private int Begin { get; set; }

        /// <summary>
        /// Время окончания события в мс
        /// </summary>
        [DataMember(Name = "e", Order = 1, EmitDefaultValue = false)]
        private int? End { get; set; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [DataMember(Name = "t", Order = 2, EmitDefaultValue = false)]
        public string Type { get; private set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [DataMember(Name = "m", Order = 3, EmitDefaultValue = false)]
        public string Message { get; private set; }

        /// <summary>
        /// Источник
        /// </summary>
        [DataMember(Name = "s", Order = 4, EmitDefaultValue = false)]
        public string Source { get; private set; }

        /// <summary>
        /// Вложенные события
        /// </summary>
        [DataMember(Name = "es", Order = 5, EmitDefaultValue = false)]
        public List<TraceEvent> Events { get; private set; }

        /// <summary>
        /// Время начала события
        /// </summary>
        public TimeSpan BeginTime { get { return TimeSpan.FromMilliseconds(Begin); } }

        /// <summary>
        /// Время окончания события
        /// </summary>
        public TimeSpan? EndTime { get { return End == null ? default(TimeSpan?) : TimeSpan.FromMilliseconds(End.Value); } }

        /// <summary>
        /// Длительность события
        /// </summary>
        public TimeSpan? Duration
        {
            get
            {
                if (End == null) return null;
                return TimeSpan.FromMilliseconds(End.Value - Begin);
            }
        }

        /// <summary>
        /// Разница между длительностью события и длительностью вложенных событий
        /// </summary>
        public TimeSpan? Different
        {
            get
            {
                if (Duration == null) return null;

                if (Events == null || Events.Count == 0)
                    return TimeSpan.Zero;

                return Duration - TimeSpan.FromMilliseconds(Events.Sum(e => (e.End - e.Begin) ?? 0));
            }
        }

        public TimeSpan? TotalDiff
        {
            get
            {
                if (Events == null || Events.Count == 0)
                    return Different;

                var r = Different ?? TimeSpan.Zero;
                foreach (var e in Events)
                    r = r + (e.TotalDiff ?? TimeSpan.Zero);

                return r;
            }
        }

        /// <summary>
        /// Получить дату и время начала события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DateTime GetBeginDateTime(TraceId id)
        {
            return (id.Timestamp + BeginTime).ToLocalTime();
        }

        /// <summary>
        /// Получить дату и время окончания события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DateTime? GetEndDateTime(TraceId id)
        {
            if (End == null) return null;
            return (id.Timestamp + EndTime.Value).ToLocalTime();
        }

        /// <summary>
        /// Создание события
        /// </summary>
        /// <param name="id">идентификатор трассировки</param>
        /// <param name="message">сообщение</param>
        /// <param name="type">тип события</param>
        /// <returns></returns>
        public static TraceEvent Create(TraceId id
            , string message
            , string type = null)
        {
            var begin = (int)Math.Ceiling((DateTime.UtcNow - id.Timestamp).TotalMilliseconds);
            var @event = new TraceEvent()
            {
                Begin = begin,
                Message = message,
                Source = CurrentSource,
                Type = type
            };

            return @event;
        }

        /// <summary>
        /// Установить время завершения события
        /// </summary>
        /// <param name="id">идентификатор трассировки</param>
        internal void SetEnd(TraceId id)
        {
            End = (int)Math.Ceiling((DateTime.UtcNow - id.Timestamp).TotalMilliseconds);
        }

        /// <summary>
        /// Добавить вложенное событие
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public TraceEvent AppendEvent(TraceEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            if (Events == null)
                Events = new List<TraceEvent>();

            Events.Add(@event);

            return this;
        }


        /// <summary>
        /// Отображаемый текст события
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}", Message);
        }
    }
}
