using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Настройки коллектора пивота
    /// </summary>
    public class TracePivotCollectorSettings : BaseSettings
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
        /// Интервал записи
        /// </summary>
        [ConfigurationProperty("write", DefaultValue = "00:05:00")]
        public virtual TimeSpan Write
        {
            get { return (TimeSpan)base["write"]; }
            set { base["write"] = value; }
        }

        /// <summary>
        /// Интервал сброса
        /// </summary>
        [ConfigurationProperty("reset", DefaultValue = "01:00:00")]
        public virtual TimeSpan Reset
        {
            get { return (TimeSpan)base["reset"]; }
            set { base["reset"] = value; }
        }

        /// <summary>
        /// Строки пивота
        /// </summary>
        [ConfigurationProperty("rows")]
        public virtual TracePivotRowSettingsCollection Rows
        {
            get { return (TracePivotRowSettingsCollection)base["rows"]; }
            set { base["rows"] = value; }
        }

        /// <summary>
        /// колонки пивота
        /// </summary>
        [ConfigurationProperty("columns")]
        public virtual TracePivotColumnSettingsCollection Columns
        {
            get { return (TracePivotColumnSettingsCollection)base["columns"]; }
            set { base["columns"] = value; }
        }
    }

    /// <summary>
    /// Коллекция настроек коллекторов пивота
    /// </summary>
    [ConfigurationCollection(typeof(TracePivotCollectorSettings), AddItemName = "add")]
    public class TracePivotCollectorSettingsCollection
        : BaseSettingsCollection<TracePivotCollectorSettings>
    {
    }
}
