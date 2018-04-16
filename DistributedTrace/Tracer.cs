using DistributedTrace.Pivot;
using DistributedTrace.Config;
using DistributedTrace.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace
{
    public class Tracer
    {
        public static Tracer Default = new Tracer();

        public Tracer()
        {
            Collectors = new TraceCollectors(Config.Traces);
        }

        public DistributedTraceConfig Config { get { return DistributedTraceConfig.Default; } }

        public TraceCollectors Collectors { get; private set; }

        protected virtual void On(TraceId id, TraceEvent @event)
        {
            Collectors.Collect(id, @event);
        }

        public static void OnEvent(TraceId id, TraceEvent @event)
        {
            Default.On(id, @event);
        }
    }
}
