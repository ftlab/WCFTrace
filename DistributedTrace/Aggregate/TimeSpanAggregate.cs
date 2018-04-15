using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Stat
{
    /// <summary>
    /// Аггрегат временного интервала
    /// </summary>
    public class TimeSpanAggregate : IAggregate<TimeSpan?>
    {
        /// <summary>
        /// Аггрегат тиков
        /// </summary>
        private NumericAggregate<long?> _ticks;

        /// <summary>
        /// Аггрегат временного интервала
        /// </summary>
        /// <param name="type"></param>
        public TimeSpanAggregate(AggregateType type)
        {
            _ticks = new NumericAggregate<long?>(type);
        }

        /// <summary>
        /// Пустой аггрегат
        /// </summary>
        public bool IsEmpty
        {
            get { return _ticks.IsEmpty; }
        }

        /// <summary>
        /// Кол-во
        /// </summary>
        public int Count
        {
            get { return _ticks.Count; }
        }

        /// <summary>
        /// Тип аггрегата
        /// </summary>
        public AggregateType Type
        {
            get { return _ticks.Type; }
        }

        /// <summary>
        /// Значение
        /// </summary>
        public TimeSpan? Value
        {
            get
            {
                var value = _ticks.Value;

                if (value == null) return null;
                return TimeSpan.FromTicks(value.Value);
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="value"></param>
        public void Add(TimeSpan? value)
        {
            long? ticks = null;
            if (value != null)
                ticks = value.Value.Ticks;

            _ticks.Add(ticks);
        }

        /// <summary>
        /// Сброс
        /// </summary>
        public void Reset()
        {
            _ticks.Reset();
        }
    }
}
