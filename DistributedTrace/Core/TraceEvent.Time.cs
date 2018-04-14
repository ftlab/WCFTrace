using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Временные метрики
    /// </summary>
    public partial class TraceEvent
    {
        /// <summary>
        /// Время начала события
        /// </summary>
        public TimeSpan BeginTime
        {
            get { return TimeSpan.FromMilliseconds(_begin); }
            set { _begin = (int)value.TotalMilliseconds; }
        }

        /// <summary>
        /// Время окончания события
        /// </summary>
        public TimeSpan? EndTime
        {
            get { return _end == null ? null : (TimeSpan?)TimeSpan.FromMilliseconds(_end.Value); }
            set { _end = value == null ? null : (int?)value.Value.TotalMilliseconds; }
        }

        /// <summary>
        /// Длительность события
        /// </summary>
        public TimeSpan? Duration
        {
            get
            {
                if (_end == null) return null;
                return TimeSpan.FromMilliseconds(_end.Value - _begin);
            }
        }

        /// <summary>
        /// Разница между длительностью события и длительностью вложенных событий
        /// </summary>
        public TimeSpan? ExcludedTime
        {
            get
            {
                if (_end == null) return null;

                var inclusive = Events().Sum(e => (e._end - e._begin) ?? 0);

                return TimeSpan.FromMilliseconds((_end.Value - _begin) - inclusive);
            }
        }

        /// <summary>
        /// Установить время завершения события
        /// </summary>
        /// <param name="id">идентификатор трассировки</param>
        internal void SetEnd(TraceId id)
        {
            _end = (int)Math.Ceiling((DateTime.UtcNow - id.Timestamp).TotalMilliseconds);
        }

        /// <summary>
        /// Получить дату и время начала события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DateTime GetBeginDateTime(TraceId id)
        {
            return (id.Timestamp + BeginTime).ToLocalTime();
        }

        /// <summary>
        /// Получить дату и время окончания события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DateTime? GetEndDateTime(TraceId id)
        {
            if (_end == null) return null;
            return (id.Timestamp + EndTime.Value).ToLocalTime();
        }
    }
}
