using DistributedTrace.Aggregate;
using DistributedTrace.Core;
using System;
using System.Collections.Generic;

namespace DistributedTrace.Pivot
{
    /// <summary>
    /// Измеряемая величина трассировки
    /// </summary>
    public class TraceEventMeasure : IAggregate<TimeSpan?>
    {
        /// <summary>
        /// аггрегат интервала
        /// </summary>
        private TimeSpanAggregate _ts;

        /// <summary>
        /// Измеряемая величина трассировки
        /// </summary>
        /// <param name="type">тип аггрегата</param>
        /// <param name="measureType">тип измерямой величины</param>
        public TraceEventMeasure(
            AggregateType type
            , MeasureType measureType)
        {
            _ts = new TimeSpanAggregate(Type);
            MeasureType = measureType;
        }

        /// <summary>
        /// Тип измеряемой величины
        /// </summary>
        public MeasureType MeasureType { get; private set; }

        /// <summary>
        /// пустой аггрегат
        /// </summary>
        public bool IsEmpty { get { return _ts.IsEmpty; } }

        /// <summary>
        /// Кол-во величин
        /// </summary>
        public int Count { get { return _ts.Count; } }

        /// <summary>
        /// Тип аггрегата
        /// </summary>
        public AggregateType Type { get { return _ts.Type; } }

        /// <summary>
        /// Значение измеряемой величины
        /// </summary>
        public TimeSpan? Value { get { return _ts.Value; } }

        /// <summary>
        /// Добавить список
        /// </summary>
        /// <param name="events">события</param>
        public void AddRange(IEnumerable<TraceEvent> events)
        {
            if (events == null) return;

            foreach (var @event in events)
                Add(@event);
        }

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="event">событие</param>
        public void Add(TraceEvent @event)
        {
            TimeSpan? ts = null;

            if (@event != null)
            {
                if (MeasureType == MeasureType.Count)
                    ts = null;
                else if (MeasureType == MeasureType.BeginTime)
                    ts = @event.BeginTime;
                else if (MeasureType == MeasureType.EndTime)
                    ts = @event.EndTime;
                else if (MeasureType == MeasureType.Duration)
                    ts = @event.Duration;
                else if (MeasureType == MeasureType.ExcludedTime)
                    ts = @event.ExcludedTime;
                else throw new NotSupportedException(MeasureType.ToString());
            }

            _ts.Add(ts);
        }

        /// <summary>
        /// Добавить значение
        /// </summary>
        /// <param name="value"></param>
        public void Add(TimeSpan? value)
        {
            _ts.Add(value);
        }

        /// <summary>
        /// Сброс
        /// </summary>
        public void Reset()
        {
            _ts.Reset();
        }
    }
}
