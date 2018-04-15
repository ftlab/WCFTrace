using DistributedTrace.Config;
using DistributedTrace.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Collector
{
    public class TracePivotTable
    {
        private TracePivotColumnCollection _columns = new TracePivotColumnCollection();

        private List<TracePivotColumnSettings> _columnSettings = new List<TracePivotColumnSettings>();

        private TracePivotRowCollection _rows = new TracePivotRowCollection();

        private List<TracePivotRowSettings> _rowSettings = new List<TracePivotRowSettings>();

        public TracePivotTable()
        {
        }

        public List<TracePivotColumnSettings> ColumnSettings { get { return _columnSettings; } }

        public TracePivotColumnCollection Columns { get { return _columns; } }

        public List<TracePivotRowSettings> RowSettings { get { return _rowSettings; } }

        public TracePivotRowCollection Rows { get { return _rows; } }

        public void AddColumn(TracePivotColumnSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            ColumnSettings.Add(settings);

            var column = new TracePivotColumn(this, settings);
            Columns.Add(column);
        }

        public void AddRow(TracePivotRowSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            RowSettings.Add(settings);
        }

        public void AddEvent(TraceEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            TracePivotRowCollection rows = Rows;

            for (int i = 0; i < RowSettings.Count; i++)
            {
                var setting = RowSettings[i];

                if (setting.Type == RowType.EventName)
                {
                    TracePivotRow row;
                    if (rows.TryGetValue(@event.Name, out row) == false)
                    {
                        row = new TracePivotRow(this, setting, @event.Name);
                        rows.Add(row);
                    }

                    foreach (var column in Columns)
                    {
                        if (column.Value.Settings.Filter.Type == FilterType.EventName)
                        {
                            var eventName = column.Value.Settings.Filter.EventName;
                            row[column.Key].Measure.AddRange(@event.ByName(eventName));
                        }
                    }
                }
            }
        }
    }
}
