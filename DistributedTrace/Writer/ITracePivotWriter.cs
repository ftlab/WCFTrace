using DistributedTrace.Pivot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Writer
{
    /// <summary>
    /// Механизм записи пивота
    /// </summary>
    public interface ITracePivotWriter
    {
        /// <summary>
        /// Запись
        /// </summary>
        /// <param name="pivot">пивот</param>
        void Write(TracePivotTable pivot);
    }
}
