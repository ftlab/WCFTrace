using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Pivot
{
    public class TracePivotValue
    {
        public TracePivotColumn Column;

        public TracePivotRow Row;

        public TraceEventMeasure Measure;
    }
}
