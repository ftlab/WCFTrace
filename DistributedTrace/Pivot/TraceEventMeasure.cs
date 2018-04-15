using DistributedTrace.Core;
using DistributedTrace.Stat;
using System;
using System.Collections.Generic;

namespace DistributedTrace.Collector
{
    public class TraceEventMeasure : IAggregate<TimeSpan?>
    {
        private TimeSpanAggregate _ts;

        public TraceEventMeasure(
            AggregateType type
            , MeasureType measureType)
        {
            _ts = new TimeSpanAggregate(Type);
            MeasureType = measureType;
        }

        public MeasureType MeasureType { get; private set; }

        public bool IsEmpty { get { return _ts.IsEmpty; } }

        public int Count { get { return _ts.Count; } }

        public AggregateType Type { get { return _ts.Type; } }

        public TimeSpan? Value { get { return _ts.Value; } }

        public void AddRange(IEnumerable<TraceEvent> events)
        {
            if (events == null) return;

            foreach (var @event in events)
                Add(@event);
        }

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

        public void Add(TimeSpan? value)
        {
            _ts.Add(value);
        }

        public void Reset()
        {
            _ts.Reset();
        }
    }
}
