using DistributedTrace.Collector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class TracePivotRowSettings : ConfigurationElement
    {
        public string Name;

        public RowType Type;
    }
}
