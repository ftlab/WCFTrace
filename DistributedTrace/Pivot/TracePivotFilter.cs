using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Collector
{
    public class TracePivotFilter
    {
        public FilterType Type;

        public string TraceName;

        public string EventName;

        public string Property;

        public string Value;

        public string Path;
    }
}
