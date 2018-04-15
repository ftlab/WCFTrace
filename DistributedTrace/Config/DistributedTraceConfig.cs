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
            Default = (DistributedTraceConfig)ConfigurationManager.GetSection("DistributedTrace");
            if (Default == null)
                Default = new DistributedTraceConfig();
        }
    }
}
