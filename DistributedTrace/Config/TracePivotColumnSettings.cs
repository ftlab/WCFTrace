using DistributedTrace.Collector;
using DistributedTrace.Stat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class TracePivotColumnSettings
    {
        public string Name;

        public MeasureType Measure;

        public TracePivotFilter Filter;

        public AggregateType Type;
    }
}
