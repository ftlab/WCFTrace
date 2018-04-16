using DistributedTrace.Config;
using System;
using System.Collections.Generic;

namespace DistributedTrace.Pivot
{
    /// <summary>
    /// Строка пивота
    /// </summary>
    public class TracePivotRow
    {
        /// <summary>
        /// пивот
        /// </summary>
        private TracePivotTable _table;

        /// <summary>
        /// настройки
        /// </summary>
        private TracePivotRowSettings _settings;

        /// <summary>
        /// измеряемые величины
        /// </summary>
        private Dictionary<string, TracePivotValue> _values = new Dictionary<string, TracePivotValue>();

        /// <summary>
        /// значение строки
        /// </summary>
        private string _rowValue;

        /// <summary>
        /// Родительская строка
        /// </summary>
        private TracePivotRow _parent;

        /// <summary>
        /// Дочерние строки
        /// </summary>
        private TracePivotRowCollection _childs = new TracePivotRowCollection();

        /// <summary>
        /// Строка пивота
        /// </summary>
        /// <param name="table">пивот</param>
        /// <param name="settings">настройки</param>
        /// <param name="rowValue">значение строки</param>
        /// <param name="parent">родительская строка</param>
        internal TracePivotRow(
            TracePivotTable table
            , TracePivotRowSettings settings
            , string rowValue
            , TracePivotRow parent)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (settings == null) throw new ArgumentNullException("settings");
            _table = table;
            _settings = settings;
            _rowValue = rowValue;
            _parent = parent;

            foreach (var c in table.Columns)
            {
                var column = c.Value;

                var pv = new TracePivotValue();
                pv.Row = this;
                pv.Column = column;
                pv.Measure = new TraceEventMeasure(column.Settings.Type, column.Settings.Measure);

                _values.Add(c.Key, pv);
            }
        }

        /// <summary>
        /// Значение строки
        /// </summary>
        public string RowValue { get { return _rowValue; } }

        /// <summary>
        /// Пивот
        /// </summary>
        public TracePivotTable Table { get { return _table; } }

        /// <summary>
        /// Настройки
        /// </summary>
        public TracePivotRowSettings Settings { get { return _settings; } }

        /// <summary>
        /// Родительская строка
        /// </summary>
        public TracePivotRow Parent { get { return _parent; } }

        /// <summary>
        /// Индексатор колонок
        /// </summary>
        /// <param name="column">колонка</param>
        /// <returns>измеряемая величина</returns>
        public TracePivotValue this[string column]
        {
            get
            {
                return _values[column];
            }
        }

        /// <summary>
        /// Дочерние строки
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TracePivotRow> Childs()
        {
            foreach (var child in _childs.Values)
                yield return child;
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        /// <param name="child"></param>
        public void AddRow(TracePivotRow child)
        {
            if (child == null) throw new ArgumentNullException("child");
            _childs.Add(child);
        }
    }

    /// <summary>
    /// Колекция строк
    /// </summary>
    public class TracePivotRowCollection : Dictionary<string, TracePivotRow>
    {
        /// <summary>
        /// Добавить строку
        /// </summary>
        /// <param name="row"></param>
        public void Add(TracePivotRow row)
        {
            if (row == null) throw new ArgumentNullException("row");

            Add(row.RowValue, row);
        }
    }
}
