using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    public class BaseSettings : ConfigurationElement
    {
        public virtual object GetKey()
        {
            var keyFields = Properties.Cast<ConfigurationProperty>()
                .Where(x => x.IsKey)
                .ToArray();

            if (keyFields.Length != 1) throw new NotSupportedException();

            return this[keyFields[0]];
        }

        public T GetValue<T>(string propName)
        {
            return (T)this[propName];
        }

        public void SetValue<T>(string propName, T value)
        {
            this[propName] = value;
        }
    }
}
