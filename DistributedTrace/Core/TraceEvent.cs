using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Событие трассировки
    /// </summary>
    [DataContract]
    public class TraceEvent
    {
        /// <summary>
        /// Дата и время начала события
        /// </summary>
        [DataMember]
        public DateTime Start { get; set; }

        /// <summary>
        /// Дата и время завершения события
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public DateTime? End { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// Источник
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Вложенные события
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<TraceEvent> Events { get; set; }

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
        public void WriteTo(StringBuilder builder, string prefLine = null)
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

            builder.AppendFormat("{0:YYYY-MM-dd HH:mm:ss}", Start);

            if (End != null)
            {
                var ts = End.Value - Start;
                if (ts > TimeSpan.FromSeconds(1))
                    ts = TimeSpan.FromSeconds(Math.Ceiling(ts.TotalSeconds));

                builder.AppendFormat(", [{0}]", ts);
            }

            if (Events != null)
                Events.ForEach(l =>
                {
                    builder.AppendLine();
                    l.WriteTo(builder, prefLine + "\t");
                });
        }

        /// <summary>
        /// Отображаемый текст события
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var bld = new StringBuilder();
            WriteTo(bld);
            return bld.ToString();
        }
    }
}
