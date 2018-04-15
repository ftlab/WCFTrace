using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class TraceCollectorSettings : ConfigurationElement
    {
        [ConfigurationProperty("traceName", IsKey = true, IsRequired = true)]
        public string TraceName
        {
            get { return (string)base["traceName"]; }
            set { base["traceName"] = value; }
        }

        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }
    }

    [ConfigurationCollection(typeof(TraceCollectorSettings), AddItemName = "add")]
    public class TraceCollectorSettingsCollection
        : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TraceCollectorSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TraceCollectorSettings)element).TraceName;
        }
    }
}
