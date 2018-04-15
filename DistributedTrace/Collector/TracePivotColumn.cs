using DistributedTrace.Config;
using DistributedTrace.Stat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Collector
{
    public class TracePivotColumn
    {
        private TracePivotTable _table;

        private TracePivotColumnSettings _settings;

        internal TracePivotColumn(
            TracePivotTable table
            , TracePivotColumnSettings settings)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (settings == null) throw new ArgumentNullException("settings");
            _table = table;
            _settings = settings;
        }

        public TracePivotTable Table { get { return _table; } }

        public TracePivotColumnSettings Settings { get { return _settings; } }

        public string Name { get { return Settings.Name; } }
    }

    public class TracePivotColumnCollection : Dictionary<string, TracePivotColumn>
    {
        public void Add(TracePivotColumn column)
        {
            if (column == null) throw new ArgumentNullException("column");
            Add(column.Name, column);
        }
    }
}
