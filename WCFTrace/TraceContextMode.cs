using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFTrace
{
    /// <summary>
    /// Режим трассировки
    /// </summary>
    public enum TraceContextMode
    {
        /// <summary>
        /// Создание новой трассировки
        /// </summary>
        New,
        /// <summary>
        /// Добавление в текущую трассировку
        /// </summary>
        Add,
        /// <summary>
        /// Добавление в текущую трассировку или создание новой
        /// </summary>
        AddOrNew
    }
}
