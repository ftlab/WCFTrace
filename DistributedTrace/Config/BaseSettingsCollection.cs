using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class BaseSettingsCollection<T>
        : ConfigurationElementCollection
        , IEnumerable<T>
        where T : BaseSettings, new()
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BaseSettings)element).GetKey();
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return this.Cast<T>().GetEnumerator();
        }
    }
}
