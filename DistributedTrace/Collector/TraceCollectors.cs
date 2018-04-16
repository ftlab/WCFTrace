using DistributedTrace.Config;
using DistributedTrace.Core;
using DistributedTrace.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Pivot
{
    /// <summary>
    /// Коллектор событий трассировок
    /// </summary>
    public class TraceCollector
    {
        /// <summary>
        /// настройки 
        /// </summary>
        private TraceCollectorSettings _settings;

        /// <summary>
        /// Коллектор трассировок
        /// </summary>
        /// <param name="settings"></param>
        public TraceCollector(TraceCollectorSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            _settings = settings;
        }

        /// <summary>
        /// Настройки
        /// </summary>
        public TraceCollectorSettings Settings { get { return _settings; } }

        /// <summary>
        /// Флаг - включен
        /// </summary>
        public bool Enabled { get { return Settings.Enabled; } }

        /// <summary>
        /// Наименование трассировки
        /// </summary>
        public string TraceName { get { return Settings.TraceName; } }

        /// <summary>
        /// Механизм записи трассировки
        /// </summary>
        public ITraceWriter Writer { get { return TraceWriter.Default; } }

        /// <summary>
        /// Сбор
        /// </summary>
        /// <param name="id">идентификатор трассы</param>
        /// <param name="event">событие</param>
        public void Collect(TraceId id, TraceEvent @event)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (@event == null) throw new ArgumentNullException("event");

            if (Enabled == false) return;

            if (id.Name != TraceName) return;

            Writer.Write(id, @event);
        }
    }

    /// <summary>
    /// Коллекция коллекторов трассировок
    /// </summary>
    public class TraceCollectors : Dictionary<string, TraceCollector>
    {
        /// <summary>
        /// настройки
        /// </summary>
        private TraceCollectorSettingsCollection _settings;

        /// <summary>
        /// Коллекция коллекторов трассировок
        /// </summary>
        /// <param name="settings"></param>
        public TraceCollectors(TraceCollectorSettingsCollection settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            _settings = settings;

            foreach (var item in _settings.Cast<TraceCollectorSettings>())
                Add(item);
        }

        /// <summary>
        /// Настройки
        /// </summary>
        public TraceCollectorSettingsCollection Settings { get { return _settings; } }

        /// <summary>
        /// Добавить коллектор
        /// </summary>
        /// <param name="settings"></param>
        public void Add(TraceCollectorSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            Add(new TraceCollector(settings));
        }

        /// <summary>
        /// Добавить коллектор
        /// </summary>
        /// <param name="collector"></param>
        public void Add(TraceCollector collector)
        {
            if (collector == null) throw new ArgumentNullException("collector");

            Add(collector.TraceName, collector);
        }

        /// <summary>
        /// Сбор
        /// </summary>
        /// <param name="id"></param>
        /// <param name="event"></param>
        public void Collect(TraceId id, TraceEvent @event)
        {
            foreach (var collector in this.Values)
            {
                collector.Collect(id, @event);
            }
        }
    }
}
