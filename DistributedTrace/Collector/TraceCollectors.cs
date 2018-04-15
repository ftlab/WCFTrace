using DistributedTrace.Config;
using DistributedTrace.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Collector
{
    public class TraceCollector
    {
        private TraceCollectorSettings _config;

        public TraceCollector(TraceCollectorSettings config)
        {
            if (config == null) throw new ArgumentNullException("config");

            _config = config;
        }

        public string Name { get { return Config.TraceName; } }

        public TraceCollectorSettings Config { get { return _config; } }

        public bool Enabled { get { return Config.Enabled; } }

        public string TraceName { get { return Config.TraceName; } }

        public ITraceWriter Writer { get { return TraceWriter.Default; } }

        public void Collect(TraceId id, TraceEvent @event)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (@event == null) throw new ArgumentNullException("event");

            if (Enabled == false) return;

            if (id.Name != TraceName) return;

            Writer.Write(id, @event);
        }
    }

    public class TraceCollectors : Dictionary<string, TraceCollector>
    {
        private TraceCollectorSettingsCollection _config;

        public TraceCollectors(TraceCollectorSettingsCollection config)
        {
            if (config == null) throw new ArgumentNullException("config");
            _config = config;

            foreach (var item in _config.Cast<TraceCollectorSettings>())
                Add(item);
        }

        public TraceCollectorSettingsCollection Config { get { return _config; } }

        public void Add(TraceCollectorSettings collectorConfig)
        {
            if (collectorConfig == null) throw new ArgumentNullException("collectorConfig");

            Add(new TraceCollector(collectorConfig));
        }

        public void Add(TraceCollector collector)
        {
            if (collector == null) throw new ArgumentNullException("collector");

            Add(collector.Name, collector);
        }

        public void Collect(TraceId id, TraceEvent @event)
        {
            foreach (var collector in this.Values)
            {
                collector.Collect(id, @event);
            }
        }
    }
}
