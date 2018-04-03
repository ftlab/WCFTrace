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

        public int? Duration { get { return End - Begin; } }

        public int? Different
        {
            get
            {
                if (Duration == null) return null;

                if (Events == null || Events.Count == 0)
                    return 0;

                return Duration - Events.Sum(e => e.Duration ?? 0);
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

        /// <summary>
        /// Запись события
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="prefLine"></param>
        public void WriteTo(StringBuilder builder
            , TraceId id
            , string prefLine = null)
        {
            if (builder == null) throw new ArgumentNullException("builder");

            prefLine = prefLine ?? string.Empty;

            builder.Append(prefLine);

            if (string.IsNullOrEmpty(Type) == false)
                builder.AppendFormat("{0}> ", Type);
            if (string.IsNullOrEmpty(Source) == false)
                builder.AppendFormat("{0}: ", Source);
            if (string.IsNullOrEmpty(Message) == false)
                builder.AppendFormat("{0}, ", Message);

            builder.AppendFormat("{0:HH:mm:ss}", GetBeginDateTime(id).ToLocalTime());

            if (Duration != null)
            {
                var ts = TimeSpan.FromMilliseconds(Duration.Value);
                builder.AppendFormat(", [{0}]", ts);
            }

            if (Different != null)
            {
                var ts = TimeSpan.FromMilliseconds(Different.Value);
                builder.AppendFormat(", [{0}]", ts);
            }

            if (Events != null)
                Events.ForEach(l =>
                {
                    builder.AppendLine();
                    l.WriteTo(builder, id, prefLine + "  ");
                });
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
