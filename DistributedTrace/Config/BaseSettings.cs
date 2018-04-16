using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Базовые настройки
    /// </summary>
    public abstract class BaseSettings : ConfigurationElement
    {
        /// <summary>
        /// Получить ключ настройки
        /// </summary>
        /// <returns></returns>
        public virtual object GetKey()
        {
            var keyFields = Properties.Cast<ConfigurationProperty>()
                .Where(x => x.IsKey)
                .ToArray();

            if (keyFields.Length != 1) throw new NotSupportedException();

            return this[keyFields[0]];
        }

        /// <summary>
        /// Получить значение свойства
        /// </summary>
        /// <typeparam name="T">тип значения</typeparam>
        /// <param name="propName">имя свойства</param>
        /// <returns></returns>
        public virtual T GetValue<T>(string propName)
        {
            return (T)this[propName];
        }

        /// <summary>
        /// Установить значение свойства
        /// </summary>
        /// <typeparam name="T">тип значения</typeparam>
        /// <param name="propName">имя свойства</param>
        /// <param name="value">значение</param>
        public virtual void SetValue<T>(string propName, T value)
        {
            this[propName] = value;
        }
    }
}
