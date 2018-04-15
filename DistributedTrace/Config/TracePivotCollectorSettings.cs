using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class TracePivotCollectorSettings : ConfigurationElement
    {
        [ConfigurationProperty("traceName", IsKey = true, IsRequired = true)]
        public string TraceName
        {
            get { return (string)base["traceName"]; }
            set { base["traceName"] = value; }
        }

        [ConfigurationProperty("write", DefaultValue = "00:05:00")]
        public TimeSpan Write
        {
            get { return (TimeSpan)base["write"]; }
            set { base["write"] = value; }
        }

        [ConfigurationProperty("reset", DefaultValue = "01:00:00")]
        public TimeSpan Reset
        {
            get { return (TimeSpan)base["reset"]; }
            set { base["reset"] = value; }
        }

        [ConfigurationProperty("columns")]
        public TracePivotColumnSettingsCollection Columns
        {
            get { return (TracePivotColumnSettingsCollection)base["columns"]; }
            set { base["columns"] = value; }
        }
    }

    [ConfigurationCollection(typeof(TracePivotCollectorSettings), AddItemName = "add")]
    public class TracePivotCollectorSettingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TracePivotCollectorSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TracePivotCollectorSettings)element).TraceName;
        }
    }
}
