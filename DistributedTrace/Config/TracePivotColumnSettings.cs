using DistributedTrace.Collector;
using DistributedTrace.Stat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class TracePivotColumnSettings : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("measure", DefaultValue = MeasureType.Duration)]
        public MeasureType Measure
        {
            get { return (MeasureType)base["measure"]; }
            set { base["measure"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = AggregateType.Avg)]
        public AggregateType Type
        {
            get { return (AggregateType)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("filter", IsRequired = false, DefaultValue = null)]
        public TracePivotFilterSettings Filter
        {
            get { return (TracePivotFilterSettings)base["filter"]; }
            set { base["filter"] = value; }
        }
    }

    [ConfigurationCollection(typeof(TracePivotColumnSettings), AddItemName = "add")]
    public class TracePivotColumnSettingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TracePivotColumnSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TracePivotColumnSettings)element).Name;
        }
    }
}
