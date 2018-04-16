using DistributedTrace.Aggregate;
using DistributedTrace.Pivot;
using System.Configuration;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Настройки колонки пивота
    /// </summary>
    public class TracePivotColumnSettings : BaseSettings
    {
        /// <summary>
        /// Имя колонки
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public virtual string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        /// <summary>
        /// Тип измеряемой величины
        /// </summary>
        [ConfigurationProperty("measure", DefaultValue = MeasureType.Duration)]
        public virtual MeasureType Measure
        {
            get { return (MeasureType)base["measure"]; }
            set { base["measure"] = value; }
        }

        /// <summary>
        /// Тип аггрегата
        /// </summary>
        [ConfigurationProperty("type", DefaultValue = AggregateType.Avg)]
        public virtual AggregateType Type
        {
            get { return (AggregateType)base["type"]; }
            set { base["type"] = value; }
        }

        /// <summary>
        /// Фильтр
        /// </summary>
        [ConfigurationProperty("filter", IsRequired = false, DefaultValue = null)]
        public virtual TracePivotFilterSettings Filter
        {
            get { return (TracePivotFilterSettings)base["filter"]; }
            set { base["filter"] = value; }
        }
    }

    /// <summary>
    /// Коллекция настроек колонок пивота
    /// </summary>
    [ConfigurationCollection(typeof(TracePivotColumnSettings), AddItemName = "add")]
    public class TracePivotColumnSettingsCollection
        : BaseSettingsCollection<TracePivotColumnSettings>
    {
    }
}
