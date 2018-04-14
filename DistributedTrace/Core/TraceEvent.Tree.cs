using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Разбор дерева событий
    /// </summary>
    public partial class TraceEvent
    {
        /// <summary>
        /// Вложенные события
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TraceEvent> Events()
        {
            if (_events == null) yield break;

            foreach (var e in _events)
                yield return e;
        }

        /// <summary>
        /// Развернуть событие
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeNode<TraceEvent>> Flatten()
        {
            return this.Flatten(e => e.Events());
        }

        /// <summary>
        /// Обойти событие
        /// </summary>
        /// <param name="observer"></param>
        public void Visit(Action<TreeNode<TraceEvent>> observer)
        {
            this.Visit(e => e.Events(), observer);
        }

        /// <summary>
        /// Добавить вложенное событие
        /// </summary>
        /// <param name="event"></param>
        public void AddEvent(TraceEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            if (_events == null)
                _events = new List<TraceEvent>();

            _events.Add(@event);
        }
    }
}
