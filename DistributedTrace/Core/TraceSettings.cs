using System.Configuration;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Настройки трассировки
    /// </summary>
    public class TraceSettings
    {
        /// <summary>
        /// Настройки по-умолчанию
        /// </summary>
        public static TraceSettings Default = new TraceSettings();

        /// <summary>
        /// доступна трассировка
        /// </summary>
        private bool? _enabled;

        /// <summary>
        /// Доступна трассировка
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                if (_enabled != null) return _enabled.Value;

                var enabledValue = ConfigurationManager.AppSettings["DistributedTrace.Enabled"];
                if (string.IsNullOrEmpty(enabledValue))
                    _enabled = true;
                else
                    _enabled = bool.Parse(enabledValue);

                return _enabled.Value;
            }
        }
    }
}
