using DistributedTrace.Collector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class TracePivotRowSettings : BaseSettings
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = RowType.EventName)]
        public RowType Type
        {
            get { return (RowType)base["type"]; }
            set { base["type"] = value; }
        }
    }

    [ConfigurationCollection(typeof(TracePivotRowSettings), AddItemName = "add")]
    public class TracePivotRowSettingsCollection
        : BaseSettingsCollection<TracePivotRowSettings>
    {

    }
}
