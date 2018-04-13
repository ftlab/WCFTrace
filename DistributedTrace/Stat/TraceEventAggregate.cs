using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedTrace.Core;

namespace DistributedTrace.Stat
{
    public class TraceEventAggregate : IAggregate<TraceEvent>
    {
        public TraceEventAggregate(AggregateType type)
        {
            Type = type;
        }

        public bool IsEmpty
        {
            get { return Count < 1; }
        }

        public int Count { get; private set; }

        public AggregateType Type { get; private set; }

        public TraceEvent Value { get; private set; }

        public void Add(TraceEvent v)
        {
            if (v == null) return;

            if (IsEmpty)
            {
                Value = v;
            }
            else
            {
                if (Type == AggregateType.Sum)
                {
                    //Value = SumTrace(Value, v);
                }
            }
            Count++;
        }

        public void Reset()
        {
            Value = null;
            Count = 0;
        }
    }
}
