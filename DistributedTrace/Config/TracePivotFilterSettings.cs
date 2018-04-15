using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Collector
{
    public class TracePivotFilterSettings : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public FilterType Type
        {
            get { return (FilterType)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("eventName")]
        public string EventName
        {
            get { return (string)base["eventName"]; }
            set { base["eventName"] = value; }
        }

        [ConfigurationProperty("property")]
        public string Property
        {
            get { return (string)base["property"]; }
            set { base["property"] = value; }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }
    }
}
