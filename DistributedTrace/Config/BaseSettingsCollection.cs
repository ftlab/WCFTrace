using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DistributedTrace.Config
{
    /// <summary>
    /// Базовая коллекция настроек
    /// </summary>
    /// <typeparam name="T">Тип настройки</typeparam>
    public abstract class BaseSettingsCollection<T>
        : ConfigurationElementCollection
        where T : BaseSettings, new()
    {
        /// <summary>
        /// Получить настройку по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T GetByKey(object key)
        {
            return (T)BaseGet(key);
        }

        /// <summary>
        /// Получить настройку по индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual T GetByIndex(int index)
        {
            return (T)BaseGet(index);
        }

        /// <summary>
        /// получить настройку по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T this[object key] { get { return GetByKey(key); } }

        /// <summary>
        /// Создать новый элемент
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        /// <summary>
        /// Получить ключ элемента
        /// </summary>
        /// <param name="element">элемент</param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BaseSettings)element).GetKey();
        }
    }
}
