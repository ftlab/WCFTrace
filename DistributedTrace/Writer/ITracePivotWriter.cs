using DistributedTrace.Pivot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Writer
{
    public interface ITracePivotWriter
    {
        void Write(TracePivotTable pivot);
    }
}
