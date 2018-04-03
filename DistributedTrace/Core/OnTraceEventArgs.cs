using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Core
{
    public class OnTraceEventArgs : EventArgs
    {
        public OnTraceEventArgs(TraceEvent @event)
        {
            Event = @event;
        }

        public TraceEvent Event { get; private set; }
    }
}
