using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Настройки коллектора трассировок
    /// </summary>
    public class TraceCollectorSettings : BaseSettings
    {
        /// <summary>
        /// Имя трассировки
        /// </summary>
        [ConfigurationProperty("traceName", IsKey = true, IsRequired = true)]
        public virtual string TraceName
        {
            get { return (string)base["traceName"]; }
            set { base["traceName"] = value; }
        }

        /// <summary>
        /// Включен коллектор
        /// </summary>
        [ConfigurationProperty("enabled", DefaultValue = true)]
        public virtual bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }
    }

    /// <summary>
    /// Коллекция настроек коллекторов трассировок
    /// </summary>
    [ConfigurationCollection(typeof(TraceCollectorSettings), AddItemName = "add")]
    public class TraceCollectorSettingsCollection
        : BaseSettingsCollection<TraceCollectorSettings>
    {

    }
}
