using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedTrace.Pivot;

namespace DistributedTrace.Writer
{
    public class TracePivotWriter : ITracePivotWriter
    {
        public static ITracePivotWriter Default = new TracePivotWriter();

        public void Write(TracePivotTable pivot)
        {
            if (pivot == null) throw new ArgumentNullException("pivot");
        }
    }
}
