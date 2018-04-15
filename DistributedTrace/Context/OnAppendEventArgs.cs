using System;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Аргументы события добавления события в контекст трассировки
    /// </summary>
    public class OnAppendEventArgs : EventArgs
    {
        /// <summary>
        /// Аргументы события добавления события в контекст трассировки
        /// </summary>
        /// <param name="event"></param>
        public OnAppendEventArgs(TraceEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");
            Event = @event;
        }

        /// <summary>
        /// Событие трассировки
        /// </summary>
        public TraceEvent Event { get; private set; }
    }
}
