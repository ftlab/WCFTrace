using DistributedTrace.Config;
using DistributedTrace.Core;
using DistributedTrace.Pivot;

namespace DistributedTrace
{
    /// <summary>
    /// Трассировщик
    /// </summary>
    public class Tracer
    {
        /// <summary>
        /// по-умолчанию
        /// </summary>
        public static Tracer Default = new Tracer();

        /// <summary>
        /// Трассировщик
        /// </summary>
        public Tracer()
        {
            Collectors = new TraceCollectors(Config.Traces);
        }

        /// <summary>
        /// Настройки
        /// </summary>
        public DistributedTraceConfig Config { get { return DistributedTraceConfig.Default; } }

        /// <summary>
        /// Коллектора
        /// </summary>
        public TraceCollectors Collectors { get; private set; }

        /// <summary>
        /// Реакция на событие
        /// </summary>
        /// <param name="id"></param>
        /// <param name="event"></param>
        protected virtual void On(TraceId id, TraceEvent @event)
        {
            Collectors.Collect(id, @event);
        }

        /// <summary>
        /// Реакция на событие
        /// </summary>
        /// <param name="id"></param>
        /// <param name="event"></param>
        public static void OnEvent(TraceId id, TraceEvent @event)
        {
            Default.On(id, @event);
        }
    }
}
