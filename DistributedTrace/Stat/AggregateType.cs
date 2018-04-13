using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Stat
{
    /// <summary>
    /// Тип аггрегации
    /// </summary>
    public enum AggregateType
    {
        /// <summary>
        /// Минимальное значение
        /// </summary>
        Min,
        /// <summary>
        /// Максимальное значение
        /// </summary>
        Max,
        /// <summary>
        /// Среднее значение
        /// </summary>
        Avg,
        /// <summary>
        /// Сумма
        /// </summary>
        Sum,
        /// <summary>
        /// Первое значение
        /// </summary>
        First,
        /// <summary>
        /// Последнее значение
        /// </summary>
        Last
    }
}
