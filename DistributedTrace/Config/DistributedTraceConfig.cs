using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Секция всех настроек
    /// </summary>
    public class DistributedTraceConfig : ConfigurationSection
    {
        /// <summary>
        /// Секция по-умолчанию
        /// </summary>
        public static DistributedTraceConfig Default;

        /// <summary>
        /// Секция всех настроек
        /// </summary>
        static DistributedTraceConfig()
        {
            Default = (DistributedTraceConfig)ConfigurationManager.GetSection("distributedTrace");

            if (Default == null)
                Default = new DistributedTraceConfig();
        }

        /// <summary>
        /// Коллекция настроек коллекторов трассировок
        /// </summary>
        [ConfigurationProperty("traces")]
        public virtual TraceCollectorSettingsCollection Traces
        {
            get { return (TraceCollectorSettingsCollection)base["traces"]; }
            set { base["traces"] = value; }
        }

        /// <summary>
        /// Коллекция настроек коллекторов аггрегатов трассировок
        /// </summary>
        [ConfigurationProperty("pivots")]
        public virtual TracePivotCollectorSettingsCollection Pivots
        {
            get { return (TracePivotCollectorSettingsCollection)base["pivots"]; }
            set { base["pivots"] = value; }
        }
    }
}
