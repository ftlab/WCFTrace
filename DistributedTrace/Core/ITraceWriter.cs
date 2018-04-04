using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Core
{
    public interface ITraceWriter
    {
        void Write(TraceId id, TraceEvent @event);
    }
}
