using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Collector
{
    public enum MeasureType
    {
        Count,
        BeginTime,
        EndTime,
        Duration,
        ExcludedTime
    }
}
