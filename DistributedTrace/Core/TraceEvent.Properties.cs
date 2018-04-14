using System.Collections.Generic;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Свойства события
    /// </summary>
    public partial class TraceEvent
    {
        /// <summary>
        /// Содержит свойства
        /// </summary>
        public bool ContainsProperties
        {
            get
            {
                return _properties != null && _properties.Count > 0;
            }
        }

        /// <summary>
        /// Свойства события
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> Properties()
        {
            if (_properties == null) yield break;

            foreach (var kvp in _properties)
                yield return kvp;
        }

        /// <summary>
        /// Получить свойство
        /// </summary>
        /// <param name="name">имя свойства</param>
        /// <returns>значение</returns>
        public string GetProperty(string name)
        {
            if (_properties == null) return null;

            return _properties[name];
        }

        /// <summary>
        /// Установить значение свойства
        /// </summary>
        /// <param name="name">имя свойства</param>
        /// <param name="value">значение</param>
        public void SetProperty(string name, string value)
        {
            if (_properties == null)
                _properties = new TraceEventProperties();

            _properties[name] = value;
        }

        /// <summary>
        /// Индексатор свойств событий
        /// </summary>
        /// <param name="propertyName">имя свойства</param>
        /// <returns>значение</returns>
        public string this[string propertyName]
        {
            get { return GetProperty(propertyName); }
            set { SetProperty(propertyName, value); }
        }
    }
}
