using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Pivot
{
    /// <summary>
    /// Величина пивота
    /// </summary>
    public class TracePivotValue
    {
        /// <summary>
        /// Колонка
        /// </summary>
        public TracePivotColumn Column;

        /// <summary>
        /// Строка
        /// </summary>
        public TracePivotRow Row;


        /// <summary>
        /// Измеряемая величина
        /// </summary>
        public TraceEventMeasure Measure;
    }
}
