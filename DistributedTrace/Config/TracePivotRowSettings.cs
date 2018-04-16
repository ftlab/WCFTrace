using DistributedTrace.Pivot;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Настройки строк пивота
    /// </summary>
    public class TracePivotRowSettings : BaseSettings
    {
        /// <summary>
        /// Имя строки
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public virtual string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        /// <summary>
        /// Тип строки
        /// </summary>
        [ConfigurationProperty("type", DefaultValue = RowType.EventName)]
        public virtual RowType Type
        {
            get { return (RowType)base["type"]; }
            set { base["type"] = value; }
        }
    }

    /// <summary>
    /// Коллекция настроек строк пивота
    /// </summary>
    [ConfigurationCollection(typeof(TracePivotRowSettings), AddItemName = "add")]
    public class TracePivotRowSettingsCollection
        : BaseSettingsCollection<TracePivotRowSettings>
    {

    }
}
