using DistributedTrace.Aggregate;
using DistributedTrace.Config;
using System;
using System.Collections.Generic;

namespace DistributedTrace.Pivot
{
    /// <summary>
    /// Колонка пивота
    /// </summary>
    public class TracePivotColumn
    {
        /// <summary>
        /// пивот
        /// </summary>
        private TracePivotTable _table;

        /// <summary>
        /// настройки
        /// </summary>
        private TracePivotColumnSettings _settings;

        /// <summary>
        /// Колонка пивота
        /// </summary>
        /// <param name="table">пивот</param>
        /// <param name="settings">настройки</param>
        internal TracePivotColumn(
            TracePivotTable table
            , TracePivotColumnSettings settings)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (settings == null) throw new ArgumentNullException("settings");
            _table = table;
            _settings = settings;
        }

        /// <summary>
        /// Пивот
        /// </summary>
        public TracePivotTable Table { get { return _table; } }

        /// <summary>
        /// Настройки
        /// </summary>
        public TracePivotColumnSettings Settings { get { return _settings; } }

        /// <summary>
        /// Наименование колонки
        /// </summary>
        public string Name { get { return Settings.Name; } }

        /// <summary>
        /// Тип измеряемой величины
        /// </summary>
        public MeasureType Measure { get { return Settings.Measure; } }

        /// <summary>
        /// Тип аггрегата
        /// </summary>
        public AggregateType Type { get { return Settings.Type; } }

        /// <summary>
        /// Фильтр
        /// </summary>
        public TracePivotFilterSettings Filter { get { return Settings.Filter; } }
    }

    /// <summary>
    /// Коллекция колонок
    /// </summary>
    public class TracePivotColumnCollection
        : Dictionary<string, TracePivotColumn>
    {
        /// <summary>
        /// Добавить колонку
        /// </summary>
        /// <param name="column"></param>
        public void Add(TracePivotColumn column)
        {
            if (column == null) throw new ArgumentNullException("column");
            Add(column.Name, column);
        }
    }
}
