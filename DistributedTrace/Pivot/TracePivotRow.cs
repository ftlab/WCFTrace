using DistributedTrace.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Pivot
{
    public class TracePivotRow
    {
        private TracePivotTable _table;

        private TracePivotRowSettings _settings;

        private Dictionary<string, TracePivotValue> _values = new Dictionary<string, TracePivotValue>();

        private string _rowValue;

        internal TracePivotRow(
            TracePivotTable table
            , TracePivotRowSettings settings
            , string rowValue)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (settings == null) throw new ArgumentNullException("settings");
            _table = table;
            _settings = settings;
            _rowValue = rowValue;

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

        public string RowValue { get { return _rowValue; } }

        public TracePivotTable Table { get { return _table; } }

        public TracePivotRowSettings Settings { get { return _settings; } }

        public TracePivotRow Parent { get; set; }

        public TracePivotValue this[string column]
        {
            get
            {
                return _values[column];
            }
            set
            {
                _values[column] = value;
            }
        }

        public IEnumerable<TracePivotRow> Rows()
        {
            yield break;
        }
    }

    public class TracePivotRowCollection : Dictionary<string, TracePivotRow>
    {
        public void Add(TracePivotRow row)
        {
            if (row == null) throw new ArgumentNullException("row");

            Add(row.RowValue, row);
        }
    }
}
