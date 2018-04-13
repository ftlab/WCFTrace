using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Свойства события
    /// </summary>
    [CollectionDataContract(
        Namespace = Namespace.Main
        , Name = "props"
        , ItemName = "p"
        , KeyName = "k"
        , ValueName = "v")]
    public class TraceEventProperties : Dictionary<string, string>
    {
        /// <summary>
        /// индексатор
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns>значение</returns>
        public new string this[string key]
        {
            get
            {
                string value;
                if (TryGetValue(key, out value) == false)
                    value = null;
                return value;
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
