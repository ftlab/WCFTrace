﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Событие трассировки
    /// </summary>
    [DataContract(Name = "ev", Namespace = Namespace.Main)]
    public class TraceEvent
    {
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
        public string Message { get; set; }

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

        public TimeSpan? SumDiff
        {
            get
            {
                if (Events == null || Events.Count == 0)
                    return Different;

                var r = Different ?? TimeSpan.Zero;
                foreach (var e in Events)
                    r = r + (e.SumDiff ?? TimeSpan.Zero);

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
        /// <param name="source">источник</param>
        /// <param name="type">тип события</param>
        /// <returns></returns>
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
        /// Посетить событие
        /// </summary>
        /// <param name="onEvent">обработчик посещения</param>
        public void Visit(Action<TraceEvent, int, int> onEvent)
        {
            if (onEvent == null) return;

            VisitTree(onEvent, 0, 0);
        }

        /// <summary>
        /// рекурсивное посещение события
        /// </summary>
        /// <param name="onEvent">обработчик посещения</param>
        /// <param name="level">уровень</param>
        /// <param name="pos">позиция</param>
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
            var sb = new StringBuilder();
            WriteTo(sb);
            return sb.ToString();
        }

        /// <summary>
        /// Запись события
        /// </summary>
        /// <param name="sb">построитель строки</param>
        public void WriteTo(StringBuilder sb)
        {
            Visit((e, level, index) =>
            {
                if (level != 0)
                    sb.AppendLine();

                sb.AppendFormat("{0}t: {1}, s: {2}, m: {3}, b: {4}, e: {5}", new String(' ', level * 2)
                    , e.Type, e.Source, e.Message
                    , e.BeginTime.GetDisplayText()
                    , e.EndTime == null ? null : e.EndTime.Value.GetDisplayText());
            });
        }

        /// <summary>
        /// Запись события
        /// </summary>
        /// <param name="sb">построитель строки</param>
        /// <param name="id">идентификатор трассировки</param>
        public void WriteTo(StringBuilder sb, TraceId id)
        {
            if (id == null)
            {
                WriteTo(sb);
                return;
            }

            sb.Append(id.ToString());

            var prev = default(DateTime);

            Visit((e, level, index) =>
            {
                sb.AppendLine();
                sb.Append(new String(' ', level * 2));

                var bdt = e.GetBeginDateTime(id);
                if ((bdt - prev).TotalSeconds > 1)
                {
                    sb.AppendFormat("[{0:HH:mm:ss}] ", bdt);
                    prev = bdt;
                }

                if (string.IsNullOrEmpty(e.Type) == false)
                    sb.AppendFormat("{0}> ", e.Type);

                if (string.IsNullOrEmpty(e.Source) == false)
                    sb.AppendFormat("{0}: ", e.Source);

                if (string.IsNullOrEmpty(e.Message) == false)
                    sb.AppendFormat("{0} ", e.Message);

                if (e.Duration != null)
                    sb.AppendFormat("[{0}] ", e.Duration.Value.GetDisplayText());

                if (e.Different != null)
                    sb.AppendFormat("[{0}] ", e.Different.Value.GetDisplayText());

                if (level == 0 && index == 0 && e.SumDiff != null)
                {
                    var sd = e.SumDiff;
                    if (sd != null)
                        sb.AppendFormat("[{0}] ", sd.Value.GetDisplayText());
                }
            });
        }
    }
}
