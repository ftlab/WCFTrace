using DistributedTrace.Pivot;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Настройки фильтра
    /// </summary>
    public class TracePivotFilterSettings : ConfigurationElement
    {
        /// <summary>
        /// Тип фильтра
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public virtual FilterType Type
        {
            get { return (FilterType)base["type"]; }
            set { base["type"] = value; }
        }

        /// <summary>
        /// Имя события
        /// </summary>
        [ConfigurationProperty("eventName")]
        public virtual string EventName
        {
            get { return (string)base["eventName"]; }
            set { base["eventName"] = value; }
        }

        /// <summary>
        /// Имя свойства
        /// </summary>
        [ConfigurationProperty("property")]
        public string PropertyName
        {
            get { return (string)base["property"]; }
            set { base["property"] = value; }
        }

        /// <summary>
        /// Значение свойства
        /// </summary>
        [ConfigurationProperty("value")]
        public string PropertyValue
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }
    }
}
