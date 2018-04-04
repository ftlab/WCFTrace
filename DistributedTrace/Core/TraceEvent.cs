using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Событие трассировки
    /// </summary>
    [DataContract(Name = "ev", Namespace = Namespace.Value)]
    public class TraceEvent
    {
        private TraceEvent()
        {
        }

        /// <summary>
        /// Дата и время начала события
        /// </summary>
        [DataMember(Name = "b", Order = 0)]
        private int Begin { get; set; }

        /// <summary>
        /// Дата и время завершения события
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

        public TimeSpan BeginTime { get { return TimeSpan.FromMilliseconds(Begin); } }

        public TimeSpan? Duration
        {
            get
            {
                if (End == null) return null;
                return TimeSpan.FromMilliseconds(End.Value - Begin);
            }
        }

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

        public DateTime GetBeginDateTime(TraceId id)
        {
            return id.Timestamp + BeginTime;
        }

        public static TraceEvent Create(TraceId id
            , string message
            , string source = null
            , string type = null)
        {
            var begin = (int)Math.Ceiling((DateTime.UtcNow - id.Timestamp).TotalMilliseconds);
            var @event = new TraceEvent()
            {
                Begin = begin,
                Message = message,
                Source = source,
                Type = type
            };

            return @event;
        }

        internal void SetEnd(TraceId id)
        {
            End = (int)Math.Ceiling((DateTime.UtcNow - id.Timestamp).TotalMilliseconds);
        }

        /// <summary>
        /// Добавить событие
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

        public void Visit(Action<TraceEvent, int, int> onEvent)
        {
            if (onEvent == null) return;

            VisitTree(onEvent, 0, 0);
        }

        private void VisitTree(Action<TraceEvent, int, int> onEvent, int level, int pos)
        {
            onEvent(this, level, pos);

            if (Events != null)
                for (int i = 0; i < Events.Count; i++)
                    Events[i].VisitTree(onEvent, level + 1, i);

        }

        /// <summary>
        /// Отображаемый текст события
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Begin: {0} ms", Begin);
        }
    }
}
