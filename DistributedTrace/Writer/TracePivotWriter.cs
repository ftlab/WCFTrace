using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedTrace.Pivot;

namespace DistributedTrace.Writer
{
    /// <summary>
    /// Механизм записи пивота
    /// </summary>
    public class TracePivotWriter : ITracePivotWriter
    {
        /// <summary>
        /// По-умолчанию
        /// </summary>
        public static ITracePivotWriter Default = new TracePivotWriter();

        /// <summary>
        /// Запись
        /// </summary>
        /// <param name="pivot"></param>
        public void Write(TracePivotTable pivot)
        {
            if (pivot == null) throw new ArgumentNullException("pivot");
        }
    }
}
