using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class DistributedTraceConfig : ConfigurationSection
    {
        public static DistributedTraceConfig Default;

        static DistributedTraceConfig()
        {
            Default = (DistributedTraceConfig)ConfigurationManager.GetSection("distributedTrace");

            var cl = Default.Pivots
                .OfType<TracePivotCollectorSettings>().First()
                .Columns.OfType<TracePivotColumnSettings>().First();

            if (Default == null)
                Default = new DistributedTraceConfig();
        }

        [ConfigurationProperty("traces")]
        public TraceCollectorSettingsCollection Traces
        {
            get { return (TraceCollectorSettingsCollection)base["traces"]; }
            set { base["traces"] = value; }
        }

        [ConfigurationProperty("pivots")]
        public TracePivotCollectorSettingsCollection Pivots
        {
            get { return (TracePivotCollectorSettingsCollection)base["pivots"]; }
            set { base["pivots"] = value; }
        }
    }
}
